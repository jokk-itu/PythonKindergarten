using System.IO;
using System.Collections.Generic;

namespace MiniTwitApi.Server
{
    public class Latest
    {
        private static readonly Latest _latest = new Latest();
        private readonly object _fileLock = new object();
        
        public void Update(long latest)
        {
            lock (_fileLock)
                File.WriteAllText("latest.txt", latest.ToString());
        }

        public long Read()
        {
            lock (_fileLock)
                return long.Parse(File.ReadAllText("latest.txt"));
        }

        public static Latest GetInstance()
        {
            return _latest;
        }
    }
}