using NKnife.Circe.Base.Modules.Services;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Data.Databases.Internal
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IIoCManager>? _iocManagerLazy;
        private Lazy<IAppWorkspaceService>? _surroundingsLazy;

        public IIoCManager IoCManager => _iocManagerLazy!.Value;
        public IAppWorkspaceService AppWorkspace => _surroundingsLazy!.Value;

        /// <inheritdoc />
        public override void Initialize()
        {
            _surroundingsLazy = GetModule<IAppWorkspaceService>();
        }

        public void SetIoCManager(Lazy<IIoCManager> iocManagerLazy)
        {
            _iocManagerLazy = iocManagerLazy;
        }
    }
}