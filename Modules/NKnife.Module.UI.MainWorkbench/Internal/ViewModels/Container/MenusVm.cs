using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RAY.Common;
using RAY.Plugins.WPF.Common;

namespace NKnife.Module.UI.MainWorkbench.Internal.ViewModels.Container
{
    public class MenusVm(IUIManager __uiManager) : ObservableRecipient
    {
        private ObservableCollection<CategoryPoint> _categoryMenus = new ();

        public ObservableCollection<CategoryPoint> CategoryMenus
        {
            get => _categoryMenus;
            set => SetProperty(ref _categoryMenus, value);
        }
    }
}