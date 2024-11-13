using NKnife.Circe.Base.Modules.Service;
using NKnife.Module.Manager.PreferenceService.Internal;
using RAY.Common;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Manager.PreferenceService
{
    public class PreferenceServiceModule : BasePicoModule<IPreferenceService>, ISupportUsingModule
    {
        private readonly Context _context = new ();
        private IPreferenceService? _optionManager;
        private Lazy<IPreferenceService>? _optionManagerLazy;

        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> moduleManagerLazy)
        {
            _context.SetModulesManager(moduleManagerLazy);

            return Task.FromResult((IPicoPlugin)this);
        }

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
        public override Lazy<IPreferenceService> Build(params object[] args)
        {
            return _optionManagerLazy ??= new Lazy<IPreferenceService>(() =>
            {
                return _optionManager ??= new DefaultPreferenceService(_context.AppWorkspace);
            });
        }

        /// <inheritdoc />
        public override void Dispose() { }
    }

    internal class Context : BaseModuleContext
    {
        private Lazy<IAppWorkspaceService>? _surroundingsLazy;
        public IAppWorkspaceService AppWorkspace => _surroundingsLazy!.Value;

        public override void Initialize()
        {
            _surroundingsLazy = GetModule<IAppWorkspaceService>();
        }
    }
}