


using AvalonDock.Layout;

namespace NKnife.Module.UI.MainWorkbench.Internal.View.Container;
internal class DockLayoutInitializer : ILayoutUpdateStrategy
{
    public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorable, ILayoutContainer container)
    {
        if (container is LayoutAnchorablePane destPane && destPane.FindParent<LayoutFloatingWindow>() != null)
            return false;

        anchorable.AutoHideWidth = 256;
        anchorable.FloatingWidth = 256;
        anchorable.AutoHideHeight = 128;
        return false;
    }

    public void AfterInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorableShown)
    {
    }

    public bool BeforeInsertDocument(LayoutRoot layout, LayoutDocument anchorableToShow,
        ILayoutContainer destinationContainer)
    {
        return false;
    }

    public void AfterInsertDocument(LayoutRoot layout, LayoutDocument anchorableShown)
    {
    }
}
