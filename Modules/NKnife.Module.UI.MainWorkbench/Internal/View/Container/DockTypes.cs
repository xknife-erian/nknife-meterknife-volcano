using RAY.Library;

namespace NKnife.Module.UI.MainWorkbench.Internal.View.Container
{
    public static class DockTypes
    {
        public static void Register(VmPair vmPair)
        {
            PaneModels.Add(vmPair.ViewModel.Name, vmPair.View);
        }

        internal static Dictionary<string, Type> PaneModels { get; set; } = new();

    }
}