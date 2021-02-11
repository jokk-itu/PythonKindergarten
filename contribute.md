# Contribute

## Our repository setup
We have choosen to use a **"mono repository"** as the setup for our repository. This is because we mainly will develop only one system, and would like to keep it on the same repository.

## Our branching model
In our repository, we have a development branch, which is a nearly stable version of our system. This development branch is merged into master once a week, for the release, making it a **"Branch by Release"**. \\ 

From the development branch, we have topic branches that represent the feature we are working on. These will be merged into development when they are somewhat stable. We will name the topic branches with "topic\work-topic", to avoid confusion.

## Our distributed development workflow
For our distributed developement workflow we have chosen to work with the **centralized workflow**. Had we been working in a team with a "senior developer"/a developer that had a lot more knowledge of the system, we could consider a intergation-manager workflow.

## How do we expect contributions to look like?
### Commit guidelines
We aim that contributions, focusing on commits, follow the clean code guidelines.

Heres a model for a Git commit message (retrieved from
[here](https://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html)):

*Capitalized, short (50 chars or less) summary

More detailed explanatory text, if necessary.  Wrap it to about 72
characters or so.  In some contexts, the first line is treated as the
subject of an email and the rest of the text as the body.  The blank
line separating the summary from the body is critical (unless you omit
the body entirely); tools like rebase can get confused if you run the
two together.

Write your commit message in the imperative: "Fix bug" and not "Fixed bug"
or "Fixes bug."  This convention matches up with commit messages generated
by commands like git merge and git revert.

Further paragraphs come after blank lines.

- Bullet points are okay, too

- Typically a hyphen or asterisk is used for the bullet, followed by a
  single space, with blank lines in between, but conventions vary here

- Use a hanging indent*

### How we collaborate
As we are a bit larger private team, we would call ourselves a **Private Managed Team**. For the course period, we will not take in contributions from others. This could changed later.

## Who is responsible for integrating/reviewing contributions?
For pull requests, we will send review request to the other developers in the team. There should always be atleast one other reviewer, however depening on the importance, more reviewers should be added.
