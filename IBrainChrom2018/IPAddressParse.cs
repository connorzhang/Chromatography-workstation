using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace IBrainChrom2018;

internal static class IPAddressParse
{
	public static string GetLocalIPAddress()
	{
		string text = "";
		IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
		IPAddress[] array = addressList;
		foreach (IPAddress iPAddress in array)
		{
			if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				text = text + iPAddress.ToString() + ",";
			}
		}
		return text.Remove(text.Length - 1);
	}

	public static string smethod_1(string string_0)
	{
		string input = UrlBrowser(string_0);
		string pattern = "IP: \\[(?<IP>[0-9\\.]*)\\]";
		return Regex.Match(input, pattern).Groups["IP"].Value;
	}

	private static string UrlBrowser(string string_0)
	{
		Uri requestUri = new Uri(string_0);
		HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(requestUri);
		try
		{
			using HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			using StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
			return streamReader.ReadToEnd();
		}
		catch (Exception ex)
		{
			return ex.Message;
		}
		finally
		{
			httpWebRequest.Abort();
		}
	}
}
