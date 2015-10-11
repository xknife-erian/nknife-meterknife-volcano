﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using MeterKnife.Common.Base;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Interfaces;
using MeterKnife.Common.Tunnels;
using MeterKnife.Common.Util;
using NKnife.IoC;

namespace MeterKnife.Kernel.Services
{
    public class TemperatureService : ITemperatureService
    {
        private static readonly ILog _logger = LogManager.GetLogger<DataPathService>();

        private readonly BaseCareCommunicationService _Comm = DI.Get<BaseCareCommunicationService>();
        private readonly CareTemperatureHandler _TemperatureHandler = new CareTemperatureHandler();
        private readonly Dictionary<CommPort, bool> _PortStartMap = new Dictionary<CommPort, bool>();

        public TemperatureService()
        {
            TemperatureValues = new double[1];
        }

        public int Interval
        {
            get { return 5000; }
        }

        public double[] TemperatureValues { get; private set; }

        public bool StartCollect(CommPort carePort)
        {
            Task.Factory.StartNew(() =>
            {
                var isStart = true;
                if (!_PortStartMap.TryGetValue(carePort, out isStart))
                {
                    _PortStartMap.Add(carePort, true);
                }
                _PortStartMap[carePort] = true;
                if (_PortStartMap.Count > 1) //如果采集值的数量（多路温度采集）大于1时
                {
                    var v = TemperatureValues[0];
                    TemperatureValues = new double[_PortStartMap.Count];
                    TemperatureValues[0] = v;
                }
                _Comm.Bind(carePort, _TemperatureHandler);
                while (_PortStartMap[carePort])
                {
                    _Comm.SendCommands(carePort, CommandUtil.TEMP());
                    Thread.Sleep(Interval);
                }
            });
            return true;
        }

        public bool CloseCollect(CommPort carePort)
        {
            _PortStartMap[carePort] = false;
            _Comm.Remove(carePort, _TemperatureHandler);
            return true;
        }
    }
}