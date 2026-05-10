using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraNavBar;
using DevExpress.XtraPrinting.Preview;
using DevExpress.XtraReports.UI;
using DevExpress.XtraVerticalGrid;

namespace IBrainChrom2018;

public class CtrlLangPS
{
	private static CtrlLangPS myself = null;

	public static CtrlLangPS Instance => myself;

	private CtrlLangPS()
	{
	}

	public static CtrlLangPS Create()
	{
		if (myself == null)
		{
			myself = new CtrlLangPS();
		}
		return myself;
	}

	public void UpdateLanguageForAllControl(UserControl form)
	{
		UpdateLanguageForAllControl(form.Controls);
		if (form.Text != "")
		{
			form.Text = Lang.PS(form.Text);
		}
	}

	public void UpdateLanguageForAllControl(Form form)
	{
		UpdateLanguageForAllControl(form.Controls);
		if (form.Text != "")
		{
			form.Text = Lang.PS(form.Text);
		}
		BarManager barManager = FindBarManager(form);
		if (barManager != null)
		{
			UpdateLanguageForAllControl(barManager);
		}
		DocumentViewerBarManager documentViewerBarManager = FindBarManager2(form);
		if (documentViewerBarManager != null)
		{
			UpdateLanguageForAllControl(documentViewerBarManager);
		}
		RibbonControl ribbonControl = FindRibbonControl(form.Controls);
		if (ribbonControl != null)
		{
			UpdateLanguageForAllControl(ribbonControl);
		}
	}

