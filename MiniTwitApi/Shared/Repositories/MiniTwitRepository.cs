using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MiniTwitApi.Shared.Models;
using MiniTwitApi.Shared.Repositories.Abstractions;

namespace MiniTwitApi.Shared.Repositories 
{
    public class MiniTwitRepository : IMiniTwitRepository, IDisposable
    {
        private SqliteConnection _connection;
        private string _connectionString = "";
        public MiniTwitRepository() 
        {
            _connection = new SqliteConnection("Data Source=../../tmp/minitwit.db");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        public async Task<bool> UserExistsAsync(string username) 
        {
            using (var command = new SqliteCommand($"SELECT user.user_id FROM user WHERE username = '{username}'", _connection))
            {
                //command.Parameters.AddWithValue("@username", username);
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    return reader.Read();
                }
            }
        }

        public async Task<IList<MessageDTO>> QueryMessagesAsync(string userId, int limit = 20) 
        {
            var messages = new List<MessageDTO>();

            using (var command = new SqliteCommand($"SELECT message.* FROM message WHERE message.flagged = 0 AND message.author_id = {userid} ORDER BY message.pub_date DESC LIMIT {limit}", _connection))
            {
                //command.Parameters.AddWithValue("@userId", userId);
                //command.Parameters.AddWithValue("@limit", limit);
                command.Prepare();
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var message = new MessageDTO()
                        {
                            Id = int.Parse(reader["message_id"].ToString()),
                            Author = int.Parse(reader["author_id"].ToString()),
                            Text = reader["text"].ToString(),
                            PublishDate = int.Parse(reader["pub_date"].ToString()),
                            Flagged = int.Parse(reader["flagged"].ToString()),
                        };
                        messages.Add(message);
                    }
                }                
            }
            return messages;
        }

        public async Task<List<MessageDTO>> QueryMessagesAsync(int limit = 20) 
        {
            var messages = new List<MessageDTO>();
            
            using (var command = new SqliteCommand($"SELECT message.* FROM message WHERE message.flagged = 0 ORDER BY message.pub_date DESC LIMIT {limit}", _connection))
            {
                //command.Parameters.AddWithValue("@limit", limit);
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var message = new MessageDTO()
                        {
                            Id = int.Parse(reader["message_id"].ToString()),
                            Author = int.Parse(reader["author_id"].ToString()),
                            Text = reader["text"].ToString(),
                            PublishDate = int.Parse(reader["pub_date"].ToString()),
                            Flagged = int.Parse(reader["flagged"].ToString()),
                        };
                        messages.Add(message);
                    }
                }
            }
            return messages;
        }

        public async Task InsertUserAsync(UserDTO user) 
        {
            using (var command = new SqliteCommand($"INSERT INTO user (username, email, pw_hash) VALUES ('{user.Username}', '{user.Email}', '{user.Password}')", _connection))
            {
                //command.Parameters.AddWithValue("@username", user.Username);
                //command.Parameters.AddWithValue("@email", user.Email);
                //command.Parameters.AddWithValue("@pwd", user.Password);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }
        
        public async Task<UserDTO> QueryUserByIdAsync(int userId) 
        {
            using(var command = new SqliteCommand($"SELECT user FROM user WHERE user_id = {userid}", _connection)) 
            {
                //command.Parameters.AddWithValue("@userid", userId);
                command.Prepare();
                using(var reader = await command.ExecuteReaderAsync()) 
                {
                    while(reader.Read()) 
                    {
                        return new UserDTO() 
                        {
                            Id = int.Parse(reader["user_id"].ToString()),
                            Username = reader["username"].ToString(),
                            Email = reader["email"].ToString(),
                            Password = reader["pwd"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public async Task<UserDTO> QueryUserByUsernameAsync(string username) 
        {
            using(var command = new SqliteCommand($"SELECT * FROM user WHERE username = '{username}'", _connection)) 
            {

                //command.Parameters.AddWithValue("@username", username);
                command.Prepare();
                using(var reader = await command.ExecuteReaderAsync()) 
                {
                    while(reader.Read()) 
                    {
                        return new UserDTO() 
                        {
                            Id = int.Parse(reader["user_id"].ToString()),
                            Username = reader["username"].ToString(),
                            Email = reader["email"].ToString(),
                            Password = reader["pw_hash"].ToString()
                        };
                    }
                }
            }

            return null;
        }

        public async Task InsertFollowAsync(FollowerDTO follow) 
        {
            using (var command = new SqliteCommand($"INSERT INTO follower (who_id, whom_id) VALUES ({follow.WhoId}, {follow.WhomId})", _connection))
            {
                //command.Parameters.AddWithValue("@who_id", follow.WhoId);
                //command.Parameters.AddWithValue("@whom_id", follow.WhomId);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }
        
        public async Task RemoveFollowAsync(FollowerDTO follow)
        {
            using (var command = new SqliteCommand($"DELETE FROM follower WHERE who_id={follow.WhoId} and whom_id={follow.WhomId}", _connection))
            {
                //command.Parameters.AddWithValue("@who_id", follow.WhoId);
                //command.Parameters.AddWithValue("@whom_id", follow.WhomId);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        //TODO might need a limit from query
        public async Task<IList<FollowerDTO>> QueryFollowers(string userid, int limit = 20) 
        {
            var followers = new List<FollowerDTO>();
            using (var command = new SqliteCommand($"SELECT follower.whom_id, follower.who_id FROM user INNER JOIN follower ON follower.whom_id=user.user_id WHERE follower.who_id={userid} LIMIT {limit}", _connection))
            {
                //command.Parameters.AddWithValue("@userid", userid);
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var follower = new FollowerDTO() 
                        {
                            WhoId = int.Parse(reader["who_id"].ToString()),
                            WhomId = int.Parse(reader["whom_id"].ToString())
                        };
                        followers.Add(follower);
                    }
                }
            }
            return followers;
        }

        public async Task InsertMessageAsync(MessageDTO message)
        {
            using (var command = new SqliteCommand($"INSERT INTO message (author_id, text, pub_date, flagged) VALUES ({message.Author}, '{message.Text}', {message.PublishDate}, {message.Flagged})", _connection))
            {
                command.Parameters.AddWithValue("@author_id", message.Author);
                command.Parameters.AddWithValue("@text", message.Text);
                command.Parameters.AddWithValue("@pub_date", message.PublishDate);
                command.Parameters.AddWithValue("@flagged", message.Flagged);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }
    }
}