﻿using System;
using System.Data;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Util;

namespace NKnife.MeterKnife.Common.Base
{
    /// <summary>
    /// 一个描述采集数据源的接口
    /// </summary>
    public interface ICollectSource
    {
        IMeter Meter { get; set; }

        DataSet DataSet { get; }

        MeterRange MeterRange { get; set; }

        bool HasData { get; }

        void Clear();

        FiguredDataFilter Filter { get; set; }

        /// <summary>
        /// 导出到Excle文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        bool Save(string fileFullName);

        bool Add(double value);

        /// <summary>
        /// 当收到仪器采集数据时
        /// </summary>
        event EventHandler<CollectDataEventArgs> ReceviedCollectData;
    }
}
