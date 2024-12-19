using RAY.Windows;
using System.Text;

namespace NKnife.Circe.App.Handlers
{
    class VerifySerialKeyHandler : BaseAppLifecycleHandler
    {
        private readonly string _errorMsg;

        public VerifySerialKeyHandler()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Mercury软件未注册序列号无法使用。");
            sb.AppendLine();
            sb.AppendLine("请联系雷奥工作人员获取正版安装包与序列号。");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("客服电话: 010-69737829");
            sb.AppendLine("官方网站: https://www.labautobio.com");
            _errorMsg = sb.ToString();
        }

        /// <inheritdoc />
        public override Task<bool> HandleStartupAsync(string[] startupArgs)
        {
#if DEBUG
            return base.HandleStartupAsync(startupArgs);
#else
            // 检测是否已注册
            var license = RegistryUtil.ReadSerialKey();

            if(string.IsNullOrEmpty(license)
               || license.Length != 32)
            {
                MessageBox.Show(_errorMsg, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                AbortStartup?.Invoke();
                return Task.FromResult(false);
            }

            return base.HandleStartupAsync(startupArgs);
#endif
        }
    }
}