﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Instruments.Agilent
{
    public class Ag34401 : BaseMeter
    {
        public Ag34401()
        {
            Parameters = new Ag34401Parameters();
        }
    }

    public class Ag34401Parameters : MultiMeterParameters
    {
    }

    public class MultiMeterParameters : IMeterParameters
    {
        public MultiMeterParameters()
        {
            NPLC = 10;
        }

        [Category("基本"), DisplayName("测量")]
        public DMMMeasure DMMMeasure { get; set; }
        [Category("基本"), DisplayName("范围")]
        public DMMRange Range { get; set; }
        [Category("基本"), DisplayName("自动调零")]
        public DMMZeroSet DMMZeroSet { get; set; }

        [Category("积分时间"), DisplayName("NPLC")]
        public ushort NPLC { get; set; }
        [Category("积分时间"), DisplayName("分辩率")]
        public DMMRate Rate { get; set; }

    }

    public enum DMMMeasure
    {
        直流电压,交流电压,直流电流,交流电流,电阻2W,电阻4W,频率,周期
    }

    public enum DMMRange
    {
        自动,
    }

    public enum DMMRate
    {
    }

    public enum DMMZeroSet
    {
        关闭,单次,开启
    }
}