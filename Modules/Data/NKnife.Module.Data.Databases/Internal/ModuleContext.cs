using NKnife.Circe.Base.Modules.Manager;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Data.Databases.Internal
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IIoCManager>? _iocManagerLazy;
        private Lazy<ISurroundingsManager>? _surroundingsLazy;

        public IIoCManager IoCManager => _iocManagerLazy!.Value;
        public ISurroundingsManager Surroundings => _surroundingsLazy!.Value;

        /// <inheritdoc />
        public override void Initialize()
        {
            _surroundingsLazy = GetModule<ISurroundingsManager>();
        }

        public void SetIoCManager(Lazy<IIoCManager> iocManagerLazy)
        {
            _iocManagerLazy = iocManagerLazy;
        }
    }
}