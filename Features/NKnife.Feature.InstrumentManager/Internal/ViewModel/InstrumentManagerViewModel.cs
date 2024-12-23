using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using RAY.Windows.Common.ViewModels.Layout;

namespace NKnife.Feature.InstrumentManager.Internal.ViewModel
{
    class InstrumentManagerViewModel : BaseToolPaneViewModelV1
    {
        public InstrumentManagerViewModel()
        {
            InstrumentCollection =
            [
                new InstrumentInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "Agilent 34401",
                    ImagePath   = "Y:\\Circe\\Agilent 34401.jpg"
                },
                new InstrumentInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "Keithley DMM6500",
                    ImagePath   = "Y:\\Circe\\Keithley DMM6500.jpg"
                },
                new InstrumentInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "Keysight 34461A",
                    ImagePath   = "Y:\\Circe\\Keysight 34461A.jpg"
                },
            ];
        }
        public ObservableCollection<InstrumentInfo> InstrumentCollection { get; set; }= new();
    }

    public class InstrumentInfo : ObservableObject
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
