using System;
using System.Reflection;
using System.Linq;
using System.IO;

namespace HybridWebView.Shared
{
	public class HybridWebViewEngine
	{
		Assembly Assembly;
		string Prefix { get; set; }
		string AssemblyName { get; set; }
		string DocumentPath { get; set; }


		public HybridWebViewEngine()
		{
			Assembly = Assembly.GetExecutingAssembly();
			AssemblyName = Assembly.GetName().Name;
			Prefix = AssemblyName + ".www";
		}

		public void Start()
		{
			CopyResourceToTempFolder();	
		}

		public void CopyResourceToTempFolder()
		{
			DocumentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/www";

			var resourceNames = Assembly.GetManifestResourceNames().Where(_ => _.StartsWith(Prefix, StringComparison.Ordinal)).ToList();

			foreach (var item in resourceNames)
			{
				var str = ConvertNamespaceStringToPathString(item.Replace(Prefix + ".", ""));

				var destination = DocumentPath + "/" + str;
				Console.WriteLine(destination);
				EnsureFolder(destination);

				using (Stream resouceFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(item))
				using (Stream output = File.Create(destination))
				{
					resouceFile.CopyTo(output);
				}
			}

		}

		public string ConvertNamespaceStringToPathString(string value)
		{
			int lastIndex = value.LastIndexOf('.');
			if (lastIndex > 0)
			{
				value = value.Substring(0, lastIndex).Replace(".", "/")
					+ value.Substring(lastIndex);

				Console.WriteLine("ss: {0}", value);
			}

			return value;
		}

		void EnsureFolder(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			if ((directoryName.Length > 0) && (!Directory.Exists(directoryName)))
			{
				Directory.CreateDirectory(directoryName);
			}
		}

		public string GetHTMLString(string file = "index.html")
		{
			var html = string.Empty;

			using (var streamReader = new StreamReader(DocumentPath + "/" +  file))
			{
				html = streamReader.ReadToEnd();
			}

			return html;
		}

		public string GetBaseUrl()
		{
			return DocumentPath + "/";
		}

	}
}
