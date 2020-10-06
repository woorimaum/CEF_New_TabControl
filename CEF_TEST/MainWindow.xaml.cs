using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CEF_TEST
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ChromiumWebBrowser> Tabs { get; }

        public MainWindow()
        {
            InitializeComponent();

            CefSettings settings = new CefSettings
            {
                RemoteDebuggingPort = 8088
            };

            settings.CachePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\CEF";
            settings.CefCommandLineArgs.Add("disable-usb-keyboard-detect", "1");
            settings.CefCommandLineArgs.Add("persist_session_cookies", "1");
            settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            settings.CefCommandLineArgs.Add("disable-gpu", "1");

            Cef.Initialize(settings);

            #region Tab Control 
            //Tabs = Enumerable.Range(1, 2).Select(x => new ChromiumWebBrowser
            //{
            //    Address = "google.com"
            //}).ToList();

            //ChromiumWebBrowser abc = new ChromiumWebBrowser();            
            //ChromiumWebBrowser ab = new ChromiumWebBrowser();
            //Tabs = new List<ChromiumWebBrowser>();
            //Tabs.Add(abc);
            //Tabs.Add(ab);

            //abc.Load("https://www.google.com");
            //ab.Load("https://www.google.com");

            var tt = Environment.CurrentDirectory;
            CefLifeSpanHandler cefLifeSpanHandler = new CefLifeSpanHandler();
            cefLifeSpanHandler.popup_request += Life_popup_request;

            Tabs = new ObservableCollection<ChromiumWebBrowser>();
            Tabs.Add(new ChromiumWebBrowser() { Address = tt + @"\test.html", LifeSpanHandler = cefLifeSpanHandler });

            DataContext = this;
            #endregion
        }

        private void Life_popup_request(string obj)
        {
            //function for open pop up in a new browser
            Carregar_popup_new_browser(obj);
        }

        private void Carregar_popup_new_browser(string url)
        {
            //open pop up in second browser
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Tabs.Add(new ChromiumWebBrowser() { Address = url });

                tabcontrol.SelectedIndex = Tabs.Count - 1;
                // splashScreen.Close();
            }));

            
            // chrome_popup.Load(url);
        }
    }
}
