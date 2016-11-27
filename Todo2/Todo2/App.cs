using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshMvvm;
using Todo2.Data;
using Todo2.PageModels;
using Xamarin.Forms;

namespace Todo2
{
    public class App : Application
    {
        public static Size ScreenSize;
        public static double ScreenSizeCoefficient;

        public static SQLiteRepository Repository;

        public App()
        {
            LoadResources();

            Repository = FreshIOC.Container.Resolve<SQLiteRepository>();

            var page = FreshPageModelResolver.ResolvePageModel<TaskItemListPageModel>();
            page.Title = "Todo2";
            var navContainer = new FreshNavigationContainer(page, page.Title);

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

        private void LoadResources()
        {
            ScreenSizeCoefficient = Math.Sqrt(ScreenSize.Height*ScreenSize.Height + ScreenSize.Width*ScreenSize.Width)*0.004;
            Resources = new ResourceDictionary();

            var buttonStyle = new Style(typeof(Button))
            {
                Setters =
                {
                    new Setter { Property  = Button.BackgroundColorProperty, Value = Color.FromHex("#2196F3") },
                    new Setter {Property = Button.WidthRequestProperty, Value = 40*ScreenSizeCoefficient },
                    new Setter { Property = Button.FontSizeProperty, Value = 7*ScreenSizeCoefficient }
                }
            };

            var priorityLabelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter { Property = Label.HeightRequestProperty, Value = 25*ScreenSizeCoefficient },
                    new Setter { Property = Label.WidthRequestProperty, Value = 25*ScreenSizeCoefficient },
                    new Setter { Property = Label.FontSizeProperty, Value = 15*ScreenSizeCoefficient }
                }
            };

            var nameLabelStyle = new Style(typeof(Label))
            {
                Setters =
                { new Setter { Property = Label.FontSizeProperty, Value = 7*ScreenSizeCoefficient } }
            };

            var switchLabelStyle = new Style(typeof(Label))
            {
                Setters =
                {
                    new Setter { Property = Label.FontSizeProperty, Value = 10*ScreenSizeCoefficient },
                    new Setter { Property = Label.MarginProperty, Value = 4*ScreenSizeCoefficient },
                    new Setter { Property = Label.TextColorProperty, Value = Color.Black }
                }
            };

            var entryStyle = new Style(typeof(Entry))
            {
                Setters =
                {
                    new Setter { Property = Entry.FontSizeProperty, Value = 8*ScreenSizeCoefficient }
                }
            };

            var doneImageStyle = new Style(typeof(Image))
            {
                Setters =
                {
                    new Setter { Property = Image.MarginProperty, Value = 2*ScreenSizeCoefficient }
                }
            };

            Resources.Add("buttonStyle", buttonStyle);
            Resources.Add("priorityLabelStyle", priorityLabelStyle);
            Resources.Add("nameLabelStyle", nameLabelStyle);
            Resources.Add("switchLabelStyle", switchLabelStyle);
            Resources.Add("entryStyle", entryStyle);
            Resources.Add("doneImageStyle", doneImageStyle);
            Resources.Add("rowHeigh", (int)(25 * ScreenSizeCoefficient));
        }
    }
}
