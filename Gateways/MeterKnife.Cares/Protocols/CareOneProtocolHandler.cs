using System;
using System.Collections.Generic;
using System.Diagnostics;
using Common.Logging;
using NKnife.Protocol;
using NKnife.Tunnel.Base;

namespace MeterKnife.Cares.Protocols
{
    public abstract class CareOneProtocolHandler : BaseProtocolHandler<byte[]>
    {
        private static readonly ILog Logger = LogManager.GetLogger<CareOneProtocolHandler>();

        protected CareOneProtocolHandler()
        {
            _id = Guid.NewGuid();
            Commands = new List<byte[]>();
        }

        public override sealed List<byte[]> Commands { get; set; }

        public override void Recevied(long sessionId, IProtocol<byte[]> protocol)
        {
            if (!(protocol is CareTalking))
            {
                Debug.Assert(false, "Protocol��������, ����CareSaying����");
                Logger.Warn("Protocol��������, ����CareSaying����");
                return;
            }
            Recevied((CareTalking)protocol);
        }

        public abstract void Recevied(CareTalking protocol);

        private readonly Guid _id;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is CareOneProtocolHandler)) return false;
            return Equals((CareOneProtocolHandler) obj);
        }

        protected bool Equals(CareOneProtocolHandler other)
        {
            return _id.Equals(other._id);
        }

        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}