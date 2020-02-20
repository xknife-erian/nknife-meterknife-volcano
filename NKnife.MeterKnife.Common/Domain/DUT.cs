﻿using System;
using System.ComponentModel.DataAnnotations;
using NKnife.Db.Base;
using NKnife.Interface;
using NKnife.Metrology;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    ///     被测物
    /// </summary>
    public class DUT : IId
    {
        public DUT()
        {
            Id = $"DUT{SequentialGuid.Create().ToString("N").ToUpper()}";
            CreateTime = DateTime.Now;
        }

        /// <summary>
        ///     被测物名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     被测物分类
        /// </summary>
        public string Classify { get; set; }

        /// <summary>
        ///     计量单位
        /// </summary>
        public IMetrology Unit { get; set; }

        /// <summary>
        ///     被测物的设计值
        /// </summary>
        public double ExpectValue { get; set; }

        /// <summary>
        ///     标定值
        /// </summary>
        public MetrologyValue[] MetrologyValues { get; set; }

        public string Description { get; set; }
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     被测物的照片存放路径
        /// </summary>
        public string ImagesPath { get; set; }

        /// <summary>
        ///     被测物的测量报告存放路径
        /// </summary>
        public string ReportsPath { get; set; }

        /// <summary>
        ///     被测物ID，也是编号，全局不可重复
        /// </summary>
        [Key]
        [Index]
        [Required]
        public string Id { get; set; }

        #region Overrides of Object

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{Name}/{ExpectValue}/{Id}";
        }

        #endregion
    }
}