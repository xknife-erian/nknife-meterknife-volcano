using MeterKnife.Util.Serial.Common;

namespace MeterKnife.Util
{
    /// <summary>���ڲ�����ӿ�
    /// </summary>
    public interface ISerialPortHold
    {
        /// <summary>�����Ƿ�򿪱��
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>��ʼ��������ͨѶ����
        /// </summary>
        /// <param name="portName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        bool Initialize(string portName, SerialConfig config);

        /// <summary> �رմ���
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>���ô��ڶ�ȡ��ʱ
        /// </summary>
        /// <param name="timeout"></param>
        void SetSyncModelWaitTimeout(int timeout);

        /// <summary>�������ݼ��̶�ȡ�ظ�����
        /// </summary>
        /// <param name="cmd">�����͵�����</param>
        /// <param name="recv">�ظ�������</param>
        /// <returns>�ظ������ݵĳ���</returns>
        int SendReceived(byte[] cmd, out byte[] recv);
    }
}