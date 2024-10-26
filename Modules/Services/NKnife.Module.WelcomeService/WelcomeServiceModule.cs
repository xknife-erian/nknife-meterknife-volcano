using NKnife.Circe.Base;
using NKnife.Module.WelcomeService.Internal;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.WelcomeService
{
    public class WelcomeServiceModule : BasePicoModule<IAppWelcome>
    {
        private readonly ModuleContext _context = new ();
        private IAppWelcome? _appWelcome;

        private Lazy<IAppWelcome>? _appWelcomeLazy;

        /// <inheritdoc />
        public override Lazy<IAppWelcome> Build(params object[] args)
        {
            return _appWelcomeLazy ??= new Lazy<IAppWelcome>(() =>
            {
                _appWelcome ??= new DefaultAppWelcome();
                return _appWelcome;
            });
        }

        #region Overrides of BasePicoPlugin
        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            _context.Initialize();

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