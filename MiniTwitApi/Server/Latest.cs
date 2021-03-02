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
                writer.Close();
            }
        }

        public int Read()
        {
            lock (_fileLock)
            {
                using var reader = new StreamReader("latest.txt");
                var latest = int.Parse(reader.ReadLine());
                reader.Close();
                return latest;
            }
        }

        public static Latest GetInstance()
        {
            return _latest;
        }
    }
}