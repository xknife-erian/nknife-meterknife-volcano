using RAY.Common;
using RAY.Common.Plugin.Modules;

namespace NKnife.Module.UI.MainWorkbench
{
    public class UIManagerModule : BasePicoModule<IUIManager>
    {
        /// <inheritdoc />
        public override Task<bool> StartServiceAsync()
        {
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
        public override Lazy<IUIManager> Build(params object[] args)
        {
            return new Lazy<IUIManager>(ModuleContext.Instance.WorkbenchViewModel);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
        }
    }
}
