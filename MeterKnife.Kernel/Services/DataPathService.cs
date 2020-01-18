﻿using NKnife.Interface;

namespace MeterKnife.Kernel.Services
{
    public class DataPathService : IEnvironmentItem
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public bool StartService()
        {
//            var userdata = DI.Get<MeterKnifeUserData>();
//            var dataPath = userdata.GetValue(MeterKnifeUserData.DATA_PATH, string.Empty);
//            DI.Get<IMeterKernel>().DataPath = dataPath;
//            _logger.Info(string.Format("数据存储路径:{0}", dataPath));
            return true;
        }

        public bool CloseService()
        {
            return true;
        }

        public int Order
        {
            get { return 999; }
        }

        public string Description
        {
            get { return "数据路径检查器"; }
        }


    }
}
