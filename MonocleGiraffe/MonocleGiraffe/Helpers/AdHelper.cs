﻿using GalaSoft.MvvmLight.Ioc;
using MonocleGiraffe.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using XamarinImgur.Interfaces;

namespace MonocleGiraffe.Helpers
{
    public class AdHelper
    {
        private string appKey;
        private string bannerId;

        public AdHelper(ISecretsProvider secretsProvider)
        {
            Init(secretsProvider);
        }

        private async void Init(ISecretsProvider secretsProvider)
        {
            var allConfig = await secretsProvider.GetSecrets();
            var config = allConfig["Ad_Config"];
            appKey = (string)config["App_Key"];
            bannerId = (string)config["Banner_Id"];
            AdDuplex.AdDuplexClient.Initialize(appKey);
        }

        private AdControl banner;
        public AdControl Banner
        {
            get
            {
                if (banner == null)
                {
                    banner = new AdControl();
                    banner.Init(appKey, bannerId);
                }
                if (BannerParent != null)
                    BannerParent.Children.Remove(banner);
                return banner;
            }
        }

        public Panel BannerParent { get; set; }

        private bool? isStoreContextTypePresent;
        private bool IsStoreContextTypePresent
        {
            get
            {
                if (isStoreContextTypePresent == null)
                    isStoreContextTypePresent = ApiInformation.IsTypePresent("Windows.Services.Store.StoreContext");
                return (bool)isStoreContextTypePresent;
            }                
        }

        public async Task<bool> ShowAds()
        {
            if (!IsStoreContextTypePresent)
                return true;
            AddOnsHelper addOnsHelper = SimpleIoc.Default.GetInstance<AddOnsHelper>();
            IReadOnlyDictionary<string, StoreLicense> licenses = await addOnsHelper.GetAddOnLicenses();
            foreach (var item in licenses)
            {
                if (item.Value.IsActive)
                    return false;
            }
            return true;
        }
    }
}
