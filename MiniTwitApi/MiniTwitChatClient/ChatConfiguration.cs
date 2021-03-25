﻿using System;
using System.Collections.Generic;
using MiniTwitChatClient.Abstractions;

namespace MiniTwitChatClient
{
    public class ChatConfiguration : IChatConfiguration
    {
        public string BrokerHost { get; }
        public int BrokerPort { get; }
        public string BrokerUser { get; }
        public string BrokerPassword { get; }

        public ChatConfiguration(string host, int port, string user, string password)
        {
            BrokerHost = host;
            BrokerPort = port;
            BrokerUser = user;
            BrokerPassword = password;
        }
    }
}