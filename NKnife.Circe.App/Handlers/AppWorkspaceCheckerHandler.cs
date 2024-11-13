using System.Text;
using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.Circe.Base.Modules.Manager;
using NLog;
using RAY.Common.Plugin.Manager;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    public class AppWorkspaceCheckerHandler : BaseAppLifecycleHandler
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly string _errorMsg;
        private static IAppWorkspaceManager? s_surroundingsManager;

        public AppWorkspaceCheckerHandler()
        {
            var sb = new StringBuilder();
            sb.AppendLine("无法找到应用程序运行时工作空间的管理器。");
            sb.AppendLine($"可能未实现[{nameof(IAppWorkspaceManager)}]的基本功能，请检查软件环境是否安装妥善。");
            _errorMsg = sb.ToString();
        }

        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager = Ioc.Default.GetRequiredService<IPluginManager>();

            var surroundingsManagerBuilder = pluginManager.FindModuleBuilder<IAppWorkspaceManager>();
            s_surroundingsManager = surroundingsManagerBuilder?.Build().Value;
            if(s_surroundingsManager != null)
                s_surroundingsManager.Initialize();
            else
                s_logger.Fatal(_errorMsg);

            return await base.HandleStartupAsync(startupArgs);
        }

        /// <inheritdoc />
        public override Task<bool> HandleExitAsync(int appExitCode)
        {
            if (s_surroundingsManager != null)
                s_surroundingsManager.Stop();
            return base.HandleExitAsync(appExitCode);
        }
    }
}