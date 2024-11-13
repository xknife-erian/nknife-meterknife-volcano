using NKnife.Circe.Base.Modules.Manager;
using NKnife.Module.Manager.OptionManager.Internal;
using RAY.Common;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Manager.OptionManager
{
    public class OptionManagerModule : BasePicoModule<IOptionManager>, ISupportUsingModule
    {
        private readonly Context _context = new ();
        private IOptionManager? _optionManager;
        private Lazy<IOptionManager>? _optionManagerLazy;

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
        public override Lazy<IOptionManager> Build(params object[] args)
        {
            return _optionManagerLazy ??= new Lazy<IOptionManager>(() =>
            {
                return _optionManager ??= new DefaultOptionManager(_context.AppWorkspace);
            });
        }

        /// <inheritdoc />
        public override void Dispose() { }
    }

    internal class Context : BaseModuleContext
    {
        private Lazy<IAppWorkspaceManager>? _surroundingsLazy;
        public IAppWorkspaceManager AppWorkspace => _surroundingsLazy!.Value;

        public override void Initialize()
        {
            _surroundingsLazy = GetModule<IAppWorkspaceManager>();
        }
    }
}