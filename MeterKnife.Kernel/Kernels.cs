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
        private static readonly ILog _logger = LogManager.GetLogger<Kernels>();

        private readonly IAppTrayService _AppTrayService;
        private readonly IPluginService _PluginService;
        private readonly IGatewayService _GatewayService;
        private readonly IDatasService _DatasService;
        private readonly IMeasureService _MeasureService;

        public Kernels()
        {
            _AppTrayService = DI.Get<IAppTrayService>();
            _PluginService = DI.Get<IPluginService>();
            _GatewayService = DI.Get<IGatewayService>();
            _DatasService = DI.Get<IDatasService>();
            _MeasureService = DI.Get<IMeasureService>();
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"����{_AppTrayService.Description}...");
            _AppTrayService.StartService();

            displayMessage($"����{_PluginService.Description}...");
            _PluginService.StartService();
            displayMessage("ע�����в�����...");

            displayMessage($"����{_DatasService.Description}...");
            _DatasService.StartService();

            displayMessage($"����{_GatewayService.Description}...");
            _GatewayService.StartService();

            displayMessage($"����{_MeasureService.Description}...");
            _MeasureService.StartService();

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public void UnloadCoreService()
        {
            _GatewayService.CloseService();
            _DatasService.CloseService();
            _PluginService.CloseService();
            _MeasureService.StartService();
            _AppTrayService.CloseService();
        }
    }
}