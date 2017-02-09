﻿using GalaSoft.MvvmLight.Command;
using MonocleGiraffe.Portable.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinImgur.Models;
using static MonocleGiraffe.Portable.Helpers.Initializer;

namespace MonocleGiraffe.Portable.ViewModels.Settings
{
    public class ImgurSettingsViewModel : BindableBase
    {
        public ImgurSettingsViewModel(bool isInDesignMode)
        {
            if (isInDesignMode)
                InitDesignTime();
            else
                Init();
        }

        public async Task Refresh()
        {
            if (AuthenticationHelper.IsAuthIntended())
                if (!IsLoaded)
                    await Load();
        }

        private async Task Init()
        {
            if (AuthenticationHelper.IsAuthIntended())
                await Load();
        }

        private async Task Load()
        {
            try
            {
                State = BUSY;
                await LoadSettings();
                State = AUTHENTICATED;
            }
            catch
            {
                State = NOT_AUTHENTICATED;
            }
        }

        public bool IsLoaded { get; private set; }

        private async Task LoadSettings()
        {
            var userName = await SecretsHelper.GetUserName();
            Account account = await Accounts.GetAccount(userName);
            Bio = account.Bio;
            await Task.Delay(100);
            AccountSettings settings = await Accounts.GetAccountSettings(userName);
            PublicImages = settings.PublicImages;
            MessagingEnabled = settings.MessagingEnabled;
            AlbumPrivacyIndex = ToIndex(settings.AlbumPrivacy);
            ShowMature = settings.ShowMature;
            SubscribeNewsletter = settings.NewsletterSubscribed;
            IsLoaded = true;
        }

        private int ToIndex(string albumPrivacy)
        {
            switch (albumPrivacy)
            {
                case "public":
                    return 0;
                case "hidden":
                    return 1;
                case "secret":
                default:
                    return 2;
            }
        }

        private string ToAlbumPrivacy(int index)
        {
            switch (index)
            {
                case 0:
                    return "public";
                case 1:
                    return "hidden";
                case 2:
                default:
                    return "secret";
            }
        }

        private const string AUTHENTICATED = "Authenticated";
        private const string NOT_AUTHENTICATED = "NotAuthenticated";
        private const string BUSY = "Busy";
        string state = NOT_AUTHENTICATED;
        public string State { get { return state; } set { Set(ref state, value); } }

        string bio = default(string);
        public string Bio { get { return bio; } set { Set(ref bio, value); } }

        bool publicImages = default(bool);
        public bool PublicImages { get { return publicImages; } set { Set(ref publicImages, value); } }

        bool messagingEnabled = default(bool);
        public bool MessagingEnabled { get { return messagingEnabled; } set { Set(ref messagingEnabled, value); } }

        int albumPrivacyIndex = default(int);
        public int AlbumPrivacyIndex { get { return albumPrivacyIndex; } set { Set(ref albumPrivacyIndex, value); } }

        bool showMature = default(bool);
        public bool ShowMature { get { return showMature; } set { Set(ref showMature, value); } }

        bool subscribeNewsletter = default(bool);
        public bool SubscribeNewsletter { get { return subscribeNewsletter; } set { Set(ref subscribeNewsletter, value); } }

        RelayCommand signInCommand;
        public RelayCommand SignInCommand
           => signInCommand ?? (signInCommand = new RelayCommand(async () =>
           {
               await Load();
           }));

        RelayCommand saveCommand;
        public RelayCommand SaveCommand
           => saveCommand ?? (saveCommand = new RelayCommand(async () =>
           {
               await Save();
           }));

        private async Task Save()
        {
            State = BUSY;
            JObject payload = new JObject();
            payload["bio"] = Bio;
            payload["public_images"] = PublicImages;
            payload["messaging_enabled"] = MessagingEnabled;
            payload["album_privacy"] = ToAlbumPrivacy(AlbumPrivacyIndex);
            payload["show_mature"] = ShowMature;
            payload["newsletter_subscribed"] = SubscribeNewsletter;
            await Accounts.SaveAccountSettings(await SecretsHelper.GetUserName(), payload);
            State = AUTHENTICATED;
        }

        private void InitDesignTime()
        {
            State = AUTHENTICATED;
            Bio = "My awesome bio. Lorem Ipsum Dolor sit amet.";
            PublicImages = true;
            MessagingEnabled = true;
            AlbumPrivacyIndex = 1;
            ShowMature = false;
            SubscribeNewsletter = false;
        }
    }
}
