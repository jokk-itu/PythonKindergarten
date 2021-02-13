using System;

namespace MiniTwitApi.Shared.Repositories 
{
    public static class EpochConverter
    {
        public static long ToEpoch(DateTime dateTime)
        {
            return (long) (dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
        }
        public static long ToMsEpoch(DateTime dateTime)
        {
            return (long) (dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
        

        public static DateTime FromEpoch(long epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddSeconds(epoch);
        }

        public static DateTime FromMicroEpoch(long epoch)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified).AddSeconds(epoch / 1000000);
        }
    }
}   