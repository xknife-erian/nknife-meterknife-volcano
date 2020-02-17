﻿using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using NKnife.Interface;
using NKnife.MeterKnife.Common.Tunnels;
using NKnife.MeterKnife.Util.Serial.Common;

namespace NKnife.MeterKnife.Common.Domain
{
    /// <summary>
    ///     描述一个数据端口，一般是只能打开一次的独占数据端口。比如串口，TCPIP端口等。
    /// </summary>
    public sealed class Slot
    {
        private readonly string _id;

        public Slot()
        {
            _id = Guid.NewGuid().ToString();
        }

        public SlotType SlotType { get; set; }

        public string Config { get; set; }

        public override string ToString()
        {
            switch (SlotType)
            {
                case SlotType.MeterCare:
                    var c = JsonConvert.DeserializeObject<(short, SerialConfig)>(Config);
                    return $"{nameof(SlotType.MeterCare)} : COM{c.Item1}";
            }
            return $"{SlotType}/{Config}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Slot)) return false;
            return Equals((Slot) obj);
        }

        private bool Equals(Slot other)
        {
            return _id.Equals(other._id) && SlotType == other.SlotType;
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode() * 397;
        }
    }
}