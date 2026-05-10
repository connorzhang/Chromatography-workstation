using System;
using System.IO;
using System.Windows.Forms;

namespace IBrainChrom2018;

public class User
{
	public enum Level
	{
		管理员,
		分析员,
		检验员,
		访问员
	}

	public bool at_Instru1 = true;

	public bool at_Instru2 = true;

	public bool at_Instru3 = true;

	public bool at_Instru4 = true;

	public InstruWinsInfo[] instrusWinsInfo = null;

	public Options options = new Options();

	public string personInfo = "";

	public PictureBoxSizeMode sgSizeMode;

	public string signGraph = "";

	public string u_description = "";

	public string u_name = "";

	public bool uar_EditCalibration = true;

	public bool uar_EditChromatogram = true;

	public bool uar_EditMethod = true;

	public bool uar_EditReportStyle = true;

	public bool uar_EditSequence = true;

	public bool uar_OpenAuditTrailSettings;

	public bool uar_OpenConfiguration;

	public bool uar_OpenUserAccounts;

	public bool uar_SelectMethod = true;

	public DateTime ui_createDT = DateTime.Now;

	public DateTime ui_lastLogin;

	public DateTime ui_lastPasswordSet;

	public DateTime TipTime;

	public Level ULevel;

	protected string ui_password = "";

	public PasswordState ui_passwordState = PasswordState.Blank;

	public User()
	{
		instrusWinsInfo = new InstruWinsInfo[SysCfgDlg.sysConfig.pageInstrus.Length];
	}

	private void method_0()
	{
		options = new Options();
		for (int i = 0; i < instrusWinsInfo.Length; i++)
		{
			instrusWinsInfo[i] = new InstruWinsInfo();
			instrusWinsInfo[i].valid = false;
		}
	}

	public void LoadFromFile(BinaryReader binaryReader_0)
	{
		byte b = binaryReader_0.ReadByte();
		if (b == 1)
		{
			u_name = binaryReader_0.ReadString();
			u_description = binaryReader_0.ReadString();
			uar_OpenUserAccounts = binaryReader_0.ReadBoolean();
			uar_OpenConfiguration = binaryReader_0.ReadBoolean();
			uar_EditMethod = binaryReader_0.ReadBoolean();
			uar_EditChromatogram = binaryReader_0.ReadBoolean();
			uar_EditCalibration = binaryReader_0.ReadBoolean();
			uar_EditSequence = binaryReader_0.ReadBoolean();
			uar_EditReportStyle = binaryReader_0.ReadBoolean();
			uar_SelectMethod = binaryReader_0.ReadBoolean();
			uar_OpenAuditTrailSettings = binaryReader_0.ReadBoolean();
			ui_createDT = DateTime.FromBinary(binaryReader_0.ReadInt64());
			ui_password = binaryReader_0.ReadString();
			ui_passwordState = (PasswordState)binaryReader_0.ReadByte();
			ui_lastPasswordSet = DateTime.FromBinary(binaryReader_0.ReadInt64());
			ui_lastLogin = DateTime.FromBinary(binaryReader_0.ReadInt64());
			at_Instru1 = binaryReader_0.ReadBoolean();
			at_Instru2 = binaryReader_0.ReadBoolean();
			at_Instru3 = binaryReader_0.ReadBoolean();
			at_Instru4 = binaryReader_0.ReadBoolean();
			personInfo = binaryReader_0.ReadString();
			signGraph = binaryReader_0.ReadString();
			sgSizeMode = (PictureBoxSizeMode)binaryReader_0.ReadByte();
		}
		else
		{
			Class49.smethod_33(b);
		}
	}

	public void LoadFromObject(User user)
	{
		u_name = user.u_name;
		u_description = user.u_description;
		uar_OpenUserAccounts = user.uar_OpenUserAccounts;
		uar_OpenConfiguration = user.uar_OpenConfiguration;
		uar_EditMethod = user.uar_EditMethod;
		uar_EditChromatogram = user.uar_EditChromatogram;
		uar_EditCalibration = user.uar_EditCalibration;
		uar_EditSequence = user.uar_EditSequence;
		uar_EditReportStyle = user.uar_EditReportStyle;
		uar_SelectMethod = user.uar_SelectMethod;
		uar_OpenAuditTrailSettings = user.uar_OpenAuditTrailSettings;
		ui_createDT = user.ui_createDT;
		ui_password = user.ui_password;
		ui_passwordState = user.ui_passwordState;
		ui_lastPasswordSet = user.ui_lastPasswordSet;
		ui_lastLogin = user.ui_lastLogin;
		at_Instru1 = user.at_Instru1;
		at_Instru2 = user.at_Instru2;
		at_Instru3 = user.at_Instru3;
		at_Instru4 = user.at_Instru4;
		personInfo = user.personInfo;
		signGraph = user.signGraph;
		sgSizeMode = user.sgSizeMode;
	}

