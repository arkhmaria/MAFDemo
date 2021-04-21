using DemoHost.Annotations;
using Microsoft.WindowsAPICodePack.Dialogs;
using Pipeline.HostViews;
using System;
using System.AddIn.Hosting;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace DemoHost
{
    public class MainWindowModel : INotifyPropertyChanged
    {
        private IExportPluginView plugin;
        private string selectedFolderPath;
        private ObservableCollection<FileInfo> fileItemsList;
        private FileInfo selectedFileItem;
        private FrameworkElement pluginPanel;

        public ICommand OpenFolder { get; }

        public ICommand Export { get; }

        public ObservableCollection<FileInfo> FileItemsList
        {
            get => fileItemsList;
            set
            {
                fileItemsList = value;
                OnPropertyChanged();
            }
        }

        public FileInfo SelectedFileItem
        {
            get => selectedFileItem;
            set
            {
                selectedFileItem = value;
                OnPropertyChanged();
            }
        }

        public string SelectedFolderPath
        {
            get => selectedFolderPath;
            set
            {
                selectedFolderPath = value;
                OnPropertyChanged();
            }
        }

        public FrameworkElement PluginPanel
        {
            get => pluginPanel;
            set
            {
                pluginPanel = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowModel()
        {
            OpenFolder = new RelayCommand(args => true, args => SelectFolder());
            Export = new RelayCommand(
                args => plugin != null && plugin.CanExport(SelectedFileItem?.FullName),
                args => plugin.Export(SelectedFileItem.FullName));

            UpdateFilesList("C:\\");

            ActivatePlugins();
        }

        private void ActivatePlugins()
        {
            string pipelinePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Pipeline");

            AddInStore.Update(pipelinePath);

            var addInTokens = AddInStore.FindAddIns(typeof(IExportPluginView), pipelinePath);
            
            if (addInTokens.Count > 0)
            {
                plugin = addInTokens.First().Activate<IExportPluginView>(AddInSecurityLevel.FullTrust);

                plugin.Initialize(new PluginApi());

                MessageBox.Show($"Plugin '{plugin.DisplayName}' has been activated", "MAF Demo message", MessageBoxButton.OK);

                PluginPanel = plugin.GetPanelUI();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SelectFolder()
        {
            var dialog = new CommonOpenFileDialog { IsFolderPicker = true };

            CommonFileDialogResult result = dialog.ShowDialog();

            if (result == CommonFileDialogResult.Ok)
            {
                UpdateFilesList(dialog.FileName);
            }
        }

        private void UpdateFilesList(string newPath)
        {
            if (Directory.Exists(newPath) && string.Compare(newPath, selectedFolderPath, StringComparison.InvariantCultureIgnoreCase) != 0)
            {
                SelectedFolderPath = newPath;
                FileItemsList = new ObservableCollection<FileInfo>();

                foreach (var fileName in Directory.EnumerateFiles(newPath))
                {
                    FileItemsList.Add(new FileInfo(fileName));
                }
            }
        }
    }
}