	public void UpdateLanguageForAllControl(Control.ControlCollection myControlList)
	{
		int count = myControlList.Count;
		for (int i = 0; i < count; i++)
		{
			string text = myControlList[i].GetType().ToString();
			string name = myControlList[i].Name;
			string text2 = myControlList[i].Text;
			string text3 = "";
			if (text.Contains("System.Windows.Forms.Button"))
			{
				Button button = (Button)myControlList[i];
				if (button.Text != "")
				{
					((Button)myControlList[i]).Text = Lang.PS(button.Text);
				}
			}
			else if (text.Contains("System.Windows.Forms.Label"))
			{
				Label label = (Label)myControlList[i];
				if (label.Text != "")
				{
					((Label)myControlList[i]).Text = Lang.PS(label.Text);
				}
			}
			else if (text.Contains("DevExpress.XtraEditors.SimpleButton"))
			{
				SimpleButton simpleButton = (SimpleButton)myControlList[i];
				if (simpleButton.Text != "")
				{
					((SimpleButton)myControlList[i]).Text = Lang.PS(simpleButton.Text);
				}
				if (simpleButton.ToolTip != "")
				{
					((SimpleButton)myControlList[i]).ToolTip = Lang.PS(simpleButton.ToolTip);
				}
			}
			else if (text.Contains("DevExpress.XtraEditors.DropDownButton"))
			{
				DropDownButton dropDownButton = (DropDownButton)myControlList[i];
				if (dropDownButton.Text != "")
				{
					((DropDownButton)myControlList[i]).Text = Lang.PS(dropDownButton.Text);
				}
				if (dropDownButton.ToolTip != "")
				{
					((DropDownButton)myControlList[i]).ToolTip = Lang.PS(dropDownButton.ToolTip);
				}
			}
			else if (text.Contains("DevExpress.XtraEditors.BarButtonItem"))
			{
				SimpleButton simpleButton2 = (SimpleButton)myControlList[i];
				if (simpleButton2.Text != "")
				{
					((SimpleButton)myControlList[i]).Text = Lang.PS(simpleButton2.Text);
				}
				if (simpleButton2.ToolTip != "")
				{
					((SimpleButton)myControlList[i]).ToolTip = Lang.PS(simpleButton2.ToolTip);
				}
			}
			else if (!text.Contains("ToolStripMenuItem"))
			{
				if (text.Contains("DevExpress.XtraEditors.CheckButton"))
				{
					CheckButton checkButton = (CheckButton)myControlList[i];
					if (checkButton.Text != "")
					{
						((CheckButton)myControlList[i]).Text = Lang.PS(checkButton.Text);
					}
					if (checkButton.ToolTip != "")
					{
						((CheckButton)myControlList[i]).ToolTip = Lang.PS(checkButton.ToolTip);
					}
				}
				else if (text.Contains("DevExpress.XtraEditors.BarCheckItem"))
				{
					SimpleButton simpleButton3 = (SimpleButton)myControlList[i];
					if (simpleButton3.Text != "")
					{
						((SimpleButton)myControlList[i]).Text = Lang.PS(simpleButton3.Text);
					}
					if (simpleButton3.ToolTip != "")
					{
						((SimpleButton)myControlList[i]).ToolTip = Lang.PS(simpleButton3.ToolTip);
					}
				}
				else if (text.Contains("DevExpress.XtraEditors.ButtonEdit"))
				{
					ButtonEdit buttonEdit = (ButtonEdit)myControlList[i];
					for (int j = 0; j < buttonEdit.Properties.Buttons.Count; j++)
					{
						buttonEdit.Properties.Buttons[j].Caption = Lang.PS(buttonEdit.Properties.Buttons[j].Caption);
					}
				}
				else if (text.Contains("System.Windows.Forms.ComboBox"))
				{
					System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)myControlList[i];
					if (comboBox.Text != "")
					{
						comboBox.Text = Lang.PS(comboBox.Text);
					}
					for (int k = 0; k < comboBox.Items.Count; k++)
					{
						comboBox.Items[k] = Lang.PS(comboBox.Items[k].ToString());
					}
				}
				else if (text.Contains("System.Windows.Forms.GroupBox"))
				{
					GroupBox groupBox = (GroupBox)myControlList[i];
					if (groupBox.Text != "")
					{
						((GroupBox)myControlList[i]).Text = Lang.PS(groupBox.Text);
					}
				}
				else if (text.Contains("System.Windows.Forms.DataGridView"))
				{
					DataGridView dataGridView = (DataGridView)myControlList[i];
					for (int l = 0; l < dataGridView.ColumnCount; l++)
					{
						if (dataGridView.Columns[l].HeaderText != "")
						{
							dataGridView.Columns[l].HeaderText = Lang.PS(dataGridView.Columns[l].HeaderText);
						}
					}
				}
				else if (text == "DevExpress.XtraGrid.GridControl")
				{
					GridControl gridControl = (GridControl)myControlList[i];
					GridView gridView = (GridView)gridControl.MainView;
					for (int m = 0; m < gridView.Columns.Count; m++)
					{
						if (gridView.Columns[m].Caption != "")
						{
							gridView.Columns[m].Caption = Lang.PS(gridView.Columns[m].Caption);
						}
					}
				}
				else if (text == "DevExpress.XtraNavBar.NavBarControl")
				{
					NavBarControl navBarControl = (NavBarControl)myControlList[i];
					for (int n = 0; n < navBarControl.Groups.Count; n++)
					{
						if (navBarControl.Groups[n].Caption != "")
						{
							navBarControl.Groups[n].Caption = Lang.PS(navBarControl.Groups[n].Caption);
						}
					}
				}
				else
				{
					switch (text)
					{
					case "DevExpress.XtraNavBar.NavBarControl":
					{
						NavBarControl navBarControl2 = (NavBarControl)myControlList[i];
						for (int num7 = 0; num7 < navBarControl2.Groups.Count; num7++)
						{
							if (navBarControl2.Groups[num7].Caption != "")
							{
								navBarControl2.Groups[num7].Caption = Lang.PS(navBarControl2.Groups[num7].Caption);
							}
						}
						break;
					}
					case "DevExpress.XtraBars.Ribbon.RibbonControl":
					{
						RibbonControl myBarManager = (RibbonControl)myControlList[i];
						UpdateLanguageForAllControl(myBarManager);
						break;
					}
					case "System.Windows.Forms.MenuStrip":
					{
						MenuStrip menuStrip = (MenuStrip)myControlList[i];
						menuStrip.Text = Lang.PS(menuStrip.Text);
						for (int num4 = 0; num4 < menuStrip.Items.Count; num4++)
						{
							ToolStripMenuItem toolStripMenuItem2 = (ToolStripMenuItem)menuStrip.Items[num4];
							if (toolStripMenuItem2 != null)
							{
								toolStripMenuItem2.Text = Lang.PS(toolStripMenuItem2.Text);
								for (int num5 = 0; num5 < toolStripMenuItem2.DropDown.Items.Count; num5++)
								{
									ToolStripItem toolStripItem2 = toolStripMenuItem2.DropDown.Items[num5];
									toolStripItem2.Text = Lang.PS(toolStripItem2.Text);
								}
							}
						}
						break;
					}
					case "System.Windows.Forms.ToolStrip":
					{
						ToolStrip toolStrip = (ToolStrip)myControlList[i];
						toolStrip.Text = Lang.PS(toolStrip.Text);
						for (int num2 = 0; num2 < toolStrip.Items.Count; num2++)
						{
							ToolStripItem toolStripItem = toolStrip.Items[num2];
							if (toolStripItem != null)
							{
								toolStripItem.Text = Lang.PS(toolStripItem.Text);
							}
						}
						break;
					}
					case "System.Windows.Forms.StatusStrip":
					{
						StatusStrip statusStrip = (StatusStrip)myControlList[i];
						statusStrip.Text = Lang.PS(statusStrip.Text);
						for (int num6 = 0; num6 < statusStrip.Items.Count; num6++)
						{
							ToolStripItem toolStripItem3 = statusStrip.Items[num6];
							if (toolStripItem3 != null)
							{
								toolStripItem3.Text = Lang.PS(toolStripItem3.Text);
							}
						}
						break;
					}
					case "System.Windows.Forms.ContextMenuStrip":
					{
						ContextMenuStrip contextMenuStrip = (ContextMenuStrip)myControlList[i];
						contextMenuStrip.Text = Lang.PS(contextMenuStrip.Text);
						for (int num3 = 0; num3 < contextMenuStrip.Items.Count; num3++)
						{
							ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)contextMenuStrip.Items[num3];
							toolStripMenuItem.Text = Lang.PS(toolStripMenuItem.Text);
						}
						break;
					}
					case "DevExpress.XtraPrinting.Preview.DocumentViewer":
					{
						DocumentViewer documentViewer = (DocumentViewer)myControlList[i];
						documentViewer.Status = Lang.PS(documentViewer.Status);
						break;
					}
					case "DevExpress.XtraEditors.ComboBoxEdit":
					{
						ComboBoxEdit comboBoxEdit = (ComboBoxEdit)myControlList[i];
						if (comboBoxEdit.Text == "PDFCreator")
						{
							continue;
						}
						for (int num = 0; num < comboBoxEdit.Properties.Items.Count; num++)
						{
							comboBoxEdit.Properties.Items[num] = Lang.PS(comboBoxEdit.Properties.Items[num].ToString());
						}
						break;
					}
					case "DevExpress.XtraEditors.TextBoxMaskBox":
					{
						TextBoxMaskBox textBoxMaskBox = (TextBoxMaskBox)myControlList[i];
						break;
					}
					case "DevExpress.XtraEditors.CheckEdit":
					{
						CheckEdit checkEdit = (CheckEdit)myControlList[i];
						checkEdit.Text = Lang.PS(checkEdit.Text);
						break;
					}
					case "DevExpress.XtraEditors.Preview.LabelControlWithMetric":
					{
						LabelControl labelControl2 = (LabelControl)myControlList[i];
						labelControl2.Text = Lang.PS(labelControl2.Text);
						break;
					}
					case "DevExpress.XtraEditors.LabelControl":
					{
						LabelControl labelControl = (LabelControl)myControlList[i];
						labelControl.Text = Lang.PS(labelControl.Text);
						break;
					}
					case "DevExpress.XtraEditors.TextEdit":
					{
						TextEdit textEdit = (TextEdit)myControlList[i];
						textEdit.Text = Lang.PS(textEdit.Text);
						break;
					}
					}
				}
			}
			if (myControlList[i].Controls.Count > 0)
			{
				UpdateLanguageForAllControl(myControlList[i].Controls);
			}
			BarManager barManager = FindBarManager(myControlList[i]);
			if (barManager != null)
			{
				UpdateLanguageForAllControl(barManager);
			}
			PropertyGridControl propertyGridControl = FindPropertyGrid(myControlList[i]);
			if (propertyGridControl != null)
			{
				UpdateLanguageForAllControl(propertyGridControl);
			}
		}
	}

	private PropertyGridControl FindPropertyGrid(Control ctrl)
	{
		PropertyGridControl result = null;
		FieldInfo[] fields = ctrl.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		for (int i = 0; i < fields.Length; i++)
		{
			string text = fields[i].ToString();
			if (text.Contains("DevExpress.XtraVerticalGrid.PropertyGridControl"))
			{
				return (PropertyGridControl)fields[i].GetValue(ctrl);
			}
		}
		return result;
	}

	public void UpdateLanguageForAllControl(PropertyGridControl propGrid)
	{
		int count = propGrid.RepositoryItems.Count;
		for (int i = 0; i < count; i++)
		{
			string text = propGrid.RepositoryItems[i].GetType().ToString();
			string name = propGrid.RepositoryItems[i].Name;
			if (text.Contains("DevExpress.XtraEditors.Repository.RepositoryItemComboBox"))
			{
				RepositoryItemComboBox repositoryItemComboBox = (RepositoryItemComboBox)propGrid.RepositoryItems[i];
				for (int j = 0; j < repositoryItemComboBox.Items.Count; j++)
				{
					string key = repositoryItemComboBox.Items[j].ToString();
					repositoryItemComboBox.Items[j] = Lang.PS(key);
				}
			}
			else if (text.Contains("DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox"))
			{
				RepositoryItemImageComboBox repositoryItemImageComboBox = (RepositoryItemImageComboBox)propGrid.RepositoryItems[i];
				for (int k = 0; k < repositoryItemImageComboBox.Items.Count; k++)
				{
					string description = repositoryItemImageComboBox.Items[k].Description;
					repositoryItemImageComboBox.Items[k].Description = Lang.PS(description);
				}
			}
			else if (text.Contains("DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit"))
			{
				RepositoryItemMRUEdit repositoryItemMRUEdit = (RepositoryItemMRUEdit)propGrid.RepositoryItems[i];
				for (int l = 0; l < repositoryItemMRUEdit.Items.Count; l++)
				{
					string key2 = repositoryItemMRUEdit.Items[l].ToString();
					repositoryItemMRUEdit.Items[l] = Lang.PS(key2);
				}
			}
			else
			{
				name = propGrid.RepositoryItems[i].Name;
			}
		}
	}

	private BarManager FindBarManager(Control ctrl)
	{
		BarManager result = null;
		FieldInfo[] fields = ctrl.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		for (int i = 0; i < fields.Length; i++)
		{
			string text = fields[i].ToString();
			if (text.Contains("DevExpress.XtraBars.BarManager"))
			{
				return (BarManager)fields[i].GetValue(ctrl);
			}
		}
		return result;
	}

	public void UpdateLanguageForAllControl(BarManager myBarManager)
	{
		int count = myBarManager.Items.Count;
		for (int i = 0; i < count; i++)
		{
			string text = myBarManager.Items[i].GetType().ToString();
			string name = myBarManager.Items[i].Name;
			if (text.Contains("DevExpress.XtraBars.BarLargeButtonItem"))
			{
				BarLargeButtonItem barLargeButtonItem = (BarLargeButtonItem)myBarManager.Items[i];
				if (barLargeButtonItem.Caption != "")
				{
					barLargeButtonItem.Caption = Lang.PS(barLargeButtonItem.Caption);
				}
				if (barLargeButtonItem.Description != "")
				{
					barLargeButtonItem.Description = Lang.PS(barLargeButtonItem.Description);
				}
				if (barLargeButtonItem.Hint != "")
				{
					barLargeButtonItem.Hint = Lang.PS(barLargeButtonItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarCheckItem"))
			{
				BarCheckItem barCheckItem = (BarCheckItem)myBarManager.Items[i];
				if (barCheckItem.Caption != "")
				{
					barCheckItem.Caption = Lang.PS(barCheckItem.Caption);
				}
				if (barCheckItem.Description != "")
				{
					barCheckItem.Description = Lang.PS(barCheckItem.Description);
				}
				if (barCheckItem.Hint != "")
				{
					barCheckItem.Hint = Lang.PS(barCheckItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarButtonItem"))
			{
				BarButtonItem barButtonItem = (BarButtonItem)myBarManager.Items[i];
				if (barButtonItem.Caption != "")
				{
					barButtonItem.Caption = Lang.PS(barButtonItem.Caption);
				}
				if (barButtonItem.Description != "")
				{
					barButtonItem.Description = Lang.PS(barButtonItem.Description);
				}
				if (barButtonItem.Hint != "")
				{
					barButtonItem.Hint = Lang.PS(barButtonItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarSubItem"))
			{
				BarSubItem barSubItem = (BarSubItem)myBarManager.Items[i];
				if (barSubItem.Caption != "")
				{
					barSubItem.Caption = Lang.PS(barSubItem.Caption);
				}
				if (barSubItem.Description != "")
				{
					barSubItem.Description = Lang.PS(barSubItem.Description);
				}
				if (barSubItem.Hint != "")
				{
					barSubItem.Hint = Lang.PS(barSubItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarStaticItem"))
			{
				BarStaticItem barStaticItem = (BarStaticItem)myBarManager.Items[i];
				if (barStaticItem.Caption != "")
				{
					barStaticItem.Caption = Lang.PS(barStaticItem.Caption);
				}
				if (barStaticItem.Description != "")
				{
					barStaticItem.Description = Lang.PS(barStaticItem.Description);
				}
				if (barStaticItem.Hint != "")
				{
					barStaticItem.Hint = Lang.PS(barStaticItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarEditItem"))
			{
				BarEditItem barEditItem = (BarEditItem)myBarManager.Items[i];
				string text2 = barEditItem.Edit.GetType().ToString();
				if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemComboBox"))
				{
					RepositoryItemComboBox repositoryItemComboBox = (RepositoryItemComboBox)barEditItem.Edit;
					for (int j = 0; j < repositoryItemComboBox.Items.Count; j++)
					{
						string key = repositoryItemComboBox.Items[j].ToString();
						repositoryItemComboBox.Items[j] = Lang.PS(key);
					}
				}
				else if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox"))
				{
					RepositoryItemImageComboBox repositoryItemImageComboBox = (RepositoryItemImageComboBox)barEditItem.Edit;
					for (int k = 0; k < repositoryItemImageComboBox.Items.Count; k++)
					{
						string description = repositoryItemImageComboBox.Items[k].Description;
						repositoryItemImageComboBox.Items[k].Description = Lang.PS(description);
					}
				}
				else if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit"))
				{
					RepositoryItemMRUEdit repositoryItemMRUEdit = (RepositoryItemMRUEdit)barEditItem.Edit;
					for (int l = 0; l < repositoryItemMRUEdit.Items.Count; l++)
					{
						string key2 = repositoryItemMRUEdit.Items[l].ToString();
						repositoryItemMRUEdit.Items[l] = Lang.PS(key2);
					}
				}
				else
				{
					text2 = barEditItem.Edit.GetType().ToString();
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarToolbarsListItem"))
			{
				BarToolbarsListItem barToolbarsListItem = (BarToolbarsListItem)myBarManager.Items[i];
			}
			else
			{
				name = myBarManager.Items[i].Name;
			}
		}
	}

	private DocumentViewerBarManager FindBarManager2(Control ctrl)
	{
		DocumentViewerBarManager result = null;
		FieldInfo[] fields = ctrl.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		for (int i = 0; i < fields.Length; i++)
		{
			string text = fields[i].ToString();
			if (text.Contains("DevExpress.XtraPrinting.Preview.DocumentViewerBarManager"))
			{
				return (DocumentViewerBarManager)fields[i].GetValue(ctrl);
			}
		}
		return result;
	}

	public void UpdateLanguageForAllControl(DocumentViewerBarManager myBarManager)
	{
		int count = myBarManager.Bars.Count;
		for (int i = 0; i < count; i++)
		{
			string text = myBarManager.Bars[i].Text;
			if (text != "")
			{
				myBarManager.Bars[i].Text = Lang.PS(text);
			}
			string barName = myBarManager.Bars[i].BarName;
			if (barName != "")
			{
				myBarManager.Bars[i].BarName = Lang.PS(barName);
			}
		}
		int count2 = myBarManager.Items.Count;
		for (int j = 0; j < count2; j++)
		{
			string text2 = myBarManager.Items[j].GetType().ToString();
			string name = myBarManager.Items[j].Name;
			if (text2.Contains("DevExpress.XtraPrinting.Preview.PrintPreviewBarItem"))
			{
				PrintPreviewBarItem printPreviewBarItem = (PrintPreviewBarItem)myBarManager.Items[j];
				if (printPreviewBarItem.Caption != "")
				{
					printPreviewBarItem.Caption = Lang.PS(printPreviewBarItem.Caption);
				}
				if (printPreviewBarItem.Description != "")
				{
					printPreviewBarItem.Description = Lang.PS(printPreviewBarItem.Description);
				}
				if (printPreviewBarItem.Hint != "")
				{
					printPreviewBarItem.Hint = Lang.PS(printPreviewBarItem.Hint);
				}
			}
			else if (text2.Contains("DevExpress.XtraPrinting.Preview.PrintPreviewSubItem"))
			{
				PrintPreviewSubItem printPreviewSubItem = (PrintPreviewSubItem)myBarManager.Items[j];
				if (printPreviewSubItem.Caption != "")
				{
					printPreviewSubItem.Caption = Lang.PS(printPreviewSubItem.Caption);
				}
				if (printPreviewSubItem.Description != "")
				{
					printPreviewSubItem.Description = Lang.PS(printPreviewSubItem.Description);
				}
				if (printPreviewSubItem.Hint != "")
				{
					printPreviewSubItem.Hint = Lang.PS(printPreviewSubItem.Hint);
				}
			}
			else if (text2.Contains("DevExpress.XtraPrinting.Preview.ZoomBarEditItem"))
			{
				ZoomBarEditItem zoomBarEditItem = (ZoomBarEditItem)myBarManager.Items[j];
				if (zoomBarEditItem.Caption != "")
				{
					zoomBarEditItem.Caption = Lang.PS(zoomBarEditItem.Caption);
				}
				if (zoomBarEditItem.Description != "")
				{
					zoomBarEditItem.Description = Lang.PS(zoomBarEditItem.Description);
				}
				if (zoomBarEditItem.Hint != "")
				{
					zoomBarEditItem.Hint = Lang.PS(zoomBarEditItem.Hint);
				}
			}
			else if (text2.Contains("DevExpress.XtraPrinting.Preview.PrintPreviewStaticItem"))
			{
				PrintPreviewStaticItem printPreviewStaticItem = (PrintPreviewStaticItem)myBarManager.Items[j];
				if (printPreviewStaticItem.Caption != "")
				{
					printPreviewStaticItem.Caption = Lang.PS(printPreviewStaticItem.Caption);
				}
				if (printPreviewStaticItem.Description != "")
				{
					printPreviewStaticItem.Description = Lang.PS(printPreviewStaticItem.Description);
				}
				if (printPreviewStaticItem.Hint != "")
				{
					printPreviewStaticItem.Hint = Lang.PS(printPreviewStaticItem.Hint);
				}
			}
			else if (text2.Contains("DevExpress.XtraBars.BarStaticItem"))
			{
				BarStaticItem barStaticItem = (BarStaticItem)myBarManager.Items[j];
				if (barStaticItem.Caption != "")
				{
					barStaticItem.Caption = Lang.PS(barStaticItem.Caption);
				}
				if (barStaticItem.Description != "")
				{
					barStaticItem.Description = Lang.PS(barStaticItem.Description);
				}
				if (barStaticItem.Hint != "")
				{
					barStaticItem.Hint = Lang.PS(barStaticItem.Hint);
				}
			}
			else
			{
				name = myBarManager.Items[j].Name;
			}
		}
	}

	protected RibbonControl FindRibbonControl(Control.ControlCollection controls)
	{
		if (controls.OfType<Control>().FirstOrDefault((Control x) => x is RibbonControl) is RibbonControl result)
		{
			return result;
		}
		foreach (Control control in controls)
		{
			if (control.HasChildren)
			{
				RibbonControl ribbonControl = FindRibbonControl(control.Controls);
				if (ribbonControl != null)
				{
					return ribbonControl;
				}
			}
		}
		return null;
	}

	public void UpdateLanguageForAllControl(RibbonControl myBarManager)
	{
		int count = myBarManager.Items.Count;
		for (int i = 0; i < count; i++)
		{
			string text = myBarManager.Items[i].GetType().ToString();
			string name = myBarManager.Items[i].Name;
			if (text.Contains("DevExpress.XtraBars.BarLargeButtonItem"))
			{
				BarLargeButtonItem barLargeButtonItem = (BarLargeButtonItem)myBarManager.Items[i];
				if (barLargeButtonItem.Caption != "")
				{
					barLargeButtonItem.Caption = Lang.PS(barLargeButtonItem.Caption);
				}
				if (barLargeButtonItem.Description != "")
				{
					barLargeButtonItem.Description = Lang.PS(barLargeButtonItem.Description);
				}
				if (barLargeButtonItem.Hint != "")
				{
					barLargeButtonItem.Hint = Lang.PS(barLargeButtonItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarCheckItem"))
			{
				BarCheckItem barCheckItem = (BarCheckItem)myBarManager.Items[i];
				if (barCheckItem.Caption != "")
				{
					barCheckItem.Caption = Lang.PS(barCheckItem.Caption);
				}
				if (barCheckItem.Description != "")
				{
					barCheckItem.Description = Lang.PS(barCheckItem.Description);
				}
				if (barCheckItem.Hint != "")
				{
					barCheckItem.Hint = Lang.PS(barCheckItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarButtonItem"))
			{
				BarButtonItem barButtonItem = (BarButtonItem)myBarManager.Items[i];
				if (barButtonItem.Caption != "")
				{
					barButtonItem.Caption = Lang.PS(barButtonItem.Caption);
				}
				if (barButtonItem.Description != "")
				{
					barButtonItem.Description = Lang.PS(barButtonItem.Description);
				}
				if (barButtonItem.Hint != "")
				{
					barButtonItem.Hint = Lang.PS(barButtonItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarSubItem"))
			{
				BarSubItem barSubItem = (BarSubItem)myBarManager.Items[i];
				if (barSubItem.Caption != "")
				{
					barSubItem.Caption = Lang.PS(barSubItem.Caption);
				}
				if (barSubItem.Description != "")
				{
					barSubItem.Description = Lang.PS(barSubItem.Description);
				}
				if (barSubItem.Hint != "")
				{
					barSubItem.Hint = Lang.PS(barSubItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarStaticItem"))
			{
				BarStaticItem barStaticItem = (BarStaticItem)myBarManager.Items[i];
				if (barStaticItem.Caption != "")
				{
					barStaticItem.Caption = Lang.PS(barStaticItem.Caption);
				}
				if (barStaticItem.Description != "")
				{
					barStaticItem.Description = Lang.PS(barStaticItem.Description);
				}
				if (barStaticItem.Hint != "")
				{
					barStaticItem.Hint = Lang.PS(barStaticItem.Hint);
				}
			}
			else if (text.Contains("DevExpress.XtraBars.BarEditItem"))
			{
				BarEditItem barEditItem = (BarEditItem)myBarManager.Items[i];
				string text2 = barEditItem.Edit.GetType().ToString();
				if (barEditItem.Caption != "")
				{
					barEditItem.Caption = Lang.PS(barEditItem.Caption);
				}
				if (barEditItem.Description != "")
				{
					barEditItem.Description = Lang.PS(barEditItem.Description);
				}
				if (barEditItem.Hint != "")
				{
					barEditItem.Hint = Lang.PS(barEditItem.Hint);
				}
				if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemComboBox"))
				{
					RepositoryItemComboBox repositoryItemComboBox = (RepositoryItemComboBox)barEditItem.Edit;
					for (int j = 0; j < repositoryItemComboBox.Items.Count; j++)
					{
						string key = repositoryItemComboBox.Items[j].ToString();
						repositoryItemComboBox.Items[j] = Lang.PS(key);
					}
				}
				else if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox"))
				{
					RepositoryItemImageComboBox repositoryItemImageComboBox = (RepositoryItemImageComboBox)barEditItem.Edit;
					for (int k = 0; k < repositoryItemImageComboBox.Items.Count; k++)
					{
						string description = repositoryItemImageComboBox.Items[k].Description;
						repositoryItemImageComboBox.Items[k].Description = Lang.PS(description);
					}
				}
				else if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit"))
				{
					RepositoryItemMRUEdit repositoryItemMRUEdit = (RepositoryItemMRUEdit)barEditItem.Edit;
					for (int l = 0; l < repositoryItemMRUEdit.Items.Count; l++)
					{
						string key2 = repositoryItemMRUEdit.Items[l].ToString();
						repositoryItemMRUEdit.Items[l] = Lang.PS(key2);
					}
				}
				else if (text2.Contains("DevExpress.XtraEditors.Repository.RepositoryItemMRUEdit"))
				{
					RepositoryItemMRUEdit repositoryItemMRUEdit2 = (RepositoryItemMRUEdit)barEditItem.Edit;
					for (int m = 0; m < repositoryItemMRUEdit2.Items.Count; m++)
					{
						string key3 = repositoryItemMRUEdit2.Items[m].ToString();
						repositoryItemMRUEdit2.Items[m] = Lang.PS(key3);
					}
				}
				else
				{
					text2 = barEditItem.Edit.GetType().ToString();
				}
			}
			else
			{
				name = myBarManager.Items[i].Name;
			}
		}
		for (int n = 0; n < myBarManager.Pages.Count; n++)
		{
			string text3 = myBarManager.Pages[n].Text;
			myBarManager.Pages[n].Text = Lang.PS(text3);
			for (int num = 0; num < myBarManager.Pages[n].Groups.Count; num++)
			{
				string text4 = myBarManager.Pages[n].Groups[num].Text;
				myBarManager.Pages[n].Groups[num].Text = Lang.PS(text4);
			}
		}
	}

	public void UpdateLanguageForAllControl(XtraReport myReport)
	{
		int count = myReport.Bands.Count;
		for (int i = 0; i < count; i++)
		{
			UpdateLanguageForAllControl(myReport.Bands[i]);
		}
	}

	private void UpdateLanguageForAllControl(Band myBand)
	{
		int count = myBand.Controls.Count;
		for (int i = 0; i < count; i++)
		{
			string text = myBand.Controls[i].GetType().ToString();
			string name = myBand.Controls[i].Name;
			if (text.Contains("DevExpress.XtraReports.UI.XRTable"))
			{
				XRTable xRTable = (XRTable)myBand.Controls[i];
				for (int j = 0; j < xRTable.Rows.Count; j++)
				{
					for (int k = 0; k < xRTable.Rows[j].Cells.Count; k++)
					{
						string text2 = xRTable.Rows[j].Cells[k].Text;
						if (text2 != "")
						{
							xRTable.Rows[j].Cells[k].Text = Lang.PS(xRTable.Rows[j].Cells[k].Text);
						}
					}
				}
			}
			else if (text.Contains("DevExpress.XtraReports.UI.XRLabel"))
			{
				XRLabel xRLabel = (XRLabel)myBand.Controls[i];
				if (xRLabel.Text != "")
				{
					xRLabel.Text = Lang.PS(xRLabel.Text);
				}
			}
		}
	}
}
