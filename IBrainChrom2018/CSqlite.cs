using System;
using System.Data;
using System.Data.SQLite;

namespace IBrainChrom2018;

public class CSqlite
{
	public SQLiteConnection Conn;

	public string ConnString;

	public CSqlite(string Dbpath)
	{
		try
		{
			ConnString = "Data Source= ";
			ConnString += Dbpath;
			Conn = new SQLiteConnection(ConnString);
			Conn.Open();
		}
		catch (Exception ex)
		{
			Program.WriteLine(ex.Message);
		}
	}

	public SQLiteConnection DbConn()
	{
		Conn.Open();
		return Conn;
	}

	public void Close()
	{
		Conn.Close();
	}

	public DataTable SelectToDataTable(string SQL)
	{
		DataTable dataTable = new DataTable();
		try
		{
			SQLiteCommand cmd = new SQLiteCommand(SQL, Conn);
			SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(cmd);
			sQLiteDataAdapter.Fill(dataTable);
		}
		catch (Exception ex)
		{
			Program.WriteLine(ex.Message);
		}
		return dataTable;
	}

	public DataSet SelectToDataSet(string SQL, string subtableName)
	{
		SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();
		SQLiteCommand selectCommand = new SQLiteCommand(SQL, Conn);
		sQLiteDataAdapter.SelectCommand = selectCommand;
		DataSet dataSet = new DataSet();
		dataSet.Tables.Add(subtableName);
		sQLiteDataAdapter.Fill(dataSet, subtableName);
		return dataSet;
	}

	public DataSet SelectToDataSet(string SQL, string subtableName, DataSet DataSetName)
	{
		SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();
		SQLiteCommand selectCommand = new SQLiteCommand(SQL, Conn);
		sQLiteDataAdapter.SelectCommand = selectCommand;
		new DataTable();
		DataSet dataSet = new DataSet();
		sQLiteDataAdapter.Fill(DataSetName, subtableName);
		return DataSetName;
	}

	public SQLiteDataAdapter SelectToOleDbDataAdapter(string SQL)
	{
		SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter();
		SQLiteCommand selectCommand = new SQLiteCommand(SQL, Conn);
		sQLiteDataAdapter.SelectCommand = selectCommand;
		return sQLiteDataAdapter;
	}

	public bool ExecuteSQLNonquery(string SQL)
	{
		SQLiteCommand sQLiteCommand = new SQLiteCommand(SQL, Conn);
		try
		{
			sQLiteCommand.ExecuteNonQuery();
			return true;
		}
		catch (Exception ex)
		{
			Program.WriteLine(ex.Message);
			return false;
		}
	}
}
