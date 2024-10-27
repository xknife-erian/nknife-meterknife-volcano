using NKnife.Module.Data.ProviderFactory.Internal;
using NLog;
using RAY.Common.Plugin.Modules;
using RAY.Common.Provider;

namespace NKnife.Module.Data.ProviderFactory
{
    public class ProviderFactoryModule : BasePicoModule<IProviderFactory>
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
        private readonly ModuleContext _moduleContext = new();

        private Lazy<IProviderFactory>? _providerFactoryLazy;
        private IProviderFactory? _providerFactory;

        public override Lazy<IProviderFactory> Build(params object[] args)
        {
            return _providerFactoryLazy ??= new Lazy<IProviderFactory>(() =>
            {
                _providerFactory ??= new DefaultProviderFactory();
                return _providerFactory;
            });
        }

        public override Task<bool> StartServiceAsync()
        {
            _moduleContext.Initialize();
            return Task.FromResult(true);
        }

        public override Task<bool> StopServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override Task<bool> ResetServiceAsync()
        {
            return Task.FromResult(true);
        }

        public override void Dispose()
        {
        }
    }
}
