using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.Module.UI.MainWorkbench.Internal.ViewModels.Container
{
    public class WarnStripVm : ObservableRecipient
    {
        private string _errorTip = string.Empty;

        public string ErrorTip
        {
            get => _errorTip;
            set => SetProperty(ref _errorTip, value);
        }

        public bool HasWarnMessage => !string.IsNullOrEmpty(ErrorTip);

        public void Show(string message)
        {
            ErrorTip = message;
        }
    }
}