namespace NKnife.Circe.Base
{
    ///<summary>
    /// 应用程序的欢迎工作流程
    /// </summary>
    public interface IAppWelcome
    {
        /// <summary>
        /// 完成欢迎工作
        /// </summary>
        void CompleteWelcomeWork();

        /// <summary>
        /// 当欢迎工作完成时
        /// </summary>
        event EventHandler WelcomeWorkCompleted;

        /// <summary>
        /// 启动消息
        /// </summary>
        public string StartupMessage { get; set; }

        void StartPauseWelcomeWork();
        
        /// <summary>
        /// 当暂停欢迎工作开始时
        /// </summary>
        event EventHandler PauseWelcomeStarted;
        
        bool IsLoginSuccess { get; set; }
    }
}
