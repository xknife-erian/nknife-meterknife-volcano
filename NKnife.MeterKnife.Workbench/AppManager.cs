using System;
using System.Collections.Generic;
using System.Threading;
using NKnife.Interface;
using NKnife.MeterKnife.Workbench.Base;

namespace NKnife.MeterKnife.Workbench
{
    public class AppManager : IAppManager
    {
        private readonly List<IEnvironmentItem> _envItemList = new List<IEnvironmentItem>();

        public AppManager(IDialogService dialogService, IFileService fileService)
        {
            _envItemList.AddRange(new IEnvironmentItem[]{dialogService, fileService});
            _envItemList.Sort((x, y) => x.Order.CompareTo(y.Order));
        }

        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>       
        public void LoadCoreService(Action<string> displayMessage)
        {
            for (int i = 0; i < _envItemList.Count; i++)
            {
                var item = _envItemList[i];
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
            for (int i = _envItemList.Count - 1; i >= 0; i--)
            {
                _envItemList[i].CloseService();
            }
        }
    }
}