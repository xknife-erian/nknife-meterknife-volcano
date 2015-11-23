﻿using System;
using System.IO.Ports;
using System.Net;
using System.Windows.Forms;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using NKnife.GUI.WinForm;
using NKnife.Utility;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class InterfaceSelectorDialog : SimpleForm
    {
        public InterfaceSelectorDialog()
        {
            InitializeComponent();
            FillSerialComonbox();
            _SerialRadioButton.CheckedChanged += (s, e) =>
            {
                _SerialComboBox.Enabled = _SerialRadioButton.Checked;
                _PortNumberBox.Enabled = _SerialRadioButton.Checked;
                _RefreshButton.Enabled = _SerialRadioButton.Checked;
                _IpAddressControl.Enabled = !_SerialRadioButton.Checked;
                _PortNumericUpDown.Enabled = !_SerialRadioButton.Checked;
            };
            _LanRadioButton.CheckedChanged += (s, e) =>
            {
                _SerialComboBox.Enabled = !_LanRadioButton.Checked;
                _PortNumberBox.Enabled = !_LanRadioButton.Checked;
                _RefreshButton.Enabled = !_LanRadioButton.Checked;
                _IpAddressControl.Enabled = _LanRadioButton.Checked;
                _PortNumericUpDown.Enabled = _LanRadioButton.Checked;
            };
            _SerialRadioButton.Checked = true;
            _PortNumberBox.SelectedItem = "115200";
        }

        private void FillSerialComonbox()
        {
            _SerialComboBox.Items.Clear();
            var list = SerialPort.GetPortNames();
            foreach (var s in list)
            {
                _SerialComboBox.Items.Add(s);
            }
            if (!UtilityCollection.IsNullOrEmpty(list))
            {
                _SerialComboBox.SelectedItem = list[list.Length - 1];
            }
        }

        public bool IsSerial { get { return _SerialRadioButton.Checked; } }

        public CommPort CarePort
        {
            get
            {
                if (IsSerial)
                {
                    var v = _SerialComboBox.Text.TrimStart(new[] {'C', 'O', 'M'});
                    var c = int.Parse(v);
                    return CommPort.Build(TunnelType.Serial, c.ToString(), _PortNumberBox.Text);
                }
                else
                {
                    string value = _IpAddressControl.Text == "..."
                        ? "0.0.0.0"
                        : _IpAddressControl.Text;
                    return CommPort.Build(TunnelType.Tcpip, value, _PortNumericUpDown.Text);
                }
            }
        }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            if (IsSerial)
            {
                if (string.IsNullOrEmpty(_SerialComboBox.Text))
                {
                    MessageBox.Show(this, "必须选择一个串口");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_IpAddressControl.Text) || _IpAddressControl.Text == "...")
                {
                    MessageBox.Show(this, "必须设置一个IP地址");
                    return;
                }
                IPAddress temp;
                if (!IPAddress.TryParse(_IpAddressControl.Text, out temp))
                {
                    MessageBox.Show(this, "必须设置一个正确的IP地址");
                    return;
                }
                if (string.IsNullOrEmpty(_PortNumericUpDown.Text))
                {
                    MessageBox.Show(this, "必须设置一个TCPIP通讯端口");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _RefreshButton_Click(object sender, EventArgs e)
        {
            FillSerialComonbox();
        }

        private void _CloseButton_Click(object sender, EventArgs e)
        {
            var rs = MessageBox.Show(this, "您确认关闭应用程序么？", "关闭",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (rs == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}