using ExportPlugin.Annotations;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ExportPlugin
{
    public class PanelUIModel : INotifyPropertyChanged
    {
        private string fileNameText;
        private string statusText;
        private string convertedFilePath;

        public event PropertyChangedEventHandler PropertyChanged;
        
        public string FileNameText
        {
            get => fileNameText;
            set
            {
                fileNameText = value;
                OnPropertyChanged();
            }
        }

        public string StatusText
        {
            get => statusText;
            set
            {
                statusText = value;
                OnPropertyChanged();
            }
        }

        public string ConvertedFilePath
        {
            get => convertedFilePath;
            set
            {
                convertedFilePath = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsOpenEnabled));
            }
        }

        public bool IsOpenEnabled => !string.IsNullOrEmpty(ConvertedFilePath);

        public ICommand OpenFile { get; }

        public PanelUIModel()
        {
            OpenFile = new RelayCommand(args => Process.Start(ConvertedFilePath));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
