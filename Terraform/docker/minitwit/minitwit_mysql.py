# -*- coding: utf-8 -*-
"""
    MiniTwit
    ~~~~~~~~

    A microblogging application written with Flask and sqlite3.

    :copyright: (c) 2010 by Armin Ronacher.
    :license: BSD, see LICENSE for more details.
"""
from __future__ import with_statement
import time
import sqlite3
from hashlib import md5
from datetime import datetime
from contextlib import closing
from flask import (
    Flask,
    request,
    session,
    url_for,
    redirect,
    render_template,
    abort,
    g,
    flash,
)
from werkzeug import check_password_hash, generate_password_hash
import os

# mysql refactor
#  from mysql import connector as conn
import mysql.connector as mysql
# TODO change...
# mysqldb connection
#  DB_HOST = "172.17.0.2"
#  DB_HOST = "minitwit_mysql"
#  DB_HOST = os.environ['DB_HOST']
DB_HOST = "minitwit_itusqlimage"
print('db_host', DB_HOST)
DB_PORT = 3306
DB_USER = "root"
DB_PASSWORD = "megetsikkertkodeord"
DB_NAME = "minitwit"

# configuration
PER_PAGE = 30
DEBUG = True
SECRET_KEY = "development key"

# create our little application :)
app = Flask(__name__)
app.config.from_object(__name__)
app.config.from_envvar("MINITWIT_SETTINGS", silent=True)


def connect_db():
    """Returns a new connection to the database."""
    #  return sqlite3.connect(app.config["DATABASE"])

    # connect to mysqldb and return connection
    return mysql.connect(
        host=DB_HOST,
        port=DB_PORT,
        user=DB_USER,
        passwd=DB_PASSWORD,
        database=DB_NAME
    )


def query_db(query, args=(), one=False):
    """Queries the database and returns a list of dictionaries."""

    # query mysqldb

    # create cursor and return queries as dictionaries
    cursor = g.db.cursor(dictionary=True)
    
    # execte query as side effect
    cursor.execute(query)

    # fetch all results as rows in a list of dictionaries
    rv = cursor.fetchall()

    # return either the first row or all rows
    return (rv[0] if rv else None) if one else rv


def get_user_id(username):
    """Convenience method to look up the id for a username."""
    cur = g.db.cursor(dictionary=True)
    cur.execute(
        "select user_id from user where username = '{}'".format(username)
    )
    rv = cur.fetchone()
    print(rv)
    return rv['user_id'] if rv else None


def format_datetime(timestamp):
    """Format a timestamp for display."""
    return datetime.utcfromtimestamp(timestamp).strftime("%Y-%m-%d @ %H:%M")


def gravatar_url(email, size=80):
    """Return the gravatar image for the given email address."""
    return "http://www.gravatar.com/avatar/%s?d=identicon&s=%d" % (
        md5(email.strip().lower().encode("utf-8")).hexdigest(),
        size,
    )


@app.before_request
def before_request():
    """Make sure we are connected to the database each request and look
    up the current user so that we know he's there.
    """
    g.db = connect_db()
    g.user = None
    if "user_id" in session:
        g.user = query_db(
            "select * from user where user_id = '{}'".format(session["user_id"]),
            one=True,
        )


@app.after_request
def after_request(response):
    """Closes the database again at the end of the request."""
    g.db.close()
    return response


@app.route("/")
def timeline():
    """Shows a users timeline or if no user is logged in it will
    redirect to the public timeline.  This timeline shows the user's
    messages as well as all the messages of followed users.
    """
    if not g.user:
        return redirect(url_for("public_timeline"))
    return render_template(
        "timeline.html",
        messages=query_db(
            """
        select message.*, user.* from message, user
        where message.flagged = 0 and message.author_id = user.user_id and (
            user.user_id = {} or
            user.user_id in (select whom_id from follower
                                    where who_id = {}))
        order by message.pub_date desc limit {}"""
            .format(session["user_id"], session["user_id"], PER_PAGE),
        ),
    )


@app.route("/public")
def public_timeline():
    """Displays the latest messages of all users."""
    return render_template(
        "timeline.html",
        messages=query_db("""
        select message.*, user.* from message, user
        where message.flagged = 0 and message.author_id = user.user_id
        order by message.pub_date desc limit {}""".format(PER_PAGE)),
    )


