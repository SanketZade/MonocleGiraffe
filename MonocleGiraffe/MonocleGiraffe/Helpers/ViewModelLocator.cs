﻿using GalaSoft.MvvmLight.Ioc;
using MonocleGiraffe.Portable.ViewModels;
using MonocleGiraffe.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;

namespace MonocleGiraffe.Helpers
{
    public class ViewModelLocator : Portable.ViewModels.PageKeyHolder, IViewModelLocator
    {
        public ViewModelLocator()
        {
            if (DesignMode.DesignModeEnabled)
                InitDesignTime();
            else
                Init();
            
        }

        private void Init()
        {
            SimpleIoc.Default.Register<SplashPageViewModel>();
            SimpleIoc.Default.Register<SubGalleryPageViewModel>();
            SimpleIoc.Default.Register<FrontPageViewModel>();
            SimpleIoc.Default.Register<BrowserPageViewModel>();           
            SimpleIoc.Default.Register<TransfersPageViewModel>();
        }

        private void InitDesignTime()
        {
            var nav = new MergedNavigationService(App.Current.NavigationService);
            SimpleIoc.Default.Register<GalaSoft.MvvmLight.Views.INavigationService>(() => nav);
            Init();
        }
        
        public TransfersViewModel TransfersViewModel { get { return SimpleIoc.Default.GetInstance<TransfersPageViewModel>(); } }

        public SplashPageViewModel SplashPageViewModel { get { return SimpleIoc.Default.GetInstance<SplashPageViewModel>(); } }

        public FrontPageViewModel FrontPageViewModel { get { return SimpleIoc.Default.GetInstance<FrontPageViewModel>(); } }

        public BrowserViewModel BrowserViewModel { get { return SimpleIoc.Default.GetInstance<BrowserPageViewModel>(); } }

        public SubGalleryViewModel SubGalleryViewModel { get { return SimpleIoc.Default.GetInstance<SubGalleryPageViewModel>(); } }

        public static ViewModelLocator GetInstance()
        {
            return (ViewModelLocator)Template10.Common.BootStrapper.Current.Resources["Locator"];
        }
    }
}
