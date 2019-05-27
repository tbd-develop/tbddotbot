using System.Collections.Generic;
using System.IO;

namespace twitchstreambot
{
    public class ConfigurationFile
    {
        private string _filePath;
        private FileSystemWatcher _watcher;

        public ConfigurationFile(string filePath, bool reloadOnChanged = true)
        {
            _filePath = filePath;
            _watcher = new FileSystemWatcher(_filePath);
            _watcher.Changed += FileWasUpdated;
        }

        public void FileWasUpdated(object sender, FileSystemEventArgs args)
        {

        }
    }
}