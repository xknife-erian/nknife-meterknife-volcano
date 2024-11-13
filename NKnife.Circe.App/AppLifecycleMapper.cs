using System.Windows;
using NKnife.Circe.App.Handlers;
using NLog;
using RAY.Windows;
using RAY.Windows.App;

namespace NKnife.Circe.App
{
    /// <summary>
    /// App类的生命周期关键事件管理器，映射了App的主要事件，避免将所有逻辑都放在App类中。
    /// </summary>
    public class AppLifecycleMapper
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
        private readonly Application _application;

        private readonly IAppLifecycleHandler _firstStartupHandler;
        private readonly IAppLifecycleHandler _firstExitHandler;

        public AppLifecycleMapper(Application application)
        {
            _application = application;

            var processor = new AppUnhandledExceptionProcessor();
            processor.SetupUnhandledExceptionHandler(application);

            _firstStartupHandler = CreateStartupHandler();
            _firstExitHandler = CreateExitHandler();
        }

        //创建启动处理器责任链
        private IAppLifecycleHandler CreateStartupHandler()
        {
            var first = new FirstAppLifecycleHandler()
            {
                Next = new PluginLoadHandler()
                {
                    Next = new LogServiceSetterHandler()
                    {
                        Next = new AppWorkspaceCheckerHandler()
                        {
                            Next = new ShowWorkbenchHandler()
                        }
                    }
                }
            };

            return first;
        }

        //创建退出处理器责任链
        private static IAppLifecycleHandler CreateExitHandler()
        {
            var first = new FirstAppLifecycleHandler()
            {
                Next = new PluginUnloadHandler()
                {
                    Next = new AppWorkspaceCheckerHandler()
                }
            };
            return first;
        }

        public async Task HandleStartupAsync(string[] startupArgs)
        {
            await _firstStartupHandler.HandleStartupAsync(startupArgs);
        }

        public async Task HandleExitAsync(int appExitCode)
        {
            await _firstExitHandler.HandleExitAsync(appExitCode);
        }
    }
}