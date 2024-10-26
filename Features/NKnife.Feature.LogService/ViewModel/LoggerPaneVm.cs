using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NLog;
using RAY.Common.Services.LogService;
using RAY.Windows.Common;
using System.Windows.Input;

namespace LEIAO.Feature.LogService.ViewModel
{
    public class LoggerPaneVm : BaseViewModel
    {
        private readonly Action<IModalDialogViewModel, IModalDialogViewModel?> _showDialogAction;
        private double _callerWidth = 220;

        private LogEventInfo _currentLogger = null!;
        private int _selectViewIndex;

#if RELEASE
        private bool _isDisplayDebug = false;
        private bool _isDisplayTrace = false;
#else
        private bool _isDisplayDebug = true;
        private bool _isDisplayTrace = true;
#endif
        private bool _isDisplayError = true;
        private bool _isDisplayFatal = true;
        private bool _isDisplayInfo = true;
        private bool _isDisplayWarn = true;

        private double _levelWidth = 60;
        private double _messageWidth = 550;
        private double _timeWidth = 120;

        public LoggerPaneVm(ILogService logService, Action<IModalDialogViewModel, IModalDialogViewModel?> showDialogAction)
        {
            Title = "日志";

            var sortedLogStacks = logService.LogStacks.OrderBy(pair => pair.Key == "ALL" ? 0 : 1).ThenBy(pair => pair.Key);
            foreach (var logStackPair in sortedLogStacks)
            {
                var viewModel = new LoggerListViewVm(showDialogAction, logStackPair.Value.Logs) { Header = logStackPair.Key };
                LoggerListViewVmCollection.Add(viewModel);
            }
            _selectViewIndex  =  0;
            _showDialogAction =  showDialogAction;
            PropertyChanged   += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(IsDisplayTrace):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayTrace = IsDisplayTrace;
                        break;
                    case nameof(IsDisplayDebug):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayDebug = IsDisplayDebug;
                        break;
                    case nameof(IsDisplayInfo):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayInfo = IsDisplayInfo;
                        break;
                    case nameof(IsDisplayWarn):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayWarn = IsDisplayWarn;
                        break;
                    case nameof(IsDisplayError):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayError = IsDisplayError;
                        break;
                    case nameof(IsDisplayFatal):
                        foreach (var loggerListViewVm in LoggerListViewVmCollection)
                            loggerListViewVm.IsDisplayFatal = IsDisplayFatal;
                        break;
                    default: break;
                }
            };
        }

        public ICommand ClearAllLogCommand => new RelayCommand(() =>
        {
            foreach (var loggerListViewVm in LoggerListViewVmCollection)
            {
                loggerListViewVm.Logs.Clear();
            }
        });

        public List<LoggerListViewVm> LoggerListViewVmCollection { get; set; } = new ();

        public LogEventInfo CurrentLogger
        {
            get => _currentLogger;
            set => SetProperty(ref _currentLogger, value);
        }

        public int SelectViewIndex
        {
            get => _selectViewIndex;
            set => SetProperty(ref _selectViewIndex, value);
        }

        public double TimeWidth
        {
            get => _timeWidth;
            set => SetProperty(ref _timeWidth, value);
        }

        public double LevelWidth
        {
            get => _levelWidth;
            set => SetProperty(ref _levelWidth, value);
        }

        public double MessageWidth
        {
            get => _messageWidth;
            set => SetProperty(ref _messageWidth, value);
        }

        public double CallerWidth
        {
            get => _callerWidth;
            set => SetProperty(ref _callerWidth, value);
        }

        public int MaxRowCount { get; set; } = 50;
        public string TimeHeader { get; set; } = "Time";
        public string CallerHeader { get; set; } = "Caller";
        public string LevelHeader { get; set; } = "Level";
        public string MessageHeader { get; set; } = "Message";

        public bool IsDisplayTrace
        {
            get => _isDisplayTrace;
            set => SetProperty(ref _isDisplayTrace, value);
        }

        public bool IsDisplayDebug
        {
            get => _isDisplayDebug;
            set => SetProperty(ref _isDisplayDebug, value);
        }

        public bool IsDisplayInfo
        {
            get => _isDisplayInfo;
            set => SetProperty(ref _isDisplayInfo, value);
        }

        public bool IsDisplayWarn
        {
            get => _isDisplayWarn;
            set => SetProperty(ref _isDisplayWarn, value);
        }

        public bool IsDisplayError
        {
            get => _isDisplayError;
            set => SetProperty(ref _isDisplayError, value);
        }

        public bool IsDisplayFatal
        {
            get => _isDisplayFatal;
            set => SetProperty(ref _isDisplayFatal, value);
        }


        #region AutoWidth
        private double _viewWidth = -1;

        private void AdjustColumnWidth(double w)
        {
            if(Math.Abs(_viewWidth - w) <= 0) return;
            if(_viewWidth < 0)
                _viewWidth = w;
            var yw = (w - TimeWidth - LevelWidth) / 10;
            MessageWidth = yw * 7;
            CallerWidth  = yw * 2.75;
        }
        #endregion
    }
}