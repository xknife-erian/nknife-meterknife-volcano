using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NLog;
using RAY.Common.Authentication;
using RAY.Common.Enums;
using RAY.Common.UI;
using RAY.Library;
using RAY.Plugins.WPF.Common;

namespace NKnife.Feature.PreferenceService.Internal
{
    class DefaultFeatureSet() : BaseFeatureSet
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        private IUIManager? _uiManager;

        private ICommand ShowPluginBrowserDialog => new RelayCommand(() =>
        {
        });

        /// <inheritdoc />
        protected override Dictionary<string, ICommand> RegisterCommandDictionary()
        {
            var commands = new Dictionary<string, ICommand> { { nameof(ShowPluginBrowserDialog), ShowPluginBrowserDialog } };
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
                    RoleType.Admin
                ]);

                focalPoint.SetEnable(hasPermission);
            }
        }

        /// <inheritdoc />
        public override IEnumerable<VmPair> BuildPaneModel()
        {
            return [];
        }

        /// <inheritdoc />
        public override IEnumerable<VmPair> BuildDialogModel()
        {
            return [];
        }
    }
}