using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NKnife.Feature.ConnectorManager.Internal.View;
using NKnife.Feature.ConnectorManager.Internal.ViewModel;
using NLog;
using RAY.Common.Authentication;
using RAY.Common.Enums;
using RAY.Common.UI;
using RAY.Library;
using RAY.Plugins.WPF.Common;

namespace NKnife.Feature.ConnectorManager.Internal
{
    class DefaultFeatureSet() : BaseFeatureSet
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        private IUIManager? _uiManager;
        private ConnectorManagerViewModel? _connectorManagerViewModel;

        private ICommand ShowConnectorManagerPane => new RelayCommand(() =>
        {
            _connectorManagerViewModel ??= new ConnectorManagerViewModel { Title = "连接器管理", ContentId = Guid.NewGuid().ToString() };
            _uiManager!.ShowToolPane(_connectorManagerViewModel);
        });

        /// <inheritdoc />
        protected override Dictionary<string, ICommand> RegisterCommandDictionary()
        {
            var commands = new Dictionary<string, ICommand> { { nameof(ShowConnectorManagerPane), ShowConnectorManagerPane } };
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
            return [new VmPair(View:typeof(ConnectorManagerView), ViewModel:typeof(ConnectorManagerViewModel))];
        }

        /// <inheritdoc />
        public override IEnumerable<VmPair> BuildDialogModel()
        {
            return [];
        }
    }
}