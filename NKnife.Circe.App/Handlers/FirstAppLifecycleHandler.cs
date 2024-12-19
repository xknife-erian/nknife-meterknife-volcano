using RAY.Windows;
using RAY.Windows.WinApi;

namespace NKnife.Circe.App.Handlers
{
    public class FirstAppLifecycleHandler : BaseAppLifecycleHandler
    {
        public override async Task<bool> HandleStartupAsync(string[] startupArgs)
        {
            WindowsKernel.PreventSystemSleepAndLock();
            return await base.HandleStartupAsync(startupArgs);
        }

        public override async Task<bool> HandleExitAsync(int appExitCode)
        {
            WindowsKernel.RestoreSystemSleepAndLock();
            return await base.HandleExitAsync(appExitCode);
        }
    }
}