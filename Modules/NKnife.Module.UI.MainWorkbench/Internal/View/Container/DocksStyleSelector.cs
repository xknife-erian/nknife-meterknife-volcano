using System.Windows;
using System.Windows.Controls;

namespace NKnife.Module.UI.MainWorkbench.Internal.View.Container;
internal class DocksStyleSelector : StyleSelector
{
    public Style DocumentStyle { get; set; }

    public Style ToolPanelStyle { get; set; }

    public override Style SelectStyle(object item, DependencyObject container)
    {
        // if (item is BaseDocumentVm)
        //     return DocumentStyle;
        //
        // if (item is BasePaneVm)
        //     return ToolPanelStyle;

        return base.SelectStyle(item, container);
    }
}
