using System;
using System.Collections.Generic;
using Common.Logging;
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

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public static void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage("������ķ���...");
            _pluginService = DI.Get<IPluginService>();

            displayMessage("��������...");
            FindTypes();

            displayMessage("���غ��ķ��񼰲��...");
            _pluginService.StartService();

            displayMessage("ע�����в�����...");

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        private static void FindTypes()
        {
            var assems = UtilityAssembly.SearchAssemblyByDirectory(AppDomain.CurrentDomain.BaseDirectory);
            foreach (var assembly in assems)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.ContainsInterface(typeof(IPlugIn)))
                    {
                        var plugin = (IPlugIn) UtilityType.CreateObject(type, type, false);
                        _pluginService.Plugins.Add(plugin);
                        _logger.Info($"{type.FullName}�����ɹ�, ��ǰ��{_pluginService.Plugins.Count}��plugin.");
                    }
                }
            }
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