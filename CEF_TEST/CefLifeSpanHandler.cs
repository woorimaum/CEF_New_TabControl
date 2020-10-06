using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CEF_TEST
{
    class CefLifeSpanHandler : ILifeSpanHandler
    {
        public event Action<string> popup_request;
        
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            if (browser.IsDisposed || browser.IsPopup)
            {
                return false;
            }
            return true;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {            
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {            
        }

        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            if (popup_request != null)
            {
                popup_request(targetUrl);
            }
            // browser.MainFrame.LoadUrl(targetUrl);
            newBrowser = null;
            return true;
        }
    }
}
