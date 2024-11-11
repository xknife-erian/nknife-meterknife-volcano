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

        public DefaultSurroundingsManager()
        {
            s_logger.Info($"{UsersDocumentsPath},{Directory.Exists(UsersDocumentsPath)}");
            s_logger.Info($"{AppDeveloperPath},{Directory.Exists(AppDeveloperPath)}");
            s_logger.Info($"{AppPath},{Directory.Exists(AppPath)}");
            s_logger.Info($"{OptionPath},{Directory.Exists(OptionPath)}");
            s_logger.Info($"{LoggerPath},{Directory.Exists(LoggerPath)}");
        }
        public string UsersDocumentsPath => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <inheritdoc />
        public string AppDeveloperPath
        {
            get
            {
                if(_appDeveloperPath == null)
                {
                    var path = Path.Combine(UsersDocumentsPath, "Jeelu", "MeterKnife");
                    _appDeveloperPath = path;
                }

                if(!Directory.Exists(_appDeveloperPath))
                    Directory.CreateDirectory(_appDeveloperPath);

                return _appDeveloperPath;
            }
        }

        /// <inheritdoc />
        public string AppPath
        {
            get
            {
                var assembly = Assembly.GetEntryAssembly();
                if (assembly == null)
                    assembly = Assembly.GetExecutingAssembly();
                var assName = assembly.GetName().Name;
                var dir     = Path.Combine(AppDeveloperPath, $"{assName}");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <inheritdoc />
        public string OptionPath
        {
            get
            {
                var dir = Path.Combine(AppPath, $"{nameof(OptionPath).Replace(PATH_NAME_FLAG, string.Empty)}");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <inheritdoc />
        public string LoggerPath
        {
            get
            {
                var baseDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var dir     = Path.Combine(baseDir, "Jeelu", "MeterKnife");
                dir = Path.Combine(dir, $"{nameof(LoggerPath).Replace(PATH_NAME_FLAG, string.Empty)}");
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                return dir;
            }
        }

        /// <inheritdoc />
        public string TempDataPath { get; set; }

        #region IManager
        public bool IsLaunched { get; private set; } = false;

        public IManager Initialize(params object[] args)
        {
            IsLaunched = true;
            return this;
        }

        public Task<IManager> LaunchAsync(params object[] args)
        {
            return Task.FromResult<IManager>(this);
        }

        public bool Stop()
        {
            return true;
        }

        public string Description { get; } = "软件运行环境的管理器";

        public void Dispose()
        {
        }
        
        #endregion

    }
}
