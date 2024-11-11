using RAY.Common;

namespace NKnife.Circe.Base.Modules.Manager
{
    public interface ISurroundingsManager : IManager
    {
        /// <summary>
        ///     应用的开发者路径
        /// </summary>
        string AppDeveloperPath { get; }

        /// <summary>
        ///     应用程序的数据基础路径
        /// </summary>
        string AppPath { get; }

        /// <summary>
        ///     软件的选项保存路径
        /// </summary>
        string OptionPath { get; }

        /// <summary>
        ///     工作日志的保存路径
        /// </summary>
        string LoggerPath { get; }

        /// <summary>
        ///     应用程序的临时数据保存路径
        /// </summary>
        string TempDataPath { get; set; }
    }
}
