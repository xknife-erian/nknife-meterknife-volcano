using System;
using System.Collections.Generic;
using NKnife.Interface;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class AppManager : IAppManager
    {
        private readonly List<IEnvironmentItem> _envItemList = new List<IEnvironmentItem>();

        public AppManager(IFileService fileService, IAntCollectService antCollectService, IMeasuringLogic storageLogic)
        {
            //�������Ĵ����������յ�����ʱ�����ô洢�߼��������ݴ���
            antCollectService.Collected += async (s, e) => { await storageLogic.ProcessAsync(e.DUT, e.Measurements); };
            _envItemList.AddRange(new IEnvironmentItem[] {fileService, antCollectService});
            _envItemList.Sort((x, y) => x.Order.CompareTo(y.Order));
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>
        public void LoadCoreService(Action<string> displayMessage)
        {
            foreach (var item in _envItemList)
            {
                displayMessage($"����{item.Description}...");
                item.StartService();
            }

            displayMessage("���غ��ķ��񼰲�����,�رջ�ӭ����.");
        }

        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        public void UnloadCoreService()
        {
            for (var i = _envItemList.Count - 1; i >= 0; i--)
                _envItemList[i].CloseService();
        }
    }
}