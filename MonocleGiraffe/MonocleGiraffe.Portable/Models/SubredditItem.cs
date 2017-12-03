﻿using MonocleGiraffe.Portable.Helpers;
using Newtonsoft.Json;
using XamarinImgur.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonocleGiraffe.Portable.Models
{
    public class SubredditItem : BindableBase
    {
        public SubredditItem()
        { }

        public SubredditItem(Subreddit subreddit)
        {
            Url = subreddit.DisplayName;
            Title = subreddit.Title;
            Subscribers = subreddit.Subscribers ?? 0;      
            IsMature = subreddit.Over18 ?? false;
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { Set(ref url, value); }
        }
                                                                                                    
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        private int subscribers;
        public int Subscribers
        {
            get { return subscribers; }
            set { Set(ref subscribers, value); }
        }

        private bool isFavorited;
        public bool IsFavorited
        {
            get { return isFavorited; }
            set { Set(ref isFavorited, value); }
        }

        bool isMature;
        public bool IsMature
        {
            get { return isMature; }
            set { Set(ref isMature, value); }
        }
    }
}
