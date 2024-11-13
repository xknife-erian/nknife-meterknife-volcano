using RAY.Common;
using RAY.Common.Plugin.Modules;

namespace NKnife.Feature.OptionManager
{
    class ModuleContext : BaseModuleContext
    {
        private Lazy<IOptionManager>? _optionManagerLazy;
        public IOptionManager? OptionManager => _optionManagerLazy!.Value;
        public override void Initialize()
        {
            _optionManagerLazy = GetModule<IOptionManager>();
        }
    }
}