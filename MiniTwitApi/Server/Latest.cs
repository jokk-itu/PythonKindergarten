using System.IO;

namespace MiniTwitApi.Server
{
    public class Latest
    {
        private static readonly Latest _latest = new Latest();
        private readonly object _fileLock = new object();
        
        public void Update(int latest)
        {
            lock (_fileLock)
            {
                using var writer = new StreamWriter("latest.txt"); 
                writer.WriteLineAsync($"{latest}");
                writer.FlushAsync();
            }
        }

        public int Read()
        {
            lock (_fileLock)
            {
                using var reader = new StreamReader("latest.txt");
                return int.Parse(reader.ReadLine());
            }
        }

        public static Latest GetInstance()
        {
            return _latest;
        }
    }
}