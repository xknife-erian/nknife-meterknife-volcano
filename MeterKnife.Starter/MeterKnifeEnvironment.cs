﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Workbench;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Starter
{
    internal class MeterKnifeEnvironment : ApplicationContext
    {
        private static readonly ILog _logger = LogManager.GetLogger<MeterKnifeEnvironment>();

        private static BaseCareCommunicationService _careComm;

        public MeterKnifeEnvironment()
        {
            Initialize();
        }

        public void Initialize()
        {
            // 应用程序退出
            Application.ApplicationExit += OnApplicationExit;

            OptionInitializes();

            //开启欢迎屏幕
            Splasher.Show(typeof(SplashForm));
            Splasher.Status = "参数初始化进行中......";
            Thread.Sleep(200);
            Splasher.Status = "加载运行参数......";


            Splasher.Status = "参数初始化完成，启动主窗体";
            Thread.Sleep(200);

            //开启UI控制窗体
            var workbench = new MainWorkbench();
            workbench.FormClosed += (s, e) => Application.Exit();
            workbench.Activated += WorkbenchOnActivated;

            var thread = new Thread(BeginInitializeServices) { IsBackground = true };
            thread.Start();

            workbench.Show();
            workbench.Activate();
        }

        private void WorkbenchOnActivated(object sender, EventArgs eventArgs)
        {
            Splasher.Close();
            ((MainWorkbench) sender).Activated -= WorkbenchOnActivated;
        }

        private void BeginInitializeServices()
        {
            _logger.Info("启动Care通讯服务");
            _careComm = DI.Get<BaseCareCommunicationService>();
            _careComm.Initialize();
        }

        /// <summary>
        ///     应用程序退出
        /// </summary>
        private static void OnApplicationExit(object sender, EventArgs ex)
        {
            try
            {
                //处理程序退出前要处理的东西
                _careComm.Destroy();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                _logger.Warn(e.Message);
            }
        }

        /// <summary>
        ///     初始化“选项”服务
        /// </summary>
        /// <returns></returns>
        private bool OptionInitializes()
        {
            return true;
        }
    }
}