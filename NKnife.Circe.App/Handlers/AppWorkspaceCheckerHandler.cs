using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.Circe.Base.Modules.Services;
using NLog;
using RAY.Common.Plugin.Manager;
using RAY.Windows;
using System.Text;

namespace NKnife.Circe.App.Handlers
{
    public class AppWorkspaceCheckerHandler : BaseAppLifecycleHandler
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly string _errorMsg;
        private static IAppWorkspaceService? s_workspaceService;

        public AppWorkspaceCheckerHandler()
        {
            var sb = new StringBuilder();
            sb.AppendLine("无法找到应用程序运行时工作空间的管理器。");
            sb.AppendLine($"可能未实现[{nameof(IAppWorkspaceService)}]的基本功能，请检查软件环境是否安装妥善。");
            _errorMsg = sb.ToString();
        }

        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager = Ioc.Default.GetRequiredService<IPluginManager>();

            var workspaceServiceBuilder = pluginManager.FindModuleBuilder<IAppWorkspaceService>();
            s_workspaceService = workspaceServiceBuilder?.Build().Value;
            if(s_workspaceService != null)
                s_workspaceService.Initialize();
            else
                s_logger.Fatal(_errorMsg);

            return await base.HandleStartupAsync(startupArgs);
        }

        /// <inheritdoc />
        public override Task<bool> HandleExitAsync(int appExitCode)
        {
            if (s_workspaceService != null)
                s_workspaceService.Dispose();
            return base.HandleExitAsync(appExitCode);
        }
    }
}