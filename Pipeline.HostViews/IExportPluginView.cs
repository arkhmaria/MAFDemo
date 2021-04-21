using System.Collections.Generic;
using System.Windows;

namespace Pipeline.HostViews
{
    public interface IExportPluginView
    {
        string DisplayName { get; }

        void Initialize(IPluginApi api);

        bool CanExport(string file);

        void Export(string file);

        FrameworkElement GetPanelUI();
    }
}
