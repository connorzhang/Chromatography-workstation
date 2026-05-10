using System;
using System.Windows.Forms;
using System.Xml;
using SuperDog;

namespace IBrainChrom2018.Unit;

public class DogFeturlMgr
{
	public static bool Licenced(string strID)
	{
		string info = "";
		try
		{
			Dog dog = new Dog();
			DogStatus dogStatus = dog.Login("0fe8flL0bYZwE84969jsyffnvWPMPZSeg8FbOEVzjXRjXMWbxDMtnV6zp5pdLIHJVw+WmYc1sjCLW5Zg4kTzCmexdxv57wH+gwyRFaAGjs4ADbO8nqrVsCA5GHYBmUu8smKShCQMuJ/so3O+FOnXtcOLWblM2SXG6moAOdeb7Clnj/eUUGOh+yi1jrZKLuIixo1Cy3WkJPgFnwU14TQzjFG9gSxdjKNxqWv+MfRamt/E0qsiwnT4mI2CYRI5vqINUB3+yr0GKYyjf5LyZ+GbDOI/2I67ewP2hZPyrba6gqjTytRMGG5g0bU5IMl+j2wEqtFCyusrBwKlGLJFQ9O1oUHdGSbnWPNKrJD685X2EN5U5Od3DeyInu7D/QJ8nzcdwKM5SZhovsyVu8/AeRyt7MM4KQN3iT4PH0IgpBOfgfKdIBgbu/sWyfbOWW3VdpbpHIkI4A/5QQmwijusMOtB8slXV29sp4ofaG/VNOnTFPQWMqUmMtWOCax2PSJvlBYkIJiW4f7+lzCqMIRDxqnveJQMhTVQYnhEGMKDTqj0pMviuZpdnaH99TmNvpBq0RatTcqNO4/xcIdZ++lcaDp1k3PUAij09Tys8QkHWuQSRMrtwBMfWLonksF0Ri+tplKSNiLhC2nSMAZvjPBbWVFRa3I1jnVcVTxoVwiTtMcaHQSiY30m4uP8xaxoVrUNqybZiunfXnV6UNFsleLrLKHjNgBgaLDlo7Ip3xfDkGXSJcSe90JtLbvAkWgiavdeJ8Kma9rabYw233aKJ6oZZd1o8KSVKZ5Uj2e54sFMB04oNFtcp7pCXz75nw7N3eLLZr/dd2u3SRKQU+GsaQZgENNHJy+CR/kuwX7UaO2CXwHpPTvhmQUaHv3JhUarsLUpb4jr/rzMg+vNOrDGxccJkVo5H8Nye6ZtdRC/oH5jMCsF96BaQ+tQGMfv1Ci5obHT7dyRxLhNbkuPs5EAHiPK7JNKgg==");
			DogStatus info2 = Dog.GetInfo("<dogscope />", "<dogformat><feature><attribute name=\"id\"/><element name=\"license\"/></feature></dogformat>", "0fe8flL0bYZwE84969jsyffnvWPMPZSeg8FbOEVzjXRjXMWbxDMtnV6zp5pdLIHJVw+WmYc1sjCLW5Zg4kTzCmexdxv57wH+gwyRFaAGjs4ADbO8nqrVsCA5GHYBmUu8smKShCQMuJ/so3O+FOnXtcOLWblM2SXG6moAOdeb7Clnj/eUUGOh+yi1jrZKLuIixo1Cy3WkJPgFnwU14TQzjFG9gSxdjKNxqWv+MfRamt/E0qsiwnT4mI2CYRI5vqINUB3+yr0GKYyjf5LyZ+GbDOI/2I67ewP2hZPyrba6gqjTytRMGG5g0bU5IMl+j2wEqtFCyusrBwKlGLJFQ9O1oUHdGSbnWPNKrJD685X2EN5U5Od3DeyInu7D/QJ8nzcdwKM5SZhovsyVu8/AeRyt7MM4KQN3iT4PH0IgpBOfgfKdIBgbu/sWyfbOWW3VdpbpHIkI4A/5QQmwijusMOtB8slXV29sp4ofaG/VNOnTFPQWMqUmMtWOCax2PSJvlBYkIJiW4f7+lzCqMIRDxqnveJQMhTVQYnhEGMKDTqj0pMviuZpdnaH99TmNvpBq0RatTcqNO4/xcIdZ++lcaDp1k3PUAij09Tys8QkHWuQSRMrtwBMfWLonksF0Ri+tplKSNiLhC2nSMAZvjPBbWVFRa3I1jnVcVTxoVwiTtMcaHQSiY30m4uP8xaxoVrUNqybZiunfXnV6UNFsleLrLKHjNgBgaLDlo7Ip3xfDkGXSJcSe90JtLbvAkWgiavdeJ8Kma9rabYw233aKJ6oZZd1o8KSVKZ5Uj2e54sFMB04oNFtcp7pCXz75nw7N3eLLZr/dd2u3SRKQU+GsaQZgENNHJy+CR/kuwX7UaO2CXwHpPTvhmQUaHv3JhUarsLUpb4jr/rzMg+vNOrDGxccJkVo5H8Nye6ZtdRC/oH5jMCsF96BaQ+tQGMfv1Ci5obHT7dyRxLhNbkuPs5EAHiPK7JNKgg==", ref info);
			if (info2 == DogStatus.EmptyScopeResults)
			{
				return false;
			}
			if (info == "")
			{
				return false;
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(info);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//feature[@id='" + strID + "']/license/license_type");
			if (xmlNode == null)
			{
				return false;
			}
			switch (xmlNode.InnerText)
			{
			case "perpetual":
				return true;
			case "trial":
			{
				string innerText = xmlDocument.SelectSingleNode("//feature[@id='" + strID + "']/license/time_start").InnerText;
				if ("uninitialized" == innerText)
				{
					return true;
				}
				int num2 = int.Parse(xmlDocument.SelectSingleNode("//feature[@id='" + strID + "']/license/total_time").InnerText);
				if (int.Parse(innerText) + num2 - (DateTime.Now - new DateTime(1970, 1, 1)).Seconds > 0)
				{
					return true;
				}
				return false;
			}
			case "expiration":
			{
				int num = int.Parse(xmlDocument.SelectSingleNode("//feature[@id='" + strID + "']/license/exp_date").InnerText);
				if ((DateTime.Now - new DateTime(1970, 1, 1)).Seconds - num > 0)
				{
					return false;
				}
				return true;
			}
			default:
				return false;
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK);
			return false;
		}
	}

	public static bool LicencedGMP()
	{
		return Licenced("1311") || Licenced("1356");
	}

	public static bool LicencedDetector()
	{
		return Licenced("1312");
	}

	public static bool LicencedShY()
	{
		return Licenced("1337");
	}
}
