using System.Collections.Immutable;
using System.Collections.Specialized;
using RAY.Common.Authentication;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.LogService
{
    class ModuleContext : BaseModuleContext
    {
        private readonly Dictionary<string, StringCollection> _targetDict = new();
        public ImmutableDictionary<string, StringCollection> TargetDict => _targetDict.ToImmutableDictionary();
        public void AddTarget(string targetName, string moduleName)
        {
            if (!_targetDict.TryGetValue(targetName, out var value))
            {
                _targetDict.Add(targetName, [moduleName]);
            }
            else
            {
                value.Add(moduleName);
            }
        }

        private Lazy<IPluginManager>? _pluginManagerLazy;
        private Lazy<IAuthenticationManager>? _authManagerLazy;
        public IPluginManager PluginManager => _pluginManagerLazy!.Value;
        public IAuthenticationManager AuthManager => _authManagerLazy!.Value;
        public IModulesManager ModulesManager => ModulesManagerLazy!.Value;

        public void SetPluginManager(Lazy<IPluginManager> pluginManagerLazy)
        {
            _pluginManagerLazy = pluginManagerLazy;
        }

        public override void Initialize()
        {
            _authManagerLazy = GetModule<IAuthenticationManager>();
        }
    }
}