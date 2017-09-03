using System.Windows.Forms;
using MeterKnife.Interfaces.Gateways;

namespace MeterKnife.Views.InstrumentsDiscovery.Controls.Instruments
{
    public class HeadClickEventArgs : MouseEventArgs
    {
        public GatewayModel GatewayModel { get; set; }

        /// <summary>��ʼ�� <see cref="T:System.Windows.Forms.MouseEventArgs" /> �����ʵ����</summary>
        /// <param name="gatewayModel">����;��</param>
        /// <param name="button">
        /// <see cref="T:System.Windows.Forms.MouseButtons" /> ֵ֮һ����ָʾ�����µ����ĸ���갴ť��</param>
        /// <param name="clicks">��갴ť�������µĴ�����</param>
        /// <param name="x">��굥���� x ���꣨������Ϊ��λ����</param>
        /// <param name="y">��굥���� y ���꣨������Ϊ��λ����</param>
        /// <param name="delta">�������ת�����ƶ��������з��ż�����</param>
        public HeadClickEventArgs(GatewayModel gatewayModel, MouseButtons button, int clicks, int x, int y, int delta) 
            : base(button, clicks, x, y, delta)
        {
            GatewayModel = gatewayModel;
        }
    }
}