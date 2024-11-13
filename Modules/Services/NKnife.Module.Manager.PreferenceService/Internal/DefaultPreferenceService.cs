using System.Collections.Immutable;
using NKnife.Circe.Base.Modules.Service;
using NLog;
using RAY.Common;
using RAY.Library;
using RAY.Library.XML;

namespace NKnife.Module.Manager.PreferenceService.Internal
{
    /// <summary>
    ///     用户选项管理器
    /// </summary>
    internal sealed class DefaultPreferenceService(IAppWorkspaceService __appWorkspace)
        : QuickXml(Path.Combine(__appWorkspace.OptionPath, OPTION_FILE)), IPreferenceService
    {
        private const string OPTION_FILE = "option.conf";
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc />
        public IImmutableList<string> GetOptionKeys()
        {
            return GetKeys();
        }

        /// <inheritdoc />
        public object? GetOption(string key, Type valueType)
        {
            try
            {
                return GetValueByType(key, valueType);
            }
            catch (Exception e)
            {
                s_logger.Error(e, $"获取选项值失败。Key:{key}; Type:{valueType.Name}\r\n{e.Message}");

                return null;
            }
        }

        /// <inheritdoc />
        public T GetOption<T>(string key, T? defaultValue)
        {
            return GetValue(key, defaultValue);
        }

        /// <inheritdoc />
        public bool TryGetOption<T>(string key, out T? value)
        {
            return TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public void SetOption(string key, object value)
        {
            SetValue(key, value);
        }

        /// <inheritdoc />
        public void ForcedUpdateOption(string key, object value, Func<bool> cond)
        {
            var isNeed = cond.Invoke();
            if(isNeed)
                SetOption(key, value);
        }

        /// <inheritdoc />
        public event EventHandler<DataChangedEventArgs<KeyValuePair<string, object>>>? OptionUpdated;

        #region Implementation of IDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Stop();
        }
        #endregion

        private void OnOptionUpdated(DataChangedEventArgs<KeyValuePair<string, object>> e)
        {
            OptionUpdated?.Invoke(this, e);
        }

        #region Implementation of IManager
        /// <summary>
        ///     管理器是否已启用
        /// </summary>
        public bool IsLaunched { get; private set; }

        /// <summary>
        ///     初始化管理器
        /// </summary>
        public IManager Initialize(params object[] args)
        {
            return this;
        }

        /// <summary>启用管理器</summary>
        public Task<IManager> LaunchAsync(params object[] args)
        {
            IsLaunched = true;

            return Task.FromResult<IManager>(this);
        }

        /// <summary>终止管理器</summary>
        public bool Stop()
        {
            return true;
        }

        /// <summary>可选属性。管理器功能的描述，可能会被显示在日志等位置。</summary>
        public string Description { get; } = "用户选项管理器";
        #endregion
    }
}