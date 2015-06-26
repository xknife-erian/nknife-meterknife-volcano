﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Util;
using MeterKnife.Workbench.Dialogs;
using NKnife.IoC;
using NKnife.NLog3.Controls;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Lite
{
    public partial class MeterLiteMainForm : Form
    {
        private readonly DockPanel _DockPanel = new DockPanel();

        private int _SerialPort;

        private IPAddress _IpAddress;

        private int _SocketPort;

        private CommunicationType _CommunicationType;

        public MeterLiteMainForm()
        {
            InitializeComponent();
            InitializeDockPanel();
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_DockPanel);

            _DockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _DockPanel.Dock = DockStyle.Fill;
            _DockPanel.BringToFront();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var dialog = new InterfaceSelectorDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _SerialPort = dialog.Serial;
                _IpAddress = dialog.IpAddress;
                _SocketPort = dialog.Port;
                if (dialog.IsSerial)
                {
                    _PortLabel.Text = string.Format("COM{0}", dialog.Serial);
                    _CommunicationType = CommunicationType.Serial;
                }
                else
                {
                    _PortLabel.Text = string.Format("{0}:{1}", dialog.IpAddress, dialog.Port);
                    _CommunicationType = CommunicationType.Socket;
                }
                AddMeterView();
            }
        }

        private void _ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _AddMeterMenuItem_Click(object sender, EventArgs e)
        {
            AddMeterView();
        }

        private void AddMeterView()
        {
            Dictionary<int, List<int>> dic = DI.Get<IMeterKernel>().GpibDictionary;
            List<int> gpibList;
            if (!dic.TryGetValue(_SerialPort, out gpibList))
            {
                gpibList = new List<int>();
                dic.Add(_SerialPort, gpibList);
            }

            var dialog = new AddMeterLiteDialog();
            dialog.GpibList.AddRange(gpibList);
            dialog.Port = _SerialPort;

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                var meterView = DI.Get<MeterLiteView>();
                meterView.Port = dialog.Port;
                meterView.CommunicationType = _CommunicationType;
                meterView.SetMeter(dialog.Port, dialog.Meter);
                meterView.Text = dialog.Meter.AbbrName;
                meterView.Show(_DockPanel, DockState.Document);
                dic[_SerialPort].Add(dialog.GpibAddress);
            }
        }

        private void _CareOptionMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new CareParameterDialog(_SerialPort);
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
            }
        }

        private void _AboutMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new AboutDialog();
            dialog.ShowDialog(this);
        }

        private void _LoggerMenuItem_Click(object sender, EventArgs e)
        {
            var loggerForm = new NLogForm {TopMost = true};
            loggerForm.Show();
        }
    }
}