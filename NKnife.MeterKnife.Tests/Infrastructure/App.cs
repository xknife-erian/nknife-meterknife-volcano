namespace NKnife.MeterKnife.Tests.Infrastructure
{
    public class App
    {
        public App(ISlot slot, IDataBus dataBus, IStatementQueue statements)
        {
            Slot = slot;
            DataBus = dataBus;
            Statements = statements;
        }

        public ISlot Slot { get; }

        public IDataBus DataBus { get; }

        public IStatementQueue Statements { get; }
    }
}