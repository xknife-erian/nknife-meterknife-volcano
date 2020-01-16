using System;
using System.Net;

namespace NKnife.Socket.Events
{
    /// <summary>
    ///     ��Socket�����������Ӱ����¼����ݵ���
    /// </summary>
    public class ConnectingEventArgs : EventArgs
    {
        public ConnectingEventArgs(EndPoint serverInfo)
        {
            ServerInfo = serverInfo;
        }

        public EndPoint ServerInfo { get; set; }
    }
}