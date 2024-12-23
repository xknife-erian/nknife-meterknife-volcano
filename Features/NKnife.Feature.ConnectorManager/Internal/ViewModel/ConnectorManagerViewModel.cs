﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using NKnife.Circe.Base;
using RAY.Windows.Common.ViewModels.Layout;

namespace NKnife.Feature.ConnectorManager.Internal.ViewModel
{
    class ConnectorManagerViewModel : BaseToolPaneViewModelV1
    {
        public ConnectorManagerViewModel()
        {
            ConnectorCollection =
            [
                new ConnectorInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "NI GPIB-USB-HS",
                    ImagePath   = "Y:\\Circe\\NI GPIB-USB-HS.jpg"
                },

                new ConnectorInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "TCP/IP",
                    ImagePath   = "Y:\\Circe\\RJ45.jpg"
                },

                new ConnectorInfo()
                {
                    Id          = Guid.NewGuid(),
                    Description = "Serial Port",
                    ImagePath   = "Y:\\Circe\\SerialPort.jpg"
                },

            ];
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
