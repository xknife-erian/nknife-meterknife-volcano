﻿using System.Windows.Forms;

namespace NKnife.MeterKnife.Workbench.Views
{
    partial class MeasureView
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
            this._PlotView = new OxyPlot.WindowsForms.PlotView();
            this._ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this._DataToolStrip = new System.Windows.Forms.ToolStrip();
            this._SetDataSeriesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._PlotToolStrip = new System.Windows.Forms.ToolStrip();
            this._OriginalToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._ZoomInToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ZoomOutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._TimeZoomToolStripButton = new System.Windows.Forms.ToolStripButton();
            this._ValueRangeZoomToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._PlotThemeDropDownButton1 = new NKnife.MeterKnife.Workbench.Dialogs.Plots.PlotThemeDropDownButton();
            this._ToolStripContainer.ContentPanel.SuspendLayout();
            this._ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this._ToolStripContainer.SuspendLayout();
            this._DataToolStrip.SuspendLayout();
            this._PlotToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PlotView
            // 
            this._PlotView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PlotView.Location = new System.Drawing.Point(0, 0);
            this._PlotView.Margin = new System.Windows.Forms.Padding(1);
            this._PlotView.Name = "_PlotView";
            this._PlotView.PanCursor = System.Windows.Forms.Cursors.Hand;
            this._PlotView.Size = new System.Drawing.Size(624, 414);
            this._PlotView.TabIndex = 1;
            this._PlotView.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this._PlotView.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this._PlotView.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // _ToolStripContainer
            // 
            // 
            // _ToolStripContainer.ContentPanel
            // 
            this._ToolStripContainer.ContentPanel.Controls.Add(this._PlotView);
            this._ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(624, 414);
            this._ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this._ToolStripContainer.Name = "_ToolStripContainer";
            this._ToolStripContainer.Size = new System.Drawing.Size(624, 440);
            this._ToolStripContainer.TabIndex = 4;
            this._ToolStripContainer.Text = "toolStripContainer1";
            // 
            // _ToolStripContainer.TopToolStripPanel
            // 
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._DataToolStrip);
            this._ToolStripContainer.TopToolStripPanel.Controls.Add(this._PlotToolStrip);
            // 
            // _DataToolStrip
            // 
            this._DataToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._DataToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._SetDataSeriesToolStripButton});
            this._DataToolStrip.Location = new System.Drawing.Point(3, 0);
            this._DataToolStrip.Name = "_DataToolStrip";
            this._DataToolStrip.Size = new System.Drawing.Size(100, 26);
            this._DataToolStrip.TabIndex = 1;
            // 
            // _SetDataSeriesToolStripButton
            // 
            this._SetDataSeriesToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_series_selection;
            this._SetDataSeriesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._SetDataSeriesToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._SetDataSeriesToolStripButton.Name = "_SetDataSeriesToolStripButton";
            this._SetDataSeriesToolStripButton.Size = new System.Drawing.Size(88, 21);
            this._SetDataSeriesToolStripButton.Text = "设置数据线";
            this._SetDataSeriesToolStripButton.Click += new System.EventHandler(this.SetDataSeriesButtonClick);
            // 
            // _PlotToolStrip
            // 
            this._PlotToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this._PlotToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._OriginalToolStripButton,
            this.toolStripSeparator2,
            this._ZoomInToolStripButton,
            this._ZoomOutToolStripButton,
            this.toolStripSeparator3,
            this._TimeZoomToolStripButton,
            this._ValueRangeZoomToolStripButton,
            this.toolStripSeparator1,
            this._PlotThemeDropDownButton1});
            this._PlotToolStrip.Location = new System.Drawing.Point(103, 0);
            this._PlotToolStrip.Name = "_PlotToolStrip";
            this._PlotToolStrip.Size = new System.Drawing.Size(174, 25);
            this._PlotToolStrip.TabIndex = 0;
            // 
            // _OriginalToolStripButton
            // 
            this._OriginalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._OriginalToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_original;
            this._OriginalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._OriginalToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._OriginalToolStripButton.Name = "_OriginalToolStripButton";
            this._OriginalToolStripButton.Size = new System.Drawing.Size(23, 20);
            this._OriginalToolStripButton.Text = "恢复";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _ZoomInToolStripButton
            // 
            this._ZoomInToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomInToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_zoom_in;
            this._ZoomInToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomInToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ZoomInToolStripButton.Name = "_ZoomInToolStripButton";
            this._ZoomInToolStripButton.Size = new System.Drawing.Size(23, 20);
            this._ZoomInToolStripButton.Text = "图表放大";
            // 
            // _ZoomOutToolStripButton
            // 
            this._ZoomOutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ZoomOutToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_zoom_out;
            this._ZoomOutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ZoomOutToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ZoomOutToolStripButton.Name = "_ZoomOutToolStripButton";
            this._ZoomOutToolStripButton.Size = new System.Drawing.Size(23, 20);
            this._ZoomOutToolStripButton.Text = "图表缩小";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _TimeZoomToolStripButton
            // 
            this._TimeZoomToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._TimeZoomToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_horizontal_zoom;
            this._TimeZoomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._TimeZoomToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._TimeZoomToolStripButton.Name = "_TimeZoomToolStripButton";
            this._TimeZoomToolStripButton.Size = new System.Drawing.Size(23, 20);
            this._TimeZoomToolStripButton.Text = "横轴放大";
            // 
            // _ValueRangeZoomToolStripButton
            // 
            this._ValueRangeZoomToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._ValueRangeZoomToolStripButton.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.PlotOperateStripIcon_vertical_zoom;
            this._ValueRangeZoomToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._ValueRangeZoomToolStripButton.Margin = new System.Windows.Forms.Padding(0, 3, 0, 2);
            this._ValueRangeZoomToolStripButton.Name = "_ValueRangeZoomToolStripButton";
            this._ValueRangeZoomToolStripButton.Size = new System.Drawing.Size(23, 20);
            this._ValueRangeZoomToolStripButton.Text = "纵轴放大";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _PlotThemeDropDownButton1
            // 
            this._PlotThemeDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._PlotThemeDropDownButton1.Image = global::NKnife.MeterKnife.Workbench.Properties.Resources.ThemeStripIcon_theme_selection;
            this._PlotThemeDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._PlotThemeDropDownButton1.Name = "_PlotThemeDropDownButton1";
            this._PlotThemeDropDownButton1.Size = new System.Drawing.Size(29, 22);
            this._PlotThemeDropDownButton1.Text = "主题";
            // 
            // MeasureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 440);
            this.Controls.Add(this._ToolStripContainer);
            this.Name = "MeasureView";
            this.Text = "MeasureView";
            this._ToolStripContainer.ContentPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this._ToolStripContainer.TopToolStripPanel.PerformLayout();
            this._ToolStripContainer.ResumeLayout(false);
            this._ToolStripContainer.PerformLayout();
            this._DataToolStrip.ResumeLayout(false);
            this._DataToolStrip.PerformLayout();
            this._PlotToolStrip.ResumeLayout(false);
            this._PlotToolStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private OxyPlot.WindowsForms.PlotView _PlotView;
        private ToolStripContainer _ToolStripContainer;
        private ToolStrip _PlotToolStrip;
        private ToolStripButton _OriginalToolStripButton;
        private ToolStripButton _ZoomInToolStripButton;
        private ToolStripButton _ZoomOutToolStripButton;
        private ToolStripButton _TimeZoomToolStripButton;
        private ToolStripButton _ValueRangeZoomToolStripButton;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator1;
        private NKnife.MeterKnife.Workbench.Dialogs.Plots.PlotThemeDropDownButton _PlotThemeDropDownButton1;
        private ToolStrip _DataToolStrip;
        private ToolStripButton _SetDataSeriesToolStripButton;
    }
}