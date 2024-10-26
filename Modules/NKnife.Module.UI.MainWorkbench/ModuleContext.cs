using MvvmDialogs;
using NKnife.Module.UI.MainWorkbench.Internal;
using NKnife.Module.UI.MainWorkbench.Internal.View.Container;
using NKnife.Module.UI.MainWorkbench.Internal.ViewModels;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;
using RAY.Library;
using RAY.Windows.Dialogs;

namespace NKnife.Module.UI.MainWorkbench
{
    class ModuleContext : BaseModuleContext
    {
        #region 单例
        /// <summary>
        ///     因两个Module共用一个Context，故设计为单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static ModuleContext Instance => s_myInstance.Value;
        private static readonly Lazy<ModuleContext> s_myInstance = new(() => new ModuleContext());
        #endregion

        private Lazy<IPluginManager>? _pluginManagerLazy;

        private readonly IDialogService _dialogService;
        private readonly DialogTypeLocator _dialogTypeLocator;

        private ModuleContext()
        {
            _dialogTypeLocator = new DialogTypeLocator();
            _dialogService = new DialogService(dialogTypeLocator: _dialogTypeLocator,
                                               frameworkDialogFactory: new CustomFrameworkDialogFactory());
        }

        public IPluginManager PluginManager => _pluginManagerLazy!.Value;

        public void SetPluginManager(Lazy<IPluginManager> pluginManagerLazy)
        {
            _pluginManagerLazy = pluginManagerLazy;
        }

        /// <inheritdoc />
        public override void Initialize()
        {
        }
        
        private WorkbenchVm? _workbenchViewModel;
        public WorkbenchVm WorkbenchViewModel
        {
            get { return _workbenchViewModel ??= new WorkbenchVm(_dialogService); }
        }


        public void RegisterDialogLocator(IEnumerable<VmPair> vmPairCollection)
        {
            foreach (var vmPair in vmPairCollection)
            {
                _dialogTypeLocator.Register(vmPair);
            }
        }

        public void RegisterPaneLocator(IEnumerable<VmPair> vmPairCollection)
        {
            foreach (var vmPair in vmPairCollection)
            {
                DockTypes.Register(vmPair);
            }
        }
    }
}