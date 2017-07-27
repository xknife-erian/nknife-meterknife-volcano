﻿using System;
using System.Threading;
using GalaSoft.MvvmLight;
using MeterKnife.Interfaces.Plugins;
using MeterKnife.Utils;
using NKnife.Utility;

namespace MeterKnife.ViewModels
{
    public class MeasureViewModel : ViewModelBase
    {
        private IExtenderProvider _ExtenderProvider;
        public PlainPolyLinePlot Plot { get; } = new PlainPolyLinePlot("");

        public void SetProvider(IExtenderProvider provider)
        {
            _ExtenderProvider = provider;
        }

        public event EventHandler PlotModelUpdated;

        protected virtual void OnPlotModelUpdated()
        {
            PlotModelUpdated?.Invoke(this, EventArgs.Empty);
        }

        #region Demo数据生成

        private Thread _DemoThread;

        private bool _OnDemo = false;

        public void StartDemo()
        {
            var rand = new UtilityRandom();
            _DemoThread = new Thread(() =>
            {
                _OnDemo = true;
                Thread.Sleep(500);
                while (_OnDemo)
                {
                    var tail = rand.Next(0, 99999);
                    var v = $"9.99{tail}";
                    Plot.Add(double.Parse(v));
                    OnPlotModelUpdated();
                    Thread.Sleep(150);
                }
            });
            _DemoThread.Start();
        }

        public void StopDemo()
        {
            _OnDemo = false;
            _DemoThread?.Abort();
        }

        #endregion
    }
}