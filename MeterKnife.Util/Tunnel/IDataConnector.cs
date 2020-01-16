﻿using System;
using MeterKnife.Util.Tunnel.Events;

namespace MeterKnife.Util.Tunnel
{
    public interface IDataConnector
    {
        bool Stop();
        bool Start();

        event EventHandler<SessionEventArgs> SessionBuilt;
        event EventHandler<SessionEventArgs> SessionBroken;
        event EventHandler<SessionEventArgs> DataReceived;

        void Send(long id, byte[] data);

        void SendAll(byte[] data);

        void KillSession(long id);
    }
}