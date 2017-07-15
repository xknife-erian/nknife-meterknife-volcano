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
        public string Author { get; }

        /// <summary>
        ///     �������ϵ��ʽ
        /// </summary>
        public string Contact { get; }

        /// <summary>
        ///     ���������
        /// </summary>
        public string Detail { get; }

        /// <summary>
        ///     ����ķ���ʱ��
        /// </summary>
        public DateTime PublishDate { get; }

        /// <summary>
        ///     ����İ汾
        /// </summary>
        public Version Version { get; }
    }
}