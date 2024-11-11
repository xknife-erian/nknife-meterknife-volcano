using NKnife.Module.Data.Storages.Internal;
using RAY.Common.Plugin;
using RAY.Common.Plugin.Manager;
using RAY.Common.Plugin.Modules;
using RAY.Storages;

namespace NKnife.Module.Data.Storages
{
    public class UnitOfWorkManagerModule : BasePicoModule<IUnitOfWorkManager>, ISupportUsingIoC
    {
        private IUnitOfWorkManager? _uowManager;

        private Lazy<IUnitOfWorkManager>? _uowManagerLazy;
        private Lazy<IIoCManager>? _iocMangerLazy;

        private static bool s_isRegistered = false;

        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
            if(!s_isRegistered)
            {
                StoragesManager.Clear();
                var iocManger = _iocMangerLazy!.Value;

                foreach (var repositoryType in RepositoriesAutofacModule.RepositoryTypes)
                {
                    if(iocManger.Resolve(repositoryType) is IRepository instance)
                        StoragesManager.Register(repositoryType, instance);
                    else
                        throw new Exception($"无法解析类型 {repositoryType.Name} 的实例。");
                }
                s_isRegistered = true;
            }

            return Task.FromResult(true);
        }

        /// <inheritdoc />
        public override Task<bool> StopServiceAsync()
        {
            s_isRegistered = false;
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
        public override Lazy<IUnitOfWorkManager> Build(params object[] args)
        {
            return _uowManagerLazy ??= new Lazy<IUnitOfWorkManager>(() =>
            {
                return _uowManager ??= new DefaultUnitOfWorkManager();
            });
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _uowManagerLazy = null;
            _uowManager     = null;
        }

        #region Implementation of ISupportKernel<IIoCManager>
        /// <inheritdoc />
        public Task<IPicoPlugin> InjectAsync(Lazy<IIoCManager> iocManagerLazy)
        {
            _iocMangerLazy = iocManagerLazy;
            return Task.FromResult<IPicoPlugin>(this);
        }
        #endregion
    }
}