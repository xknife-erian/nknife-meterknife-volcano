using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NKnife.Module.Workbench.Internal.ViewModels.Container;

public class StatusStripVm : ObservableRecipient
{
    private string _infoMessage = string.Empty;

    public string InformationTip
    {
        get => _infoMessage;
        set => SetProperty(ref _infoMessage, value);
    }

    public bool HasInformationTip => !string.IsNullOrEmpty(_infoMessage);

    public void Show(string infomation)
    {
        InformationTip = infomation;
    }

    public string? AppVersion => Assembly.GetEntryAssembly()?.GetName()?.Version?.ToString();
}