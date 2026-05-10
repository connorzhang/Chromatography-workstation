using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace IBrainChrom2018.Report;

[Serializable]
[DesignerCategory("code")]
[ToolboxItem(true)]
[XmlSchemaProvider("GetTypedDataSetSchema")]
[XmlRoot("instockList")]
[HelpKeyword("vs.data.DataSet")]
public class instockList : DataSet
{
	private class sp_GetPoOrderDataTable
	{
	}

	private class storgeOrderListDataTable
	{
	}

	[Serializable]
	[XmlSchemaProvider("GetTypedTableSchema")]
	public class zufenTableDataTable : TypedTableBase<zufenTableRow>
	{
		private DataColumn columnindex;

		private DataColumn columnPeakMaxTime;

		private DataColumn columnPeakName;

		private DataColumn columnPeakPara;

		private DataColumn columnPeakAmont;

		private DataColumn columnPeakAmontPer;

		private DataColumn columnPeakArea;

		private DataColumn columnPeakAreaPer;

		private DataColumn columnPeakheight;

		private DataColumn columnPeakheightPer;

		private DataColumn columnPeakHalfheight;

		private DataColumn columnPeakV;

		private DataColumn columnPeakOtherPara;

		private DataColumn columnPeakLV;

		private DataColumn columnPeakTBPara;

		private DataColumn columnPeakUTBPara;

		private DataColumn columnPeakLPara;

		private DataColumn columnPeaktailPara;

