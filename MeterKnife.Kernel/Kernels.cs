using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel
{
    public class Kernels
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        private static IPluginService _pluginService;
        private static IGatewayService _gatewayService;

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("���ز������...");
            _pluginService = DI.Get<IPluginService>();
            _pluginService.StartService();
            displayMessage("ע�����в�����...");

            displayMessage("����Getway����...");
            _gatewayService = DI.Get<IGatewayService>();
            _gatewayService.StartService();

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public static void UnloadCoreService()
        {
            _gatewayService.CloseService();
            _pluginService.CloseService();
        }
    }
}