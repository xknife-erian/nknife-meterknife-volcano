using System;
using Common.Logging;

namespace MeterKnife.Kernel
{
    public class Kernels
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("���غ��ķ��񼰲��...");
            // ���ز�ע����
            // ClientSender.SendSplashMessage("���ز��...");
            // _PluginManager = DI.Get<IPluginManager>();
            // if (_PluginManager.StartService())
            // {
            //     ClientSender.SendSplashMessage("ע����...");
            //     _PluginManager.RegistPlugIns(DI.Get<IExtenderProvider>());
            // }
            displayMessage("���غ��ķ��񼰲�����...");
            _logger.Info("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }
        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public static void UnloadCoreService()
        {
            //��������˳�ǰҪ����Ķ���
        }
    }
}