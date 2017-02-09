using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.OS;
using MonocleGiraffe.Portable.ViewModels;
using Android.Support.V4.View;
using Android.Support.V4.App;
using MonocleGiraffe.Android.Fragments;
using MonocleGiraffe.Portable.Models;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using Android.Views;
using MonocleGiraffe.Android.Helpers;

namespace MonocleGiraffe.Android.Activities
{
    [Activity(Label = "BrowserActivity")]
    public class BrowserActivity : global::Android.Support.V7.App.AppCompatActivity
    {
        private PagerAdapter adapter;

        public NavigationService Nav
        {
            get
            {
                return (NavigationService)ServiceLocator.Current
                    .GetInstance<INavigationService>();
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            SetContentView(Resource.Layout.Browser);
            var param = Nav.GetAndRemoveParameter(Intent) ?? "GalleryInfo";
            Vm.Activate(param);
            adapter = new BrowserAdapter(Vm.Images, SupportFragmentManager);
            BrowserPager.Adapter = adapter;
            BrowserPager.SetCurrentItem(Vm.FlipViewIndex, false);
        }

        public BrowserViewModel Vm { get { return App.Locator.Browser; } }

        private ViewPager browserPager;
        public ViewPager BrowserPager
        {
            get
            {
                browserPager = browserPager ?? FindViewById<ViewPager>(Resource.Id.BrowserPager);
                return browserPager;
            }
        }

        private class BrowserAdapter : FragmentStatePagerAdapter
        {
            private IEnumerable<IGalleryItem> images;
			private int oldCount;

			public BrowserAdapter(IEnumerable<IGalleryItem> images, global::Android.Support.V4.App.FragmentManager fm) : base(fm)
            {
                this.images = images;
				oldCount = images.Count();
			}

            public override int Count
            {
                get
                {
					var newCount = images.Count();
					if (newCount != oldCount)
					{
						oldCount = newCount;
						NotifyDataSetChanged();
					}
					return newCount;
                }
            }

            public override global::Android.Support.V4.App.Fragment GetItem(int position)
            {
                return BrowserItemFragment.NewInstance(position);
            }
        }
    }    
}