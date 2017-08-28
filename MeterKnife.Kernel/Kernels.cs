using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel
{
    public class Kernels : IKernels
    {
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        private readonly IAppTrayService _AppTrayService;
        private readonly IPluginService _PluginService;
        private readonly IGatewayService _GatewayService;

        public Kernels()
        {
            _AppTrayService = DI.Get<IAppTrayService>();
            _PluginService = DI.Get<IPluginService>();
            _GatewayService = DI.Get<IGatewayService>();
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("���س������̷���...");
            _AppTrayService.StartService();

            displayMessage("���ز������...");
            _PluginService.StartService();
            displayMessage("ע�����в�����...");

            displayMessage("����Getway����...");
            _GatewayService.StartService();

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public void UnloadCoreService()
        {
            _GatewayService.CloseService();
            _PluginService.CloseService();
            _AppTrayService.CloseService();
        }
    }
}