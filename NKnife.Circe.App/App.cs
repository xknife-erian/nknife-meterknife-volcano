using System.Windows;
using RAY.Common;
using RAY.Common.NLogConf;
using RAY.Windows;
using RAY.Windows.Common;

namespace NKnife.Circe.App
{
    public partial class App
    {
        private readonly AppLifecycleMapper _appLifecycleMapper;

        public App()
        {
            NLogConfig.ConfigureNLog();
            DispatcherUtil.Initialize();
            LogStack.UIDispatcher = DispatcherUtil.CheckBeginInvokeOnUI;

            BaseAppLifecycleHandler.AbortStartup = Shutdown;

            _appLifecycleMapper = new AppLifecycleMapper(this);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await _appLifecycleMapper.HandleStartupAsync(e.Args);
        }

        /// <inheritdoc />
        protected override async void OnExit(ExitEventArgs e)
        {
            await _appLifecycleMapper.HandleExitAsync(e.ApplicationExitCode);
            base.OnExit(e);

            try
            {
                Application.Current.Shutdown();
            }
            catch (Exception)
            {
                for (var i = 0; i < 3; i++)
                {
                    await Task.Delay(1000);
                    Environment.Exit(0);
                }
            }
            finally
            {
                //通常不需要执行，为确保程序退出，如果上面的代码无法退出程序，这里再次尝试退出程序
                Environment.Exit(0);
            }
        }
    }
}