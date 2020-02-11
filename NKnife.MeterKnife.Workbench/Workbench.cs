﻿using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Workbench.Debugs;
using NKnife.MeterKnife.Workbench.Menus;
using NKnife.MeterKnife.Workbench.Properties;
using NKnife.MeterKnife.Workbench.Views;
using NKnife.Win.Quick;
using NKnife.Win.Quick.Menus;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.MeterKnife.Workbench
{
    public class Workbench : QuickForm
    {
        private readonly IHabitManager _habitManager;

        public Workbench(IHabitManager habitManager, DebuggerManager debuggerManager)
        {
            _habitManager = habitManager;
            GetHabitValueFunc = _habitManager.GetHabitValue;
            SetHabitAction = _habitManager.SetHabitValue;
            GetOptionValueFunc = _habitManager.GetOptionValue;
            SetOptionAction = _habitManager.SetOptionValue;
            GithubUpdateUser = "xknife-erian";
            GithubUpdateProject = "nknife.serial-protocol-debugger";

            Icon = Resources.meterknife_24px;
            var notifyIcon = new NotifyIcon();
            notifyIcon.Icon = Resources.meterknife_48px;
            BindNotifyIcon(notifyIcon);

            var fileMenuItem = new FileMenuItem();
            fileMenuItem.DropDownItems.Insert(0, new ToolStripSeparator());
            fileMenuItem.DropDownItems.Insert(0, DebuggerManager.GetDebugItem());

            var culture = habitManager.GetHabitValue(nameof(Global.Culture), Global.Culture);
            var themeName = habitManager.GetHabitValue("MainTheme", nameof(VS2015BlueTheme));
            var viewMenuItem = new ViewMenuItem();
            viewMenuItem.SetActiveCulture(culture);
            viewMenuItem.SetActiveTheme(themeName);
            ActiveDockPanelTheme(themeName);
            BindMainMenu(fileMenuItem, new DataMenuItem(), new MeasureMenuItem(), new ToolMenuItem(), viewMenuItem, new HelpMenuItem());
            BindTrayMenu(DebuggerManager.GetDebugItem(), DebuggerManager.GetDebugItem());
#if DEBUG
            BindMainMenu(debuggerManager.GetDebugMenu());
#endif
        }

        private void ActiveDockPanelTheme(string themeName)
        {
            switch (themeName)
            {
                case nameof(VS2015BlueTheme):
                default:
                    MainDockPanel.Theme = new VS2015BlueTheme();
                    break;
                case nameof(VS2015DarkTheme):
                    MainDockPanel.Theme = new VS2015DarkTheme();
                    break;
                case nameof(VS2015LightTheme):
                    MainDockPanel.Theme = new VS2015LightTheme();
                    break;
                case nameof(VS2013BlueTheme):
                    MainDockPanel.Theme = new VS2013BlueTheme();
                    break;
                case nameof(VS2013DarkTheme):
                    MainDockPanel.Theme = new VS2013DarkTheme();
                    break;
                case nameof(VS2013LightTheme):
                    MainDockPanel.Theme = new VS2013LightTheme();
                    break;
                case nameof(VS2012BlueTheme):
                    MainDockPanel.Theme = new VS2012BlueTheme();
                    break;
                case nameof(VS2012DarkTheme):
                    MainDockPanel.Theme = new VS2012DarkTheme();
                    break;
                case nameof(VS2012LightTheme):
                    MainDockPanel.Theme = new VS2012LightTheme();
                    break;
                case nameof(VS2005MultithreadingTheme):
                    MainDockPanel.Theme = new VS2005MultithreadingTheme();
                    break;
                case nameof(VS2005Theme):
                    MainDockPanel.Theme = new VS2005Theme();
                    break;
                case nameof(VS2003Theme):
                    MainDockPanel.Theme = new VS2003Theme();
                    break;
            }
        }

    }
}