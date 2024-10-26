using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AvalonDock.Layout;

namespace NKnife.Module.UI.MainWorkbench.Internal.View.Container;

/// <summary>
/// Docks.xaml 的交互逻辑
/// </summary>
public partial class Docks : UserControl
{
    public Docks()
    {
        InitializeComponent();
        _DockingManager_.Theme = new AvalonDock.Themes.Vs2013BlueTheme();
    }

    private void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.OriginalSource is not FrameworkElement frameworkElement)
            return;

        switch (frameworkElement.DataContext)
        {
            case LayoutDocument layoutDocument:
                layoutDocument.IsSelected = true;
                layoutDocument.IsActive = true;

                break;
            case LayoutAnchorable layoutAnchorable:
                layoutAnchorable.IsSelected = true;
                layoutAnchorable.IsActive = true;

                break;
        }
    }
}