namespace IBrainChrom2018.Unit;

public class IBrainSecurity
{
	private static string k0 = "A12345678A";

	private static IBrainSecurity myself = null;

	public static IBrainSecurity Create()
	{
		if (myself == null)
		{
			myself = new IBrainSecurity();
		}
		return myself;
	}

	private IBrainSecurity()
	{
	}

	public static string EncryptDes(string strData)
	{
		return KasuDES.Encrypt(strData, k0);
	}

	public static string DecryptDes(string strData)
	{
		return KasuDES.Decrypt(strData, k0);
	}

	public static string EncryptMd5(string strData)
	{
		return KasuMD5.MD5Encrypt(strData, k0);
	}
}
