using CommunityToolkit.Mvvm.DependencyInjection;
using RAY.Common.Plugin.Manager;
using RAY.Windows;

namespace NKnife.Circe.App.Handlers
{
    class PluginUnloadHandler : BaseAppLifecycleHandler
    {
        /// <inheritdoc />
        public override async Task<bool> HandleExitAsync(int appExitCode)
        {
            var pluginManager = Ioc.Default.GetRequiredService<IPluginManager>();
            await pluginManager.StopAsync();
            return await base.HandleExitAsync(appExitCode);
        }
    }
}