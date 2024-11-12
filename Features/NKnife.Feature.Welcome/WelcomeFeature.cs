using System.Runtime.CompilerServices;
using System.Windows.Input;
using RAY.Common;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.UI;
using RAY.Library;
using RAY.Plugins.WPF.Common;
using WelcomeWindow = NKnife.Feature.Welcome.Internal.Welcome;

[assembly: InternalsVisibleTo("UnitTest.LEIAO.Modules")]

namespace NKnife.Feature.Welcome
{
    public class WelcomeFeature : BasePicoFeatures, ISupportUsingModule
    {
        private readonly Context _context = new();
        private readonly DefaultFeatureSet _featureSet;
        private WelcomeWindow? _welcomeWindow;

        public WelcomeFeature()
        {
            _featureSet = new();

            _featureSet.MainWorkbenchLoading += async (_, e) =>
            {
                _welcomeWindow ??= new WelcomeWindow();
                _welcomeWindow.SetModuleContext(_context);
                _welcomeWindow.Show();//如果需要仅显示欢迎窗口，不显示主窗口，应该在这里调用ShowDialog()
            };
            _featureSet.MainWorkbenchLoaded += async (_, e) =>
            {
                await Task.Delay(3 * 500);
                _welcomeWindow?.Close();
            };
            _featureSet.MainWorkbenchClosing += async (_, e) =>
            {
                _welcomeWindow?.Close();
            };
        }

        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> moduleManagerLazy)
        {
            _context.SetModulesManager(moduleManagerLazy);
            return Task.FromResult<IPicoPlugin>(this);
        }

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            _context.Initialize();
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

        /// <inheritdoc />
        public override void Dispose() { }

        /// <inheritdoc />
        public override IFeatureSet? GetFeatureSet()
        {
            return _featureSet;
        }

        private class DefaultFeatureSet : BaseFeatureSet
        {
            /// <inheritdoc />
            protected override Dictionary<string, ICommand> RegisterCommandDictionary()
            {
                return new Dictionary<string, ICommand>(0);
            }

            /// <inheritdoc />
            public override void Inject(IUIManager uiManager)
            {
                uiManager.WorkbenchInitializing += (_, _) => { MainWorkbenchLoading?.Invoke(this, EventArgs.Empty); };
                uiManager.WorkbenchInitialized  += (_, _) => { MainWorkbenchLoaded?.Invoke(this, EventArgs.Empty); };
                uiManager.WorkbenchClosing      += (_, _) => { MainWorkbenchClosing?.Invoke(this, EventArgs.Empty); };
                uiManager.WorkbenchClosed       += (_, _) => { MainWorkbenchClosed?.Invoke(this, EventArgs.Empty); };
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

            public event EventHandler? MainWorkbenchLoaded;
            public event EventHandler? MainWorkbenchLoading;
            public event EventHandler? MainWorkbenchClosing;
            public event EventHandler? MainWorkbenchClosed;
        }
    }
}