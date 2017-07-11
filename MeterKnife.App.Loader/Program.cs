using System.Windows.Forms;
using Common.Logging;
using NKnife.IoC;

namespace MeterKnife.App
{
    public class Program
    {
        /// <summary>
        /// ����������Starter��Ŀͨ��������ص��á�
        /// </summary>
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DI.Initialize();

            ILog logger = LogManager.GetLogger<Application>();
            logger.Info("IOC��ܵĳ�ʼ����ɡ�");

            //������ǰ�����������µ� ApplicationContext ʵ��
            Application.Run(EnvironmentKernel.Instance(args));
        }
    }
}