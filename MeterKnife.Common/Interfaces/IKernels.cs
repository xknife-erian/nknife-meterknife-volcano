using System;

namespace MeterKnife.Interfaces
{
    public interface IKernels
    {
        /// <summary>
        ///     ���غ��ķ��񼰲��
        /// </summary>  
        void LoadCoreService(Action<string> displayMessage);
        /// <summary>
        ///     ж�غ��ķ��񼰲��
        /// </summary>
        void UnloadCoreService();
    }
}