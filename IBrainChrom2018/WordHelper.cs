using System;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Interop.Word;

namespace IBrainChrom2018;

public class WordHelper
{
	private Document document_0;

	private Application application_0;

	public Document Document
	{
		get
		{
			return document_0;
		}
		set
		{
			document_0 = value;
		}
	}

	public Application Application
	{
		get
		{
			return application_0;
		}
		set
		{
			application_0 = value;
		}
	}

	public bool CreateNewWordDocument(string templateName)
	{
		try
		{
			return CreateNewWordDocument(templateName, ref document_0, ref application_0);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static bool CreateNewWordDocument(string templateName, ref Document wDoc, ref Application WApp)
	{
		Application application = new ApplicationClass();
		application.Visible = false;
		application.Caption = "";
		application.Options.CheckSpellingAsYouType = false;
		application.Options.CheckGrammarAsYouType = false;
		object Template = templateName;
		object NewTemplate = false;
		object DocumentType = 0;
		object Visible = true;
		try
		{
			Document document = application.Documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
			wDoc = document;
			WApp = application;
			return true;
		}
		catch (Exception ex)
		{
			string message = $"创建Word文档出错，错误原因：{ex.Message}";
			throw new Exception(message, ex);
		}
	}

	public bool SaveAs(string fileName)
	{
		try
		{
			return SaveAs(fileName, document_0);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public static bool SaveAs(string fileName, Document wDoc)
	{
		object FileName = fileName;
		object FileFormat = 0;
		object LockComments = false;
		object Password = Type.Missing;
		object AddToRecentFiles = false;
		object WritePassword = Type.Missing;
		object ReadOnlyRecommended = false;
		object EmbedTrueTypeFonts = false;
		object SaveNativePictureFormat = true;
		object SaveFormsData = false;
		object SaveAsAOCELetter = false;
		object Encoding = Type.Missing;
		object InsertLineBreaks = true;
		object AllowSubstitutions = false;
		object LineEnding = 0;
		object AddBiDiMarks = true;
		try
		{
			wDoc.SaveAs(ref FileName, ref FileFormat, ref LockComments, ref Password, ref AddToRecentFiles, ref WritePassword, ref ReadOnlyRecommended, ref EmbedTrueTypeFonts, ref SaveNativePictureFormat, ref SaveFormsData, ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks, ref AllowSubstitutions, ref LineEnding, ref AddBiDiMarks);
			return true;
		}
		catch (Exception ex)
		{
			string message = $"另存文件出错，错误原因：{ex.Message}";
			throw new Exception(message, ex);
		}
	}

	public void Close()
	{
		Close(document_0, application_0);
		document_0 = null;
		application_0 = null;
	}

	public static void Close(Document wDoc, Application WApp)
	{
		object SaveChanges = -1;
		object OriginalFormat = 1;
		object RouteDocument = false;
		try
		{
			wDoc?.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
			WApp?.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void Replace(string bookmark, string value)
	{
		try
		{
			object Index = bookmark;
			if (application_0.ActiveDocument.Bookmarks.Exists(bookmark))
			{
				application_0.ActiveDocument.Bookmarks[ref Index].Select();
				application_0.Selection.TypeText(value);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	private void method_0()
	{
		object Name = Missing.Value;
		object What = 3;
		object Which = -1;
		object Count = 99999999;
		application_0.ActiveDocument.Application.Selection.GoTo(ref What, ref Which, ref Count, ref Name);
	}

	public void ReplaceSelect(string value)
	{
		try
		{
			method_0();
			application_0.Selection.TypeText(value);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void ReplacePic(string bookmark, Image image_0)
	{
		try
		{
			object Index = bookmark;
			if (application_0.ActiveDocument.Bookmarks.Exists(bookmark))
			{
				application_0.ActiveDocument.Bookmarks[ref Index].Select();
				string text = Application.StartupPath + "\\ChromPic.bmp";
				image_0.Save(text, ImageFormat.Bmp);
				object LinkToFile = true;
				object SaveWithDocument = true;
				object Range = application_0.ActiveDocument.Bookmarks[ref Index].Range;
				application_0.Selection.InlineShapes.AddPicture(text, ref LinkToFile, ref SaveWithDocument, ref Range);
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void ReplacePicSelection(Image image_0)
	{
		try
		{
			string text = Application.StartupPath + "\\ChromPic.bmp";
			image_0.Save(text, ImageFormat.Bmp);
			object LinkToFile = true;
			object SaveWithDocument = true;
			object Range = document_0.Paragraphs.Last.Range;
			application_0.Selection.InlineShapes.AddPicture(text, ref LinkToFile, ref SaveWithDocument, ref Range);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public bool FindTable(string bookmarkTable)
	{
		try
		{
			object Index = bookmarkTable;
			if (application_0.ActiveDocument.Bookmarks.Exists(bookmarkTable))
			{
				application_0.ActiveDocument.Bookmarks[ref Index].Select();
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void MoveNextCell()
	{
		try
		{
			object Unit = 12;
			object Count = 1;
			application_0.Selection.Move(ref Unit, ref Count);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void SetCellValue(string value)
	{
		try
		{
			application_0.Selection.TypeText(value);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void MoveNextRow()
	{
		try
		{
			object Extend = 1;
			object Unit = 12;
			object Count = 1;
			application_0.Selection.MoveRight(ref Unit, ref Count, ref Extend);
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

	public void InsertPicture(string pPictureFileName)
	{
		object LinkToFile = Missing.Value;
		Application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
		Application.Application.Selection.InlineShapes.AddPicture(pPictureFileName, ref LinkToFile, ref LinkToFile, ref LinkToFile);
	}

	private void method_1(int int_0)
	{
		object Name = Missing.Value;
		object What = 3;
		object Which = ((int_0 >= 0) ? ((object)2) : ((object)3));
		object Count = Math.Abs(int_0);
		application_0.Selection.GoTo(ref What, ref Which, ref Count, ref Name);
	}

	public void CreateTable(object bookmark, System.Data.DataTable Peaks)
	{
		Range range = application_0.ActiveDocument.Bookmarks[ref bookmark].Range;
		object DefaultTableBehavior = 0;
		object AutoFitBehavior = 2;
		int num = Peaks.Rows.Count + 1;
		int count = Peaks.Columns.Count;
		Table table = range.Tables.Add(range, num, count, ref DefaultTableBehavior, ref AutoFitBehavior);
		for (int i = 1; i < num + 1; i++)
		{
			for (int j = 1; j < count + 1; j++)
			{
				if (i != 1)
				{
					try
					{
						table.Cell(i, j).Range.Text = Peaks.Rows[i - 2][j - 1].ToString();
						table.Cell(i, j).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
						if (j != 1)
						{
							table.Cell(i, j).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
						}
					}
					catch
					{
					}
				}
				else
				{
					table.Cell(i, j).Range.Bold = 1;
					table.Cell(i, j).Range.Text = Peaks.Columns[j - 1].Caption;
					table.Cell(i, j).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				}
				Thread.Sleep(10);
			}
		}
	}

	public void CreateTableCurSelect(System.Data.DataTable Peaks)
	{
		Range range = application_0.Selection.Range;
		object DefaultTableBehavior = 0;
		object AutoFitBehavior = 2;
		int num = Peaks.Rows.Count + 1;
		int count = Peaks.Columns.Count;
		Table table = range.Tables.Add(range, num, count, ref DefaultTableBehavior, ref AutoFitBehavior);
		for (int i = 1; i < num + 1; i++)
		{
			for (int j = 1; j < count + 1; j++)
			{
				if (i != 1)
				{
					try
					{
						table.Cell(i, j).Range.Text = Peaks.Rows[i - 2][j - 1].ToString();
						table.Cell(i, j).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
						if (j != 1)
						{
							table.Cell(i, j).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
						}
					}
					catch
					{
					}
				}
				else
				{
					table.Cell(i, j).Range.Bold = 1;
					table.Cell(i, j).Range.Text = Peaks.Columns[j - 1].Caption;
					table.Cell(i, j).Range.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				}
				Thread.Sleep(10);
			}
		}
	}

	public void CreateTableCal(string bookmark, Image[] RImages, Peak[] peakS, CaliGnl CGnl)
	{
		if (CGnl == null)
		{
			return;
		}
		object Index = bookmark;
		if (!application_0.ActiveDocument.Bookmarks.Exists(bookmark))
		{
			return;
		}
		application_0.ActiveDocument.Bookmarks[ref Index].Select();
		for (int i = 0; i < peakS.Length; i++)
		{
			Compound compound = peakS[i].compound;
			if (compound == null)
			{
				break;
			}
			ReplaceSelect("\r\n");
			System.Data.DataTable peaks = method_2(compound);
			CreateTableCurSelect(peaks);
			ReplaceSelect("\r\n");
			ReplacePicSelection(RImages[i]);
			ReplaceSelect("\r\n");
		}
	}

	private System.Data.DataTable method_2(Compound compound_0)
	{
		System.Data.DataTable dataTable = new System.Data.DataTable();
		dataTable.Columns.Add("响应");
		dataTable.Columns.Add("浓度");
		dataTable.Columns.Add("因子");
		for (int i = 0; i < compound_0.levels.Length; i++)
		{
			if (compound_0.levels[i].used)
			{
				dataTable.Rows.Add(compound_0.levels[i].response, compound_0.levels[i].amount, compound_0.levels[i].respFactor);
			}
		}
		return dataTable;
	}
}
