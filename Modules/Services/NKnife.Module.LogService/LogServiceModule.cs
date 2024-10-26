using NKnife.Module.LogService.Internal;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Exceptions;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;
using RAY.Common.Services.LogService;

namespace NKnife.Module.LogService
{
    public class LogServiceModule : BasePicoModule<ILogService>, ISupportUsingPlugin, ISupportUsingModule
    {
        private readonly ModuleContext _moduleContext = new();
        private Lazy<ILogService>? _logServiceLazy;
        private ILogService? _logService;

        /// <inheritdoc />
        public override Lazy<ILogService> Build(params object[] args)
        {
            return _logServiceLazy ??= new Lazy<ILogService>(() =>
            {
                _logService ??= new DefaultNLogService(_moduleContext.AuthManager);

                return _logService;
            });
        }

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            _moduleContext.Initialize();
            _moduleContext.ModulesManager.AllModulesStarted += (_, _) =>
            {
                UpdateLogServiceTarget(_moduleContext.PluginManager);
            };

            return Task.FromResult(true);
        }

        private void UpdateLogServiceTarget(IPluginManager pluginManager)
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
                    _moduleContext.AddTarget(targetName, ns);
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


        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IPluginManager> pluginManagerLazy)
        {
            _moduleContext.SetPluginManager(pluginManagerLazy);
            return Task.FromResult((IPicoPlugin)this);
        }

        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> moduleManagerLazy)
        {
            _moduleContext.SetModulesManager(moduleManagerLazy);
            return Task.FromResult((IPicoPlugin)this);
        }
    }
}
