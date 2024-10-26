using NLog;
using RAY.Common;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;
using RAY.Plugins.WPF;

namespace NKnife.Module.UI.MainWorkbench
{
    public class WorkbenchModule : BasePicoModule<IWorkbench>, ISupportUsingModule, ISupportUsingPlugin
    {
        private static readonly NLog.Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly ModuleContext _context = ModuleContext.Instance;
        
        private IWorkbench? _workbench;

        /// <inheritdoc />
        public override Lazy<IWorkbench> Build(params object[] args)
        {
            return new Lazy<IWorkbench>(() =>
            {
                if(_workbench == null)
                {
                    var workbenchVm = _context.WorkbenchViewModel;

                    var menuContext = new UIManagerContext(_context.WorkbenchViewModel, _context.RegisterPaneLocator, _context.RegisterDialogLocator);
                    var menus = _context.PluginManager.InitializeFeaturePoints(menuContext);
                    foreach (var categoryPoint in menus)
                        workbenchVm.MenusVm.CategoryMenus.Add(categoryPoint);
                    
                    _workbench ??= new Internal.View.Workbench() { DataContext = workbenchVm };
                }

                return _workbench;
            });
        }

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            _context.Initialize();
            return Task.FromResult(true);
        }

        #region Implementation of IPicoPlugin
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
        #endregion

        #region Implementation of ISupportKernel<in IModulesManager>
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> modulesManagerLazy)
        {
            _context.SetModulesManager(modulesManagerLazy);
            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion

        #region Implementation of ISupportUsingPlugin
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IPluginManager> pluginManager)
        {
            _context.SetPluginManager(pluginManager);
            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion
    }
}