using NKnife.Circe.Base.Modules.Data;
using NKnife.Module.Data.Databases.Internal;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.Data.Databases
{
    public class DatabaseFactoryModule : BasePicoModule<ICirceDatabaseFactory>, ISupportUsingModule, ISupportUsingIoC
    {
        private Lazy<ICirceDatabaseFactory>? _dbFactoryLazy;
        private ICirceDatabaseFactory? _dbFactory;
        
        private readonly ModuleContext _moduleContext = new ModuleContext();

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
        public override async Task<bool> ResetServiceAsync()
        {
            await StopServiceAsync();
            await StartServiceAsync();

            return true;
        }

        /// <inheritdoc />
        public override Lazy<ICirceDatabaseFactory> Build(params object[] args)
        {
            return _dbFactoryLazy ??= new Lazy<ICirceDatabaseFactory>(() =>
            {
                return _dbFactory ??= new DefaultDatabaseFactory(_moduleContext);
            });
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _dbFactoryLazy = null;
            _dbFactory     = null;
        }

        #region Implementation of ISupportKernel<IIoCManager>
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IIoCManager> iocManagerLazy)
        {
            _moduleContext.SetIoCManager(iocManagerLazy);
            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion
        #region Implementation of ISupportKernel<IModulesManager>
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IModulesManager> moduleManagerLazy)
        {
            _moduleContext.SetModulesManager(moduleManagerLazy);
            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion
    }
}