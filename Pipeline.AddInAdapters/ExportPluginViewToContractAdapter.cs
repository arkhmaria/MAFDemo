using Pipeline.Contracts;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Windows;

namespace Pipeline.AddInAdapters
{
    [AddInAdapter]
    public class ExportPluginViewToContractAdapter : ContractBase, Pipeline.Contracts.IExportPlugin
    {
        private readonly Pipeline.AddInViews.IExportPluginView view;

        public string DisplayName => view.DisplayName;

        public ExportPluginViewToContractAdapter(Pipeline.AddInViews.IExportPluginView view)
        {
            this.view = view;
        }

        public void Initialize(IPluginApi api)
        {
            view.Initialize(new PluginApiContractToPluginViewAdapter(api));
        }

        public bool CanExport(string file)
        {
            return view.CanExport(file);
        }

        public void Export(string file)
        {
            view.Export(file);
        }

        public INativeHandleContract GetPanelUI()
        {
            FrameworkElement frameworkElement = view.GetPanelUI();
            return FrameworkElementAdapters.ViewToContractAdapter(frameworkElement);
        }
    }
}
