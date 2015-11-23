﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using Common.Logging;
using MeterKnife.Common.Base;
using ScpiKnife;

namespace MerterKnife.Common.Winforms.Controls
{
    public class ScpiParamPanel : BaseParamPanel
    {
        private static readonly ILog _logger = LogManager.GetLogger<ScpiParamPanel>();
        protected readonly List<ComboBox> _ComboBoxList = new List<ComboBox>();
        protected ScpiGroup _Commandlist;

        public ScpiParamPanel(XmlElement element)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            ParseElement(element);
        }

        public override ScpiGroup ScpiCommands
        {
            get
            {
                this.ThreadSafeInvoke(FillCommandList);
                return _Commandlist;
            }
        }

        protected virtual void FillCommandList()
        {
            _Commandlist = GetGpibCommandList();
            foreach (ComboBox comboBox in _ComboBoxList)
            {
                if (comboBox.SelectedItem == null)
                    continue;
                var cmd = (ScpiCommand)(comboBox.SelectedItem);
                _Commandlist.Add(cmd);
            }
        }

        protected static ScpiGroup GetGpibCommandList()
        {
            var cmdlist = new ScpiGroup();
            cmdlist.Add(new ScpiCommand {Command = "*CLS", Interval = 50});
            cmdlist.Add(new ScpiCommand { Command = "*CLS", Interval = 50 });
            cmdlist.Add(new ScpiCommand { Command = "*RST", Interval = 200 });
            cmdlist.Add(new ScpiCommand { Command = "*CLS", Interval = 50 });
            cmdlist.Add(new ScpiCommand { Command = "INIT", Interval = 50 });
            return cmdlist;
        }

        protected virtual void ParseElement(XmlElement element)
        {
            string isScpiStr = element.GetAttribute("format");
            bool isScpi = true;
            bool.TryParse(isScpiStr, out isScpi);
            XmlNodeList nodes = element.SelectNodes("/MeterParam/command[@isConfig='true']");
            if (nodes == null)
                return;

            int count = nodes.Count;
            _Panel.RowCount = count + 1;
            _Panel.RowStyles.Clear();
            //_Panel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; //测试

            int index = 0;
            foreach (XmlElement confEle in nodes)
            {
                _Panel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));

                var rootCmd = new ScpiCommand();
                rootCmd.Description = confEle.GetAttribute("content");
                rootCmd.Command = confEle.GetAttribute("command");

                GetLabel(rootCmd.Description, index);

                if (!confEle.HasChildNodes)
                    continue;
                bool isAddButton = false;
                Panel cbxPanel;
                ComboBox cbx = GetComboBox(index, out cbxPanel);
                foreach (XmlElement confContentEle in confEle.ChildNodes)
                {
                    #region config element

                    ScpiCommand cmd = ParseGpibCommand(isScpi, confContentEle, rootCmd.Command);
                    cbx.Items.Add(cmd);

                    if (!confContentEle.HasChildNodes)
                        continue;

                    #region 有配置子项

                    var subButton = GetSubButton();
//                    cbx.SelectedIndexChanged += (s, e) =>
//                    {
//                        var selectedCmd = cbx.SelectedItem as ScpiCommand;
//                        if (selectedCmd != null && selectedCmd.Tag != null)
//                        {
//                            subButton.Tag = cmd.Tag;
//                        }
//                    };
                    if (!isAddButton)
                    {
                        cbxPanel.Controls.Add(subButton);
                        subButton.Click += (s, e) =>
                        {
                            if (subButton.Tag != null)
                            {
                                ShowSubCommandMenu((ScpiCommand)subButton.Tag, subButton);
                            }
                        };
                        isAddButton = true;
                    }
                    foreach (XmlElement groupElement in confContentEle.ChildNodes)
                    {
                        if (groupElement.LocalName.ToLower() != "group")
                            continue;
                        if (!groupElement.HasChildNodes)
                            continue;
                        //将所有命令解析成链式后，置入Tag中，待显示时再进行链式生成菜单
                        ScpiCommand groupCmd = ParseGpibCommand(isScpi, groupElement, rootCmd.Command);
                        foreach (XmlElement gpElement in groupElement.ChildNodes)
                        {
                            ScpiCommand gpCmd = ParseGpibCommand(isScpi, gpElement, groupCmd.Command);
                            //groupCmd.Next = gpCmd;
                        }
                        //cmd.Tag = groupCmd;
                    }

                    #endregion

                    #endregion
                }
                index++;
            }
            _Panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        private static Button GetSubButton()
        {
            var button = new Button
            {
                //BackgroundImage = Resources.arrow_triangle_down,
                BackgroundImageLayout = ImageLayout.Center,
                FlatStyle = FlatStyle.Popup,
                Dock = DockStyle.Right,
                Width = 24,
                Height = 22
            };
            return button;
        }

        private void ShowSubCommandMenu(ScpiCommand command, Control control)
        {
            var menu = new ContextMenuStrip();
            var menuItem = new ToolStripMenuItem(command.Description);
            Next(command, menuItem);

            menu.Items.Add(menuItem);
            menu.Show(control, new Point(1, control.Height));
        }

        /// <summary>
        /// 递归生成菜单项
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="parentMenu">父菜单</param>
        private void Next(ScpiCommand command, ToolStripMenuItem parentMenu)
        {
//            if (command.Next != null)
//            {
//                var menu = new ToolStripMenuItem(command.Description);
//                parentMenu.DropDownItems.Add(menu);
//                Next(command.Next, menu);
//            }
        }

        private static ScpiCommand ParseGpibCommand(bool isScpi, XmlElement element, string rootCmd)
        {
            var cmd = new ScpiCommand();
            cmd.Description = element.GetAttribute("content");
            if (element.LocalName == "command")
                cmd.Command = string.Format("{0}:{1}", rootCmd, element.GetAttribute("command"));
            else if (element.LocalName == "param")
                cmd.Command = string.Format("{0} {1}", rootCmd, element.GetAttribute("command"));
            return cmd;
        }

        private void GetLabel(string content, int index)
        {
            var label = new Label();
            label.AutoSize = true;
            label.Text = content;
            label.Anchor = AnchorStyles.Right;
            _Panel.Controls.Add(label, 0, index);
        }

        private ComboBox GetComboBox(int index, out Panel cbxPanel)
        {
            cbxPanel = new Panel();
            cbxPanel.Dock = DockStyle.Fill;
            //cbxPanel.BackColor = Color.Red;
            var cbx = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 172,
                Location = new Point(1, 1),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            cbxPanel.Controls.Add(cbx);
            _ComboBoxList.Add(cbx);
            _Panel.Controls.Add(cbxPanel, 1, index);
            return cbx;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ScpiParamPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "ScpiParamPanel";
            this.Size = new System.Drawing.Size(803, 559);
            this.ResumeLayout(false);

        }
    }
}