using Android.App;
using Android.Widget;
using Android.OS;
using Android.Webkit;
using HybridWebView.Shared;

namespace DemoHybridWebView.Droid
{
	[Activity(Label = "DemoHybridWebView", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		WebView WebView { get; set; }

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			WebView = FindViewById<WebView>(Resource.Id.webView1);

			PrepareView();

		}

		public void PrepareView()
		{

			var hybrid = new HybridWebViewEngine();
			hybrid.Start();

			WebView.LoadDataWithBaseURL("file://" + hybrid.GetBaseUrl(), hybrid.GetHTMLString(), "text/html", "utf-8", null);

		}

	}
}

