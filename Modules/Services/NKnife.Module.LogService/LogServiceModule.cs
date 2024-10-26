using LEIAO.Module.LogService.Internal;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Services.LogService;
using System.Collections.Immutable;
using System.Collections.Specialized;
using LEIAO.Mercury.Modules.FrontEnd;
using LEIAO.Mercury.Modules.Manager;
using LEIAO.Mercury.Plugin;
using RAY.Common.Authentication;
using RAY.Common.Plugin.Exceptions;
using RAY.Common.Plugin.Modules;

namespace LEIAO.Module.LogService
{
    public class LogServiceModule : BasePicoModule<ILogService>, ISupportUsingPlugin
    {
        private Lazy<IPluginManager>? _pluginManagerLazy;
        private Lazy<ILogService>? _logServiceLazy;
        private ILogService? _logService;

        /// <inheritdoc />
        public override Lazy<ILogService> Build(params object[] args)
        {
            return _logServiceLazy ??= new Lazy<ILogService>(() => _logService!);
        }

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            var pluginManager = _pluginManagerLazy?.Value!;
            var authManagerBuilder = pluginManager.FindModuleBuilder<IAuthenticationManager>();

            if (authManagerBuilder == null)
                throw new PluginDontFoundException($"{nameof(IAuthenticationManager)} dont found!");
            var authManager = authManagerBuilder.Build().Value;

            UpdateLogServiceTarget(pluginManager);
            _logService ??= new DefaultNLogService(expManager, expCoordinator, authManager);

            return Task.FromResult(true);
        }

        private static void UpdateLogServiceTarget(IPluginManager pluginManager)
        {
            foreach (var plugin in pluginManager.Plugins)
            {
                if (plugin is not IAutonomousLoggingCapability logging)
                    continue;
                foreach (var targetName in logging.LogTargetNameSet)
                {
                    var ns = plugin.GetType().Namespace!;
                    if (string.IsNullOrEmpty(ns))
                        throw new InvalidOperationException("The namespace of the module is empty.");
                    Context.AddTarget(targetName, ns);
                }
            }
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

        #region Implementation of ISupportKernel<IPluginManager>

        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IPluginManager> pluginManagerLazy)
        {
            _pluginManagerLazy = pluginManagerLazy;
            return Task.FromResult((IPicoPlugin)this);
        }
        #endregion
    }

    class Context
    {
        private static readonly Dictionary<string, StringCollection> s_targetDict = new();
        public static ImmutableDictionary<string, StringCollection> TargetDict => s_targetDict.ToImmutableDictionary();

        public static void AddTarget(string targetName, string moduleName)
        {
            if (!s_targetDict.TryGetValue(targetName, out var value))
            {
                s_targetDict.Add(targetName, [moduleName]);
            }
            else
            {
                value.Add(moduleName);
            }
        }
    }
}
