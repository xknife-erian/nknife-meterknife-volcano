using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.Module.Data.Storages;
using RAY.Common.Plugin.Manager;
using RAY.Plugins;
using RAY.Plugins.WPF;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    internal class PluginLoadHandler : BaseAppLifecycleHandler
    {
        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager = LaunchPicoPluginSystem();

            await pluginManager.StartAsync();
            await pluginManager.RegisterUIAddinsAsync();

            return await base.HandleStartupAsync(startupArgs);
        }

        // 配置并启动插件系统
        private static IPluginManager LaunchPicoPluginSystem()
        {
            // 配置IoC服务提供器
            PicoPluginSystem.AddServiceProviderHandle(Ioc.Default.ConfigureServices);

            // 配置程序集处理程序

            // 配置类型处理程序
            PicoPluginSystem.AddTypeHandler(RepositoriesAutofacModule.ConfiguresRepository);

            var pluginManager = PicoPluginSystem.Launch();
            return pluginManager;
        }
    }
}
