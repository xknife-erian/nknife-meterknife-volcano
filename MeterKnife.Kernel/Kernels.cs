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
        private readonly IDatabaseService _DatabaseService;
        private readonly IMeasureService _MeasureService;

        public Kernels()
        {
            _AppTrayService = DI.Get<IAppTrayService>();
            _PluginService = DI.Get<IPluginService>();
            _GatewayService = DI.Get<IGatewayService>();
            _DatabaseService = DI.Get<IDatabaseService>();
            _MeasureService = DI.Get<IMeasureService>();
        }

        /// <summary>
        ///     加载核心服务及插件
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("加载程序托盘服务...");
            _AppTrayService.StartService();

            displayMessage("加载插件服务...");
            _PluginService.StartService();
            displayMessage("注册所有插件完成...");

            displayMessage("加载数据库服务...");
            _DatabaseService.StartService();

            displayMessage("加载Getway服务...");
            _GatewayService.StartService();

            displayMessage($"加载{_MeasureService.Description}...");
            _MeasureService.StartService();

            displayMessage("加载核心服务及插件完成,关闭欢迎界面.");
        }

        /// <summary>
        ///     卸载核心服务及插件
        /// </summary>
        public void UnloadCoreService()
        {
            _GatewayService.CloseService();
            _DatabaseService.CloseService();
            _PluginService.CloseService();
            _MeasureService.StartService();
            _AppTrayService.CloseService();
        }
    }
}