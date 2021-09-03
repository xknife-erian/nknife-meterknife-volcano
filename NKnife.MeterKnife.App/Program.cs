using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NKnife.MeterKnife.Holistic;
using NKnife.MeterKnife.Logic;
using NKnife.MeterKnife.Storage.Db;
using NKnife.Win.Forms.Forms;
using NKnife.Win.Quick.Base;
using NLog;

namespace NKnife.MeterKnife.App
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var logger = LogManager.GetCurrentClassLogger();
            logger.Trace(":-)");

            //������ӭ��Ļ
            Splasher.Show(typeof(SplashForm));

            //����Ա���á������û��������ã�
            var conf = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json", true, true)
                .Build();

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddOptions().Configure<StorageSetting>(conf.GetSection(nameof(StorageSetting)));
            serviceCollection.AddOptions().Configure<WorkbenchSetting>(conf.GetSection(nameof(WorkbenchSetting)));

            var builder = new ContainerBuilder();
            builder.Populate(serviceCollection); //��.Net��ע����ֲ��Autofac

            builder.RegisterAssemblyModules(typeof(Kernel).Assembly);
            builder.RegisterAssemblyModules(typeof(Workbench.Workbench).Assembly);
            builder.RegisterType<Startup>().AsSelf();

            using (Kernel.Container = builder.Build())
            {
                Startup startup;
                try
                {
                    startup = Kernel.Container.Resolve<Startup>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                //������ǰ�����������µ� ApplicationContext ʵ��
                Application.Run(startup);
            }
        }
    }
}