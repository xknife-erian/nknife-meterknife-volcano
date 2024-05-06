using System;
using System.Collections.Generic;
using NKnife.MeterKnife.Util.Tunnel.Generic;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Tunnels.Care
{
    /// <summary>
    /// ����CareOne��Э�����Ľ�������
    /// ʵ�ֽ����յ����ֽ�������зֽ�ɵ���Э�����ݵ�������
    /// </summary>
    public class CareDatagramDecoder : BytesDatagramDecoder
    {
        private static readonly NLog.ILogger _Logger = NLog.LogManager.GetCurrentClassLogger();

        public const byte LEAD = 0x09;

        public override byte[][] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var css = new List<byte[]>();
            bool hasData = true; //�Ƿ�������δ�������
            while (hasData)
            {
                if (data.Length > 0 && data[finishedIndex] == LEAD)
                {
                    bool parseSuccess = Single(data, finishedIndex, out var length, out var cs); //��ȡ��������
                    if (!parseSuccess)
                    {
                        hasData = false;
                        finishedIndex = data.Length; //������ʧ��ʱ����������
                        continue;
                    }
                    css.Add(cs);
                    finishedIndex = finishedIndex + length;
                }
                else
                {
                    finishedIndex++;
                }
                if (finishedIndex >= data.Length)
                {
                    hasData = false;
                }
            }
            return css.ToArray();
        }

        /// <summary>
        /// �������ݵ���ȡ
        /// </summary>
        /// <param name="data">����������</param>
        /// <param name="index">��ʼ��ȡ��λ��</param>
        /// <param name="length">��ȡ��ɵ�λ��</param>
        /// <param name="cs">��ȡ��������</param>
        /// <returns>�Ƿ���ȡ�ɹ�</returns>
        protected virtual bool Single(byte[] data, int index, out int length, out byte[] cs)
        {
            try
            {
                var sl = UtilConvert.ConvertTo<short>(data[index + 2]);
                length = 3 + sl;
                cs = new byte[length];
                Buffer.BlockCopy(data, index, cs, 0, length);
                return true;
            }
            catch (Exception e)
            {
                _Logger.Warn($"������������ʱ�쳣:{e}");
                _Logger.Warn(data.ToHexString());
                length = 0;
                cs = new byte[0];
                return false;
            }
        }
    }
}