using System;
using System.Configuration;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.App.Properties;
using NKnife.ControlKnife;

namespace MeterKnife.App
{
    internal sealed class SplashForm : Form, ISplashForm
    {
        private static readonly ILog Logger = LogManager.GetLogger<SplashForm>();

        public SplashForm()
        {
            Bitmap bitmap = Resources.mk_welcome;

            SuspendLayout();

            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterScreen;
            ShowInTaskbar = false;
            Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((0)));
            Width = bitmap.Width;
            Height = bitmap.Height;
            BackgroundImage = bitmap;

            var mainVersionLabel = new Label();
            mainVersionLabel.AutoSize = false;
            mainVersionLabel.BackColor = Color.Transparent;
            mainVersionLabel.ForeColor = Color.White;
            mainVersionLabel.Location = new Point(Width - 193, Height - 42);
            mainVersionLabel.TextAlign = ContentAlignment.MiddleRight;
            mainVersionLabel.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
            mainVersionLabel.Size = new Size(160, 18);
            mainVersionLabel.TabIndex = 0;
            mainVersionLabel.Text = $"版本: {AssemblyVersion}";
            Controls.Add(mainVersionLabel);

            string projectName = string.Empty;
            try
            {
                var reader = new AppSettingsReader();
                projectName = reader.GetValue("projectName", typeof (string)).ToString();
            }
            catch (Exception)
            {
                //_logger.Warn("读取产品所属项目名称失败");
            }
            if (!string.IsNullOrWhiteSpace(projectName))
            {
                var projectNameLabel = new Label();
                projectNameLabel.AutoSize = false;
                projectNameLabel.BackColor = Color.Transparent;
                projectNameLabel.ForeColor = Color.White;
                projectNameLabel.Location = new Point(Width - 193, Height - 62);
                projectNameLabel.TextAlign = ContentAlignment.MiddleRight;
                projectNameLabel.Font = new Font("Tahoma", 9F, FontStyle.Bold, GraphicsUnit.Point, ((0)));
                projectNameLabel.Size = new Size(160, 18);
                projectNameLabel.TabIndex = 0;
                string projectLabelText = string.Empty;
                if (projectName != "预览版" && projectName != "正式版")
                    projectLabelText = "项目: ";
                projectNameLabel.Text = $"{projectLabelText}{projectName}";
                Controls.Add(projectNameLabel);
            }

            _statusInfoLabel = new Label();
            _statusInfoLabel.AutoSize = true;
            _statusInfoLabel.BackColor = Color.Transparent;
            _statusInfoLabel.ForeColor = Color.White;
            _statusInfoLabel.Location = new Point(30, Height - 40);
            _statusInfoLabel.Size = new Size(180, 13);
            _statusInfoLabel.TabIndex = 0;
            _statusInfoLabel.Text = "开始启动主控程序...";
            Controls.Add(_statusInfoLabel);

            var updaterVersionLabel = new Label();
            updaterVersionLabel.BackColor = Color.Transparent;
            updaterVersionLabel.ForeColor = Color.White;
            updaterVersionLabel.Size = new Size(220, 13);
            updaterVersionLabel.Location = new Point(30, _statusInfoLabel.Location.Y - 6 - updaterVersionLabel.Height);
            Controls.Add(updaterVersionLabel);

            var callerVersionLabel = new Label();
            callerVersionLabel.BackColor = Color.Transparent;
            callerVersionLabel.ForeColor = Color.White;
            callerVersionLabel.Size = new Size(220, 13);
            callerVersionLabel.Location = new Point(30, updaterVersionLabel.Location.Y - 4 - callerVersionLabel.Height);
            Controls.Add(callerVersionLabel);

            ResumeLayout(false);
            PerformLayout();
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        #region ISplashForm

        private readonly Label _statusInfoLabel;

        void ISplashForm.SetStatusInfo(string newStatusInfo)
        {
            try
            {
                _statusInfoLabel.Text = newStatusInfo;
                _statusInfoLabel.Refresh();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        #endregion
    }
}