	public void LoadUserOptions()
	{
		method_0();
		string text = ResourceImageLoad.ExePath() + "Users\\" + u_name + ".uo";
		if (!File.Exists(text))
		{
			return;
		}
		Class49.OpenBinaryReader(text, out var _, out var fileStream_, out var binaryReader_);
		try
		{
			options.LoadFromFile(fileStream_, binaryReader_);
			int num = binaryReader_.ReadInt32();
			for (int i = 0; i < instrusWinsInfo.Length; i++)
			{
				if (i < num)
				{
					instrusWinsInfo[i].Load(binaryReader_);
				}
			}
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
		}
		catch
		{
			method_0();
			MessageBox.Show("删除文件");
			Class49.FileStreamClose(ref fileStream_, ref binaryReader_);
			File.Delete(text);
		}
	}

	public static void MessageNoAccessRights()
	{
		MessageBox.Show(Lang.PS("无访问权！", "No Access Rights!"));
	}

	public void SaveToFile(BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write(Class49.smethod_36());
		binaryWriter_0.Write(u_name);
		binaryWriter_0.Write(u_description);
		binaryWriter_0.Write(uar_OpenUserAccounts);
		binaryWriter_0.Write(uar_OpenConfiguration);
		binaryWriter_0.Write(uar_EditMethod);
		binaryWriter_0.Write(uar_EditChromatogram);
		binaryWriter_0.Write(uar_EditCalibration);
		binaryWriter_0.Write(uar_EditSequence);
		binaryWriter_0.Write(uar_EditReportStyle);
		binaryWriter_0.Write(uar_SelectMethod);
		binaryWriter_0.Write(uar_OpenAuditTrailSettings);
		binaryWriter_0.Write(ui_createDT.ToBinary());
		binaryWriter_0.Write(ui_password);
		binaryWriter_0.Write((byte)ui_passwordState);
		binaryWriter_0.Write(ui_lastPasswordSet.ToBinary());
		binaryWriter_0.Write(ui_lastLogin.ToBinary());
		binaryWriter_0.Write(at_Instru1);
		binaryWriter_0.Write(at_Instru2);
		binaryWriter_0.Write(at_Instru3);
		binaryWriter_0.Write(at_Instru4);
		binaryWriter_0.Write(personInfo);
		binaryWriter_0.Write(signGraph);
		binaryWriter_0.Write((byte)sgSizeMode);
	}

	public void SaveUserOptions()
	{
		Class49.OpenBinaryWriter(ResourceImageLoad.ExePath() + "Users\\" + u_name + ".uo", out var _, out var fileStream_, out var binaryWriter_);
		options.SaveToFile(fileStream_, binaryWriter_);
		binaryWriter_.Write(instrusWinsInfo.Length);
		for (int i = 0; i < instrusWinsInfo.Length; i++)
		{
			instrusWinsInfo[i].Save(binaryWriter_);
		}
		Class49.FileStreamClose(ref fileStream_, ref binaryWriter_);
	}

	public void SaveWinInfo(InstrumentForm instruForm)
	{
		int instruPageNo = instruForm.InstruPageNo;
		instrusWinsInfo[instruPageNo].valid = true;
		instrusWinsInfo[instruPageNo].instruForm = instruForm;
		instrusWinsInfo[instruPageNo].ReadFromForm();
	}

	public void ui_ModifyPassword(string newPassword)
	{
		ui_password = newPassword;
		ui_lastPasswordSet = DateTime.Now;
		if (ui_password == "")
		{
			ui_passwordState = PasswordState.Blank;
		}
		else
		{
			ui_passwordState = PasswordState.Submitted;
		}
	}

	public bool ui_passwordOK(string password)
	{
		return ui_password == password;
	}
}
