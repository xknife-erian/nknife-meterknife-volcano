using NKnife.Circe.Base.Modules;
using RAY.Common.Plugin.Modules;

namespace NKnife.Feature.Welcome
{
    public class Context : BaseModuleContext
    {

        private Lazy<IAppWelcome>? _appWelcomeLazy;

        public IAppWelcome Welcome =>
            _appWelcomeLazy?.Value ?? throw new InvalidOperationException("Welcome service not initialized");

        /// <inheritdoc />
        public override void Initialize()
        {
            _appWelcomeLazy = GetModule<IAppWelcome>();
        }
    }
}