		private DataColumn columnPeakFx;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn indexColumn => columnindex;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakMaxTimeColumn => columnPeakMaxTime;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakNameColumn => columnPeakName;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakParaColumn => columnPeakPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakAmontColumn => columnPeakAmont;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakAmontPerColumn => columnPeakAmontPer;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakAreaColumn => columnPeakArea;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakAreaPerColumn => columnPeakAreaPer;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakheightColumn => columnPeakheight;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakheightPerColumn => columnPeakheightPer;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakHalfheightColumn => columnPeakHalfheight;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakVColumn => columnPeakV;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakOtherParaColumn => columnPeakOtherPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakLVColumn => columnPeakLV;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakTBParaColumn => columnPeakTBPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakUTBParaColumn => columnPeakUTBPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakLParaColumn => columnPeakLPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeaktailParaColumn => columnPeaktailPara;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn PeakFxColumn => columnPeakFx;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public int Count => base.Rows.Count;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRow this[int index] => (zufenTableRow)base.Rows[index];

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event zufenTableRowChangeEventHandler zufenTableRowChanging;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event zufenTableRowChangeEventHandler zufenTableRowChanged;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event zufenTableRowChangeEventHandler zufenTableRowDeleting;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event zufenTableRowChangeEventHandler zufenTableRowDeleted;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableDataTable()
		{
			base.TableName = "zufenTable";
			BeginInit();
			InitClass();
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal zufenTableDataTable(DataTable table)
		{
			base.TableName = table.TableName;
			if (table.CaseSensitive != table.DataSet.CaseSensitive)
			{
				base.CaseSensitive = table.CaseSensitive;
			}
			if (table.Locale.ToString() != table.DataSet.Locale.ToString())
			{
				base.Locale = table.Locale;
			}
			if (table.Namespace != table.DataSet.Namespace)
			{
				base.Namespace = table.Namespace;
			}
			base.Prefix = table.Prefix;
			base.MinimumCapacity = table.MinimumCapacity;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected zufenTableDataTable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			InitVars();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void AddzufenTableRow(zufenTableRow row)
		{
			base.Rows.Add(row);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRow AddzufenTableRow(int index, decimal PeakMaxTime, string PeakName, decimal PeakPara, decimal PeakAmont, decimal PeakAmontPer, decimal PeakArea, decimal PeakAreaPer, decimal Peakheight, decimal PeakheightPer, decimal PeakHalfheight, decimal PeakV, decimal PeakOtherPara, decimal PeakLV, decimal PeakTBPara, decimal PeakUTBPara, decimal PeakLPara, decimal PeaktailPara, string PeakFx)
		{
			zufenTableRow zufenTableRow = (zufenTableRow)NewRow();
			object[] itemArray = new object[19]
			{
				index, PeakMaxTime, PeakName, PeakPara, PeakAmont, PeakAmontPer, PeakArea, PeakAreaPer, Peakheight, PeakheightPer,
				PeakHalfheight, PeakV, PeakOtherPara, PeakLV, PeakTBPara, PeakUTBPara, PeakLPara, PeaktailPara, PeakFx
			};
			zufenTableRow.ItemArray = itemArray;
			base.Rows.Add(zufenTableRow);
			return zufenTableRow;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRow FindByindex(int index)
		{
			return (zufenTableRow)base.Rows.Find(new object[1] { index });
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataTable Clone()
		{
			zufenTableDataTable zufenTableDataTable = (zufenTableDataTable)base.Clone();
			zufenTableDataTable.InitVars();
			return zufenTableDataTable;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataTable CreateInstance()
		{
			return new zufenTableDataTable();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			columnindex = base.Columns["index"];
			columnPeakMaxTime = base.Columns["PeakMaxTime"];
			columnPeakName = base.Columns["PeakName"];
			columnPeakPara = base.Columns["PeakPara"];
			columnPeakAmont = base.Columns["PeakAmont"];
			columnPeakAmontPer = base.Columns["PeakAmontPer"];
			columnPeakArea = base.Columns["PeakArea"];
			columnPeakAreaPer = base.Columns["PeakAreaPer"];
			columnPeakheight = base.Columns["Peakheight"];
			columnPeakheightPer = base.Columns["PeakheightPer"];
			columnPeakHalfheight = base.Columns["PeakHalfheight"];
			columnPeakV = base.Columns["PeakV"];
			columnPeakOtherPara = base.Columns["PeakOtherPara"];
			columnPeakLV = base.Columns["PeakLV"];
			columnPeakTBPara = base.Columns["PeakTBPara"];
			columnPeakUTBPara = base.Columns["PeakUTBPara"];
			columnPeakLPara = base.Columns["PeakLPara"];
			columnPeaktailPara = base.Columns["PeaktailPara"];
			columnPeakFx = base.Columns["PeakFx"];
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			columnindex = new DataColumn("index", typeof(int), null, MappingType.Element);
			base.Columns.Add(columnindex);
			columnPeakMaxTime = new DataColumn("PeakMaxTime", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakMaxTime);
			columnPeakName = new DataColumn("PeakName", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnPeakName);
			columnPeakPara = new DataColumn("PeakPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakPara);
			columnPeakAmont = new DataColumn("PeakAmont", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakAmont);
			columnPeakAmontPer = new DataColumn("PeakAmontPer", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakAmontPer);
			columnPeakArea = new DataColumn("PeakArea", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakArea);
			columnPeakAreaPer = new DataColumn("PeakAreaPer", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakAreaPer);
			columnPeakheight = new DataColumn("Peakheight", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakheight);
			columnPeakheightPer = new DataColumn("PeakheightPer", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakheightPer);
			columnPeakHalfheight = new DataColumn("PeakHalfheight", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakHalfheight);
			columnPeakV = new DataColumn("PeakV", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakV);
			columnPeakOtherPara = new DataColumn("PeakOtherPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakOtherPara);
			columnPeakLV = new DataColumn("PeakLV", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakLV);
			columnPeakTBPara = new DataColumn("PeakTBPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakTBPara);
			columnPeakUTBPara = new DataColumn("PeakUTBPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakUTBPara);
			columnPeakLPara = new DataColumn("PeakLPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeakLPara);
			columnPeaktailPara = new DataColumn("PeaktailPara", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnPeaktailPara);
			columnPeakFx = new DataColumn("PeakFx", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnPeakFx);
			base.Constraints.Add(new UniqueConstraint("Constraint1", new DataColumn[1] { columnindex }, isPrimaryKey: true));
			columnindex.AllowDBNull = false;
			columnindex.Unique = true;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRow NewzufenTableRow()
		{
			return (zufenTableRow)NewRow();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new zufenTableRow(builder);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override Type GetRowType()
		{
			return typeof(zufenTableRow);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);
			if (this.zufenTableRowChanged != null)
			{
				this.zufenTableRowChanged(this, new zufenTableRowChangeEvent((zufenTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanging(DataRowChangeEventArgs e)
		{
			base.OnRowChanging(e);
			if (this.zufenTableRowChanging != null)
			{
				this.zufenTableRowChanging(this, new zufenTableRowChangeEvent((zufenTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			if (this.zufenTableRowDeleted != null)
			{
				this.zufenTableRowDeleted(this, new zufenTableRowChangeEvent((zufenTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleting(DataRowChangeEventArgs e)
		{
			base.OnRowDeleting(e);
			if (this.zufenTableRowDeleting != null)
			{
				this.zufenTableRowDeleting(this, new zufenTableRowChangeEvent((zufenTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void RemovezufenTableRow(zufenTableRow row)
		{
			base.Rows.Remove(row);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			instockList instockList2 = new instockList();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
			xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
			xmlSchemaAny2.MinOccurs = 1m;
			xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny2);
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "namespace";
			xmlSchemaAttribute.FixedValue = instockList2.Namespace;
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "tableTypeName";
			xmlSchemaAttribute2.FixedValue = "zufenTableDataTable";
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = instockList2.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					XmlSchema xmlSchema = null;
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					memoryStream?.Close();
					memoryStream2?.Close();
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}
	}

	[Serializable]
	[XmlSchemaProvider("GetTypedTableSchema")]
	public class cmpdTableDataTable : TypedTableBase<cmpdTableRow>
	{
		private DataColumn columnresponse;

		private DataColumn columnamount;

		private DataColumn columnrespFactor;

		private DataColumn columnpicture;

		private DataColumn columnEquationV;

		private DataColumn columncorrFactor;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn responseColumn => columnresponse;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn amountColumn => columnamount;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn respFactorColumn => columnrespFactor;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn pictureColumn => columnpicture;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn EquationVColumn => columnEquationV;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataColumn corrFactorColumn => columncorrFactor;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		[Browsable(false)]
		public int Count => base.Rows.Count;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableRow this[int index] => (cmpdTableRow)base.Rows[index];

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event cmpdTableRowChangeEventHandler cmpdTableRowChanging;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event cmpdTableRowChangeEventHandler cmpdTableRowChanged;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event cmpdTableRowChangeEventHandler cmpdTableRowDeleting;

		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public event cmpdTableRowChangeEventHandler cmpdTableRowDeleted;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableDataTable()
		{
			base.TableName = "cmpdTable";
			BeginInit();
			InitClass();
			EndInit();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal cmpdTableDataTable(DataTable table)
		{
			base.TableName = table.TableName;
			if (table.CaseSensitive != table.DataSet.CaseSensitive)
			{
				base.CaseSensitive = table.CaseSensitive;
			}
			if (table.Locale.ToString() != table.DataSet.Locale.ToString())
			{
				base.Locale = table.Locale;
			}
			if (table.Namespace != table.DataSet.Namespace)
			{
				base.Namespace = table.Namespace;
			}
			base.Prefix = table.Prefix;
			base.MinimumCapacity = table.MinimumCapacity;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected cmpdTableDataTable(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			InitVars();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void AddcmpdTableRow(cmpdTableRow row)
		{
			base.Rows.Add(row);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableRow AddcmpdTableRow(decimal response, decimal amount, decimal respFactor, byte[] picture, string EquationV, decimal corrFactor)
		{
			cmpdTableRow cmpdTableRow = (cmpdTableRow)NewRow();
			object[] itemArray = new object[6] { response, amount, respFactor, picture, EquationV, corrFactor };
			cmpdTableRow.ItemArray = itemArray;
			base.Rows.Add(cmpdTableRow);
			return cmpdTableRow;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public override DataTable Clone()
		{
			cmpdTableDataTable cmpdTableDataTable = (cmpdTableDataTable)base.Clone();
			cmpdTableDataTable.InitVars();
			return cmpdTableDataTable;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataTable CreateInstance()
		{
			return new cmpdTableDataTable();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal void InitVars()
		{
			columnresponse = base.Columns["response"];
			columnamount = base.Columns["amount"];
			columnrespFactor = base.Columns["respFactor"];
			columnpicture = base.Columns["picture"];
			columnEquationV = base.Columns["EquationV"];
			columncorrFactor = base.Columns["corrFactor"];
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		private void InitClass()
		{
			columnresponse = new DataColumn("response", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnresponse);
			columnamount = new DataColumn("amount", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnamount);
			columnrespFactor = new DataColumn("respFactor", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columnrespFactor);
			columnpicture = new DataColumn("picture", typeof(byte[]), null, MappingType.Element);
			base.Columns.Add(columnpicture);
			columnEquationV = new DataColumn("EquationV", typeof(string), null, MappingType.Element);
			base.Columns.Add(columnEquationV);
			columncorrFactor = new DataColumn("corrFactor", typeof(decimal), null, MappingType.Element);
			base.Columns.Add(columncorrFactor);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableRow NewcmpdTableRow()
		{
			return (cmpdTableRow)NewRow();
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override DataRow NewRowFromBuilder(DataRowBuilder builder)
		{
			return new cmpdTableRow(builder);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override Type GetRowType()
		{
			return typeof(cmpdTableRow);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanged(DataRowChangeEventArgs e)
		{
			base.OnRowChanged(e);
			if (this.cmpdTableRowChanged != null)
			{
				this.cmpdTableRowChanged(this, new cmpdTableRowChangeEvent((cmpdTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowChanging(DataRowChangeEventArgs e)
		{
			base.OnRowChanging(e);
			if (this.cmpdTableRowChanging != null)
			{
				this.cmpdTableRowChanging(this, new cmpdTableRowChangeEvent((cmpdTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleted(DataRowChangeEventArgs e)
		{
			base.OnRowDeleted(e);
			if (this.cmpdTableRowDeleted != null)
			{
				this.cmpdTableRowDeleted(this, new cmpdTableRowChangeEvent((cmpdTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		protected override void OnRowDeleting(DataRowChangeEventArgs e)
		{
			base.OnRowDeleting(e);
			if (this.cmpdTableRowDeleting != null)
			{
				this.cmpdTableRowDeleting(this, new cmpdTableRowChangeEvent((cmpdTableRow)e.Row, e.Action));
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void RemovecmpdTableRow(cmpdTableRow row)
		{
			base.Rows.Remove(row);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public static XmlSchemaComplexType GetTypedTableSchema(XmlSchemaSet xs)
		{
			XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
			XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
			instockList instockList2 = new instockList();
			XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
			xmlSchemaAny.Namespace = "http://www.w3.org/2001/XMLSchema";
			xmlSchemaAny.MinOccurs = 0m;
			xmlSchemaAny.MaxOccurs = decimal.MaxValue;
			xmlSchemaAny.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny);
			XmlSchemaAny xmlSchemaAny2 = new XmlSchemaAny();
			xmlSchemaAny2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
			xmlSchemaAny2.MinOccurs = 1m;
			xmlSchemaAny2.ProcessContents = XmlSchemaContentProcessing.Lax;
			xmlSchemaSequence.Items.Add(xmlSchemaAny2);
			XmlSchemaAttribute xmlSchemaAttribute = new XmlSchemaAttribute();
			xmlSchemaAttribute.Name = "namespace";
			xmlSchemaAttribute.FixedValue = instockList2.Namespace;
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute);
			XmlSchemaAttribute xmlSchemaAttribute2 = new XmlSchemaAttribute();
			xmlSchemaAttribute2.Name = "tableTypeName";
			xmlSchemaAttribute2.FixedValue = "cmpdTableDataTable";
			xmlSchemaComplexType.Attributes.Add(xmlSchemaAttribute2);
			xmlSchemaComplexType.Particle = xmlSchemaSequence;
			XmlSchema schemaSerializable = instockList2.GetSchemaSerializable();
			if (xs.Contains(schemaSerializable.TargetNamespace))
			{
				MemoryStream memoryStream = new MemoryStream();
				MemoryStream memoryStream2 = new MemoryStream();
				try
				{
					XmlSchema xmlSchema = null;
					schemaSerializable.Write(memoryStream);
					IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
					while (enumerator.MoveNext())
					{
						xmlSchema = (XmlSchema)enumerator.Current;
						memoryStream2.SetLength(0L);
						xmlSchema.Write(memoryStream2);
						if (memoryStream.Length == memoryStream2.Length)
						{
							memoryStream.Position = 0L;
							memoryStream2.Position = 0L;
							while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
							{
							}
							if (memoryStream.Position == memoryStream.Length)
							{
								return xmlSchemaComplexType;
							}
						}
					}
				}
				finally
				{
					memoryStream?.Close();
					memoryStream2?.Close();
				}
			}
			xs.Add(schemaSerializable);
			return xmlSchemaComplexType;
		}
	}

	private class spreTableDataTable
	{
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public delegate void zufenTableRowChangeEventHandler(object sender, zufenTableRowChangeEvent e);

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public delegate void cmpdTableRowChangeEventHandler(object sender, cmpdTableRowChangeEvent e);

	public class zufenTableRow : DataRow
	{
		private zufenTableDataTable tablezufenTable;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public int index
		{
			get
			{
				return (int)base[tablezufenTable.indexColumn];
			}
			set
			{
				base[tablezufenTable.indexColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakMaxTime
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakMaxTimeColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakMaxTime”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakMaxTimeColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string PeakName
		{
			get
			{
				try
				{
					return (string)base[tablezufenTable.PeakNameColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakName”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakNameColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakAmont
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakAmontColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakAmont”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakAmontColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakAmontPer
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakAmontPerColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakAmontPer”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakAmontPerColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakArea
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakAreaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakArea”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakAreaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakAreaPer
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakAreaPerColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakAreaPer”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakAreaPerColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal Peakheight
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakheightColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“Peakheight”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakheightColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakheightPer
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakheightPerColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakheightPer”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakheightPerColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakHalfheight
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakHalfheightColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakHalfheight”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakHalfheightColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakV
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakVColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakV”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakVColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakOtherPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakOtherParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakOtherPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakOtherParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakLV
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakLVColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakLV”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakLVColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakTBPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakTBParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakTBPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakTBParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakUTBPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakUTBParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakUTBPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakUTBParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeakLPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeakLParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakLPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakLParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal PeaktailPara
		{
			get
			{
				try
				{
					return (decimal)base[tablezufenTable.PeaktailParaColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeaktailPara”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeaktailParaColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string PeakFx
		{
			get
			{
				try
				{
					return (string)base[tablezufenTable.PeakFxColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“zufenTable”中列“PeakFx”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablezufenTable.PeakFxColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal zufenTableRow(DataRowBuilder rb)
			: base(rb)
		{
			tablezufenTable = (zufenTableDataTable)base.Table;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakMaxTimeNull()
		{
			return IsNull(tablezufenTable.PeakMaxTimeColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakMaxTimeNull()
		{
			base[tablezufenTable.PeakMaxTimeColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakNameNull()
		{
			return IsNull(tablezufenTable.PeakNameColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakNameNull()
		{
			base[tablezufenTable.PeakNameColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakParaNull()
		{
			return IsNull(tablezufenTable.PeakParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakParaNull()
		{
			base[tablezufenTable.PeakParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakAmontNull()
		{
			return IsNull(tablezufenTable.PeakAmontColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakAmontNull()
		{
			base[tablezufenTable.PeakAmontColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakAmontPerNull()
		{
			return IsNull(tablezufenTable.PeakAmontPerColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakAmontPerNull()
		{
			base[tablezufenTable.PeakAmontPerColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakAreaNull()
		{
			return IsNull(tablezufenTable.PeakAreaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakAreaNull()
		{
			base[tablezufenTable.PeakAreaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakAreaPerNull()
		{
			return IsNull(tablezufenTable.PeakAreaPerColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakAreaPerNull()
		{
			base[tablezufenTable.PeakAreaPerColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakheightNull()
		{
			return IsNull(tablezufenTable.PeakheightColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakheightNull()
		{
			base[tablezufenTable.PeakheightColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakheightPerNull()
		{
			return IsNull(tablezufenTable.PeakheightPerColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakheightPerNull()
		{
			base[tablezufenTable.PeakheightPerColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakHalfheightNull()
		{
			return IsNull(tablezufenTable.PeakHalfheightColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakHalfheightNull()
		{
			base[tablezufenTable.PeakHalfheightColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakVNull()
		{
			return IsNull(tablezufenTable.PeakVColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakVNull()
		{
			base[tablezufenTable.PeakVColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakOtherParaNull()
		{
			return IsNull(tablezufenTable.PeakOtherParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakOtherParaNull()
		{
			base[tablezufenTable.PeakOtherParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakLVNull()
		{
			return IsNull(tablezufenTable.PeakLVColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakLVNull()
		{
			base[tablezufenTable.PeakLVColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakTBParaNull()
		{
			return IsNull(tablezufenTable.PeakTBParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakTBParaNull()
		{
			base[tablezufenTable.PeakTBParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakUTBParaNull()
		{
			return IsNull(tablezufenTable.PeakUTBParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakUTBParaNull()
		{
			base[tablezufenTable.PeakUTBParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakLParaNull()
		{
			return IsNull(tablezufenTable.PeakLParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakLParaNull()
		{
			base[tablezufenTable.PeakLParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeaktailParaNull()
		{
			return IsNull(tablezufenTable.PeaktailParaColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeaktailParaNull()
		{
			base[tablezufenTable.PeaktailParaColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsPeakFxNull()
		{
			return IsNull(tablezufenTable.PeakFxColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetPeakFxNull()
		{
			base[tablezufenTable.PeakFxColumn] = Convert.DBNull;
		}
	}

	public class cmpdTableRow : DataRow
	{
		private cmpdTableDataTable tablecmpdTable;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal response
		{
			get
			{
				try
				{
					return (decimal)base[tablecmpdTable.responseColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“response”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.responseColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal amount
		{
			get
			{
				try
				{
					return (decimal)base[tablecmpdTable.amountColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“amount”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.amountColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal respFactor
		{
			get
			{
				try
				{
					return (decimal)base[tablecmpdTable.respFactorColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“respFactor”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.respFactorColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public byte[] picture
		{
			get
			{
				try
				{
					return (byte[])base[tablecmpdTable.pictureColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“picture”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.pictureColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public string EquationV
		{
			get
			{
				try
				{
					return (string)base[tablecmpdTable.EquationVColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“EquationV”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.EquationVColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public decimal corrFactor
		{
			get
			{
				try
				{
					return (decimal)base[tablecmpdTable.corrFactorColumn];
				}
				catch (InvalidCastException innerException)
				{
					throw new StrongTypingException("表“cmpdTable”中列“corrFactor”的值为 DBNull。", innerException);
				}
			}
			set
			{
				base[tablecmpdTable.corrFactorColumn] = value;
			}
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		internal cmpdTableRow(DataRowBuilder rb)
			: base(rb)
		{
			tablecmpdTable = (cmpdTableDataTable)base.Table;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsresponseNull()
		{
			return IsNull(tablecmpdTable.responseColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetresponseNull()
		{
			base[tablecmpdTable.responseColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsamountNull()
		{
			return IsNull(tablecmpdTable.amountColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetamountNull()
		{
			base[tablecmpdTable.amountColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsrespFactorNull()
		{
			return IsNull(tablecmpdTable.respFactorColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetrespFactorNull()
		{
			base[tablecmpdTable.respFactorColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IspictureNull()
		{
			return IsNull(tablecmpdTable.pictureColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetpictureNull()
		{
			base[tablecmpdTable.pictureColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IsEquationVNull()
		{
			return IsNull(tablecmpdTable.EquationVColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetEquationVNull()
		{
			base[tablecmpdTable.EquationVColumn] = Convert.DBNull;
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public bool IscorrFactorNull()
		{
			return IsNull(tablecmpdTable.corrFactorColumn);
		}

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public void SetcorrFactorNull()
		{
			base[tablecmpdTable.corrFactorColumn] = Convert.DBNull;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public class zufenTableRowChangeEvent : EventArgs
	{
		private zufenTableRow eventRow;

		private DataRowAction eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRow Row => eventRow;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataRowAction Action => eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public zufenTableRowChangeEvent(zufenTableRow row, DataRowAction action)
		{
			eventRow = row;
			eventAction = action;
		}
	}

	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public class cmpdTableRowChangeEvent : EventArgs
	{
		private cmpdTableRow eventRow;

		private DataRowAction eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableRow Row => eventRow;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public DataRowAction Action => eventAction;

		[DebuggerNonUserCode]
		[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
		public cmpdTableRowChangeEvent(cmpdTableRow row, DataRowAction action)
		{
			eventRow = row;
			eventAction = action;
		}
	}

	private zufenTableDataTable tablezufenTable;

	private cmpdTableDataTable tablecmpdTable;

	private SchemaSerializationMode _schemaSerializationMode = SchemaSerializationMode.IncludeSchema;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public zufenTableDataTable zufenTable => tablezufenTable;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[Browsable(false)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	public cmpdTableDataTable cmpdTable => tablecmpdTable;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[Browsable(true)]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
	public override SchemaSerializationMode SchemaSerializationMode
	{
		get
		{
			return _schemaSerializationMode;
		}
		set
		{
			_schemaSerializationMode = value;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new DataTableCollection Tables => base.Tables;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	public new DataRelationCollection Relations => base.Relations;

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public instockList()
	{
		BeginInit();
		InitClass();
		CollectionChangeEventHandler value = SchemaChanged;
		base.Tables.CollectionChanged += value;
		base.Relations.CollectionChanged += value;
		EndInit();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected instockList(SerializationInfo info, StreamingContext context)
		: base(info, context, ConstructSchema: false)
	{
		if (IsBinarySerialized(info, context))
		{
			InitVars(initTable: false);
			CollectionChangeEventHandler value = SchemaChanged;
			Tables.CollectionChanged += value;
			Relations.CollectionChanged += value;
			return;
		}
		string s = (string)info.GetValue("XmlSchema", typeof(string));
		if (DetermineSchemaSerializationMode(info, context) == SchemaSerializationMode.IncludeSchema)
		{
			DataSet dataSet = new DataSet();
			dataSet.ReadXmlSchema(new XmlTextReader(new StringReader(s)));
			if (dataSet.Tables["zufenTable"] != null)
			{
				base.Tables.Add(new zufenTableDataTable(dataSet.Tables["zufenTable"]));
			}
			if (dataSet.Tables["cmpdTable"] != null)
			{
				base.Tables.Add(new cmpdTableDataTable(dataSet.Tables["cmpdTable"]));
			}
			base.DataSetName = dataSet.DataSetName;
			base.Prefix = dataSet.Prefix;
			base.Namespace = dataSet.Namespace;
			base.Locale = dataSet.Locale;
			base.CaseSensitive = dataSet.CaseSensitive;
			base.EnforceConstraints = dataSet.EnforceConstraints;
			Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
			InitVars();
		}
		else
		{
			ReadXmlSchema(new XmlTextReader(new StringReader(s)));
		}
		GetSerializationData(info, context);
		CollectionChangeEventHandler value2 = SchemaChanged;
		base.Tables.CollectionChanged += value2;
		Relations.CollectionChanged += value2;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override void InitializeDerivedDataSet()
	{
		BeginInit();
		InitClass();
		EndInit();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public override DataSet Clone()
	{
		instockList instockList2 = (instockList)base.Clone();
		instockList2.InitVars();
		instockList2.SchemaSerializationMode = SchemaSerializationMode;
		return instockList2;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override bool ShouldSerializeTables()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override bool ShouldSerializeRelations()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override void ReadXmlSerializable(XmlReader reader)
	{
		if (DetermineSchemaSerializationMode(reader) == SchemaSerializationMode.IncludeSchema)
		{
			Reset();
			DataSet dataSet = new DataSet();
			dataSet.ReadXml(reader);
			if (dataSet.Tables["zufenTable"] != null)
			{
				base.Tables.Add(new zufenTableDataTable(dataSet.Tables["zufenTable"]));
			}
			if (dataSet.Tables["cmpdTable"] != null)
			{
				base.Tables.Add(new cmpdTableDataTable(dataSet.Tables["cmpdTable"]));
			}
			base.DataSetName = dataSet.DataSetName;
			base.Prefix = dataSet.Prefix;
			base.Namespace = dataSet.Namespace;
			base.Locale = dataSet.Locale;
			base.CaseSensitive = dataSet.CaseSensitive;
			base.EnforceConstraints = dataSet.EnforceConstraints;
			Merge(dataSet, preserveChanges: false, MissingSchemaAction.Add);
			InitVars();
		}
		else
		{
			ReadXml(reader);
			InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	protected override XmlSchema GetSchemaSerializable()
	{
		MemoryStream memoryStream = new MemoryStream();
		WriteXmlSchema(new XmlTextWriter(memoryStream, null));
		memoryStream.Position = 0L;
		return XmlSchema.Read(new XmlTextReader(memoryStream), null);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	internal void InitVars()
	{
		InitVars(initTable: true);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	internal void InitVars(bool initTable)
	{
		tablezufenTable = (zufenTableDataTable)base.Tables["zufenTable"];
		if (initTable && tablezufenTable != null)
		{
			tablezufenTable.InitVars();
		}
		tablecmpdTable = (cmpdTableDataTable)base.Tables["cmpdTable"];
		if (initTable && tablecmpdTable != null)
		{
			tablecmpdTable.InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void InitClass()
	{
		base.DataSetName = "instockList";
		base.Prefix = "";
		base.EnforceConstraints = true;
		SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
		tablezufenTable = new zufenTableDataTable();
		base.Tables.Add(tablezufenTable);
		tablecmpdTable = new cmpdTableDataTable();
		base.Tables.Add(tablecmpdTable);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private bool ShouldSerializezufenTable()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private bool ShouldSerializecmpdTable()
	{
		return false;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	private void SchemaChanged(object sender, CollectionChangeEventArgs e)
	{
		if (e.Action == CollectionChangeAction.Remove)
		{
			InitVars();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
	public static XmlSchemaComplexType GetTypedDataSetSchema(XmlSchemaSet xs)
	{
		instockList instockList2 = new instockList();
		XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
		XmlSchemaSequence xmlSchemaSequence = new XmlSchemaSequence();
		XmlSchemaAny xmlSchemaAny = new XmlSchemaAny();
		xmlSchemaAny.Namespace = instockList2.Namespace;
		xmlSchemaSequence.Items.Add(xmlSchemaAny);
		xmlSchemaComplexType.Particle = xmlSchemaSequence;
		XmlSchema schemaSerializable = instockList2.GetSchemaSerializable();
		if (xs.Contains(schemaSerializable.TargetNamespace))
		{
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			try
			{
				XmlSchema xmlSchema = null;
				schemaSerializable.Write(memoryStream);
				IEnumerator enumerator = xs.Schemas(schemaSerializable.TargetNamespace).GetEnumerator();
				while (enumerator.MoveNext())
				{
					xmlSchema = (XmlSchema)enumerator.Current;
					memoryStream2.SetLength(0L);
					xmlSchema.Write(memoryStream2);
					if (memoryStream.Length == memoryStream2.Length)
					{
						memoryStream.Position = 0L;
						memoryStream2.Position = 0L;
						while (memoryStream.Position != memoryStream.Length && memoryStream.ReadByte() == memoryStream2.ReadByte())
						{
						}
						if (memoryStream.Position == memoryStream.Length)
						{
							return xmlSchemaComplexType;
						}
					}
				}
			}
			finally
			{
				memoryStream?.Close();
				memoryStream2?.Close();
			}
		}
		xs.Add(schemaSerializable);
		return xmlSchemaComplexType;
	}
}
