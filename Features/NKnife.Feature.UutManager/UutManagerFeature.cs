using NKnife.Feature.UutManager.Internal;
using NLog;
using RAY.Common.Plugin;
using RAY.Plugins.WPF.Common;

namespace NKnife.Feature.UutManager
{
    public class UutManagerFeature : BasePicoFeatures
    {
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();
        private readonly ModuleContext _moduleContext = new ModuleContext();

        private IFeatureSet? _featureSet;

        /// <inheritdoc />
        public override IFeatureSet GetFeatureSet()
        {
            return _featureSet ??= new DefaultFeatureSet();
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