﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.Events;
using NKnife.GUI.WinForm;
using NKnife.IoC;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class CareParameterDialog : SimpleForm
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareParameterDialog>();
        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private readonly CarePort _Port;
        private readonly CareConfigHandler _Handler = new CareConfigHandler();

        public CareParameterDialog(CarePort port)
        {
            _Port = port;
            InitializeComponent();
            _Usart1NumberBox.SelectedItem = "115200";
            _Usart2NumberBox.SelectedItem = "115200";
            _DhcpDisableRadioButton.CheckedChanged += (s, e) =>
            {
                _DhcpGroupBox.Enabled = _DhcpDisableRadioButton.Checked;
            };
            _UsartSwitchCheckBox.CheckedChanged += (s, e) =>
            {
                if (_UsartSwitchCheckBox.Checked)
                {
                    MessageBox.Show(this, "WIFI透传模式说明：\r\n\r\nWIFI透传模式生效后，USB与仪器的连接将暂时失效，同时指示灯将快速闪烁……\r\n如果想退出透传模式时，请按住设备按键4秒以上，并重启本软件进行应用", "透传模式",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };
            _Handler.CareConfigging += OnProtocolRecevied;
            _Comm.Bind(port, _Handler);
        }

        protected override void OnClosed(EventArgs e)
        {
            _Handler.CareConfigging -= OnProtocolRecevied;
            base.OnClosed(e);
            _Comm.Remove(_Port, _Handler);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var task = new Task(QueryCareParameter);
            task.Start();
        }

        /// <summary>
        /// 发出获取Care的参数指令
        /// </summary>
        private void QueryCareParameter()
        {
            for (int i = 0xD0; i <= 0xDF; i++)
            {
                if (i == 0xD8)
                    continue;
                var data = CommandUtil.CareGetter((byte) i);
                _Comm.SendCommands(_Port, data);
                Thread.Sleep(20);
            }
        }

        private void OnProtocolRecevied(object sender, EventArgs<CareTalking> e)
        {
            var talking = e.Item;
            if (talking == null)
                return;
            switch (talking.MainCommand)
            {
                case 0xA0://查询Care的参数，解析，并填充到面板上显示
                    ParseCareParameter(talking);
                    break;
                case 0xB0:
                    SetCareParameter();
                    break;
            }
        }

        /// <summary>
        /// 查询Care的参数，解析，并填充到面板上显示
        /// </summary>
        private void ParseCareParameter(CareTalking talking)
        {
            try
            {
                switch (talking.SubCommand)
                {
                    #region case
                    case 0xD0:
                        this.ThreadSafeInvoke(() =>
                        {
                            Text = string.Format("{0}{1}", "Care Build:", talking.Scpi);
                        });
                        break;
                    case 0xD2: //查询设备版本
                        this.ThreadSafeInvoke(() =>
                        {
                            _MainGroupBox.Text = string.Format("Care.v{0}", talking.Scpi);
                        });
                        break;
                    case 0xD3: //查询DHCP是否激活（0x00:未激活;0x01:激活）
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes[0];
                            if (bs == 0x00)
                            {
                                _DhcpDisableRadioButton.Checked = true;
                                _DhcpEnableRadioButton.Checked = false;
                            }
                            else
                            {
                                _DhcpDisableRadioButton.Checked = false;
                                _DhcpEnableRadioButton.Checked = true;
                            }
                        });
                        break;
                    case 0xD4: //查询Care的IP地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _IpAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD5: //查询Care的网关地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _GatwayAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD6: //查询Care的子网掩码；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _MaskAddressControl.SetAddressBytes(bs);
                        });
                        break;
                    case 0xD7: //查询Care的TCP Socket Server端口；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            var port = BitConverter.ToInt16(bs.Reverse().ToArray(), 0);
                            _TcpNumericUpDown.Value = port;
                        });
                        break;
                    case 0xDA: //查询温湿度传感器类型，18B20 or DHT22
                        this.ThreadSafeInvoke(() =>
                        {
                            switch (talking.Scpi.TrimEnd('\n'))
                            {
                                case "DS18B20":
                                    _18b20RadioButton.Checked = true;
                                    break;
                                case "DHT11":
                                    _Dht11RadioButton.Checked = true;
                                    break;
                                case "DHT22":
                                    _Dht22RadioButton.Checked = true;
                                    break;
                            }
                        });
                        break;
                    case 0xDB: //查询透明协议时仪器的GPIB地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes[0];
                            _USBGpibNumericUpDown.Value = (int)bs;
                            bs = talking.ScpiBytes[2];
                            _LANGpibNumericUpDown.Value = (int)bs;
                            bs = talking.ScpiBytes[1];
                            _WifiGpibNumericUpDown.Value = (int)bs;
                        });
                        break;
                    case 0xDC: //查询Care的MAC地址；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            _MacTextBox.Text = bs.ToHexString(':');
                        });
                        break;
                    case 0xDD: //查询USB串口波特率；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            var port = BitConverter.ToInt32(bs.Reverse().ToArray(), 0);
                            _Usart1NumberBox.SelectedItem = port.ToString();
                        });
                        break;
                    case 0xDE: //查询WIFI模块前置串口波特率；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes;
                            var port = BitConverter.ToInt32(bs.Reverse().ToArray(), 0);
                            _Usart2NumberBox.SelectedItem = port.ToString();
                        });
                        break;
                    case 0xDF: //查询USB串口与WIFI模块前置串口是否可以互相转发；
                        this.ThreadSafeInvoke(() =>
                        {
                            var bs = talking.ScpiBytes[0];
                            _UsartSwitchCheckBox.Checked = (bs != 0x00);
                        });
                        break;
                    #endregion
                }
            }
            catch (Exception e)
            {
                _logger.Warn("解析Care参数协议填充到面板上显示异常", e);
            }
        }

        private void SetCareParameter()
        {
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// 根据用户设置的值进行设置
        /// </summary>
        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            //是否启用DHCP
            var dhcpDisable = _DhcpDisableRadioButton.Checked;
            var dhcp = dhcpDisable ? 0x00 : 0x01;
            var config = CommandUtil.CareSetter(0xD3, (byte) dhcp);
            _Comm.SendCommands(_Port, config); //是否启用DHCP

            //如果不启用DHCP，将需要设置IP地址等……
            if (dhcpDisable)
            {
                config = CommandUtil.CareSetter(0xD4, _IpAddressControl.GetAddressBytes());
                _Comm.SendCommands(_Port, config);
                config = CommandUtil.CareSetter(0xD5, _GatwayAddressControl.GetAddressBytes());
                _Comm.SendCommands(_Port, config);
                config = CommandUtil.CareSetter(0xD6, _MaskAddressControl.GetAddressBytes());
                _Comm.SendCommands(_Port, config);
            }

            //TCP的端口
            var tcpPort = BitConverter.GetBytes((Int16) _TcpNumericUpDown.Value).Reverse().ToArray();
            config = CommandUtil.CareSetter(0xD7, tcpPort);
            _Comm.SendCommands(_Port, config);

            //默认的GPIB地址
            var defaultGpibAddress = new byte[]
            {
                (byte) _USBGpibNumericUpDown.Value,
                (byte) _WifiGpibNumericUpDown.Value,
                (byte) _LANGpibNumericUpDown.Value,
            };
            config = CommandUtil.CareSetter(0xDB, defaultGpibAddress);
            _Comm.SendCommands(_Port, config);

            //温度传感器设置
            string temp = "DS18B20";
            if (_18b20RadioButton.Checked)
                temp = "DS18B20";
            else if (_Dht11RadioButton.Checked)
                temp = "DHT11";
            else if (_Dht22RadioButton.Checked)
                temp = "DHT22";
            config = CommandUtil.CareSetter(0xDA, Encoding.ASCII.GetBytes(temp));
            _Comm.SendCommands(_Port, config);

            //USB串口波特率
            var usart1 = Int32.Parse(_Usart1NumberBox.SelectedItem.ToString());
            var usbBaud = BitConverter.GetBytes(usart1).Reverse().ToArray();
            config = CommandUtil.CareSetter(0xDD, usbBaud);
            _Comm.SendCommands(_Port, config);

            //WIFI串口波特率
            var usart2 = Int32.Parse(_Usart2NumberBox.SelectedItem.ToString());
            var wifiBaud = BitConverter.GetBytes(usart2).Reverse().ToArray();
            config = CommandUtil.CareSetter(0xDE, wifiBaud);
            _Comm.SendCommands(_Port, config);

            //串口数据互转
            var usartSwitch = _UsartSwitchCheckBox.Checked;
            var us = usartSwitch ? 0x01 : 0x00;
            config = CommandUtil.CareSetter(0xDF, (byte) us);
            _Comm.SendCommands(_Port, config);

            var talking = CommandUtil.CareReset(); //重启
            _Comm.SendCommands(_Port, talking);
            Thread.Sleep(300);

            MessageBox.Show(this, "Care参数配置完成。", "Care参数", MessageBoxButtons.OK, MessageBoxIcon.Information);

            QueryCareParameter(); //再次查询当前值
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 恢复Care的默认值
        /// </summary>
        private void _RestoreDefaultButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            var task = new Task(DefaultSetting);
            task.Start();
        }

        /// <summary>
        /// 恢复Care的默认值的线程方法
        /// </summary>
        private void DefaultSetting()
        {
            var talking = CommandUtil.CareRestoreDefault(); //恢复默认
            _Comm.SendCommands(_Port, talking);
            Thread.Sleep(80);

            talking = CommandUtil.CareReset(); //重启
            _Comm.SendCommands(_Port, talking);
            Thread.Sleep(100);
            QueryCareParameter(); //再次查询当前值

            this.ThreadSafeInvoke(() => MessageBox.Show(this, "恢复Care的默认参数值完成。", "Care参数", MessageBoxButtons.OK, MessageBoxIcon.Information));
            this.ThreadSafeInvoke(() => Cursor = Cursors.Default);
        }
    }
}
