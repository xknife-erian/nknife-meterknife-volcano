using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autofac;
using NKnife.NLog.WinForm;
using NKnife.Win.Forms.Forms;
using NLog;
using Module = System.Reflection.Module;

namespace NKnife.MeterKnife.App
{
    public class Program
    {
        internal static IContainer Container { get; private set; }

        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace(":-)");

            //������ӭ��Ļ
            Splasher.Show(typeof(SplashForm));
            FileCleaner.Run();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyModules(typeof(Logic.Global).Assembly);
            builder.RegisterAssemblyModules(typeof(Workbench.Workbench).Assembly);
            builder.RegisterType<Startup>().AsSelf();
            using (Container = builder.Build())
            {
                var startup = Container.Resolve<Startup>();
                //������ǰ�����������µ� ApplicationContext ʵ��
                Application.Run(startup);
            }
        }
    }
}