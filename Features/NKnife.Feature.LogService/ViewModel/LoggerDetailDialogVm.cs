using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NLog;

namespace NKnife.Feature.LogService.ViewModel
{
    public class LoggerDetailDialogVm : ObservableObject, IModalDialogViewModel
    {
        private LogEventInfo _currentLogger = null!;
        private bool? _dialogResult;

        public LoggerDetailDialogVm(LogEventInfo log)
        {
            CurrentLogger = log;
        }

        public LogEventInfo CurrentLogger
        {
            get => _currentLogger;
            set => SetProperty(ref _currentLogger, value);
        }

        public ICommand OkCommand => new RelayCommand(() => {
            DialogResult = true;
        });

        public bool? DialogResult
        {
            get => _dialogResult;
            set => SetProperty(ref _dialogResult, value);
        }
    }
}
