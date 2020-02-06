using System;
using System.Threading;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class AppManager : IAppManager
    {
        private readonly IAppTrayService _trayService;
        private readonly IDialogService _dialogService;

        public AppManager(IAppTrayService trayService, IDialogService dialogService)
        {
            _trayService = trayService;
            _dialogService = dialogService;
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            displayMessage($"����{_trayService.Description}...");
            _trayService.StartService();
            Thread.Sleep(1 * 1000);
            displayMessage($"����{_dialogService.Description}...");
            _dialogService.StartService();
            Thread.Sleep(1 * 1000);

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public void UnloadCoreService()
        {
            _trayService.CloseService();
        }
    }
}