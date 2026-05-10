namespace IBrainChrom2018;

public class CmdItem
{
	public byte byte_0;

	public string express = "";

	public CmdItem(string express, byte byte_1)
	{
		this.express = express;
		byte_0 = byte_1;
	}

	public override string ToString()
	{
		return express;
	}
}
