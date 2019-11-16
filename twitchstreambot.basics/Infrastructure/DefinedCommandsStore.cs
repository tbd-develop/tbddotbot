using System;
using System.IO;

namespace twitchstreambot.basics.Infrastructure
{
    public class DefinedCommandsStore
    {
        private readonly string _fileName;

        public DefinedCommandsStore(string fileName)
        {
            _fileName = Path.Combine(AppContext.BaseDirectory, fileName);
        }

        public string Load()
        {
            return File.ReadAllText(_fileName);
        }

        public void Save(string content)
        {
            File.WriteAllText(_fileName, content);
        }
    }
}