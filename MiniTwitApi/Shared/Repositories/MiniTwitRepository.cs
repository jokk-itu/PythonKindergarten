namespace MiniTwitApi.Shared.Repositories 
{
    public class MiniTwitRepository : IMiniTwitRepository, IDisposable
    {
        private string _connection;
        public MiniTwitRepository() 
        {
            _connection = @"URI=../tmp/minitwit.db";
            _connection.Open();
        }

        override public void Dispose() 
        {
            _connection.Close();
        }

        public async bool UserExistsAsync(string username) 
        {
            using (var command = new SQLiteCommand(connection_string))
            {
                command.CommandText = @"SELECT user.user_id FROM user WHERE username = @username";
                command.Parameters.AddWithValue("@userid", username);
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    return reader.Read();
                }
            }
        }

        public async Task<IList<MessageDTO>> QueryMessagesAsync(string userid, int limit) 
        {
            var messages = new List<MessageDTO>();

            using (var command = new SQLiteCommand(connection_string))
            {
                command.CommandText = @"SELECT message.*, user.* FROM message, user 
                                      WHERE message.flagged = 0 AND
                                      user.user_id = message.author_id AND user.user_id = @userId
                                      ORDER BY message.pub_date DESC LIMIT @limit";

                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@limit", limit);
                command.Prepare();
                
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var values = reader.GetValues();
                        var message = new MessageDTO()
                        {
                            Id = Int.Parse(values["message_id"]),
                            Author = Int.Parse(values["author_id"]),
                            Text = values["text"],
                            PublishDate = Int.Parse(values["pub_date"]),
                            Flagged = Int.Parse(values["flagged"]),
                        };
                        messages.Add(message);
                    }
                }                
            }
            return messages;
        }

        public async Task<List<MessageDTO>> QueryMessagesAsync(int limit) 
        {
            var messages = new List<MessageDTO>();
            
            using (var command = new SQLiteCommand(connection_string))
            {
                command.CommandText = @"SELECT message.*, user.* FROM message, user
                                        WHERE message.flagged = 0 AND message.author_id = user.user_id
                                        ORDER BY message.pub_date DESC LIMIT @limit";

                command.Parameters.AddWithValue("@limit", limit);
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var values = reader.GetValues();
                        var message = new MessageDTO()
                        {
                            Id = Int.Parse(values["message_id"]),
                            Author = Int.Parse(values["author_id"]),
                            Text = values["text"],
                            PublishDate = Int.Parse(values["pub_date"]),
                            Flagged = Int.Parse(values["flagged"]),
                        };
                        messages.Add(message);
                    }
                }
            }
            return messages;
        }

        public async Task InsertUserAsync(UserDTO user) 
        {
            using (var command = new SQLiteCommand())
            {
                command.CommandText = @"INSERT INTO user
                                        (username, email, pw_hash) VALUES (@username, @email, @pwd)";
                command.Parameters.AddWithValue("@username", user.username);
                command.Parameters.AddWithValue("@email", user.email);
                command.Parameters.AddWithValue("@pwd", user.pw_hash);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }
        
        /*
        public async Task<UserDTO> QueryUserAsync(int userId) 
        {
            //Hmmmm?
        }*/

        public async Task InsertFollowAsync(FollowerDTO follow) 
        {
            using (var command = new SQLiteCommand())
            {
                command.CommandText = @"INSERT INTO follower (who_id, whom_id) VALUES (@who_id, @whom_id)";
                command.Parameters.AddWithValue("@who_id", follow);
                command.Parameters.AddWithValue("@whom_id", follow);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }
        
        public async Task RemoveFollowAsync(FollowerDTO follow) 
        {
            using (var command = new SQLiteCommand())
            {
                command.CommandText = @"DELETE FROM follower WHERE who_id=@who_id and WHOM_ID=@whom_id";
                command.Parameters.AddWithValue("@who_id", follow);
                command.Parameters.AddWithValue("@whom_id", follow);
                command.Prepare();
                command.ExecuteNonQuery();
            }
        }

        public async Task<IList<FollowerDTO>> QueryFollowers(string userid) 
        {
            var followers = new List<FollowerDTO>();
            using (var command = new SQLiteCommand(connection_string))
            {
                command.CommandText = @"SELECT user.username FROM user
                                        INNER JOIN follower ON follower.whom_id=user.user_id
                                        WHERE follower.who_id=?
                                        LIMIT ?";
                command.Parameters.AddWithValue("@userid", "userid");
                command.Prepare();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        var values = reader.GetValues();
                        var follower = new FollowerDTO() 
                        {
                            WhoId = Int.Parse(values["who_id"]),
                            WhomId = Int.Parse(values["whom_id"])
                        };
                        followers.Add(follower);
                    }
                }
            }
            return followers;
        }
    }
}