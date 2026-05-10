using System.Runtime.InteropServices;
using System.Text;

namespace IBrainChrom2018;

public class SLUSBXpressDLL
{
	public const int SI_CP2101_VERSION = 1;

	public const int SI_CP2102_VERSION = 2;

	public const int SI_CP2103_VERSION = 3;

	public const int SI_DEVICE_IO_FAILED = 8;

	public const int SI_DEVICE_NOT_FOUND = 255;

	public const int SI_FIRMWARE_CONTROLLED = 2;

	public const int SI_FUNCTION_NOT_SUPPORTED = 10;

	public const int SI_GLOBAL_DATA_ERROR = 11;

	public const int SI_GPIO_0 = 1;

	public const int SI_GPIO_1 = 2;

	public const int SI_GPIO_2 = 4;

	public const int SI_GPIO_3 = 8;

	public const int SI_HANDSHAKE_LINE = 1;

	public const int SI_HELD_ACTIVE = 1;

	public const int SI_HELD_INACTIVE = 0;

	public const int SI_INVALID_BAUDRATE = 9;

	public const int SI_INVALID_HANDLE = 1;

	public const int SI_INVALID_PARAMETER = 6;

	public const int SI_INVALID_REQUEST_LENGTH = 7;

	public const int SI_IO_PENDING = 15;

	public const int SI_MAX_DEVICE_STRLEN = 256;

	public const int SI_MAX_READ_SIZE = 4096;

	public const int SI_MAX_WRITE_SIZE = 4096;

	public const int SI_READ_ERROR = 2;

	public const int SI_READ_TIMED_OUT = 13;

	public const int SI_RECEIVE_FLOW_CONTROL = 2;

	public const int SI_RESET_ERROR = 5;

	public const int SI_RETURN_DESCRIPTION = 1;

	public const int SI_RETURN_LINK_NAME = 2;

	public const int SI_RETURN_PID = 4;

	public const int SI_RETURN_SERIAL_NUMBER = 0;

	public const int SI_RETURN_VID = 3;

	public const int SI_RX_EMPTY = 0;

	public const int SI_RX_NO_OVERRUN = 0;

	public const int SI_RX_OVERRUN = 1;

	public const int SI_RX_QUEUE_NOT_READY = 3;

	public const int SI_RX_READY = 2;

	public const int SI_STATUS_INPUT = 0;

	public const int SI_SUCCESS = 0;

	public const int SI_SYSTEM_ERROR_CODE = 12;

	public const int SI_TRANSMIT_ACTIVE_SIGNAL = 3;

	public const int SI_WRITE_ERROR = 4;

	public const int SI_WRITE_TIMED_OUT = 14;

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_CheckRXQueue(uint cyHandle, ref uint lpdwNumBytesInQueue, ref uint lpdwQueueStatus);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_Close(uint cyHandle);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_GetNumDevices(ref int lpdwNumDevices);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_GetProductString(int dwDeviceNum, StringBuilder lpvDeviceString, int dwFlags);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_GetTimeouts(ref int dwReadTimeout, ref int dwWriteTimeout);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_Open(int dwDevice, ref uint cyHandle);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_Read(uint cyHandle, ref byte lpBuffer, int dwBytesToRead, ref int lpdwBytesReturned, int lpOverlapped);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_SetTimeouts(int dwReadTimeout, int dwWriteTimeout);

	[DllImport("SiUSBXp.dll")]
	public static extern int SI_Write(uint cyHandle, ref byte lpBuffer, int dwBytesToWrite, ref int lpdwBytesWritten, int lpOverlapped);
}
