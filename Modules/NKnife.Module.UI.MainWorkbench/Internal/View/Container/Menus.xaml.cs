using System.Windows.Controls;

namespace NKnife.Module.UI.MainWorkbench.Internal.View.Container;

/// <summary>
/// Menus.xaml 的交互逻辑
/// </summary>
public partial class Menus : UserControl
{
    public Menus()
    {
        InitializeComponent();
        //DataContextChanged += DriverStepsMenu_DataContextChanged;
    }

    // private void DriverStepsMenu_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    // {
    //     if(e.OldValue is ObservableRecipient { } oldRecipient)
    //     {
    //         oldRecipient.PropertyChanged -= PropertyChanged;
    //     }
    //
    //     if(e.NewValue is ObservableRecipient { } newRecipient)
    //     {
    //         newRecipient.PropertyChanged += PropertyChanged;
    //     }
    // }
    //
    // private void PropertyChanged(object? sender, PropertyChangedEventArgs e)
    // {
    //     if(sender is not DriverStepsMenuVm { } driverStepsMenuVm
    //        || e.PropertyName != nameof(driverStepsMenuVm.DeviceActionProps))
    //         return;
    //
    //     var props = new Dictionary<string, List<CanCreateItem>>();
    //     foreach (var (key, value) in driverStepsMenuVm.DeviceActionProps)
    //         props.Add(key, value);
    //
    //     CreateRibbonItems(props);
    // }
    //
    // private void CreateRibbonItems(Dictionary<string, List<CanCreateItem>> dict)
    // {
    //     _Item_.Items.Clear();
    //
    //     foreach (var kvp in dict)
    //     {
    //         // 创建并设置RibbonGroupBox
    //         var group = new RibbonGroupBox() { Header = kvp.Key };
    //
    //         foreach (var item in kvp.Value)
    //         {
    //             // 创建并设置MenusButtonControl
    //             var menusButton = new RibbonMenuButton()
    //             {
    //                 Header           = item.Name,
    //                 Command          = item.CreateCommand,
    //                 Size             = RibbonControlSize.Large,
    //                 Icon             = "/Assets/16/16_Assistantsmall.svg",
    //                 CommandParameter = item.DeviceActionProp
    //             };
    //
    //             // 添加MenusButtonControl到RibbonGroupBox
    //             group.Items.Add(menusButton);
    //         }
    //
    //         // 添加RibbonGroupBox到RibbonTabItem
    //         _Item_.Items.Add(group);
    //     }
    //
    //     GetToolTip();
    // }
}