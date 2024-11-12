using System.ComponentModel;
using System.Text;
using System.Windows;
using CommunityToolkit.Mvvm.DependencyInjection;
using NLog;
using RAY.Common.Plugin.Manager;
using RAY.Common.UI;
using RAY.Windows;
using RAY.Windows.Common;

namespace NKnife.Circe.App.Handlers
{
    internal class ShowWorkbenchHandler : BaseAppLifecycleHandler
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly string _errorMsg;

        public ShowWorkbenchHandler()
        {
            var sb = new StringBuilder();
            sb.AppendLine("无法找到主窗体。");
            sb.AppendLine($"可能未实现[{nameof(IWorkbench)}]的基本功能，请检查软件环境是否安装妥善。");
            _errorMsg = sb.ToString();
        }

        /// <inheritdoc />
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            var pluginManager    = Ioc.Default.GetRequiredService<IPluginManager>();
            var workbenchBuilder = pluginManager.FindModuleBuilder<IWorkbench>();
            var builderResult    = workbenchBuilder?.Build().Value;

            if(builderResult is not Window workbench)
            {
                MessageBox.Show(_errorMsg, "软件即将关闭", MessageBoxButton.OK, MessageBoxImage.Error);
                s_logger.Fatal(_errorMsg);
                AbortStartup?.Invoke();

                return false;
            }

            SimpleNamedPipeClient.SendStartArgsThroughPipe(startupArgs);

            workbench.ContentRendered += OnWorkbenchContentRendered;
            workbench.Closing         += OnWorkbenchClosing;
            workbench.Closed          += OnWorkbenchClosed;
#if RELEASE
            workbench.WindowState = WindowState.Maximized;
#endif
            workbench.Show();

            return await base.HandleStartupAsync(startupArgs);
        }

        private void OnWorkbenchClosed(object? sender, EventArgs e) { }

        private void OnWorkbenchClosing(object? sender, CancelEventArgs e) { }

        private void OnWorkbenchContentRendered(object? sender, EventArgs e) { }
    }
}