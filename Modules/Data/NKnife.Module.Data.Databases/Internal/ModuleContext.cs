using NKnife.Circe.Base.Modules.Manager;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Data.Databases.Internal
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IIoCManager>? _iocManagerLazy;
        private Lazy<IAppWorkspaceManager>? _surroundingsLazy;

        public IIoCManager IoCManager => _iocManagerLazy!.Value;
        public IAppWorkspaceManager AppWorkspace => _surroundingsLazy!.Value;

        /// <inheritdoc />
        public override void Initialize()
        {
            _surroundingsLazy = GetModule<IAppWorkspaceManager>();
        }

        public void SetIoCManager(Lazy<IIoCManager> iocManagerLazy)
        {
            _iocManagerLazy = iocManagerLazy;
        }
    }
}