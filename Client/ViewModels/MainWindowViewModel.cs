﻿using Client.Core;
using Client.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace Client.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RegisterViewWithRegion(RegionNames.WindowRegion, typeof(LoginView));
        }
    }
}