﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Storage.Base;

namespace NKnife.MeterKnife.Logic
{
    public class EngineeringLogic : IEngineeringLogic
    {
        private readonly IStorageManager _storageManager;
        private readonly IStorageDUTWrite<Engineering> _engineeringStorageDUTWrite;
        private readonly IStorageDUTRead<MeasureData> _measureDataRead;
        private readonly IStoragePlatform<Engineering> _engineeringStoragePlatform;
        private readonly IMeasuringLogic _performLogic;

        public EngineeringLogic(IStorageManager storageManager, IStoragePlatform<Engineering> engineeringStoragePlatform, IStorageDUTWrite<Engineering> engineeringStorageDUTWrite,
            IMeasuringLogic performLogic, IStorageDUTRead<MeasureData> measureDataRead)
        {
            _storageManager = storageManager;
            _engineeringStoragePlatform = engineeringStoragePlatform;
            _engineeringStorageDUTWrite = engineeringStorageDUTWrite;
            _performLogic = performLogic;
            _measureDataRead = measureDataRead;
        }

        #region Implementation of IEngineeringLogic

        /// <summary>
        /// 新建一个测量工程
        /// </summary>
        /// <returns>是否创建成功</returns>
        public async Task<bool> CreateEngineeringAsync(Engineering engineering)
        {
            //创建工程的数据库或者文件
            BuildEngineeringStore(engineering);
            //将工程的相关信息存入到工程库
            await _engineeringStorageDUTWrite.InsertAsync(engineering);
            _performLogic.SetDUTMap(engineering.CommandPools, engineering);
            //同时也在平台库中存储一份
            return await _engineeringStoragePlatform.InsertAsync(engineering);
        }

        /// <summary>
        ///     修改一个测量工程
        /// </summary>
        public async Task UpdateEngineeringAsync(Engineering engineering)
        {
            await _engineeringStoragePlatform.UpdateAsync(engineering);
            await _engineeringStorageDUTWrite.UpdateAsync(engineering);
        }

        /// <summary>
        ///     获取指定被测物的测量数据
        /// </summary>
        /// <param name="dut">指定被测物</param>
        public async Task<IEnumerable<MeasureData>> GetDUTDataAsync((Engineering, DUT) dut)
        {
            return await _measureDataRead.FindAllAsync(dut);
        }

        /// <summary>
        ///     删除一个指定的工程
        /// </summary>
        /// <param name="eng">指定的工程</param>
        public async Task RemoveEnginneringAsync(Engineering eng)
        {
            await _engineeringStoragePlatform.RemoveAsync(eng.Id);
        }

        /// <summary>
        /// 创建工程的数据库或者文件
        /// </summary>
        private void BuildEngineeringStore(Engineering engineering)
        {
            if (string.IsNullOrEmpty(engineering.Name))
            {
                var sb = new StringBuilder();
                foreach (var pool in engineering.CommandPools)
                {
                    foreach (ScpiCommand command in pool)
                    {
                        sb.Append(command.DUT.Name).Append('/');
                    }
                }

                engineering.Name = sb.ToString().TrimEnd('/');
            }

            _storageManager.CreateEngineering(engineering);
        }

        #endregion
    }
}