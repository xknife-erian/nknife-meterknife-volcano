﻿namespace MeterKnife.UITests
{
    partial class PropertyGridView
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
            this._PropertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._PropertyGrid2 = new System.Windows.Forms.PropertyGrid();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _PropertyGrid1
            // 
            this._PropertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PropertyGrid1.LineColor = System.Drawing.SystemColors.ControlDark;
            this._PropertyGrid1.Location = new System.Drawing.Point(0, 0);
            this._PropertyGrid1.Name = "_PropertyGrid1";
            this._PropertyGrid1.Size = new System.Drawing.Size(261, 197);
            this._PropertyGrid1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._PropertyGrid1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._PropertyGrid2);
            this.splitContainer1.Size = new System.Drawing.Size(261, 395);
            this.splitContainer1.SplitterDistance = 197;
            this.splitContainer1.TabIndex = 1;
            // 
            // _PropertyGrid2
            // 
            this._PropertyGrid2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._PropertyGrid2.LineColor = System.Drawing.SystemColors.ControlDark;
            this._PropertyGrid2.Location = new System.Drawing.Point(0, 0);
            this._PropertyGrid2.Name = "_PropertyGrid2";
            this._PropertyGrid2.Size = new System.Drawing.Size(261, 194);
            this._PropertyGrid2.TabIndex = 0;
            // 
            // PropertyGridView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(261, 395);
            this.Controls.Add(this.splitContainer1);
            this.Name = "PropertyGridView";
            this.Text = "PropertyGridView";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid _PropertyGrid1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PropertyGrid _PropertyGrid2;
    }
}