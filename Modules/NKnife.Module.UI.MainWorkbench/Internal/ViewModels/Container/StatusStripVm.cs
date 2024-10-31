using System.Reflection;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NKnife.Module.UI.MainWorkbench.Internal.ViewModels.Container
{
    public class StatusStripVm : ObservableRecipient
    {
        private string _infoMessage = string.Empty;

        public string InformationMessage
        {
            get => _infoMessage;
            set => SetProperty(ref _infoMessage, value);
        }

        public bool HasInformationMessage => !string.IsNullOrEmpty(_infoMessage);

        public void Show(string informationMessage)
        {
            InformationMessage = informationMessage;
        }

        public ICommand ViewLoadedCommand => new RelayCommand(() =>
        {
        });
    }
}