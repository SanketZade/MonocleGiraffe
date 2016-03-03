﻿using MonocleGiraffe.Models;
using SharpImgur.APIWrappers;
using SharpImgur.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.ApplicationModel;

namespace MonocleGiraffe.ViewModels.FrontPage
{
    public class GalleryViewModel : ViewModelBase
    {
        public GalleryViewModel()
        {
            if (DesignMode.DesignModeEnabled)
                InitDesignTime();
            else
                Init();
        }        

        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value); }
        }

        private ObservableCollection<GalleryItem> images;
        public ObservableCollection<GalleryItem> Images
        {
            get { return images; }
            set { Set(ref images, value); }
        }

        private async void Init()
        {
            Images = new ObservableCollection<GalleryItem>();
            Title = "MOST VIRAL";
            var gallery = await Gallery.GetGallery(Enums.Section.Top);
            foreach (var image in gallery)
            {
                var gItem = new GalleryItem(image);
                Images.Add(gItem);
            }
        }

        private void InitDesignTime()
        {
            Title = "MOST VIRAL";
            Images = new ObservableCollection<GalleryItem>();
            Images.Add(new GalleryItem(new Image { Title = "Paper Wizard", Animated = true, Link = "http://i.imgur.com/kJYBDHJh.gif", AccountUrl = "AvengeMeKreigerBots", Mp4 = "http://i.imgur.com/kJYBDHJ.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Upvote baby duck for good luck", Animated = false, Link = "http://i.imgur.com/j1jujAp.jpg", AccountUrl = "Snickletits", Mp4 = "" }));
            Images.Add(new GalleryItem(new Image { Title = "Slow Cooker Parmesan Honey Pork Roast", Animated = true, Link = "http://i.imgur.com/AhoWKkYh.gif", AccountUrl = "drocks27", Mp4 = "http://i.imgur.com/AhoWKkY.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "my dad's work for over 20 years", Animated = false, Link = "http://imgur.com/a/izwDI", AccountUrl = "fluffybluemarshmallow", Mp4 = "" }));
            Images.Add(new GalleryItem(new Image { Title = "Playing with blocks", Animated = true, Link = "http://i.imgur.com/QotYGysh.gif", AccountUrl = "Mikepants", Mp4 = "http://i.imgur.com/QotYGys.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Opening the cookie jar", Animated = true, Link = "http://i.imgur.com/wutL0vLh.gif", AccountUrl = "grizzzzzly", Mp4 = "http://i.imgur.com/wutL0vL.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Five Years Ago This Lady Rescued, Raised And Released This Wolf Pack.  They Are So Excited To See Her!", Animated = true, Link = "http://i.imgur.com/umzkvy1.gif", AccountUrl = "LindaDee", Mp4 = "http://i.imgur.com/umzkvy1.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "The new Firefox logo", Animated = true, Link = "http://i.imgur.com/pIfdoIW.gif", AccountUrl = "BOHdiCALis1de", Mp4 = "http://i.imgur.com/pIfdoIW.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Witty Political Title that will (hopefully) not be responded to with \"Witty Political Comment\"", Animated = false, Link = "http://imgur.com/a/a2eOY", AccountUrl = "Ayziak", Mp4 = "" }));
            Images.Add(new GalleryItem(new Image { Title = "When the teacher uses your work as an example.", Animated = true, Link = "http://i.imgur.com/UT7oVCG.gif", AccountUrl = "mubi92", Mp4 = "http://i.imgur.com/UT7oVCG.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Humanity is doomed", Animated = false, Link = "http://imgur.com/a/YfheQ", AccountUrl = "hotbreadzeke", Mp4 = "" }));
            Images.Add(new GalleryItem(new Image { Title = "He comes from a land down under...", Animated = true, Link = "http://i.imgur.com/vP8LnSw.gif", AccountUrl = "Upvotefairywholikesgifs", Mp4 = "http://i.imgur.com/vP8LnSw.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Whaaat?", Animated = true, Link = "http://i.imgur.com/3c3OQJPh.gif", AccountUrl = "netrex", Mp4 = "http://i.imgur.com/3c3OQJP.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "Nap time is...OVER!", Animated = true, Link = "http://i.imgur.com/mnVpdzjh.gif", AccountUrl = "drbatookhan", Mp4 = "http://i.imgur.com/mnVpdzj.mp4" }));
            Images.Add(new GalleryItem(new Image { Title = "MRW I arrive at a stranger's house party, notice them bickering over what movie to watch, throw on my favorite Jim Carrey flick, and they suddenly demand to know who I am...", Animated = true, Link = "http://i.imgur.com/j57jAzI.gif", AccountUrl = "ForeveraKritik", Mp4 = "http://i.imgur.com/j57jAzI.mp4" }));
        }
    }
}
