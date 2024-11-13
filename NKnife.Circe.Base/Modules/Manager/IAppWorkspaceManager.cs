using RAY.Common;

namespace NKnife.Circe.Base.Modules.Manager
{
    /// <summary>
    /// 应用程序运行时工作空间的管理器。包括运行时相关的目录与文件的管理，相关的配置管理等。<br/>
    /// 1. 路径管理：管理用户数据存储路径。获取应用程序的数据目录、日志目录、临时目录等。<br/>
    /// 2. 程序员配置管理：加载和保存应用程序的程序员配置。<br/>
    /// 3. 资源管理：管理应用程序使用的资源文件（如图片、音频、视频等）。提供资源文件的加载和卸载方法。<br/>
    /// 4. 环境变量管理：管理和获取环境变量。提供环境变量的读取和设置方法。
    /// </summary>
    public interface IAppWorkspaceManager : IManager
    {
        /// <summary>
        ///     应用程序的数据基础路径
        /// </summary>
        string AppDataPath { get; }

        /// <summary>
        ///     应用的开发者路径
        /// </summary>
        string AppDeveloperPath { get; }

        /// <summary>
        ///     软件的选项保存路径
        /// </summary>
        string OptionPath { get; }

        /// <summary>
        ///     工作日志的保存路径
        /// </summary>
        string LogPath { get; }

        /// <summary>
        ///     应用程序的临时数据保存路径
        /// </summary>
        string TempPath { get; }

        /// <summary>
        ///     应用程序的用户数据保存路径
        /// </summary>
        string DataPath { get; }

        /// <summary>
        ///     应用程序的数据备份路径
        /// </summary>
        string BackupPath { get; } 
    }
}
