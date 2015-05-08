using System.ComponentModel;
using MeterKnife.Common.Interfaces;

namespace MeterKnife.Common.Base
{
    public class DMMParameters : IMeterParameters
    {
        public DMMParameters()
        {
            NPLC = 10;
        }

        [Category("����"), DisplayName("����")]
        public DMMMeasure DMMMeasure { get; set; }

        [Category("����"), DisplayName("��Χ")]
        public DMMRange Range { get; set; }

        [Category("����"), DisplayName("�Զ�����")]
        public DMMZeroSet DMMZeroSet { get; set; }

        [Category("����ʱ��"), DisplayName("NPLC")]
        public ushort NPLC { get; set; }

        [Category("����ʱ��"), DisplayName("�ֱ���")]
        public DMMRate Rate { get; set; }

    }
}