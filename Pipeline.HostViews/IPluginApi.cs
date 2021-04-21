using System;

namespace Pipeline.HostViews
{
    public interface IPluginApi
    {
        DateTime GetLastModifiedDate(string path);
    }
}
