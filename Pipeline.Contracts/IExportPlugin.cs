using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;

namespace Pipeline.Contracts
{
    [AddInContract]
    public interface IExportPlugin : IContract
    {
        string DisplayName { get; }

        void Initialize(IPluginApi api);

        bool CanExport(string file);

        void Export(string file);

        INativeHandleContract GetPanelUI();
    }
}
