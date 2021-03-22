using System;
using System.Collections.Generic;

namespace MiniTwitChatClient
{
    public class ChatConfiguration
    {
        public string BrokerHost { get; }
        public int BrokerPort { get; }
        public string BrokerUser { get; }
        public string BrokerPassword { get; }
        public List<string> ChatThreads { get; set; }

        public ChatConfiguration(string host, int port, string user, string password, List<string> chatThreads)
        {
            BrokerHost = host;
            BrokerPort = port;
            BrokerUser = user;
            BrokerPassword = password;
            ChatThreads = chatThreads;
        }
    }
}