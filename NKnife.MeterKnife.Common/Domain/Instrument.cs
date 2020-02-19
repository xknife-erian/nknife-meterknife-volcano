﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Text;
using Newtonsoft.Json;
using NKnife.Db.Base;
using NKnife.Interface;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    ///     仪器
    /// </summary>
    public class Instrument : IId
    {
        public Instrument()
        {
            Id = Guid.NewGuid().ToString("N");
        }

        [Key]
        [Index]
        [Required]
        public string Id { get; set; }

        /// <summary>
        ///     生产厂商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 用途分类
        /// </summary>
        public string UseClassification { get; set; }

        /// <summary>
        ///     型号1
        /// </summary>
        public string Model1 { get; set; }

        /// <summary>
        ///     子型号
        /// </summary>
        public string Model2 { get; set; }

        /// <summary>
        ///     设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     设备常用简称
        /// </summary>
        public string AbbrName { get; } = string.Empty;

        /// <summary>
        ///     设备说明或信息
        /// </summary>
        public string Information { get; set; } = string.Empty;

        /// <summary>
        ///     仪器的图片
        /// </summary>
        [JsonIgnore]
        public Image Image { get; set; }

        public override bool Equals(object obj)
        {
            var bo = obj as Instrument;
            if (bo == null)
                return false;
            return Equals(bo);
        }

        #region Equality members

        protected bool Equals(Instrument other)
        {
            return string.Equals(Id, other.Id)
                   && string.Equals(Manufacturer, other.Manufacturer)
                   && string.Equals(Model1, other.Model1)
                   && string.Equals(Model2, other.Model2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^
                       ((Manufacturer != null ? Manufacturer.GetHashCode() : 0) * 397) ^
                       (Model1 != null ? Model1.GetHashCode() : 0);
            }
        }

        #endregion
    }
}
