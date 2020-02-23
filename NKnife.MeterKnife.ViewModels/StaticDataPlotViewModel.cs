﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Domain;
using NKnife.MeterKnife.ViewModels.Plots;
using OxyPlot;
using OxyPlot.Axes;

namespace NKnife.MeterKnife.ViewModels
{
    public class StaticDataPlotViewModel : PlotViewModel
    {
        private readonly IEngineeringLogic _engineeringLogic;
        private Engineering _engineering;

        public StaticDataPlotViewModel(IHabitManager habit, IEngineeringLogic engineeringLogic) 
            : base(habit)
        {
            _engineeringLogic = engineeringLogic;
        }

        public void SetEngineering(Engineering engineering)
        {
            _engineering = engineering;
            var duts = _engineering.GetIncludedDUTs();

            SetStyleSolutionByEngineering(duts);
        }

        public async Task LoadDataAsync()
        {
            var duts = _engineering.GetIncludedDUTs();
            // 可以转为并行计算
            // Parallel.For(0, duts.Count, (i, loopState) =>
            // {
            //     var r = _engineeringLogic.GetDUTDataAsync(duts[i]);
            // });
            for (int i = 0; i < duts.Count; i++)
            {
                var r = await _engineeringLogic.GetDUTDataAsync((_engineering, duts[i]));
                foreach (MeasureData data in r)
                {
                    LinearPlot.AddValues((i, data.Time, data.Data));
                }
            }
        }

        private void SetStyleSolutionByEngineering(List<DUT> duts)
        {
            var left = 0;
            var right = 0;
            var solution = new DUTSeriesStyleSolution();
            for (var index = 0; index < duts.Count; index++)
            {
                var style = DUTSeriesStyle.Build(LineStyle.Solid); //.GetAllLineStyles()[index];
                style.Color = PlotTheme.CommonlyUsedColors[index];
                style.DUT = duts[index].Id;

                style.Axis = new LinearAxis();
                style.Axis.Key = duts[index].Id;
                style.Axis.FontSize = 13d;
                style.Axis.MajorGridlineStyle = LineStyle.Dash;
                style.Axis.MinorGridlineStyle = LineStyle.Dot;
                style.Axis.MaximumPadding = 0;
                style.Axis.MinimumPadding = 0;
                style.Axis.Angle = 0;
                style.Axis.Maximum = 220;
                style.Axis.Minimum = -220;
                if (index % 2 == 0)
                {
                    style.Axis.AxisDistance = left++ * 60;
                    style.Axis.Position = AxisPosition.Left;
                }
                else
                {
                    style.Axis.AxisDistance = right++ * 60;
                    style.Axis.Position = AxisPosition.Right;
                }

                solution.Add(style);
            }

            StyleSolution = solution;
        }
    }
}