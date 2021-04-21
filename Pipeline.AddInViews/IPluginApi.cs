using System;

namespace Pipeline.AddInViews
{
    public interface IPluginApi
    {
        DateTime GetLastModifiedDate(string path);
    }
}
