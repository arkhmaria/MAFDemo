using Pipeline.HostViews;
using System;
using System.IO;

namespace DemoHost
{
    public class PluginApi : IPluginApi
    {
        public DateTime GetLastModifiedDate(string path)
        {
            if(string.IsNullOrEmpty(path))
                return DateTime.Now;
            
            FileAttributes attr = File.GetAttributes(path);

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                return Directory.GetLastWriteTime(path);
            }

            return File.GetLastWriteTime(path);
        }
    }
}
