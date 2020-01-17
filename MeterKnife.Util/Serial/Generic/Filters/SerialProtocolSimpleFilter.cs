using System;
using System.Collections.Generic;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Tunnel;
using MeterKnife.Util.Tunnel.Filters;
using NKnife.Events;

namespace MeterKnife.Util.Serial.Generic.Filters
{
    /// <summary>
    /// һ����򵥵�Э�鴦��Filter,������Handler����Э��ַ�,ֱ���׳�Э���յ��¼�
    /// </summary>
    public class SerialProtocolSimpleFilter : BytesProtocolFilter
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly byte[] _currentReceiveBuffer = new byte[1024];
        private int _currentReceiveByteLength;

        public event EventHandler<EventArgs<IEnumerable<IProtocol<byte[]>>>> ProtocolsReceived;

        protected virtual void OnProtocolsReceived(EventArgs<IEnumerable<IProtocol<byte[]>>> e)
        {
            ProtocolsReceived?.Invoke(this, e);
        }

        public override bool ProcessReceiveData(ITunnelSession session)
        {
            byte[] data = session.Data;
            int len = data.Length;
            if (len == 0)
            {
                return false;
            }

            if (_currentReceiveByteLength + len > 1024) //����������ˣ�ֻ������1024λ
            {
                //��ʱ��������ֱ������
                _Logger.Warn("�յ������ݳ���1024�������������������������");
                return false;
            }

            var tempData = new byte[_currentReceiveByteLength + len];
            Array.Copy(_currentReceiveBuffer, 0, tempData, 0, _currentReceiveByteLength);
            Array.Copy(data, 0, tempData, _currentReceiveByteLength, data.Length);

            //���ɸ���Ĵ���������
            var unfinished = new byte[] {};
            IEnumerable<IProtocol<byte[]>> protocols = ProcessDataPacket(tempData, ref unfinished);

            //��δ��ɽ����������ݴ棬���´��յ����ݺ���д���
            if (unfinished.Length > 0)
            {
                Array.Copy(unfinished, 0, _currentReceiveBuffer, 0, unfinished.Length);
                _currentReceiveByteLength = unfinished.Length;
            }
            else
            {
                _currentReceiveByteLength = 0;
            }

            if (protocols != null)
            {
                OnProtocolsReceived(new EventArgs<IEnumerable<IProtocol<byte[]>>>(protocols));
            }
            return true;
        }

        public override void ProcessSendToSession(ITunnelSession session)
        {
        }

        public override void ProcessSendToAll(byte[] data)
        {
        }

        public override void ProcessSessionBroken(long id)
        {
        }

        public override void ProcessSessionBuilt(long id)
        {
        }
    }
}