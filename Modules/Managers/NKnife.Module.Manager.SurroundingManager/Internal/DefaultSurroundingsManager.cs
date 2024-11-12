using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NKnife.Circe.Base.Modules;
using NKnife.Circe.Base.Modules.Manager;
using NLog;
using RAY.Common;

namespace NKnife.Module.Manager.SurroundingManager.Internal
{
    internal class DefaultSurroundingsManager : ISurroundingsManager
    {
        private const string PATH_NAME_FLAG = nameof(Path);
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        private string? _appDeveloperPath;
        private string? _appPath;
        private string? _optionPath;
        private string? _logPath;
        private string? _tempPath;
        private string? _dataPath;
        private string? _backupPath;

        private readonly string _usersDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <inheritdoc />
        public string AppDeveloperPath
        {
            get
            {
                if(_appDeveloperPath == null)
                {
                    var path = Path.Combine(_usersDocumentsPath, "Jeelu-MeterKnife-Circe");
                    _appDeveloperPath = path;
                }

                if(!Directory.Exists(_appDeveloperPath))
                    Directory.CreateDirectory(_appDeveloperPath);

                return _appDeveloperPath;
            }
        }

        /// <inheritdoc />
        public string AppDataPath
        {
            get
            {
                _appPath ??= Path.Combine(_usersDocumentsPath, "Jeelu-MeterKnife-Circe");

                if (!Directory.Exists(_appPath))
                    Directory.CreateDirectory(_appPath);

                return _appPath;
            }
        }

        /// <inheritdoc />
        public string OptionPath
        {
            get
            {
                _optionPath = Path.Combine(AppDataPath, GetMainPath(nameof(OptionPath)));
                if (!Directory.Exists(_optionPath))
                    Directory.CreateDirectory(_optionPath);
                return _optionPath;
            }
        }

        /// <inheritdoc />
        public string LogPath
        {
            get
            {
                _logPath ??= Path.Combine(AppDataPath, GetMainPath(nameof(LogPath)));
                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);
                return _logPath;
            }
        }

        /// <inheritdoc />
        public string TempPath
        {
            get
            {
                if(string.IsNullOrEmpty(_tempPath))
                {
                    _tempPath = Path.Combine(AppDataPath, GetMainPath(nameof(TempPath)));

                    if(Directory.Exists(_tempPath)) //第一次启动时，清空临时文件夹
                    {
                        try
                        {
                            Directory.Delete(_tempPath, true);
                        }
                        catch (Exception e)
                        {
                            s_logger.Error(e, $"删除临时文件夹出错。{e.Message}");
                        }
                    }
                    Directory.CreateDirectory(_tempPath);
                }
                return _tempPath;
            }
        }

        /// <inheritdoc />
        public string DataPath
        {
            get
            {
                _dataPath ??= Path.Combine(AppDataPath, GetMainPath(nameof(DataPath)));
                if (!Directory.Exists(_dataPath))
                    Directory.CreateDirectory(_dataPath);
                return _dataPath;
            }
        }

        /// <inheritdoc />
        public string BackupPath
        {
            get
            {
                _backupPath ??= Path.Combine(AppDataPath, GetMainPath(nameof(BackupPath)));
                if (!Directory.Exists(_backupPath))
                    Directory.CreateDirectory(_backupPath);
                return _backupPath;
            }
        }

        #region IManager
        public bool IsLaunched { get; private set; } = false;

        public IManager Initialize(params object[] args)
        {
            if(!IsLaunched)
            {
                s_logger.Debug($"{AppDeveloperPath},{Directory.Exists(AppDeveloperPath)}");
                s_logger.Info($"{nameof(Environment.SpecialFolder.MyDocuments)},{Directory.Exists(_usersDocumentsPath)}");
                s_logger.Info($"{AppDataPath},{Directory.Exists(AppDataPath)}");
                s_logger.Info($"{DataPath},{Directory.Exists(DataPath)}");
                s_logger.Info($"{OptionPath},{Directory.Exists(OptionPath)}");
                s_logger.Info($"{LogPath},{Directory.Exists(LogPath)}");
                s_logger.Info($"{TempPath},{Directory.Exists(TempPath)}");
                s_logger.Info($"{BackupPath},{Directory.Exists(BackupPath)}");
            }
            IsLaunched = true;
            return this;
        }

        public Task<IManager> LaunchAsync(params object[] args)
        {
            return Task.FromResult<IManager>(this);
        }

        public bool Stop()
        {
            if (Directory.Exists(_tempPath))
            {
                try
                {
                    Directory.Delete(_tempPath, true);
                }
                catch (Exception e)
                {
                    s_logger.Error(e, $"删除临时文件夹出错。{e.Message}");
                }
            }
            return true;
        }

        public string Description { get; } = "软件运行环境的管理器";

        public void Dispose()
        {
            Stop();
        }
        #endregion

        private static string GetMainPath(string propertyName)
        {
            return $"{propertyName.Replace(PATH_NAME_FLAG, string.Empty)}";
        }
    }
}
