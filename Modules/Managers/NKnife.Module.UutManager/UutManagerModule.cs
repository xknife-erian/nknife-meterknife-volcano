using NKnife.Circe.Base.Modules.Managers;
using NKnife.Module.UutManager.Internal;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.UutManager
{
    public class UutManagerModule : BasePicoModule<IUutManager>
    {
        private readonly DefaultModuleContext _moduleContext = new();

        private IUutManager? _uutManager;

        private Lazy<IUutManager>? _uutManagerLazy;

        /// <inheritdoc />
        public override Lazy<IUutManager> Build(params object[] args)
        {
            return _uutManagerLazy ??= new Lazy<IUutManager>(() => _uutManager ??= new DefaultUutManager());
        }

        #region Overrides of BasePicoPlugin
        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            _moduleContext.Initialize();

            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override void Dispose() { }
        #endregion
    }
}