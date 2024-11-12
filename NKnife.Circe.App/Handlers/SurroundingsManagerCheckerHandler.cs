using System.Text;
using CommunityToolkit.Mvvm.DependencyInjection;
using NKnife.Circe.Base.Modules.Manager;
using NLog;
using RAY.Common.Plugin.Manager;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    public class SurroundingsManagerCheckerHandler : BaseAppLifecycleHandler
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly string _errorMsg;
        private static ISurroundingsManager? s_surroundingsManager;

        public SurroundingsManagerCheckerHandler()
        {
            var sb = new StringBuilder();
            sb.AppendLine("无法找到环境管理器。");
            sb.AppendLine($"可能未实现[{nameof(ISurroundingsManager)}]的基本功能，请检查软件环境是否安装妥善。");
            _errorMsg = sb.ToString();
        }

        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager = Ioc.Default.GetRequiredService<IPluginManager>();

            var surroundingsManagerBuilder = pluginManager.FindModuleBuilder<ISurroundingsManager>();
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