@app.route("/<username>")
def user_timeline(username):
    """Display's a users tweets."""
    profile_user = query_db(
        "select * from user where username = '{}'".format(username), one=True
    )
    if profile_user is None:
        abort(404)
    followed = False
    if g.user:
        followed = (
            query_db(
                """select 1 from follower where
            follower.who_id = {} and follower.whom_id = {}""".format(session["user_id"], profile_user["user_id"]),
                one=True,
            )
            is not None
        )
    return render_template(
        "timeline.html",
        messages=query_db(
            """
            select message.*, user.* from message, user where message.flagged = 0 and
            user.user_id = message.author_id and user.user_id = {}
            order by message.pub_date desc limit {}""".format(profile_user["user_id"], PER_PAGE),
        ),
        followed=followed,
        profile_user=profile_user,
    )


@app.route("/<username>/follow")
def follow_user(username):
    """Adds the current user as follower of the given user."""
    if not g.user:
        abort(401)
    whom_id = get_user_id(username)
    if whom_id is None:
        abort(404)
    cur = g.db.cursor(dictionary=True)
    cur.execute(
        "insert into follower (who_id, whom_id) values ({}, {})".format(session["user_id"], whom_id),
    )
    g.db.commit()
    flash('You are now following "%s"' % username)
    return redirect(url_for("user_timeline", username=username))


@app.route("/<username>/unfollow")
def unfollow_user(username):
    """Removes the current user as follower of the given user."""
    if not g.user:
        abort(401)
    whom_id = get_user_id(username)
    if whom_id is None:
        abort(404)
    cur = g.db.cursor(dictionary=True)
    cur.execute(
        "delete from follower where who_id={} and whom_id={}".format(session["user_id"], whom_id),
    )
    g.db.commit()
    flash('You are no longer following "%s"' % username)
    return redirect(url_for("user_timeline", username=username))


@app.route("/add_message", methods=["POST"])
def add_message():
    """Registers a new message for the user."""
    if "user_id" not in session:
        abort(401)
    if request.form["text"]:
        cur = g.db.cursor(dictionary=True)
        cur.execute(
            """insert into message (author_id, text, pub_date, flagged)
            values ({}, '{}', {}, 0)""".format(session["user_id"], request.form["text"], int(time.time())),
        )
        g.db.commit()
        flash("Your message was recorded")
    return redirect(url_for("timeline"))


@app.route("/login", methods=["GET", "POST"])
def login():
    """Logs the user in."""
    if g.user:
        return redirect(url_for("timeline"))
    error = None
    if request.method == "POST":
        user = query_db(
            """select * from user where
            username = '{}'""".format(request.form["username"]),
            one=True,
        )
        if user is None:
            error = "Invalid username"
        elif not check_password_hash(user["pw_hash"], request.form["password"]):
            error = "Invalid password"
        else:
            flash("You were logged in")
            session["user_id"] = user["user_id"]
            return redirect(url_for("timeline"))
    return render_template("login.html", error=error)


@app.route("/register", methods=["GET", "POST"])
def register():
    """Registers the user."""
    if g.user:
        return redirect(url_for("timeline"))
    error = None
    if request.method == "POST":
        if not request.form["username"]:
            error = "You have to enter a username"
        elif not request.form["email"] or "@" not in request.form["email"]:
            error = "You have to enter a valid email address"
        elif not request.form["password"]:
            error = "You have to enter a password"
        elif request.form["password"] != request.form["password2"]:
            error = "The two passwords do not match"
        elif get_user_id(request.form["username"]) is not None:
            error = "The username is already taken"
        else:
            cur = g.db.cursor(dictionary=True)
            query = """insert into user (
                username, email, pw_hash) values ('{}', '{}', '{}')""".format(
                    request.form["username"],
                    request.form["email"],
                    generate_password_hash(request.form["password"]))
            cur.execute(query)
            g.db.commit()
            flash("You were successfully registered and can login now")
            return redirect(url_for("login"))
    return render_template("register.html", error=error)


@app.route("/logout")
def logout():
    """Logs the user out."""
    flash("You were logged out")
    session.pop("user_id", None)
    return redirect(url_for("public_timeline"))


# add some filters to jinja
app.jinja_env.filters["datetimeformat"] = format_datetime
app.jinja_env.filters["gravatar"] = gravatar_url


if __name__ == "__main__":
    app.run(host="0.0.0.0", port=5000)
