using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniTwitChatClient.Misc
{
    public static class Convertions
    {
        public static string GetThreadId(List<string> participants)
            => participants.Sum(participant => participant.Sum(Convert.ToInt32)).ToString();
    }
}