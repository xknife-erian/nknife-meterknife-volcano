using System.ComponentModel;
using MvvmDialogs;
using MvvmDialogs.FrameworkDialogs.OpenFile;
using RAY.Common.Enums;
using RAY.Common.UI;
using RAY.Library;
using RAY.Windows;
using RAY.Windows.Common.ViewModels.Layout;
using RAY.Windows.MessagePopup;

namespace NKnife.Module.Workbench.Internal.ViewModels;

public partial class WorkbenchVm : IUIManager
{
    /// <inheritdoc />
    public event EventHandler<EventArgs>? WorkbenchInitializing;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? WorkbenchInitialized;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? WorkbenchClosing;

    /// <inheritdoc />
    public event EventHandler<EventArgs>? WorkbenchClosed;

    /// <inheritdoc />
    public void ShowDocumentPane(INotifyPropertyChanged document)
    {
        if (document is BaseDocumentViewModelV1 viewModel)
            DocksVm.ActivateDocument(viewModel);
    }

    /// <inheritdoc />
    public void ReOpenDocumentPane(string contentId)
    {
        foreach (var document in DocksVm.Documents)
        {
            if (document.ContentId != contentId) continue;
            DocksVm.ActivateDocument(document);
            break;
        }
    }

    /// <inheritdoc />
    public void ShowToolPane(INotifyPropertyChanged document)
    {
        if (document is BaseToolPaneViewModelV1 viewModel)
            DocksVm.ActivateTool(viewModel);
    }

    /// <inheritdoc />
    public void ReOpenToolPane(string contentId)
    {
        foreach (var tool in DocksVm.Tools)
        {
            if (tool.ContentId != contentId) continue;
            DocksVm.ActivateTool(tool);
            break;
        }
    }

    /// <inheritdoc />
    public void ShowDialog(INotifyPropertyChanged document, INotifyPropertyChanged? ownerViewModel)
    {
        if (document is IModalDialogViewModel modalDialog)
            _dialogService.ShowDialog(ownerViewModel ?? this, modalDialog);
    }

    /// <inheritdoc />
    public void ShowStandaloneWindow(INotifyPropertyChanged viewModel, bool isModal = false) { }

    /// <inheritdoc />
    public void ShowErrorMessage(string message, int fade)
    {
        ErrorStripVm.Show(message);
    }

    /// <inheritdoc />
    public void ShowWarningMessage(string message, int fade)
    {
        WarnStripVm.Show(message);
    }

    /// <inheritdoc />
    public void ShowInformationMessage(string message)
    {
        StatusStripVm.Show(message);
    }

    /// <inheritdoc />
    public void ShowBusyIndicator(BusyIndicator busyIndicator) { }

    /// <inheritdoc />
    public void CloseBusyIndicator(BusyIndicator busyIndicator) { }

    /// <inheritdoc />
    public bool ShowOpenFileDialog(FileOperationDialogSettings settings, out string[] fullFiles)
    {
        var dialogSetting = new OpenFileDialogSettings
        {
            Title = settings.Title,
            Filter = settings.Filter,
            CheckFileExists = settings.CheckFileExists
        };
        var complete = _dialogService.ShowOpenFileDialog(this, dialogSetting);

        if (complete == true)
        {
            fullFiles = dialogSetting.FileNames;

            return true;
        }
        fullFiles = [];

        return false;
    }

    /// <inheritdoc />
    public bool? ShowSaveFileDialog(FileOperationDialogSettings settings)
    {
        throw new NotSupportedException();
    }

    public bool? ShowFolderBrowserDialog(FolderOperationDialogSettings setting, out string? folder)
    {
        folder = null;
        var transformSetting = setting.Transform();
        var isSelectedFolder = _dialogService.ShowFolderBrowserDialog(this, transformSetting);
        if (isSelectedFolder == true)
            folder = transformSetting.SelectedPath;

        return isSelectedFolder;
    }

    /// <inheritdoc />
    public OperationResult ShowMessageBox(INotifyPropertyChanged? document, MboxSettings setting)
    {
        var transformSetting = setting.Transform();
        var mboxResult = _dialogService.ShowMessageBox(document ?? this, transformSetting);

        return mboxResult.Transform();
    }

    private MessagePopup? _messagePopupView = null;
    
    /// <inheritdoc />
    public void ShowMessagePopup(MessageType type,
                                 string message,
                                 TimeSpan delayCloseSeconds,
                                 INotifyPropertyChanged? ownerViewModel = null)
    {
        this.OnUI(() =>
        {
            if (_messagePopupView != null)
            {
                _messagePopupView.Close();
            }
            var vm = new MessagePopupVm(type, message, delayCloseSeconds);
            _messagePopupView = new MessagePopup() { DataContext = vm, DelayCloseTime = delayCloseSeconds };

            _messagePopupView.Show();
        });
    }

    /// <inheritdoc />
    public bool CloseDialog(INotifyPropertyChanged document)
    {
        return _dialogService.SafetyClose(document);
    }

    //TODO: 关闭窗体询问相关的设计不应该是UIManager的职责，应该是WorkbenchVm的职责
    #region 关闭窗体相关
    /// <inheritdoc />
    public bool IsAskCloseOrNot { get; set; }

    /// <inheritdoc />
    public string MessageForAskCloseOrNot { get; set; } = string.Empty;

    /// <inheritdoc />
    public bool AskCloseOrNot()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void InvokeWorkbenchClosing()
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public void InvokeWorkbenchClosed()
    {
        throw new NotImplementedException();
    }
    #endregion
}