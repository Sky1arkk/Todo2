using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshMvvm;
using Todo2.Data;
using Todo2.PageModels;
using Todo2.Pages;
using Xamarin.Forms;

namespace Todo2
{
    public class App : Application
    {
        public static Size ScreenSize;
        public static double ScreenSizeCoefficient = ScreenSize.Height / ScreenSize.Width;

        public static SQLiteRepository Repository;

        public App()
        {
            Repository = FreshIOC.Container.Resolve<SQLiteRepository>();

            var page = FreshPageModelResolver.ResolvePageModel<TaskItemListPageModel>();
            var navContainer = new FreshNavigationContainer(page);

            MainPage = navContainer;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
