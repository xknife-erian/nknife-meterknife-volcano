using System.Windows;
using AvalonDock.Layout;

namespace NKnife.Module.Workbench.Internal.View.Container;
internal class DockLayoutInitializer : ILayoutUpdateStrategy
{
    public bool BeforeInsertAnchorable(LayoutRoot layout, LayoutAnchorable anchorable, ILayoutContainer container)
    {
        if (container is LayoutAnchorablePane destPane && destPane.FindParent<LayoutFloatingWindow>() != null)
            return false;

        anchorable.AutoHideWidth = 256;
        anchorable.FloatingWidth = 256;
        anchorable.AutoHideHeight = 128;

        var toolsPane = layout.Descendents().OfType<LayoutAnchorablePane>().FirstOrDefault();

        if (toolsPane != null)
        {
            toolsPane.Children.Add(anchorable);
        }
        else
        {
            var pane = new LayoutAnchorablePane() { DockHeight = new GridLength(300), DockMinWidth = 300 };
            pane.Children.Add(anchorable);
            layout.RootPanel.Children.Insert(0, pane);
        }

        return true;
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
