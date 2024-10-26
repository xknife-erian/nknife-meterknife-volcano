using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MvvmDialogs;
using NLog;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;
using LEIAO.Mercury.Extensions;
using RAY.Common;
using RAY.Library.Collection;
using RAY.Library;

namespace LEIAO.Feature.LogService.ViewModel
{
    public class LoggerListViewVm : ObservableObject
    {
        private double _callerWidth = 220;

        private LogEventInfo _currentLogger = null!;
        private string _header = string.Empty;
        private double _levelWidth = 60;
        private double _messageWidth = 450;
        private double _timeWidth = 130;
        private double _userWidth = 110;
        private bool _isFirst = false;

#if RELEASE
        private bool _isDisplayDebug = false;
        private bool _isDisplayTrace = false;
#else
        private bool _isDisplayDebug = true;
        private bool _isDisplayTrace = true;
#endif
        private bool _isDisplayInfo = true;
        private bool _isDisplayWarn = true;
        private bool _isDisplayError = true;
        private bool _isDisplayFatal = true;

        public LoggerListViewVm(Action<IModalDialogViewModel, IModalDialogViewModel?> showDialogAction, ObservableCollection<RayLogEventInfo> logSource)
        {
            _showDialogAction = showDialogAction;
            _logSource = logSource;
            
            logSource.CollectionChanged += LogSourceOnCollectionChanged;
            
            UpdateLogs();
            
            PropertyChanged += (_, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(IsDisplayTrace):
                    case nameof(IsDisplayDebug):
                    case nameof(IsDisplayInfo):
                    case nameof(IsDisplayWarn):
                    case nameof(IsDisplayError):
                    case nameof(IsDisplayFatal):
                        UpdateLogs();

                        break;
                }
            };
        }

        private void UpdateLogs()
        {
            Logs.Clear();

            var filteredLogs = _logSource.Where(IsLogDisplay).ToList();

            Logs.AddRange(filteredLogs);
        }

        private void LogSourceOnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems?[0] is not RayLogEventInfo info) break;

                    if (IsLogDisplay(info))
                    {
                        Logs.Insert(0, info);
                    }
                    break;
            }
        }

        private bool IsLogDisplay(LogEventInfo log)
        {
            return (IsDisplayTrace && log.Level == LogLevel.Trace)
                   || (IsDisplayDebug && log.Level == LogLevel.Debug)
                   || (IsDisplayInfo && log.Level == LogLevel.Info)
                   || (IsDisplayWarn && log.Level == LogLevel.Warn)
                   || (IsDisplayError && log.Level == LogLevel.Error)
                   || (IsDisplayFatal && log.Level == LogLevel.Fatal);
        }

        public ICommand ViewSizeChangedCommand => new RelayCommand<double>(AdjustColumnWidth);

        public ICommand ViewLoadedCommand => new RelayCommand<double>(w =>
        {
            if (!_isFirst)
            {
                AdjustColumnWidth(w);
                _isFirst = true;
            }
        });

        public ObservableCollection<RayLogEventInfo> Logs { get; } = new();

        public string Header
        {
            get => _header;
            set => SetProperty(ref _header, value);
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
        
        public double UserWidth
        {
            get => _userWidth;
            set => SetProperty(ref _userWidth, value);
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
        public string UserHeader { get; set; } = "User";
        public string MessageHeader { get; set; } = "Message";

        public ICommand LoggerItemClickCommand => new RelayCommand<LogEventInfo>(log =>
        {
            if (log != null)
                _showDialogAction(new LoggerDetailDialogVm(log), null);
        });

        public LogEventInfo CurrentLogger
        {
            get => _currentLogger;
            set => SetProperty(ref _currentLogger, value);
        }

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
        private readonly Action<IModalDialogViewModel, IModalDialogViewModel?> _showDialogAction;
        private readonly ObservableCollection<RayLogEventInfo> _logSource;

        private void AdjustColumnWidth(double w)
        {
            if (Math.Abs(_viewWidth - w) <= 0) return;
            if (_viewWidth < 0)
                _viewWidth = w;
            var yw = (w - TimeWidth - UserWidth - LevelWidth) / 10;
            MessageWidth = yw * 7;
            CallerWidth = yw * 2.75;
        }
        #endregion
    }
}