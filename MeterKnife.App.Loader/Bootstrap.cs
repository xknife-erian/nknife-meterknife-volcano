using System;
using System.Threading;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Common.Logging;
using NKnife.ControlKnife;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class Bootstrap
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DI.Initialize();

            AutoUpdater.OpenDownloadPage = true;
            AutoUpdater.LetUserSelectRemindLater = true;
            AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            AutoUpdater.RemindLaterAt = 2;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
            AutoUpdater.Start("http://127.0.0.1");//("http://rbsoft.org/updates/AutoUpdaterTest.xml");

            //������ӭ��Ļ
            Splasher.Show(typeof(SplashForm));

            FileCleaner.Run();

            //������ǰ�����������µ� ApplicationContext ʵ��
            Application.Run(EnvironmentCore.Instance(args));
        }

        private static void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    var dr = MessageBox.Show($"���°汾{args.CurrentVersion}����������ʹ�õİ汾��{args.InstalledVersion}��\r\n�Ƿ������°汾?",
                        @"�п��ø���", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dr.Equals(DialogResult.Yes))
                    {
                        try
                        {
                            AutoUpdater.DownloadUpdate();
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, e.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"û�п��õĸ��£����Ժ����ԡ�", @"û�п��ø���",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
//                MessageBox.Show(@"���ӵ�Զ�̸��·�����ʱ�����쳣�����黥���������Ƿ�ͨ����", @"������ʧ��",
//                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}