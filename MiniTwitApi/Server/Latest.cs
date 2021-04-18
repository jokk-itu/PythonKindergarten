using System.IO;
using System.Collections.Generic;
using System;
using Serilog;
namespace MiniTwitApi.Server
{
    public class Latest
    {
        private static readonly Latest _latest = new Latest();
        private readonly object _fileLock = new object();
        
        public void Update(long latest)
        {
            Log.Information($"Latest was updated with {latest}");
            lock (_fileLock)
                File.WriteAllText($"latest.txt", latest.ToString());
        }

        public long Read()
        {
            lock (_fileLock){
                var latest = long.Parse(File.ReadAllText($"latest.txt"));
                Log.Information($"Latest was read ({latest})");
                return latest;
                }
        }

        public static Latest GetInstance()
        {
            return _latest;
        }
    }
}