﻿// ReSharper disable once CheckNamespace

namespace NKnife.Metrology
{
    public interface IMetrology
    {
        string[] Units { get; }
    }

    /// <summary>
    ///     压力
    /// </summary>
    public class Pressure : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }
    
    /// <summary>
    ///     流量
    /// </summary>
    public class Flow : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     湿度
    /// </summary>
    public class Humidity : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     声音
    /// </summary>
    public class Sound : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     Length: Meter (m) The meter is the metric unit of length. It's defined as the length of the path light travels in a
    ///     vacuum during 1/299,792,458 of a second.
    ///     米。等于氪 86 原子在真空中发射的橙色光波波长的 1，650，763.73 倍。
    /// </summary>
    public class Length : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     Mass: Kilogram (kg) The kilogram is the metric unit of mass. It's the mass of the international prototype of the
    ///     kilogram: a standard platinum/iridium 1 kg mass housed near Paris at the International Bureau of Weights and
    ///     Measures (BIPM).
    ///     公斤。等于保存在巴黎国际计量局的铂铱公斤国际原器的质量。
    /// </summary>
    public class Mass : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     Time: Second (s) The basic unit of time is the second. The second is defined as the duration of 9,192,631,770
    ///     oscillations of radiation corresponding to the transition between the two hyperfine levels of cesium-133.
    ///     秒。等于铯 133 原子基态的两个超精细能 级之间跃迁所对应的辐射的 9192631770 个周期的持续时间。
    /// </summary>
    public class Time : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     Amount of a Substance: Mole (mol) The mole is defined as the amount of a substance that contains as many entities
    ///     as there are atoms in 0.012 kilograms of carbon-12. When the mole unit is used, the entities must be specified. For
    ///     example, the entities may be atoms, molecules, ions, electrons, cows, houses, or anything else.
    ///     摩尔。是一物质系统的物质的量。它是：构成物质系统的结 构粒子数目和 0.012 公斤碳-12中的原子数目相等，则这个系统的物质的量为 1 摩尔（mol）。
    ///     结构粒子可以是原子、分子、离子、电子、光子等，或是这些粒子的指定组合体；在使用该单位时必须指明结构粒子的种类。
    /// </summary>
    public class AmountOfSubstance : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }

    /// <summary>
    ///     Luminous Intensity: candela (cd) The unit of luminous intensity, or light, is the candela. The candela is the
    ///     luminous intensity, in a given direction, of a source emitting monochromatic radiation of frequency 540 x 1012
    ///     hertz with radiant intensity in that direction of 1/683 watt per steradian.
    ///     坎德拉。坎德拉是一光源在给定方向上的发光强度，该光源发出频率为 540×10 12 赫兹的单色辐射，且在此方向上的辐射强度为1683瓦特每球面度（W/sr）。
    /// </summary>
    public class LuminousIntensity : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "nV"};

        #endregion
    }
}
                                                        