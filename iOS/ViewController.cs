using System;
using Foundation;
using UIKit;
using HybridWebView.Shared;

namespace DemoHybridWebView.iOS
{
	public partial class ViewController : UIViewController
	{
		int count = 1;

		public ViewController(IntPtr handle) : base(handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			PrepareView();
		}

		public override void DidReceiveMemoryWarning()
		{
			base.DidReceiveMemoryWarning();
			// Release any cached data, images, etc that aren't in use.		
		}

		public void PrepareView()
		{

			var hybrid = new HybridWebViewEngine();
			hybrid.Start();
			var baseUrl = new NSUrl(hybrid.GetBaseUrl());

			WebView.LoadHtmlString(hybrid.GetHTMLString(), baseUrl);

		}
	}
}
