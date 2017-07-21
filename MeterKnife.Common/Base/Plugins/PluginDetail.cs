using System;

namespace MeterKnife.Interfaces.Plugins
{
    /// <summary>
    ///     ������з�����ϸ��Ϣ
    /// </summary>
    public class PluginDetail
    {
        /// <summary>
        ///     ���������
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     �������ϵ��ʽ
        /// </summary>
        public string Contact { get; set; }

        /// <summary>
        ///     ���������
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        ///     ����ķ���ʱ��
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        ///     ����İ汾
        /// </summary>
        public Version Version { get; set; }
    }
}