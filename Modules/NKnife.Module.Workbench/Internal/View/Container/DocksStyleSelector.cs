using System.Windows;
using System.Windows.Controls;
using RAY.Windows.Common.ViewModels.Layout;

namespace NKnife.Module.Workbench.Internal.View.Container;

internal class DocksStyleSelector : StyleSelector
{
    public Style DocumentStyle { get; set; }

    public Style ToolPanelStyle { get; set; }

    public override Style SelectStyle(object item, DependencyObject container)
    {
        if (item is BaseDocumentViewModelV1)
            return DocumentStyle;

        if (item is BaseToolPaneViewModelV1)
            return ToolPanelStyle;

        return base.SelectStyle(item, container);
    }
}
