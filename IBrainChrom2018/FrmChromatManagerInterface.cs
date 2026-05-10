namespace IBrainChrom2018;

public interface FrmChromatManagerInterface
{
	string CurrentGCID { get; }

	TcpServerSocket GetCurrentTcpSocket();
}
