﻿using FluentAssertions;
using MeterKnife.Models;
using MeterKnife.Plots;
using NKnife.Base;
using NUnit.Framework;

namespace MeterKnife.Unittests
{
    [TestFixture]
    public class PlainPolyLinePlotTests
    {
        [OneTimeTearDown]
        public void CleanUp()
        {
        }

        [OneTimeSetUp]
        public void Setup()
        {
        }

        public class PlainPolyLinePlotShip : PlainPolyLinePlot
        {
            public PlainPolyLinePlotShip()
                : base(new PlotTheme())
            {
            }

            public static Pair<double, double> UpdateRangeMethod(double[] values, ref bool isFirst, ref double max, ref double min)
            {
                return UpdateRange(values, ref isFirst, ref max, ref min);
            }
        }

        [Test]
        public void UpdateRangeTest1()
        {
            var isFirst = true;
            double max = 0, min = 0;
            var pair = PlainPolyLinePlotShip.UpdateRangeMethod(new[] {1d}, ref isFirst, ref max, ref min);
            isFirst.Should().Be(false);
            max.Should().Be(1.1F);
            min.Should().Be(0.9F);
            pair.Should().Be(Pair<double, double>.Build(0.9F, 1.1F));
        }

        [Test]
        public void GetMinPrecisionValueTest1()
        {
            var v = PlainPolyLinePlot.GetMinPrecisionValue(0);
            v.Should().Be(1);
            v = PlainPolyLinePlot.GetMinPrecisionValue(1);
            v.Should().Be(0.1);
            v = PlainPolyLinePlot.GetMinPrecisionValue(2);
            v.Should().Be(0.01);
            v = PlainPolyLinePlot.GetMinPrecisionValue(24);
            v.Should().Be(0.000000000000000000000001);
        }
    }
}