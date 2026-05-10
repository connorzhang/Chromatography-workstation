using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Serialization;

namespace IBrainChrom2018;

[Serializable]
[StructLayout(LayoutKind.Sequential)]
public class AIA
{
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class AttrArr
	{
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class Attr
		{
			public DataArr data = new DataArr();

			public char[] name = new char[0];
		}

		public Attr[] attrs = new Attr[0];

		public void AddAttr(string name)
		{
			Array.Resize(ref attrs, attrs.Length + 1);
			Attr attr = (attrs[attrs.Length - 1] = new Attr());
			attr.name = name.ToCharArray();
			attr.data.nc_type = NC_Type.NC_CHAR;
		}

		public void Clear()
		{
			for (int i = 0; i < attrs.Length; i++)
			{
				Array.Resize(ref attrs[i].name, 0);
				attrs[i].data.Clear();
			}
			Array.Resize(ref attrs, 0);
		}

		public void Read(FileStream fileStream_0, BinaryReader binaryReader_0, byte version)
		{
			if (version != 1)
			{
				throw new Exception("不支持CDF版本：" + version);
			}
			Array.Resize(ref attrs, reverseReadWrite_0.ReadInt(binaryReader_0));
			for (int i = 0; i < attrs.Length; i++)
			{
				if (attrs[i] == null)
				{
					attrs[i] = new Attr();
				}
				attrs[i].name = smethod_1(fileStream_0, binaryReader_0);
				attrs[i].data.nc_type = (NC_Type)reverseReadWrite_0.ReadUInt(binaryReader_0);
				attrs[i].data.ElemsNum = reverseReadWrite_0.ReadInt(binaryReader_0);
				attrs[i].data.Read(fileStream_0, binaryReader_0);
			}
		}

		public void ToStringArr(ref string[] string_0)
		{
			Array.Resize(ref string_0, 0);
			Array.Resize(ref string_0, attrs.Length + 1);
			int num = attrs.Length;
			string_0[0] = "NC_ATTRIBUTE:\t" + num + "个对像";
			for (int i = 0; i < attrs.Length; i++)
			{
				object[] array = new object[11]
				{
					"    ",
					(i + 1).ToString("00"),
					'.',
					new string(attrs[i].name),
					": ",
					attrs[i].data.nc_type.ToString(),
					" (",
					attrs[i].data.ElemsNum.ToString(),
					")[",
					attrs[i].data.ToString(),
					"]"
				};
				string_0[i + 1] = string.Concat(array);
			}
		}

		public void Write(FileStream fileStream_0, BinaryWriter binaryWriter_0, byte version)
		{
			if (version != 1)
			{
				throw new Exception("不支持CDF版本：" + version);
			}
			reverseReadWrite_0.WriteInt(binaryWriter_0, attrs.Length);
			for (int i = 0; i < attrs.Length; i++)
			{
				smethod_3(fileStream_0, binaryWriter_0, attrs[i].name);
				reverseReadWrite_0.WriteUInt(binaryWriter_0, (uint)attrs[i].data.nc_type);
				reverseReadWrite_0.WriteInt(binaryWriter_0, attrs[i].data.ElemsNum);
				attrs[i].data.Write(fileStream_0, binaryWriter_0);
			}
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class DataArr
	{
		public byte[] bytes = new byte[0];

		public char[] chars = new char[0];

		public double[] doubles = new double[0];

		public float[] floats = new float[0];

		public int[] ints = new int[0];

		public NC_Type nc_type = NC_Type.NC_BYTE;

		public short[] shorts = new short[0];

		public int ElemsNum
		{
			get
			{
				return nc_type switch
				{
					NC_Type.NC_BYTE => bytes.Length, 
					NC_Type.NC_CHAR => chars.Length, 
					NC_Type.NC_SHORT => shorts.Length, 
					NC_Type.NC_INT => ints.Length, 
					NC_Type.NC_FLOAT => floats.Length, 
					NC_Type.NC_DOUBLE => doubles.Length, 
					_ => -1, 
				};
			}
			set
			{
				switch (nc_type)
				{
				case NC_Type.NC_BYTE:
					Array.Resize(ref bytes, value);
					break;
				case NC_Type.NC_CHAR:
					Array.Resize(ref chars, value);
					break;
				case NC_Type.NC_SHORT:
					Array.Resize(ref shorts, value);
					break;
				case NC_Type.NC_INT:
					Array.Resize(ref ints, value);
					break;
				case NC_Type.NC_FLOAT:
					Array.Resize(ref floats, value);
					break;
				case NC_Type.NC_DOUBLE:
					Array.Resize(ref doubles, value);
					break;
				}
			}
		}

		public int TypeBytes
		{
			get
			{
				switch (nc_type)
				{
				case NC_Type.NC_BYTE:
				case NC_Type.NC_CHAR:
					return 1;
				case NC_Type.NC_SHORT:
					return 2;
				case NC_Type.NC_INT:
				case NC_Type.NC_FLOAT:
					return 4;
				case NC_Type.NC_DOUBLE:
					return 8;
				default:
					return 0;
				}
			}
		}

		public void Clear()
		{
			Array.Resize(ref bytes, 0);
			Array.Resize(ref chars, 0);
			Array.Resize(ref shorts, 0);
			Array.Resize(ref ints, 0);
			Array.Resize(ref floats, 0);
			Array.Resize(ref doubles, 0);
		}

		public void LoadFromObject(DataArr dataArr)
		{
			nc_type = dataArr.nc_type;
			bytes = (byte[])dataArr.bytes.Clone();
			chars = (char[])dataArr.chars.Clone();
			shorts = (short[])dataArr.shorts.Clone();
			ints = (int[])dataArr.ints.Clone();
			floats = (float[])dataArr.floats.Clone();
			doubles = (double[])dataArr.doubles.Clone();
		}

		public void Read(FileStream fileStream_0, BinaryReader binaryReader_0)
		{
			for (int i = 0; i < ElemsNum; i++)
			{
				switch (nc_type)
				{
				case NC_Type.NC_BYTE:
					bytes[i] = binaryReader_0.ReadByte();
					break;
				case NC_Type.NC_CHAR:
					chars[i] = (char)binaryReader_0.ReadByte();
					break;
				case NC_Type.NC_SHORT:
					shorts[i] = reverseReadWrite_0.ReadShort(binaryReader_0);
					break;
				case NC_Type.NC_INT:
					ints[i] = reverseReadWrite_0.ReadInt(binaryReader_0);
					break;
				case NC_Type.NC_FLOAT:
					floats[i] = reverseReadWrite_0.ReadFloat(binaryReader_0);
					break;
				case NC_Type.NC_DOUBLE:
					doubles[i] = reverseReadWrite_0.ReadDouble(binaryReader_0);
					break;
				}
			}
			if (nc_type == NC_Type.NC_CHAR && chars.Length != 0 && chars[chars.Length - 1] == '\0')
			{
				Array.Resize(ref chars, chars.Length - 1);
			}
			smethod_0(fileStream_0);
		}

		public override string ToString()
		{
			string text = "";
			string text2 = "";
			if (ElemsNum > 100)
			{
				text2 = "0.00";
			}
			if (nc_type != NC_Type.NC_CHAR)
			{
				bool flag = true;
				for (int i = 0; i < ElemsNum; i++)
				{
					string text3;
					if (flag)
					{
						text3 = "";
						flag = false;
					}
					else
					{
						text3 = " ";
					}
					switch (nc_type)
					{
					case NC_Type.NC_BYTE:
						text = text + text3 + bytes[i];
						break;
					case NC_Type.NC_SHORT:
						text = text + text3 + shorts[i];
						break;
					case NC_Type.NC_INT:
						text = text + text3 + ints[i];
						break;
					case NC_Type.NC_FLOAT:
						text = text + text3 + floats[i].ToString(text2);
						break;
					case NC_Type.NC_DOUBLE:
						text = text + text3 + doubles[i].ToString("0.000");
						break;
					}
				}
				return text;
			}
			byte[] array = new byte[2];
			bool flag2 = false;
			for (int j = 0; j < ElemsNum; j++)
			{
				if (chars[j] != 0)
				{
					if (Convert.ToInt32(chars[j]) > 128)
					{
						array[0] = (byte)chars[j++];
						array[1] = (byte)chars[j];
						text += Encoding.Default.GetString(array);
					}
					else
					{
						text += chars[j];
					}
					flag2 = false;
				}
				else
				{
					if (!flag2)
					{
						text += ", ";
					}
					flag2 = true;
				}
			}
			if (flag2 && text.Length != 0)
			{
				text = text.Remove(text.Length - 2, 2);
			}
			return text;
		}

		public void Write(FileStream fileStream_0, BinaryWriter binaryWriter_0)
		{
			for (int i = 0; i < ElemsNum; i++)
			{
				switch (nc_type)
				{
				case NC_Type.NC_BYTE:
					binaryWriter_0.Write(bytes[i]);
					break;
				case NC_Type.NC_CHAR:
					binaryWriter_0.Write(chars[i]);
					break;
				case NC_Type.NC_SHORT:
					reverseReadWrite_0.WriteShort(binaryWriter_0, shorts[i]);
					break;
				case NC_Type.NC_INT:
					reverseReadWrite_0.WriteInt(binaryWriter_0, ints[i]);
					break;
				case NC_Type.NC_FLOAT:
					reverseReadWrite_0.WriteFloat(binaryWriter_0, floats[i]);
					break;
				case NC_Type.NC_DOUBLE:
					reverseReadWrite_0.WriteDouble(binaryWriter_0, doubles[i]);
					break;
				}
			}
			smethod_2(fileStream_0, binaryWriter_0);
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class DimArr
	{
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class Dim
		{
			public uint dimLength;

			public char[] name = new char[0];
		}

		public Dim[] dims = new Dim[0];

		public void AddDim(string name)
		{
			Array.Resize(ref dims, dims.Length + 1);
			(dims[dims.Length - 1] = new Dim()).name = name.ToCharArray();
		}

		public void Clear()
		{
			for (int i = 0; i < dims.Length; i++)
			{
				Array.Resize(ref dims[i].name, 0);
			}
			Array.Resize(ref dims, 0);
		}

		public void Read(FileStream fileStream_0, BinaryReader binaryReader_0, byte version)
		{
			if (version != 1)
			{
				throw new Exception("不支持CDF版本：" + version);
			}
			int num = reverseReadWrite_0.ReadInt(binaryReader_0);
			if (num == 0)
			{
				return;
			}
			Array.Resize(ref dims, num);
			for (int i = 0; i < dims.Length; i++)
			{
				if (dims[i] == null)
				{
					dims[i] = new Dim();
				}
				dims[i].name = smethod_1(fileStream_0, binaryReader_0);
				dims[i].dimLength = reverseReadWrite_0.ReadUInt(binaryReader_0);
			}
		}

		public void ToStringArr(ref string[] string_0)
		{
			Array.Resize(ref string_0, 0);
			Array.Resize(ref string_0, dims.Length + 1);
			object[] array = new object[4]
			{
				"NC_DIMENSION:",
				Convert.ToChar(9),
				dims.Length.ToString(),
				"个对像"
			};
			string_0[0] = string.Concat(array);
			for (int i = 0; i < dims.Length; i++)
			{
				string_0[i + 1] = "    id " + i.ToString("00") + '.' + new string(dims[i].name) + ": " + dims[i].dimLength.ToString();
			}
		}

		public void Write(FileStream fileStream_0, BinaryWriter binaryWriter_0, byte version)
		{
			if (version != 1)
			{
				throw new Exception("不支持CDF版本：" + version);
			}
			reverseReadWrite_0.WriteInt(binaryWriter_0, dims.Length);
			for (int i = 0; i < dims.Length; i++)
			{
				smethod_3(fileStream_0, binaryWriter_0, dims[i].name);
				reverseReadWrite_0.WriteUInt(binaryWriter_0, dims[i].dimLength);
			}
		}
	}

	private enum Enum5
	{
		const_0 = 12,
		const_1 = 10,
		const_2 = 11
	}

	public enum NC_Type
	{
		NC_BYTE = 1,
		NC_CHAR = 2,
		NC_DOUBLE = 6,
		NC_FLOAT = 5,
		NC_INT = 4,
		NC_SHORT = 3
	}

	[Serializable]
	[StructLayout(LayoutKind.Explicit)]
	public struct ReverseReadWrite
	{
		[FieldOffset(0)]
		private byte byte_0;

		[FieldOffset(1)]
		private byte byte_1;

		[FieldOffset(2)]
		private byte byte_2;

		[FieldOffset(3)]
		private byte byte_3;

		[FieldOffset(4)]
		private byte byte_4;

		[FieldOffset(5)]
		private byte byte_5;

		[FieldOffset(6)]
		private byte byte_6;

		[FieldOffset(7)]
		private byte byte_7;

		[FieldOffset(0)]
		private double double_0;

		[FieldOffset(0)]
		private float float_0;

		[FieldOffset(0)]
		private short short_0;

		[FieldOffset(0)]
		private int int_0;

		[FieldOffset(0)]
		private uint uint_0;

		private void method_0(BinaryReader binaryReader_0)
		{
			byte_3 = binaryReader_0.ReadByte();
			byte_2 = binaryReader_0.ReadByte();
			byte_1 = binaryReader_0.ReadByte();
			byte_0 = binaryReader_0.ReadByte();
		}

		public double ReadDouble(BinaryReader binaryReader_0)
		{
			byte_7 = binaryReader_0.ReadByte();
			byte_6 = binaryReader_0.ReadByte();
			byte_5 = binaryReader_0.ReadByte();
			byte_4 = binaryReader_0.ReadByte();
			byte_3 = binaryReader_0.ReadByte();
			byte_2 = binaryReader_0.ReadByte();
			byte_1 = binaryReader_0.ReadByte();
			byte_0 = binaryReader_0.ReadByte();
			return double_0;
		}

		public float ReadFloat(BinaryReader binaryReader_0)
		{
			method_0(binaryReader_0);
			return float_0;
		}

		public int ReadInt(BinaryReader binaryReader_0)
		{
			method_0(binaryReader_0);
			return int_0;
		}

		public short ReadShort(BinaryReader binaryReader_0)
		{
			byte_1 = binaryReader_0.ReadByte();
			byte_0 = binaryReader_0.ReadByte();
			return short_0;
		}

		public uint ReadUInt(BinaryReader binaryReader_0)
		{
			method_0(binaryReader_0);
			return uint_0;
		}

		private void method_1(BinaryWriter binaryWriter_0)
		{
			binaryWriter_0.Write(byte_3);
			binaryWriter_0.Write(byte_2);
			binaryWriter_0.Write(byte_1);
			binaryWriter_0.Write(byte_0);
		}

		public void WriteDouble(BinaryWriter binaryWriter_0, double double64)
		{
			double_0 = double64;
			binaryWriter_0.Write(byte_7);
			binaryWriter_0.Write(byte_6);
			binaryWriter_0.Write(byte_5);
			binaryWriter_0.Write(byte_4);
			binaryWriter_0.Write(byte_3);
			binaryWriter_0.Write(byte_2);
			binaryWriter_0.Write(byte_1);
			binaryWriter_0.Write(byte_0);
		}

		public void WriteFloat(BinaryWriter binaryWriter_0, float float32)
		{
			float_0 = float32;
			method_1(binaryWriter_0);
		}

		public void WriteInt(BinaryWriter binaryWriter_0, int int32)
		{
			int_0 = int32;
			method_1(binaryWriter_0);
		}

		public void WriteShort(BinaryWriter binaryWriter_0, short int16)
		{
			short_0 = int16;
			binaryWriter_0.Write(byte_1);
			binaryWriter_0.Write(byte_0);
		}

		public void WriteUInt(BinaryWriter binaryWriter_0, uint uint32)
		{
			uint_0 = uint32;
			method_1(binaryWriter_0);
		}
	}

	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class VarArr
	{
		[Serializable]
		[StructLayout(LayoutKind.Sequential)]
		public class Var
		{
			public uint begin;

			public DataArr data = new DataArr();

			public DataArr dimIDs = new DataArr();

			public char[] name = new char[0];

			public AttrArr svAttr = new AttrArr();
		}

		public Var[] vars = new Var[0];

		public void AddVar(string name, DimArr dimArr, string[] dimIDs, string[] attrNames)
		{
			Array.Resize(ref vars, vars.Length + 1);
			Var var = (vars[vars.Length - 1] = new Var());
			var.name = name.ToCharArray();
			if (dimArr != null && dimIDs != null)
			{
				var.dimIDs.nc_type = NC_Type.NC_INT;
				int num = 0;
				while (num < dimIDs.Length)
				{
					for (int i = 0; i < dimArr.dims.Length; i++)
					{
						if (dimIDs[num].Equals(dimArr.dims[i].name.ToString()))
						{
							int num2 = var.dimIDs.ints.Length;
							Array.Resize(ref var.dimIDs.ints, num2 + 1);
							var.dimIDs.ints[num2] = i;
							num++;
							break;
						}
					}
				}
			}
			if (attrNames == null)
			{
				return;
			}
			for (int j = 0; j < attrNames.Length; j++)
			{
				if (attrNames[j] != null && attrNames[j] != "")
				{
					var.svAttr.AddAttr(attrNames[j]);
				}
			}
		}

		public void Clear()
		{
			for (int i = 0; i < vars.Length; i++)
			{
				Array.Resize(ref vars[i].name, 0);
				vars[i].dimIDs.Clear();
				vars[i].svAttr.Clear();
				vars[i].data.Clear();
			}
			Array.Resize(ref vars, 0);
		}

		public void Read(FileStream fileStream_0, BinaryReader binaryReader_0, byte version)
		{
			if (version != 1)
			{
				throw new Exception("不支持CDF版本：" + version);
			}
			Array.Resize(ref vars, reverseReadWrite_0.ReadInt(binaryReader_0));
			for (int i = 0; i < vars.Length; i++)
			{
				if (vars[i] == null)
				{
					vars[i] = new Var();
				}
				vars[i].name = smethod_1(fileStream_0, binaryReader_0);
				vars[i].dimIDs.nc_type = NC_Type.NC_INT;
				vars[i].dimIDs.ElemsNum = reverseReadWrite_0.ReadInt(binaryReader_0);
				vars[i].dimIDs.Read(fileStream_0, binaryReader_0);
				switch (reverseReadWrite_0.ReadUInt(binaryReader_0))
				{
				case 0u:
					reverseReadWrite_0.ReadUInt(binaryReader_0);
					break;
				default:
					throw new Exception("解释错误!");
				case 12u:
					vars[i].svAttr.Read(fileStream_0, binaryReader_0, version);
					break;
				}
				vars[i].data.nc_type = (NC_Type)reverseReadWrite_0.ReadUInt(binaryReader_0);
				vars[i].data.ElemsNum = reverseReadWrite_0.ReadInt(binaryReader_0) / vars[i].data.TypeBytes;
				vars[i].begin = reverseReadWrite_0.ReadUInt(binaryReader_0);
				long position = fileStream_0.Position;
				fileStream_0.Position = vars[i].begin;
				vars[i].data.Read(fileStream_0, binaryReader_0);
				fileStream_0.Position = position;
			}
		}

		public void ToStringArr(ref string[] string_0)
		{
			Array.Resize(ref string_0, 0);
			int num = 4;
			for (int i = 0; i < vars.Length; i++)
			{
				num += 3;
				num += 1 + vars[i].svAttr.attrs.Length;
			}
			Array.Resize(ref string_0, num);
			int num2 = vars.Length;
			string_0[0] = "NC_VARIABLE:  " + num2 + "个对像";
			num = 1;
			int num3 = -1;
			for (int j = 0; j < vars.Length; j++)
			{
				string text = new string(vars[j].name);
				string text2 = "    " + (j + 1).ToString("00") + '.' + text;
				if (text != "ordinate_values")
				{
					text2 = text2 + ": " + vars[j].data.ToString();
				}
				else
				{
					num3 = j;
				}
				string_0[num++] = text2;
				string_0[num++] = "       dimID (" + vars[j].dimIDs.ElemsNum.ToString() + ")[" + vars[j].dimIDs.ToString() + ']';
				num2 = vars[j].svAttr.attrs.Length;
				string_0[num++] = "       var-specific attr: " + num2;
				string[] string_1 = new string[0];
				vars[j].svAttr.ToStringArr(ref string_1);
				for (int k = 0; k < vars[j].svAttr.attrs.Length; k++)
				{
					string_0[num++] = "   " + string_1[k + 1];
				}
				object[] array = new object[9]
				{
					"       ",
					vars[j].data.nc_type.ToString(),
					'(',
					vars[j].data.ElemsNum.ToString(),
					')',
					"   bytes: ",
					(vars[j].data.TypeBytes * vars[j].data.ElemsNum).ToString(),
					"   begin: ",
					vars[j].begin.ToString("X4")
				};
				string_0[num++] = string.Concat(array);
			}
			if (num3 != -1)
			{
				string_0[num++] = "";
				string_0[num++] = "ordinate_values:";
				string_0[num++] = vars[num3].data.ToString();
			}
		}

		public void Write(FileStream fileStream_0, BinaryWriter binaryWriter_0, byte version)
		{
			method_0(fileStream_0, binaryWriter_0, version);
			method_1(fileStream_0, binaryWriter_0, version);
		}

		private void method_0(FileStream fileStream_0, BinaryWriter binaryWriter_0, byte byte_0)
		{
			if (byte_0 != 1)
			{
				throw new Exception("不支持CDF版本：" + byte_0);
			}
			reverseReadWrite_0.WriteInt(binaryWriter_0, vars.Length);
			for (int i = 0; i < vars.Length; i++)
			{
				smethod_3(fileStream_0, binaryWriter_0, vars[i].name);
				reverseReadWrite_0.WriteInt(binaryWriter_0, vars[i].dimIDs.ElemsNum);
				vars[i].dimIDs.Write(fileStream_0, binaryWriter_0);
				if (vars[i].svAttr.attrs.Length == 0)
				{
					reverseReadWrite_0.WriteUInt(binaryWriter_0, 0u);
					reverseReadWrite_0.WriteUInt(binaryWriter_0, 0u);
				}
				else
				{
					reverseReadWrite_0.WriteUInt(binaryWriter_0, 12u);
					vars[i].svAttr.Write(fileStream_0, binaryWriter_0, byte_0);
				}
				reverseReadWrite_0.WriteUInt(binaryWriter_0, (uint)vars[i].data.nc_type);
				reverseReadWrite_0.WriteInt(binaryWriter_0, vars[i].data.ElemsNum * vars[i].data.TypeBytes);
				vars[i].begin = (uint)fileStream_0.Position;
				reverseReadWrite_0.WriteUInt(binaryWriter_0, 0u);
			}
		}

		private void method_1(FileStream fileStream_0, BinaryWriter binaryWriter_0, byte byte_0)
		{
			if (byte_0 != 1)
			{
				throw new Exception("不支持CDF版本：" + byte_0);
			}
			for (int i = 0; i < vars.Length; i++)
			{
				long position = fileStream_0.Position;
				fileStream_0.Position = vars[i].begin;
				reverseReadWrite_0.WriteUInt(binaryWriter_0, (uint)position);
				fileStream_0.Position = position;
				vars[i].data.Write(fileStream_0, binaryWriter_0);
			}
		}
	}

	private const string string_0 = "CDF";

	private const uint uint_0 = 0u;

	protected DimArr dimArr = new DimArr();

	protected AttrArr gAttrArr = new AttrArr();

	private uint uint_1;

	[NonSerialized]
	[XmlIgnore]
	private static ReverseReadWrite reverseReadWrite_0 = default(ReverseReadWrite);

	public string[] ssDim = new string[0];

	public string[] ssGAttr = new string[0];

	public string[] ssVar = new string[0];

	public string strHeader = "";

	public string strNumRec = "";

	protected VarArr varArr = new VarArr();

	public static byte version = 1;

	public void Read(FileStream fileStream_0, BinaryReader binaryReader_0)
	{
		char[] array = binaryReader_0.ReadChars(3);
		string text = array.ToString().ToUpper();
		text = new string(array);
		if (!text.Equals("CDF"))
		{
			throw new Exception("文件类型不匹配[要求CDF]：" + array.ToString());
		}
		version = binaryReader_0.ReadByte();
		if (version != 1)
		{
			throw new Exception("不支持CDF版本：" + version);
		}
		uint_1 = reverseReadWrite_0.ReadUInt(binaryReader_0);
		if (reverseReadWrite_0.ReadUInt(binaryReader_0) == 10)
		{
			dimArr.Read(fileStream_0, binaryReader_0, version);
		}
		if (reverseReadWrite_0.ReadUInt(binaryReader_0) == 12)
		{
			gAttrArr.Read(fileStream_0, binaryReader_0, version);
		}
		if (reverseReadWrite_0.ReadUInt(binaryReader_0) == 11)
		{
			varArr.Read(fileStream_0, binaryReader_0, version);
		}
	}

	private static void smethod_0(FileStream fileStream_0)
	{
		long num = fileStream_0.Position % 4;
		if (num != 0)
		{
			fileStream_0.Position += 4 - num;
		}
	}

	private static char[] smethod_1(FileStream fileStream_0, BinaryReader binaryReader_0)
	{
		int num = reverseReadWrite_0.ReadInt(binaryReader_0);
		char[] array = new char[num];
		byte[] array2 = binaryReader_0.ReadBytes(num);
		for (int i = 0; i < num; i++)
		{
			array[i] = (char)array2[i];
		}
		smethod_0(fileStream_0);
		return array;
	}

	public void ToStringArr()
	{
		strHeader = "CDF " + version;
		strNumRec = uint_1.ToString();
		dimArr.ToStringArr(ref ssDim);
		gAttrArr.ToStringArr(ref ssGAttr);
		varArr.ToStringArr(ref ssVar);
	}

	public void Write(FileStream fileStream_0, BinaryWriter binaryWriter_0)
	{
		binaryWriter_0.Write("CDF".ToCharArray());
		binaryWriter_0.Write(version);
		if (version != 1)
		{
			throw new Exception("不支持CDF版本：" + version);
		}
		reverseReadWrite_0.WriteUInt(binaryWriter_0, uint_1);
		reverseReadWrite_0.WriteUInt(binaryWriter_0, 10u);
		dimArr.Write(fileStream_0, binaryWriter_0, version);
		reverseReadWrite_0.WriteUInt(binaryWriter_0, 12u);
		gAttrArr.Write(fileStream_0, binaryWriter_0, version);
		reverseReadWrite_0.WriteUInt(binaryWriter_0, 11u);
		varArr.Write(fileStream_0, binaryWriter_0, version);
	}

	private static void smethod_2(FileStream fileStream_0, BinaryWriter binaryWriter_0)
	{
		long num = fileStream_0.Position % 4;
		if (num != 0)
		{
			for (int i = 0; i < 4 - num; i++)
			{
				binaryWriter_0.Write(0);
			}
		}
	}

	private static void smethod_3(FileStream fileStream_0, BinaryWriter binaryWriter_0, char[] char_0)
	{
		reverseReadWrite_0.WriteInt(binaryWriter_0, char_0.Length);
		binaryWriter_0.Write(char_0);
		smethod_2(fileStream_0, binaryWriter_0);
	}
}
