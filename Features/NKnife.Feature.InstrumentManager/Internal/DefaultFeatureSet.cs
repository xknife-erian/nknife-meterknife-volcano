using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NKnife.Feature.InstrumentManager.Internal.View;
using NKnife.Feature.InstrumentManager.Internal.ViewModel;
using NLog;
using RAY.Common.Authentication;
using RAY.Common.Enums;
using RAY.Common.UI;
using RAY.Library;
using RAY.Plugins.WPF.Common;

namespace NKnife.Feature.InstrumentManager.Internal
{
    class DefaultFeatureSet() : BaseFeatureSet
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        private IUIManager? _uiManager;
        private InstrumentManagerViewModel? _instrumentManagerViewModel;

        private ICommand ShowInstrumentManagerPane => new RelayCommand(() =>
        {
            _instrumentManagerViewModel ??= new InstrumentManagerViewModel { Title = "仪器管理", ContentId = Guid.NewGuid().ToString() };
            _uiManager!.ShowToolPane(_instrumentManagerViewModel);
        });

        /// <inheritdoc />
        protected override Dictionary<string, ICommand> RegisterCommandDictionary()
        {
            var commands = new Dictionary<string, ICommand> { { nameof(ShowInstrumentManagerPane), ShowInstrumentManagerPane } };
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
            return [new VmPair(View:typeof(InstrumentManagerView), ViewModel:typeof(InstrumentManagerViewModel))];
        }

        /// <inheritdoc />
        public override IEnumerable<VmPair> BuildDialogModel()
        {
            return [];
        }
    }
}