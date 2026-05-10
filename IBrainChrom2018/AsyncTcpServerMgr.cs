namespace IBrainChrom2018;

public class AsyncTcpServerMgr
{
	public string string_10 = "COM1";

	public string string_11 = "COM2";

	public AsyncTcpServer mainTcpServer;

	public AsyncTcpServer modus0TcpServer;

	public AsyncTcpServer modus1TcpServer;

	public MbSerialPort ModbusComClient;

	public ModBusData modBusData_0 = new ModBusData();

	public ModbusSlave mComModbus = new ModbusSlave();

	public ModbusSlave mComModbus2 = new ModbusSlave();

	public static int iMainPort = 25001;

	public static int modus0Port = 502;

	public static int modus1Port = 503;

    public static int testPort = 5900;

    private static AsyncTcpServerMgr self = null;

	private AsyncTcpServerMgr()
	{
	}

	public static AsyncTcpServerMgr Create()
	{
		if (self == null)
		{
			self = new AsyncTcpServerMgr();
		}
		return self;
	}

	public TcpServerSocket GetTcpServerSocket(string strGCID)
	{
		if (mainTcpServer == null)
		{
			return null;
		}
		return mainTcpServer.GetOneInstrum(strGCID);
	}
}
