using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using NKnife.MeterKnife.Common.Domain;

namespace NKnife.MeterKnife.Workbench.Base.Gateways
{
    /// <summary>
    ///     ���������ϵ���item���������仯ʱ�������¼�������
    /// </summary>
    public class InstrumentCollectionChangedEventArgs : EventArgs
    {
        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, Instrument instrument)
        {
            ChangedAction = action;
            Instruments = new[] {instrument};
        }

        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, params Instrument[] instruments)
        {
            ChangedAction = action;
            Instruments = instruments;
        }

        public InstrumentCollectionChangedEventArgs(NotifyCollectionChangedAction action, ICollection<Instrument> instruments)
        {
            ChangedAction = action;
            Instruments = new Instrument[instruments.Count];
            instruments.CopyTo(Instruments, 0);
        }

        public Instrument[] Instruments { get; set; }
        public NotifyCollectionChangedAction ChangedAction { get; set; }
    }
}