using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.Module.UI.MainWorkbench.Internal.ViewModels.Container
{
    public class StatusStripVm : ObservableRecipient
    {
        private string _infoTip = string.Empty;

        public string InformationTip
        {
            get => _infoTip;
            set => SetProperty(ref _infoTip, value);
        }

        public bool HasInformationMessage => !string.IsNullOrEmpty(_infoTip);

        public void Show(string infomation)
        {
            InformationTip = infomation;
        }

        public string? AppVersion => Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();

    }
}