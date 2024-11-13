using RAY.Common;
using RAY.Common.Plugin.Modules;

namespace NKnife.Feature.PreferenceService
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IPreferenceService>? _preferenceManagerLazy;
        public IPreferenceService? PreferenceService => _preferenceManagerLazy!.Value;
        public override void Initialize()
        {
            _preferenceManagerLazy = GetModule<IPreferenceService>();
        }
    }
}