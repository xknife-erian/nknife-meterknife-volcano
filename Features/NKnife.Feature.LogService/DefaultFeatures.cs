using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NKnife.Feature.LogService.View;
using NKnife.Feature.LogService.ViewModel;
using NLog;
using RAY.Common;
using RAY.Common.Authentication;
using RAY.Common.Enums;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Services.LogService;
using RAY.Common.UI;
using RAY.Library;
using RAY.Plugins.WPF.Common;
using RAY.Plugins.WPF.Ribbons;

namespace NKnife.Feature.LogService
{
    public class DefaultFeatures : BasePicoFeatures, ISupportUsingModule
    {

        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
        private Lazy<IModulesManager>? _modulesManagerLazy;
        
        #region Implementation of ISupportKernel<IModulesManager>
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> moduleManagerLazy)
        {
            _modulesManagerLazy = moduleManagerLazy;

            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion

        private class LoggerFeatureSet(Lazy<ILogService>? logServiceLazy) : BaseFeatureSet
        {
            private readonly Lazy<ILogService>? _logServiceLazy = logServiceLazy;
            private LoggerPaneVm? _loggerPaneVm;
            private IUIManager? _uiManager;

            private ICommand ShowLoggerPane => new RelayCommand(() =>
            {
                s_logger.Debug("UI：用户打开【日志】");
                var logService = _logServiceLazy!.Value;
                _loggerPaneVm ??= new LoggerPaneVm(logService, _uiManager!.ShowDialog)
                {
                    Title = "日志",
                    ContentId = Guid.NewGuid().ToString()
                };
                _uiManager!.ShowDocumentPane(_loggerPaneVm);
            });

            /// <inheritdoc />
            protected override Dictionary<string, ICommand> RegisterCommandDictionary()
            {
                var commands = new Dictionary<string, ICommand> { { nameof(ShowLoggerPane), ShowLoggerPane } };

                return commands;
            }

            /// <inheritdoc />
            public override void Inject(IUIManager uiManager)
            {
                _uiManager = uiManager;
                uiManager.WorkbenchInitialized += UiManagerWorkbenchInitialized;
            }

            private void UiManagerWorkbenchInitialized(object? sender, EventArgs e)
            {
                if (FocalPoints == null) return;
                foreach (var focalPointPair in FocalPoints)
                {
                    var focalPoint = focalPointPair.Value;

                    var hasPermission = PermissionManager.HasPermission([
                        RoleType.SuperAdmin,
                        RoleType.Admin,
                        RoleType.Laboratory
                    ]);

                    focalPoint.SetEnable(hasPermission);
                }
            }

            /// <inheritdoc />
            public override IEnumerable<VmPair> BuildPaneModel()
            {
                return new VmPair[] { new(View: typeof(LoggerPane), ViewModel: typeof(LoggerPaneVm)) };
            }

            /// <inheritdoc />
            public override IEnumerable<VmPair> BuildDialogModel()
            {
                return new VmPair[] { new(View: typeof(LoggerDetailDialog), ViewModel: typeof(LoggerDetailDialogVm)) };
            }
        }

        #region Overrides of BasePicoPlugin
        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        private IFeatureSet? _featureSet;

        /// <inheritdoc />
        public override IFeatureSet GetFeatureSet()
        {
            var logServiceLazy = _modulesManagerLazy!.Value.FindModuleBuilder<ILogService>();

            return _featureSet ??= new LoggerFeatureSet(logServiceLazy!.Build());
        }

        /// <inheritdoc />
        public override void Dispose() { }
        #endregion

    }
}