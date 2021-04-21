using Pipeline.Contracts;
using System;
using System.AddIn.Pipeline;

namespace Pipeline.HostAdapters
{
    [HostAdapter]
    public class PluginApiHostViewToContractAdapter : ContractBase, IPluginApi
    {
        private readonly HostViews.IPluginApi view;

        public PluginApiHostViewToContractAdapter(HostViews.IPluginApi view)
        {
            this.view = view;
        }

        public DateTime GetLastModifiedDate(string path)
        {
            return view.GetLastModifiedDate(path);
        }
    }
}
