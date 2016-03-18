﻿using MonocleGiraffe.Helpers;
using MonocleGiraffe.ViewModels;
using SharpImgur.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace MonocleGiraffe.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrontPage : Page
    {
        MainViewModel mainVM = StateHelper.ViewModel;
        public FrontPage()
        {
            this.InitializeComponent();           
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        private async void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {            
            Debug.WriteLine(await SecretsHelper.GetAccessToken());
            Debug.WriteLine(await SecretsHelper.GetRefreshToken());
        }
    }
}
