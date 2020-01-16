using System;
using System.Collections.Generic;
using System.Diagnostics;
using MeterKnife.Common.DataModels;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Tunnel.Base;

namespace MeterKnife.Common.Tunnels.CareOne
{
    public abstract class CareOneProtocolHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();

        protected CareOneProtocolHandler()
        {
            _Id = Guid.NewGuid();
            Commands = new List<byte[]>();
        }

        public override sealed List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareTalking))
            {
                Debug.Assert(false, "Protocol��������, ����CareSaying����");
                _logger.Warn("Protocol��������, ����CareSaying����");
                return;
            }
            Received((CareTalking)protocol);
        }

        public abstract void Received(CareTalking protocol);

        private readonly Guid _Id;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CareOneProtocolHandler)) return false;
            return Equals((CareOneProtocolHandler) obj);
        }

        protected bool Equals(CareOneProtocolHandler other)
        {
            return _Id.Equals(other._Id);
        }

        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }
    }
}