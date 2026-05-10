using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SQLite;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace IBrainChrom2018.Unit;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class DBBase
{
	public enum ConnState
	{
		UnConnect,
		Connected
	}

	public enum DBProvider
	{
		JetOLEDB4,
		SQLOLEDB,
		UNKNOW
	}

	private static DBBase dbBase = null;

	public OleDbConnection conn = null;

	private OleDbCommand command = null;

	private OleDbDataAdapter adapter = null;

	private string strConnectionString = "";

	private string strSoftVersion = "VER.16.10.22.01";

	private string strDBVersion = "";

	private const string ConstDBVersion = "1.0";

	private DataSet dsConfig = null;

	private ConnState state = ConnState.UnConnect;

	private DataSet m_dsSystem = null;

	private OleDbCommandBuilder m_cmdSystem = null;

	private OleDbDataAdapter m_adapterSystem = null;

	private DataView m_dvSystem = null;

	private string strDBName = "";

	private string strConnString = "";

	private SystemParam sysParam = SystemParam.Create();

	public DBProvider Provider
	{
		get
		{
			int num = strConnectionString.IndexOf("Jet.OLEDB.4");
			if (num != -1)
			{
				return DBProvider.JetOLEDB4;
			}
			num = strConnectionString.IndexOf("SQLOLEDB");
			if (num != -1)
			{
				return DBProvider.SQLOLEDB;
			}
			return DBProvider.UNKNOW;
		}
	}

	public string StrConn
	{
		get
		{
			return strConnectionString;
		}
		set
		{
			strConnectionString = value;
		}
	}

	public ConnState State => state;

	public OleDbConnection Connection => conn;

	public OleDbDataAdapter Adapter => adapter;

	private DBBase()
	{
		string startupPath = Application.StartupPath;
		if (sysParam.iDbConnectType == 0)
		{
			string text = (IsPath(sysParam.strDbName) ? sysParam.strDbName : (startupPath + "\\" + sysParam.strDbName));
			if (!File.Exists(text))
			{
				throw new Exception("数据库文件不存在!");
			}
			strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;jet oledb:database password=china-infinite;Data Source=" + text;
		}
		else if (sysParam.iDbConnectType == 2)
		{
			strConnectionString = sysParam.strDbName;
		}
		state = ConnState.UnConnect;
		strConnString = strConnectionString;
		conn = new OleDbConnection(strConnectionString);
		command = new OleDbCommand("", conn);
		adapter = new OleDbDataAdapter("", conn);
		state = ConnState.Connected;
	}

	public static DBBase Create()
	{
		if (dbBase == null)
		{
			dbBase = new DBBase();
		}
		return dbBase;
	}

	public void CloseConnection()
	{
		if (conn != null && conn.State == ConnectionState.Open)
		{
			conn.Close();
		}
	}

	public DataSet Select(string strSelect)
	{
		if (state == ConnState.UnConnect)
		{
			return null;
		}
		DataSet dataSet = new DataSet();
		adapter.SelectCommand.CommandText = strSelect;
		adapter.Fill(dataSet);
		return dataSet;
	}

	public int ExecuteNonQuery(string strSql)
	{
		if (state == ConnState.UnConnect)
		{
			return 0;
		}
		int num = 0;
		command.CommandText = strSql;
		if (conn.State == ConnectionState.Closed)
		{
			conn.Open();
		}
		while (conn.State == ConnectionState.Connecting)
		{
			Thread.Sleep(1000);
		}
		return command.ExecuteNonQuery();
	}

	public object ExecuteScalar(string strSelect)
	{
		if (state == ConnState.UnConnect)
		{
			return null;
		}
		command.CommandText = strSelect;
		object obj = new object();
		lock (command)
		{
			lock (obj)
			{
				if (conn.State == ConnectionState.Closed)
				{
					conn.Open();
				}
				while (conn.State == ConnectionState.Connecting)
				{
					Thread.Sleep(1000);
				}
				obj = command.ExecuteScalar();
			}
		}
		return obj;
	}

	private bool IsPath(string strname)
	{
		int num = strname.IndexOf("\\");
		if (num > -1)
		{
			return true;
		}
		return false;
	}

	public static string NewCode()
	{
		return Guid.NewGuid().ToString().Replace("-", "");
	}

	public int ExeNoQuery(string strUser)
	{
		if (state == ConnState.UnConnect)
		{
			return 0;
		}
		if (conn.State == ConnectionState.Closed)
		{
			conn.Open();
		}
		while (conn.State == ConnectionState.Connecting)
		{
			Thread.Sleep(1000);
		}
		OleDbCommand oleDbCommand = new OleDbCommand(strUser, conn);
		return oleDbCommand.ExecuteNonQuery();
	}

	public DataTable ReCmdTable(string sql)
	{
		if (conn.State == ConnectionState.Closed)
		{
			conn.Open();
		}
		while (conn.State == ConnectionState.Connecting)
		{
			Thread.Sleep(1000);
		}
		OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(sql, conn);
		oleDbDataAdapter.SelectCommand.CommandText = sql;
		DataTable dataTable = new DataTable();
		try
		{
			oleDbDataAdapter.Fill(dataTable);
		}
		catch (Exception)
		{
			dataTable = null;
		}
		finally
		{
		}
		return dataTable;
	}

	public bool CreateDbBaseAccess()
	{
		string sql = "select top 1 * from OLog";
		string strUser = "Create Table OLog([DateTime] datetime,[Type] Text(255),NetChromUserName Text(255),IntrumentName Text(255),[Moudle] Text(255) ,[Describe] memo, ComputerUsername Text(255),ComputerName Text(255),VerInfo Text(255),Primary key([DateTime]))";
		DataTable dataTable = ReCmdTable(sql);
		if (dataTable == null)
		{
			ExeNoQuery(strUser);
		}
		string sql2 = "select top 1 * from VOC";
		string strUser2 = "Create Table VOC([Code] Text(36),[DateTime] datetime, [ID] int,[dexcotIndex]int,[ComponentName] Text(255),[FileName] memo, [Area] float,[AreaPer] float,[floatRev0] float,[floatRev1] float,[strRev0] memo,[strRev1] memo,Primary key([code]))";
		DataTable dataTable2 = ReCmdTable(sql2);
		if (dataTable2 == null)
		{
			ExeNoQuery(strUser2);
		}
		else if (dataTable2.Columns.Count != 12)
		{
			ExeNoQuery("drop table VOC");
			ExeNoQuery(strUser2);
		}
		return true;
	}

	public DataTable GetDataTableAccess(string strSql)
	{
		return ReCmdTable(strSql);
	}

	public bool InsertIntoTableAccess(string strType, string strUserName, string strIntrumentName, string strMoudle, string strDesc)
	{
		string text = $"Version {strSoftVersion} ";
		string hostName = Dns.GetHostName();
		string userName = Environment.UserName;
		string text2 = " INSERT INTO [OLog] ( [DateTime] ,[Type]  ,[NetChromUserName]  ,[IntrumentName] ,[Moudle]  ,     ";
		text2 += "[Describe] ,[ComputerUsername]  ,[ComputerName]  ,[VerInfo] )   ";
		text2 = text2 + " values( now(),'" + strType;
		text2 = text2 + "','" + strUserName + "','" + strIntrumentName;
		text2 = text2 + "','" + strMoudle + "','" + strDesc;
		text2 = text2 + "','" + userName + "','" + hostName + "','" + text;
		text2 += " ') ";
		ExeNoQuery(text2);
		return true;
	}

	public bool InsertIntoVocAccess(int ComponentID, int dexcotID, string strComponentName, string strFileName, float fAreaPer, float fArea, float floatRev0, float floatRev1, string strRev0, string strRev1)
	{
		string text = " INSERT INTO [VOC]([Code],[DateTime] ,[ID] ,[dexcotIndex],[ComponentName],[FileName]  ,[Area],[AreaPer] ,[floatRev0],[floatRev1],[strRev0],[strRev1]) values( '";
		text = text + NewCode() + "', ";
		text = text + "now(), " + ComponentID + "," + dexcotID + ", '";
		text = text + strComponentName + "','" + strFileName + "'," + fArea + "," + fAreaPer + "," + floatRev0 + "," + floatRev1 + ",'";
		text = text + strRev0 + "','" + strRev1 + "' )";
		ExeNoQuery(text);
		return true;
	}

	public DataTable GetDataTableVocAccess(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd HH:mm:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd HH:mm:ss");
		string sql = "select * from VOC where [ID]=" + ComponentID + " and [DateTime]>=#" + text + "#  and [DateTime]<=#" + text2 + "#  order by [DateTime] asc";
		return ReCmdTable(sql);
	}

	public int DeleteDataTableVocAccess(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd HH:mm:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd HH:mm:ss");
		string strUser = "delete from VOC where [ID]=" + ComponentID + " and [DateTime]>=#" + text + "#  and [DateTime]<=#" + text2 + "#";
		return ExeNoQuery(strUser);
	}

	public int DeleteDataTableVocAccess(int ComponentID, string fileName)
	{
		string strUser = "delete from VOC where [ID]=" + ComponentID + " and [FileName]>=#" + fileName + "#";
		return ExeNoQuery(strUser);
	}

	public bool CreateDbBaseSqlLite()
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (!File.Exists(text))
		{
			SQLiteConnection.CreateFile("MyDatabase.sqlite");
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " ";
			text2 += " CREATE TABLE [OLog] ( ";
			text2 += "[DateTime] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[Type] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[NetChromUserName] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[IntrumentName] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[Moudle] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[Describe] TEXT NOT NULL ON CONFLICT FAIL,  ";
			text2 += " [ComputerUsername] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += "[ComputerName] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text2 += " [VerInfo] TEXT NOT NULL ON CONFLICT FAIL); ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			string text3 = " ";
			text3 += " CREATE TABLE [VOC] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[DateTime] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[ID] INT NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[dexcotIndex] INT NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[ComponentName] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[FileName] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[Area] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[AreaPer] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[floatRev0] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[floatRev1] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text3 += " [strRev0] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[strRev1] CHAR NOT NULL ON CONFLICT FAIL);  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [HISDATA] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[16] CHAR ,  ";
			text3 += "[17] CHAR ,  ";
			text3 += "[18] CHAR ,  ";
			text3 += "[19] CHAR ,  ";
			text3 += "[20] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [流路] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[流路] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [A流路1] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [A流路2] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [A流路3] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [A流路4] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [A流路5] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [A流路6] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [A流路7] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [A流路8] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 += " CREATE TABLE [B流路1] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [B流路2] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [B流路3] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			text3 = " ";
			text3 += " CREATE TABLE [B流路4] ( ";
			text3 += "[Code] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[方法名称] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[通道] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text3 += "[1] CHAR ,  ";
			text3 += "[2] CHAR ,  ";
			text3 += "[3] CHAR ,  ";
			text3 += "[4] CHAR ,  ";
			text3 += "[5] CHAR ,  ";
			text3 += "[6] CHAR ,  ";
			text3 += "[7] CHAR ,  ";
			text3 += "[8] CHAR ,  ";
			text3 += "[9] CHAR ,  ";
			text3 += "[10] CHAR ,  ";
			text3 += "[11] CHAR ,  ";
			text3 += "[12] CHAR ,  ";
			text3 += "[13] CHAR ,  ";
			text3 += "[14] CHAR ,  ";
			text3 += "[15] CHAR ,  ";
			text3 += "[谱图] CHAR  );  ";
			sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text3 = " ";
			string text4 = " ";
			text4 += " CREATE TABLE [MINE] ( ";
			text4 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[采样点] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[爆炸区域] CHAR NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[1] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[2] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[3] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[4] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[5] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[6] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[7] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[8] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[9] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[10] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[11] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[12] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[13] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[14] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[15] FLOAT NOT NULL ON CONFLICT FAIL); ";
			sQLiteCommand = new SQLiteCommand(text4, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			text4 = " ";
			text4 += " CREATE TABLE [History] ( ";
			text4 += "[时间] DATETIME NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[总烃] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[甲烷] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[非甲烷总烃] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[苯] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[甲苯] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[间对二甲苯] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[邻二甲苯] FLOAT NOT NULL ON CONFLICT FAIL,  ";
			text4 += "[苯乙烯] FLOAT NOT NULL ON CONFLICT FAIL);";
			sQLiteCommand = new SQLiteCommand(text4, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoLYThcRLTSqlLite(int ComponentID, int dexcotID, float thcAmount, float ch4Amount, float nmhcAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [LYTHCRLT]([总烃] ,[甲烷] ,[非甲烷总烃]) values( ";
			text2 = text2 + thcAmount.ToString("0.000") + "," + ch4Amount.ToString("0.000") + "," + nmhcAmount.ToString("0.000") + " )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoLYThcRLTSqlLite(int ComponentID, int dexcotID, string strDB, float thcAmount, float ch4Amount, float nmhcAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpolHis.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [" + strDB + "]([总烃] ,[甲烷] ,[非甲烷总烃],[时间]) values( ";
			switch (dexcotID)
			{
			case 1:
				text2 = text2 + thcAmount.ToString("0.000") + "," + ch4Amount.ToString("0.000") + "," + nmhcAmount.ToString("0.000") + ",' ' )";
				break;
			case 2:
				text2 = text2 + thcAmount.ToString("0.000") + "," + ch4Amount.ToString("0.000") + "," + nmhcAmount.ToString("0.000") + ",'" + LYTHCtrl2.selfCtrl.strStartTime + "' )";
				break;
			case 3:
				text2 = text2 + thcAmount.ToString("0.000") + "," + ch4Amount.ToString("0.000") + "," + nmhcAmount.ToString("0.000") + ",'" + LYTHCtrl2.selfCtrl.strStopTime + "' )";
				break;
			}
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoLYBTEXRLTSqlLite(int ComponentID, int dexcotID, float[] fBtexAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [LYBTEXRLT]([苯] ,[甲苯] ,[乙苯],[对二甲苯],[间二甲苯],[异丙苯],[邻二甲苯],[苯乙烯]) values( ";
			text2 = text2 + fBtexAmount[0].ToString("0.000") + "," + fBtexAmount[1].ToString("0.000") + "," + fBtexAmount[2].ToString("0.000") + "," + fBtexAmount[3].ToString("0.000") + "," + fBtexAmount[4].ToString("0.000") + "," + fBtexAmount[5].ToString("0.000") + "," + fBtexAmount[6].ToString("0.000") + "," + fBtexAmount[7].ToString("0.000") + " )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoLYThcSqlLite(int ComponentID, int dexcotID, string samplingSite, string strFileName, float thcAmount, float ch4Amount, float nmhcAmount, float floatRev1, string strRev0, string strRev1)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [LYTHC]([DateTime] ,[dexcotIndex] ,[SamplingSite],[FileName]  ,[ThcAmount] ,[Ch4Amount] ,[NmhcAmount],[floatRev1],[strRev0],[strRev1]) values( ";
			text2 = text2 + "datetime('now','localtime'),0,'" + samplingSite + "','" + strFileName + "',";
			text2 = text2 + thcAmount.ToString("0.000") + "," + ch4Amount.ToString("0.000") + "," + nmhcAmount.ToString("0.000") + "," + floatRev1.ToString("0.000") + ",'" + strRev0 + "','" + strRev1 + "' )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoLYBtexSqlLite(int ComponentID, int dexcotID, string samplingSite, string strFileName, float[] btexAmount, float floatRev1, string strRev0, string strRev1)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [LYBTEX]([DateTime],[dexcotIndex],[SamplingSite],[FileName],[btexAmount1],[btexAmount2],[btexAmount3],[btexAmount4],[btexAmount5],[btexAmount6],[btexAmount7],[btexAmount8],[floatRev1],[strRev0],[strRev1]) values( ";
			text2 = text2 + "datetime('now','localtime'),0,'" + samplingSite + "','" + strFileName + "',";
			text2 = text2 + btexAmount[0].ToString("0.000") + "," + btexAmount[1].ToString("0.000") + "," + btexAmount[2].ToString("0.000") + "," + btexAmount[3].ToString("0.000") + "," + btexAmount[4].ToString("0.000") + "," + btexAmount[5].ToString("0.000") + "," + btexAmount[6].ToString("0.000") + "," + btexAmount[7].ToString("0.000") + "," + floatRev1.ToString("0.000") + ",'" + strRev0 + "','" + strRev1 + "' )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoTableSqlLite(string strType, string strUserName, string strIntrumentName, string strMoudle, string strDesc)
	{
		string text = $"Version {strSoftVersion} ";
		string hostName = Dns.GetHostName();
		string userName = Environment.UserName;
		string text2 = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text2))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text2);
			sQLiteConnection.Open();
			string text3 = " ";
			text3 += " INSERT INTO [OLog] ( [DateTime] ,[Type]  ,[NetChromUserName]  ,[IntrumentName] ,[Moudle]  ,     ";
			text3 += "[Describe] ,[ComputerUsername]  ,[ComputerName]  ,[VerInfo] )   ";
			text3 = text3 + " values( datetime('now','localtime'),'" + strType;
			text3 = text3 + "','" + strUserName + "','" + strIntrumentName;
			text3 = text3 + "','" + strMoudle + "','" + strDesc;
			text3 = text3 + "','" + userName + "','" + hostName + "','" + text;
			text3 += " ') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text3, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public DataTable GetDataTableSqlLite(string string_17)
	{
		try
		{
			DataTable dataTable = new DataTable();
			string text = Application.StartupPath + "\\ngmpol.dll";
			if (File.Exists(text))
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				SQLiteCommand cmd = new SQLiteCommand(string_17, sQLiteConnection);
				SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
				sQLiteDataAdapter.Fill(dataTable);
				sQLiteConnection.Close();
			}
			return dataTable;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public DataTable GetDataTableSqlLite(string string_17, string strDB)
	{
		try
		{
			DataTable dataTable = new DataTable();
			string text = Application.StartupPath + "\\" + strDB;
			if (File.Exists(text))
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				SQLiteCommand cmd = new SQLiteCommand(string_17, sQLiteConnection);
				SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
				sQLiteDataAdapter.Fill(dataTable);
				sQLiteConnection.Close();
			}
			return dataTable;
		}
		catch (Exception)
		{
			return null;
		}
	}

	public bool DeleteDataTableSqlLite(string name)
	{
		try
		{
			DataTable dataTable = new DataTable();
			string text = Application.StartupPath + "\\ngmpol.dll";
			if (File.Exists(text))
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				string commandText = "DROP TABLE '" + name + "'";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
				sQLiteCommand.ExecuteNonQuery();
				sQLiteConnection.Close();
				return true;
			}
			return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public int DeleteDataTableSqlLite(string name, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy-MM-dd HH:mm:ss");
		string text2 = endtTime.ToString("yyyy-MM-dd HH:mm:ss");
		string commandText = "delete from " + name + " where [时间]>='" + text + "'  and [时间]<='" + text2 + "'";
		DataTable dataTable = new DataTable();
		string text3 = Application.StartupPath + "\\ngmpol.dll";
		int result = 0;
		if (File.Exists(text3))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text3);
			sQLiteConnection.Open();
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			result = sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return result;
	}

	public DataTable GetDataTableRLTHistorySqlLite(int ComponentID, string componentName, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy-MM-dd hh:mm:ss");
		string text2 = endtTime.ToString("yyyy-MM-dd hh:mm:ss");
		string text3 = "";
		text3 = ((ComponentID != 1) ? ("select " + componentName + " from " + componentName + " where [时间]>='" + text + "'  and [时间]<='" + text2 + "'") : ("select * from " + componentName + " where [时间]>='" + text + "'  and [时间]<='" + text2 + "'"));
		return GetDataTableSqlLite(text3);
	}

	public DataTable GetDataTableRZHistorySqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd hh:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd hh:ss");
		string string_ = "select * from RZHistory where [时间]>='" + text + "'  and [时间]<='" + text2 + "'";
		return GetDataTableSqlLite(string_);
	}

	public int DeleteDataTableRZHistorySqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd HH:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd HH:ss");
		string commandText = "delete from RZHistory where [时间]>='" + text + "'  and [时间]<='" + text2 + "'";
		DataTable dataTable = new DataTable();
		string text3 = Application.StartupPath + "\\ngmpol.dll";
		int result = 0;
		if (File.Exists(text3))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text3);
			sQLiteConnection.Open();
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			result = sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return result;
	}

	public DataTable GetDataTableVocSqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
		string text2 = endtTime.ToString("yyyy-MM-dd") + " 23:59:59";
		string string_ = "select * from VOC where [ID]=" + ComponentID + " and [DateTime]>='" + text + "'  and [DateTime]<='" + text2 + "'";
		return GetDataTableSqlLite(string_);
	}

	public int DeleteDataTableVocSqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
		string text2 = endtTime.ToString("yyyy-MM-dd") + " 23:59:59";
		string commandText = "delete from VOC where [ID]=" + ComponentID + " and [DateTime]>=#" + text + "#  and [DateTime]<=#" + text2 + "#";
		DataTable dataTable = new DataTable();
		string text3 = Application.StartupPath + "\\ngmpol.dll";
		int result = 0;
		if (File.Exists(text3))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text3);
			sQLiteConnection.Open();
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			result = sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return result;
	}

	public bool InsertIntoVocSqlLite(int ComponentID, int dexcotID, string strComponentName, string strFileName, float fAreaPer, float fArea, float floatRev0, float floatRev1, string strRev0, string strRev1)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [VOC]([Code],[DateTime] ,[ID] ,[dexcotIndex] ,[ComponentName],[FileName]  ,[Area] ,[AreaPer] ,[floatRev0],[floatRev1],[strRev0],[strRev1]) values( '";
			text2 = text2 + NewCode() + "', ";
			text2 = text2 + "strftime('%Y-%m-%d %H:%M:%S','now','localtime')," + ComponentID + "," + dexcotID + ", '";
			text2 = text2 + strComponentName + "','" + strFileName + "'," + fArea + "," + fAreaPer + "," + floatRev0 + "," + floatRev1 + ",'";
			text2 = text2 + strRev0 + "','" + strRev1 + "' )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoVocTableSqlLite(int ComponentID, int dexcotID, string strFileName, string[] zufenAmount)
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string commandText = "";
			switch (ComponentID)
			{
			case 0:
			{
				commandText = " INSERT INTO [vocTable]([时间] ,[总烃] ,[甲烷] ,[非甲烷总烃],[苯],[甲苯],[间对二甲苯],[邻二甲苯],[乙苯],[异丙苯],[苯乙烯],[苯系物],[备用2],[FileName]) values(";
				commandText += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
				for (int k = 0; k < 12; k++)
				{
					commandText = commandText + "'" + zufenAmount[k] + "',";
				}
				commandText = commandText + "'" + strFileName + "') ";
				break;
			}
			case 1:
			{
				commandText = " INSERT INTO [RNBTEX]([时间],[苯],[甲苯],[间对二甲苯],[邻二甲苯],[乙苯],[异丙苯],[苯乙烯],[苯系物],[备用2],[FileName]) values(";
				commandText += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
				for (int j = 3; j < 12; j++)
				{
					commandText = commandText + "'" + zufenAmount[j] + "',";
				}
				commandText = commandText + "'" + strFileName + "') ";
				break;
			}
			case 2:
			{
				commandText = " INSERT INTO [RNNMHC]([时间] ,[总烃] ,[甲烷] ,[非甲烷总烃],[FileName]) values(";
				commandText += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
				for (int i = 0; i < 3; i++)
				{
					commandText = commandText + "'" + zufenAmount[i] + "',";
				}
				commandText = commandText + "'" + strFileName + "') ";
				break;
			}
			}
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoCoalTableSqlLite(int ComponentID, int dexcotID, string[] zufenAmount, string strFileName, string strFileName2, string strFileName3)
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [coalTable]([时间] ,[柱箱] ,[氧化室] ,[一氧化碳],[甲烷],[二氧化碳],[乙烯],[乙烷],[丙烷],[丙烯],[乙炔],[氧气],[氮气],[FID1],[FID2],[TCD]) values(";
			text2 += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
			for (int i = 0; i < 12; i++)
			{
				text2 = text2 + "'" + zufenAmount[i] + "',";
			}
			text2 = text2 + "'" + strFileName + "','" + strFileName2 + "','" + strFileName3 + "') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoRNVocTableSqlLite(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [History]([时间] ,[总烃] ,[甲烷] ,[非甲烷总烃],[苯],[甲苯],[间对二甲苯],[邻二甲苯],[苯乙烯]) values(";
			text2 += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
			for (int i = 0; i < 7; i++)
			{
				text2 = text2 + "'" + zufenAmount[i] + "',";
			}
			text2 = text2 + "'" + zufenAmount[7] + "') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoRNNMHCTableSqlLite(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [RNNMHC]([时间] ,[总烃] ,[甲烷] ,[非甲烷总烃]) values(";
			text2 += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
			for (int i = 0; i < 2; i++)
			{
				text2 = text2 + "'" + zufenAmount[i] + "',";
			}
			text2 = text2 + "'" + zufenAmount[2] + "') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoRNBTEXTableSqlLite(int ComponentID, int dexcotID, string[] zufenAmount)
	{
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			string commandText = "";
			sQLiteConnection.Open();
			switch (ComponentID)
			{
			case 0:
			{
				commandText = " INSERT INTO [RNBTEX]([时间],[苯],[甲苯],[间对二甲苯],[邻二甲苯],[苯乙烯]) values(";
				commandText += "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),";
				for (int i = 3; i < 7; i++)
				{
					commandText = commandText + "'" + zufenAmount[i] + "',";
				}
				commandText = commandText + "'" + zufenAmount[7] + "') ";
				break;
			}
			}
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoHistorySqlLite(int ComponentID, int dexcotID, string strFileName, float fTHC, float fCh4)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [History]([时间] ,[总烃] ,[甲烷]) values( ";
			text2 = text2 + "datetime('now','localtime')," + fTHC + "," + fCh4 + ")";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoRLTHistorySqlLite(int ComponentID, string site, string componentName, float data)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [" + componentName + "]([时间] ,[地点] ,[" + componentName + "]) values( ";
			text2 = text2 + "datetime('now','localtime'),'" + site + "'," + data + ")";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoRZHistorySqlLite(int ComponentID, int dexcotID, string strFileName, float[] amountComponent)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [RZHistory]([时间] ,[热值],[高热值华白数],[低热值华百数],[密度],[相对密度],[临界温度],[临界压力],[组份1浓度],[组份2浓度],[组份3浓度],[组份4浓度],[组份5浓度],[组份6浓度],[组份7浓度],[组份8浓度],[组份9浓度],[组份10浓度]) values( ";
			text2 = text2 + "datetime('now','localtime')," + amountComponent[10] + "," + amountComponent[12] + "," + amountComponent[13] + "," + amountComponent[14] + "," + amountComponent[15] + "," + amountComponent[16] + "," + amountComponent[17] + "," + amountComponent[0] + "," + amountComponent[1] + "," + amountComponent[2] + "," + amountComponent[3] + "," + amountComponent[4] + "," + amountComponent[5] + "," + amountComponent[6] + "," + amountComponent[7] + "," + amountComponent[8] + "," + amountComponent[9] + ")";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public DataTable GetDataTablelythcSqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd hh:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd hh:ss");
		string string_ = "select * from LYTHC where [DateTime]>='" + text + "'  and [DateTime]<='" + text2 + "'";
		return GetDataTableSqlLite(string_);
	}

	public int DeleteDataTableLYThcSqlLite(int ComponentID, DateTime startTime, DateTime endtTime)
	{
		string text = startTime.ToString("yyyy/MM/dd HH:mm:ss");
		string text2 = endtTime.ToString("yyyy/MM/dd HH:mm:ss");
		string commandText = "delete from LYTHC where [DateTime]>='" + text + "'  and [DateTime]<='" + text2 + "'";
		DataTable dataTable = new DataTable();
		string text3 = Application.StartupPath + "\\ngmpol.dll";
		int result = 0;
		if (File.Exists(text3))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text3);
			sQLiteConnection.Open();
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			result = sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return result;
	}

	public int DeleteDataTableLYThcRLTSqlLiteAll()
	{
		string commandText = "delete from LYTHCRLT";
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		int result = 0;
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
			result = sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return result;
	}

	public bool updateIntoMineSqlLite2(int ComponentID, string strName, string fileName1, string[] peakName, string[] zufenAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = "UPDATE coalTable SET";
			for (int i = 0; i < peakName.Length - 1; i++)
			{
				text2 = text2 + "[" + peakName[i] + "] = '" + zufenAmount[i] + "',";
			}
			text2 = text2 + "[" + peakName[peakName.Length - 1] + "] = '" + zufenAmount[peakName.Length - 1] + "'";
			text2 = text2 + "WHERE " + strName + " ='" + fileName1 + "'";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoHISDATASql(int ComponentID, string strTime, string strSite, string method, string interval, string fileName, string[] zufenAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			switch (ComponentID)
			{
			case 1:
			{
				SQLiteConnection sQLiteConnection3 = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection3.Open();
				string text4 = " INSERT INTO [" + strSite + "]([Code] ,[时间] ,[方法名称] ,[通道],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[谱图]) values('";
				text4 = text4 + NewCode() + "','" + strTime + "','" + method + "','" + interval + "',";
				for (int k = 0; k < 20; k++)
				{
					text4 = text4 + "'" + zufenAmount[k] + "',";
				}
				text4 = text4 + "'" + fileName + "') ";
				SQLiteCommand sQLiteCommand3 = new SQLiteCommand(text4, sQLiteConnection3);
				sQLiteCommand3.ExecuteNonQuery();
				sQLiteConnection3.Close();
				break;
			}
			case 2:
			{
				SQLiteConnection sQLiteConnection2 = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection2.Open();
				string text3 = " INSERT INTO [" + strSite + "]([Code] ,[时间] ,[方法名称],[通道],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[谱图]) values('";
				text3 = text3 + NewCode() + "','" + strTime + "','" + method + "','" + interval + "',";
				for (int j = 0; j < 15; j++)
				{
					text3 = text3 + "'" + zufenAmount[j] + "',";
				}
				text3 = text3 + "'" + fileName + "') ";
				SQLiteCommand sQLiteCommand2 = new SQLiteCommand(text3, sQLiteConnection2);
				sQLiteCommand2.ExecuteNonQuery();
				sQLiteConnection2.Close();
				break;
			}
			default:
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				string text2 = " INSERT INTO [" + strSite + "]([Code] ,[时间] ,[方法名称] ,[通道],[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16] ,[17] ,[18],[19],[20],[谱图]) values('";
				text2 = text2 + NewCode() + "','" + strTime + "','" + method + "','" + interval + "',";
				for (int i = 0; i < 20; i++)
				{
					text2 = text2 + "'" + zufenAmount[i] + "',";
				}
				text2 = text2 + "'" + fileName + "') ";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
				sQLiteCommand.ExecuteNonQuery();
				sQLiteConnection.Close();
				break;
			}
			}
		}
		return true;
	}

	public bool InsertIntoPortableSql(int ComponentID, string strTime, string strSite, string fileName, string[] zufenAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpolPortable.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [" + strSite + "]([时间] ,[总烃] ,[甲烷],[非甲烷总烃],[总烃保留时间],[甲烷保留时间],[被测单位],[采样地点],[检测单位],[检测员],[单位],[谱图]) values('";
			text2 = text2 + strTime + "',";
			for (int i = 0; i < zufenAmount.Length; i++)
			{
				text2 = text2 + "'" + zufenAmount[i] + "',";
			}
			text2 = text2 + "'" + fileName + "') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool DeleteDataTableSqlLite(int id, string name)
	{
		try
		{
			DataTable dataTable = new DataTable();
			string text = Application.StartupPath + "\\ngmpolPortable.dll";
			if (File.Exists(text))
			{
				SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
				sQLiteConnection.Open();
				string commandText = "DROP TABLE '" + name + "'";
				SQLiteCommand sQLiteCommand = new SQLiteCommand(commandText, sQLiteConnection);
				sQLiteCommand.ExecuteNonQuery();
				sQLiteConnection.Close();
				return true;
			}
			return false;
		}
		catch (Exception)
		{
			return false;
		}
	}

	public bool InsertIntoMineSqlLite(int ComponentID, int dexcotID, string[] zufenName, string[] zufenAmount, string strJiedian)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [MINE]([时间] ,[采样点] ,[爆炸区域] ,[1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15]) values( ";
			text2 = text2 + "datetime('now','localtime'),'" + strJiedian + "'," + dexcotID + "," + zufenAmount[0] + ", ";
			text2 = text2 + zufenAmount[1] + "," + zufenAmount[2] + "," + zufenAmount[3] + "," + zufenAmount[4] + "," + zufenAmount[5] + "," + zufenAmount[6] + ",";
			text2 = text2 + zufenAmount[7] + "," + zufenAmount[8] + "," + zufenAmount[9] + "," + zufenAmount[10] + "," + zufenAmount[11] + "," + zufenAmount[12] + "," + zufenAmount[13] + "," + zufenAmount[14] + " )";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoMineSqlLite(int ComponentID, string strName, int dexcotID, string[] zufenName, string[] zufenAmount, string strJiedian)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [" + strName + "]([时间] ,[采样点] ,[爆炸区域] ,";
			for (int i = 0; i < ComponentID - 1; i++)
			{
				text2 = text2 + "[" + i + "],";
			}
			text2 = text2 + "[" + (ComponentID - 1) + "]) values(";
			text2 = text2 + "datetime('now','localtime'),'" + strJiedian + "'," + dexcotID + ",";
			for (int j = 0; j < ComponentID - 1; j++)
			{
				text2 = text2 + "'" + zufenAmount[j] + "', ";
			}
			text2 = text2 + "'" + zufenAmount[ComponentID - 1] + " ')";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public bool InsertIntoMineSqlLite(int ComponentID, string strSite, string[] zufenAmount)
	{
		DataTable dataTable = new DataTable();
		string text = Application.StartupPath + "\\ngmpol.dll";
		if (File.Exists(text))
		{
			SQLiteConnection sQLiteConnection = new SQLiteConnection("Data Source=" + text);
			sQLiteConnection.Open();
			string text2 = " INSERT INTO [" + strSite + "]([时间] ,[地点] ,[六氟化硫] ,[氮气],[甲烷],[乙炔],[氧气],[乙烷],[一氧化碳],[乙烯],[二氧化碳],[硫化氢],[丙烷],[异丁烷],[正丁烷],[二氧化硫]) values(";
			text2 = text2 + "strftime('%Y-%m-%d %H:%M:%S','now','localtime'),'" + strSite.Substring(2) + "',";
			for (int i = 0; i < 13; i++)
			{
				text2 = text2 + "'" + zufenAmount[i] + "',";
			}
			text2 = text2 + "'" + zufenAmount[8] + "') ";
			SQLiteCommand sQLiteCommand = new SQLiteCommand(text2, sQLiteConnection);
			sQLiteCommand.ExecuteNonQuery();
			sQLiteConnection.Close();
		}
		return true;
	}

	public DataTable GetDataTableMineSqlLite(int ComponentID, string strName, DateTime startTime, DateTime endtTime)
	{
		if (ComponentID == 1)
		{
			string text = startTime.ToString("yyyy-MM-dd HH:mm:ss");
			string text2 = endtTime.ToString("yyyy-MM-dd HH:mm:ss");
			string string_ = "select * from " + strName + " where[时间]>='" + text + "'  and [时间]<='" + text2 + "'";
			return GetDataTableSqlLite(string_, "ngmpolPortable.dll");
		}
		string text3 = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
		string text4 = endtTime.ToString("yyyy-MM-dd") + " 23:59:59";
		string string_2 = "select * from " + strName + " where[时间]>='" + text3 + "'  and [时间]<='" + text4 + "'";
		return GetDataTableSqlLite(string_2);
	}

	public DataTable GetDataTableRowSqlLite(int ComponentID, string strName, DateTime startTime, string strTime)
	{
		string text = startTime.ToString("yyyy-MM-dd") + " 00:00:00";
		string string_ = "select * from " + strName + " where[时间]=='" + strTime + "'";
		return GetDataTableSqlLite(string_);
	}
}
