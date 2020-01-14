﻿namespace MeterKnife.Plugins.ToolsMenu
{
    partial class KeysightChannelToolView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._SplitContainer = new System.Windows.Forms.SplitContainer();
            this._StopButton = new System.Windows.Forms.Button();
            this._CommandComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this._LoopTimeBox = new System.Windows.Forms.NumericUpDown();
            this._SendButton = new System.Windows.Forms.Button();
            this._LoopEnableCheckBox = new System.Windows.Forms.CheckBox();
            this._AddressBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this._ResultListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).BeginInit();
            this._SplitContainer.Panel1.SuspendLayout();
            this._SplitContainer.Panel2.SuspendLayout();
            this._SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._LoopTimeBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._AddressBox)).BeginInit();
            this.SuspendLayout();
            // 
            // _SplitContainer
            // 
            this._SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._SplitContainer.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._SplitContainer.Location = new System.Drawing.Point(0, 0);
            this._SplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SplitContainer.Name = "_SplitContainer";
            this._SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _SplitContainer.Panel1
            // 
            this._SplitContainer.Panel1.Controls.Add(this._StopButton);
            this._SplitContainer.Panel1.Controls.Add(this._CommandComboBox);
            this._SplitContainer.Panel1.Controls.Add(this.label2);
            this._SplitContainer.Panel1.Controls.Add(this._LoopTimeBox);
            this._SplitContainer.Panel1.Controls.Add(this._SendButton);
            this._SplitContainer.Panel1.Controls.Add(this._LoopEnableCheckBox);
            this._SplitContainer.Panel1.Controls.Add(this._AddressBox);
            this._SplitContainer.Panel1.Controls.Add(this.label1);
            // 
            // _SplitContainer.Panel2
            // 
            this._SplitContainer.Panel2.Controls.Add(this._ResultListBox);
            this._SplitContainer.Size = new System.Drawing.Size(674, 495);
            this._SplitContainer.SplitterDistance = 40;
            this._SplitContainer.SplitterWidth = 6;
            this._SplitContainer.TabIndex = 0;
            // 
            // _StopButton
            // 
            this._StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._StopButton.Enabled = false;
            this._StopButton.Location = new System.Drawing.Point(585, 9);
            this._StopButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._StopButton.Name = "_StopButton";
            this._StopButton.Size = new System.Drawing.Size(69, 28);
            this._StopButton.TabIndex = 7;
            this._StopButton.Text = "停止";
            this._StopButton.UseVisualStyleBackColor = true;
            this._StopButton.Click += new System.EventHandler(this._StopButton_Click);
            // 
            // _CommandComboBox
            // 
            this._CommandComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._CommandComboBox.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._CommandComboBox.FormattingEnabled = true;
            this._CommandComboBox.Location = new System.Drawing.Point(135, 12);
            this._CommandComboBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._CommandComboBox.Name = "_CommandComboBox";
            this._CommandComboBox.Size = new System.Drawing.Size(225, 23);
            this._CommandComboBox.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(483, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "ms";
            // 
            // _LoopTimeBox
            // 
            this._LoopTimeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._LoopTimeBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._LoopTimeBox.Location = new System.Drawing.Point(419, 12);
            this._LoopTimeBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LoopTimeBox.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this._LoopTimeBox.Name = "_LoopTimeBox";
            this._LoopTimeBox.Size = new System.Drawing.Size(64, 23);
            this._LoopTimeBox.TabIndex = 1;
            this._LoopTimeBox.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // _SendButton
            // 
            this._SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._SendButton.Location = new System.Drawing.Point(513, 9);
            this._SendButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._SendButton.Name = "_SendButton";
            this._SendButton.Size = new System.Drawing.Size(69, 28);
            this._SendButton.TabIndex = 2;
            this._SendButton.Text = "发送";
            this._SendButton.UseVisualStyleBackColor = true;
            this._SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // _LoopEnableCheckBox
            // 
            this._LoopEnableCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._LoopEnableCheckBox.AutoSize = true;
            this._LoopEnableCheckBox.Location = new System.Drawing.Point(370, 13);
            this._LoopEnableCheckBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._LoopEnableCheckBox.Name = "_LoopEnableCheckBox";
            this._LoopEnableCheckBox.Size = new System.Drawing.Size(51, 21);
            this._LoopEnableCheckBox.TabIndex = 1;
            this._LoopEnableCheckBox.Text = "循环";
            this._LoopEnableCheckBox.UseVisualStyleBackColor = true;
            // 
            // _AddressBox
            // 
            this._AddressBox.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._AddressBox.Location = new System.Drawing.Point(84, 12);
            this._AddressBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._AddressBox.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this._AddressBox.Name = "_AddressBox";
            this._AddressBox.Size = new System.Drawing.Size(45, 23);
            this._AddressBox.TabIndex = 3;
            this._AddressBox.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "仪器地址:";
            // 
            // _ResultListBox
            // 
            this._ResultListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ResultListBox.FormattingEnabled = true;
            this._ResultListBox.ItemHeight = 16;
            this._ResultListBox.Location = new System.Drawing.Point(0, 0);
            this._ResultListBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ResultListBox.Name = "_ResultListBox";
            this._ResultListBox.Size = new System.Drawing.Size(674, 449);
            this._ResultListBox.TabIndex = 0;
            // 
            // KeysightChannelToolView
            // 
            this.AcceptButton = this._SendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 495);
            this.Controls.Add(this._SplitContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "KeysightChannelToolView";
            this.Text = "Aglient 82357x工具";
            this._SplitContainer.Panel1.ResumeLayout(false);
            this._SplitContainer.Panel1.PerformLayout();
            this._SplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._SplitContainer)).EndInit();
            this._SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._LoopTimeBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._AddressBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer _SplitContainer;
        private System.Windows.Forms.NumericUpDown _LoopTimeBox;
        private System.Windows.Forms.Button _SendButton;
        private System.Windows.Forms.CheckBox _LoopEnableCheckBox;
        private System.Windows.Forms.NumericUpDown _AddressBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox _ResultListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _CommandComboBox;
        private System.Windows.Forms.Button _StopButton;
    }
}