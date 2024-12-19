using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.Circe.Base;
using RAY.Windows.Common.ViewModels.Layout;

namespace NKnife.Feature.ConnectorManager.Internal.ViewModel
{
    class ConnectorManagerViewModel : BaseToolPaneViewModelV1
    {
        public ConnectorManagerViewModel()
        {
            ConnectorCollection = new();
            ConnectorCollection.Add(new ConnectorInfo()
            {
                Id = Guid.NewGuid(),
                Description = "Agilent 34401",
                ImagePath = "Y:\\Agilent34401.jpg"
            });
        }
        public ObservableCollection<ConnectorInfo> ConnectorCollection { get; set; }= new();
    }

    public class ConnectorInfo : ObservableObject
    {
        public Guid Id
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string? ImagePath
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string? Description
        {
            get;
            set => SetProperty(ref field, value);
        }
    }
}
