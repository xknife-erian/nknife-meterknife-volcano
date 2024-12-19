using NKnife.Circe.Base.Modules.Managers;
using RAY.Common.Plugin.Modules;

namespace NKnife.Feature.UutManager.Internal
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IUutManager>? _uutManagerLazy;

        public IUutManager UutManager => _uutManagerLazy!.Value;

        public override void Initialize()
        {
            _uutManagerLazy = GetModule<IUutManager>();
        }
    }
}