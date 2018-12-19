using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Interfaces;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Interfaces.Measures;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Kernel.Plugins;
using NKnife.IoC;
using NKnife.Utility;

namespace MeterKnife.Kernel
{
    public class Kernels : IKernels
    {
        private static readonly ILog Logger = LogManager.GetLogger<Kernels>();

        private readonly IAppTrayService _appTrayService;
        private readonly IPluginService _pluginService;
        private readonly IGatewayService _gatewayService;
        private readonly IDatasService _datasService;
        private readonly IMeasureService _measureService;

        public Kernels()
        {
            _appTrayService = DI.Get<IAppTrayService>();
            _pluginService = DI.Get<IPluginService>();
            _gatewayService = DI.Get<IGatewayService>();
            _datasService = DI.Get<IDatasService>();
            _measureService = DI.Get<IMeasureService>();
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"����{_appTrayService.Description}...");
            _appTrayService.StartService();

            displayMessage($"����{_pluginService.Description}...");
            _pluginService.StartService();
            displayMessage("ע�����в�����...");

            displayMessage($"����{_datasService.Description}...");
            _datasService.StartService();

            displayMessage($"����{_gatewayService.Description}...");
            _gatewayService.StartService();

            displayMessage($"����{_measureService.Description}...");
            _measureService.StartService();

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public void UnloadCoreService()
        {
            _gatewayService.CloseService();
            _datasService.CloseService();
            _pluginService.CloseService();
            _measureService.StartService();
            _appTrayService.CloseService();
        }
    }
}