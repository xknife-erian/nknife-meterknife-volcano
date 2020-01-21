﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using NKnife.Db;
using NKnife.MeterKnife.Common;
using NKnife.MeterKnife.Common.Base;

namespace NKnife.MeterKnife.Storage.Base
{
    public abstract class BaseStorageRead<T> : IStorageRead<T, string>
    {
        protected readonly IStorageManager _storageManager;

        protected BaseStorageRead(IStorageManager storageManager)
        {
            _storageManager = storageManager;
            TableName = BuildTableName(typeof(T).Name);
        }

        protected static string BuildTableName(string typeName)
        {
            return $"{typeName}s";
        }

        #region Implementation of IStorageRead<T,in string>

        /// <summary>
        ///     当前实现面向的数据表表名
        /// </summary>
        public string TableName { get; }

        /// <summary>
        ///     分页查询方法
        /// </summary>
        /// <param name="pageNumber">当前页码。从0开始。</param>
        /// <param name="pageSize">每页的数据数量。</param>
        /// <param name="direction">查询数据时的排序方向。</param>
        /// <returns>当前页的数据集合</returns>
        public virtual async Task<IEnumerable<T>> PageAsync(int pageNumber, int pageSize, SortDirection direction)
        {
            var conn = _storageManager.OpenReadConnection();
            //offset代表从第几条记录“之后“开始查询，limit表明查询多少条结果
            var sql = $"SELECT * FROM {TableName} LIMIT {pageSize * pageNumber} OFFSET {pageSize}";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        /// <summary>
        ///     根据指定的ID获取指定的记录并转换为对象
        /// </summary>
        /// <param name="id">指定的ID</param>
        /// <returns></returns>
        public virtual async Task<T> FindOneByIdAsync(string id)
        {
            var conn = _storageManager.OpenReadConnection();
            return await conn.QueryFirstAsync<T>($"SELECT * FROM {TableName} WHERE {nameof(IRecord<T>.Id)}='{id}'");
        }

        /// <summary>
        ///     指定ID的记录是否存在
        /// </summary>
        /// <param name="id">指定的记录ID</param>
        /// <returns>记录是否存在，true时存在指定ID的记录，false反之。</returns>
        public virtual async Task<bool> ExistAsync(string id)
        {
            var conn = _storageManager.OpenReadConnection();
            var sql = $"SELECT COUNT(*) FROM {TableName} WHERE {nameof(IRecord<T>.Id)}='{id}'";
            return await conn.ExecuteAsync(sql) > 0;
        }

        /// <summary>
        ///     查询记录的数据记录统计数量
        /// </summary>
        /// <returns>数量</returns>
        public virtual async Task<long> CountAsync()
        {
            var conn = _storageManager.OpenReadConnection();
            var sql = $"SELECT COUNT(*) FROM {TableName}";
            var count = await conn.ExecuteScalarAsync<long>(sql);
            return count;
        }

        /// <summary>
        ///     获取所有记录
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            var conn = _storageManager.OpenReadConnection();
            var sql = $"SELECT * FROM {TableName}'";
            var result = await conn.QueryAsync<T>(sql);
            return result;
        }

        #endregion
    }
}