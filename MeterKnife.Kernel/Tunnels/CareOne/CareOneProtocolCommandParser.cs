﻿using NKnife.Protocol.Generic;

namespace MeterKnife.Kernel.Tunnels.CareOne
{
    public class CareOneProtocolCommandParser : BytesProtocolCommandParser
    {
        public override byte[] GetCommand(byte[] datagram)
        {
            return new[] {datagram[3], datagram[4]};
        }
    }
}