using CommunityToolkit.Mvvm.DependencyInjection;
using RAY.Common.NLogConf;
using RAY.Common.Plugin.Manager;
using RAY.Common.Services.LogService;
using RAY.Windows;

namespace NKnife.Circe.App.Handlers
{
    class LogServiceSetterHandler : BaseAppLifecycleHandler
    {
        /// <inheritdoc />
        public override Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager = Ioc.Default.GetRequiredService<IPluginManager>();
            var logService    = pluginManager.FindModuleBuilder<ILogService>();
            WpfLoggerViewTarget.SetLogService(logService);

            return base.HandleStartupAsync(startupArgs);
        }
    }
}