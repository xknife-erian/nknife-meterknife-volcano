using System.Windows;
using NKnife.Circe.App.Handlers;
using NLog;
using RAY.Windows;
using RAY.Windows.App;

namespace NKnife.Circe.App
{
    /// <summary>
    /// App����������ڹؼ��¼���������ӳ����App����Ҫ�¼������⽫�����߼�������App���С�
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

        //��������������������
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

        //�����˳�������������
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