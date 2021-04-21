using Pipeline.Contracts;
using System;
using System.AddIn.Pipeline;

namespace Pipeline.AddInAdapters
{
    [AddInAdapter]
    class PluginApiContractToPluginViewAdapter : AddInViews.IPluginApi
    {
        private readonly IPluginApi contract;

        private ContractHandle handle;

        public PluginApiContractToPluginViewAdapter(IPluginApi contract)
        {
            this.contract = contract;
            handle = new ContractHandle(contract);
        }

        public DateTime GetLastModifiedDate(string path)
        {
            return contract.GetLastModifiedDate(path);
        }
    }
}
