using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Windows;
using Pipeline.HostViews;

namespace Pipeline.HostAdapters
{
    [HostAdapter]
    public class ExportPluginContractToHostAdapter : IExportPluginView
    {
        private readonly Contracts.IExportPlugin contract;
        private readonly ContractHandle handle;

        public string DisplayName => contract.DisplayName;

        public ExportPluginContractToHostAdapter(Contracts.IExportPlugin contract)
        {
            this.contract = contract;
            handle = new ContractHandle(contract);
        }

        public void Initialize(IPluginApi api)
        {
            contract.Initialize(new PluginApiHostViewToContractAdapter(api));
        }

        public bool CanExport(string file)
        {
            return contract.CanExport(file);
        }

        public void Export(string file)
        {
            contract.Export(file);
        }

        public FrameworkElement GetPanelUI()
        {
            INativeHandleContract handleContract = contract.GetPanelUI();
            return FrameworkElementAdapters.ContractToViewAdapter(handleContract);
        }
    }
}
