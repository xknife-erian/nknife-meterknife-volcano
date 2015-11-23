﻿using System;
using MeterKnife.Common.DataModels;
using OxyPlot;
using OxyPlot.Axes;

namespace MerterKnife.Common.Winforms.Plots
{
    public class TemperatureDataPlot : DataPlot
    {
        public override string ValueHead
        {
            get { return "temperature"; }
        }

        protected override double GetLeftAxisAngle()
        {
            return 0;
        }

        protected override OxyColor GetMainSeriesColor()
        {
            return OxyColor.FromArgb(255, 86, 96, 225);
        }

        protected override void UpdateRange(FiguredData fd)
        {
            UpdateRange(fd.TemperatureExtremePoint.Item1, fd.TemperatureExtremePoint.Item2);
        }

        public override void Update(FiguredData fd)
        {
            var yzl = SelectValue(fd);
            var value = double.Parse(yzl.ToString());

            _PlotModel.Title = value.ToString();

            var dataPoint = DateTimeAxis.CreateDataPoint(DateTime.Now, value);
            _Series.Points.Add(dataPoint);
            _Series.PlotModel.InvalidatePlot(true);

            UpdateRange(fd);
        }
    }
}