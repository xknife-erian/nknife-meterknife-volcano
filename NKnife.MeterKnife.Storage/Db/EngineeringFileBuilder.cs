﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NKnife.Db;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.MeterKnife.Util;
using NKnife.Util;

namespace NKnife.MeterKnife.Storage.Db
{
    public class EngineeringFileBuilder
    {
        private readonly IHabitManager _habitManager;
        private readonly IPathManager _pathManager;

        public EngineeringFileBuilder(IHabitManager habitManager, IPathManager pathManager)
        {
            _habitManager = habitManager;
            _pathManager = pathManager;
        }

        public void CreateEngineeringSqliteFile(IStorageManager storageManager, Engineering engineering)
        {
            var fileFullName = GetEngineeringSqliteFileName(engineering);
            var dir = Path.GetDirectoryName(fileFullName);
            UtilFile.CreateDirectory(dir);
            using (var command = storageManager.OpenConnection(engineering).CreateCommand())
            {
                DbUtil.CheckTable(command, storageManager.CurrentDbType, GetTablesSqlMap(storageManager.CurrentDbType, engineering));
            }
        }

        /// <summary>
        /// 获取指定工程的建表SQL语句
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="engineering">指定工程</param>
        /// <returns>SQL语句的字典，Key是表名，Value是建表的语句</returns>
        private Dictionary<string, string> GetTablesSqlMap(DatabaseType dbType, Engineering engineering)
        {
            var map = new Dictionary<string, string>();
            var dutList = new List<DUT>();
            foreach (var pool in engineering.CommandPools)
            {
                foreach (ScpiCommand command in pool)
                {
                    if (command.DUT != null && !dutList.Contains(command.DUT))
                    {
                        var d = command.DUT;
                        dutList.Add(d);
                        map.Add(d.Id, SqlHelper.GetCreateTableSql($"{d.Id}s", dbType, typeof(MeasureData)));
                    }
                }
            }
            map.Add(nameof(Engineering), SqlHelper.GetCreateTableSql(dbType, typeof(Engineering)));
            return map;
        }

        private string GetEngineeringSqliteFileName(Engineering engineering)
        {
            var t = engineering.CreateTime;
            var fileFullName = $"E-{t:yyMMdd-HHmmss}.mks";
            var path = _habitManager.GetOptionValue(HabitKey.Data_MetricalData_Path, _pathManager.UserDocumentsPath);
            if (!Directory.Exists(path))
                UtilFile.CreateDirectory(path);
            fileFullName = Path.Combine(path, $"{t:yyyyMM}{Path.DirectorySeparatorChar}", fileFullName);
            engineering.Path = fileFullName;
            return fileFullName;
        }

    }
}