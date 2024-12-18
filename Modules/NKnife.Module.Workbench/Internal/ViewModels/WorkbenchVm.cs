using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NKnife.Module.Workbench.Internal.ViewModels.Container;

namespace NKnife.Module.Workbench.Internal.ViewModels;
public partial class WorkbenchVm : ObservableRecipient
{
    private readonly IDialogService _dialogService;
    private bool _isBusy;

    public WorkbenchVm(IDialogService dialogService, ModuleContext context)
    {
        _dialogService = dialogService;
        MenusVm        = new MenusVm(this);
        DocksVm        = new DocksVm(context);
    }

    public ICommand WorkbenchInitializingCmd => new RelayCommand(() =>
    {
        WorkbenchInitializing?.Invoke(this, EventArgs.Empty);
    });

    public ICommand WorkbenchInitializedCmd => new RelayCommand(() =>
    {
        WorkbenchInitialized?.Invoke(this, EventArgs.Empty);
    });
    public ICommand WorkbenchClosingCmd => new RelayCommand(() =>
    {
        WorkbenchClosing?.Invoke(this, EventArgs.Empty);
    });

    public ICommand WorkbenchClosedCmd => new RelayCommand(() =>
    {
        WorkbenchClosed?.Invoke(this, EventArgs.Empty);
    });

    public bool IsBusy
    {
        get => _isBusy;
        set => SetProperty(ref _isBusy, value);
    }

    public MenusVm MenusVm { get; }
    public DocksVm DocksVm { get; }
    public StatusStripVm StatusStripVm { get; } = new();
    public WarnStripVm WarnStripVm { get; } = new();
    public ErrorStripVm ErrorStripVm { get; } = new();
}