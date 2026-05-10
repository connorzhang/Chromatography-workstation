using System;

namespace IBrainChrom2018;

public class AsyncEventArgs : EventArgs
{
	public string _msg;

	public ChannelTCPClientState _state;

	public bool IsHandled { get; set; }

	public AsyncEventArgs(string msg)
	{
		_msg = msg;
		IsHandled = false;
	}

	public AsyncEventArgs(ChannelTCPClientState state)
	{
		_state = state;
		IsHandled = false;
	}

	public AsyncEventArgs(string msg, ChannelTCPClientState state)
	{
		_msg = msg;
		_state = state;
		IsHandled = false;
	}
}
