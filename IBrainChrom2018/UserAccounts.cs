using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class UserAccounts
{
	private const string string_0 = "用户已存在！";

	private const string string_1 = "User exists!";

	public int expirationWarning = 5;

	public int lifeTime = 30;

	public int minLength = 1;

	public bool useExpirWarning = true;

	public bool useLifeTime = true;

	public bool useMinLength = true;

	public User[] users = new User[0];

	public bool AddUser(User user)
	{
		for (int i = 0; i < users.Length; i++)
		{
			if (users[i].u_name.Equals(user.u_name))
			{
				switch (Class49.sysLanguage_0)
				{
				case SysLanguage.CN:
					MessageBox.Show("用户已存在！");
					break;
				case SysLanguage.EN:
					MessageBox.Show("User exists!");
					break;
				}
				return false;
			}
		}
		Array.Resize(ref users, users.Length + 1);
		users[users.Length - 1] = user;
		return true;
	}

	public void DefaultUser()
	{
		User user = new User();
		user.u_name = "Administrator";
		user.u_description = "it's a demo";
		user.uar_OpenUserAccounts = true;
		user.uar_OpenConfiguration = true;
		user.uar_EditMethod = true;
		user.uar_EditChromatogram = true;
		user.uar_EditCalibration = true;
		user.uar_EditSequence = true;
		user.uar_EditReportStyle = true;
		user.uar_SelectMethod = true;
		user.uar_OpenAuditTrailSettings = true;
		user.ui_ModifyPassword("");
		AddUser(user);
		user = new User();
		user.u_name = "Anonymous";
		user.u_description = "Anonymous";
		user.at_Instru2 = false;
		user.at_Instru4 = false;
		user.ui_ModifyPassword("");
		AddUser(user);
	}

	public void DeleteUser(string userName)
	{
		for (int i = 0; i < users.Length; i++)
		{
			if (users[i].u_name.Equals(userName))
			{
				for (int j = i; j < users.Length - 1; j++)
				{
					users[j].LoadFromObject(users[j + 1]);
				}
				Array.Resize(ref users, users.Length - 1);
				break;
			}
		}
	}

	public void Dispose()
	{
		Array.Resize(ref users, 0);
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		if (b == 1)
		{
			useMinLength = binaryReader_0.ReadBoolean();
			useLifeTime = binaryReader_0.ReadBoolean();
			useExpirWarning = binaryReader_0.ReadBoolean();
			minLength = binaryReader_0.ReadInt32();
			lifeTime = binaryReader_0.ReadInt32();
			expirationWarning = binaryReader_0.ReadInt32();
			Array.Resize(ref users, binaryReader_0.ReadInt32());
			for (int i = 0; i < users.Length; i++)
			{
				if (users[i] == null)
				{
					users[i] = new User();
				}
				users[i].LoadFromFile(binaryReader_0);
			}
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	public void LoadFromObject(UserAccounts userAccounts)
	{
		useMinLength = userAccounts.useMinLength;
		useLifeTime = userAccounts.useLifeTime;
		useExpirWarning = userAccounts.useExpirWarning;
		minLength = userAccounts.minLength;
		lifeTime = userAccounts.lifeTime;
		expirationWarning = userAccounts.expirationWarning;
		Array.Resize(ref users, 0);
		for (int i = 0; i < userAccounts.users.Length; i++)
		{
			User user = new User();
			user.LoadFromObject(userAccounts.users[i]);
			AddUser(user);
		}
	}

	public User RetUser(string userName)
	{
		for (int i = 0; i < users.Length; i++)
		{
			if (users[i].u_name.Equals(userName))
			{
				return users[i];
			}
		}
		return null;
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write(useMinLength);
		binaryWriter_0.Write(useLifeTime);
		binaryWriter_0.Write(useExpirWarning);
		binaryWriter_0.Write(minLength);
		binaryWriter_0.Write(lifeTime);
		binaryWriter_0.Write(expirationWarning);
		binaryWriter_0.Write(users.Length);
		for (int i = 0; i < users.Length; i++)
		{
			users[i].SaveToFile(binaryWriter_0);
		}
	}

	public User UserLogin(string userName, string password)
	{
		User user = RetUser(userName);
		if (user != null && user.ui_passwordOK(password))
		{
			return user;
		}
		return null;
	}
}
