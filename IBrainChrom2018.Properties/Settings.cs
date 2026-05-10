using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace IBrainChrom2018.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("10")]
	public decimal CurvePointsDensity => (decimal)this["CurvePointsDensity"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DefaultSettingValue("Provider=SQLNCLI11;Data Source=(local);Persist Security Info=True;Password=sa123456;User ID=sa;Initial Catalog=toySQLTest")]
	public string ConnectionString => (string)this["ConnectionString"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DefaultSettingValue("Data Source=.;Initial Catalog=toySQLTest;Persist Security Info=True;User ID=sa;Password=sa123456")]
	public string toySQLConnectionString => (string)this["toySQLConnectionString"];

	[ApplicationScopedSetting]
	[DebuggerNonUserCode]
	[SpecialSetting(SpecialSetting.ConnectionString)]
	[DefaultSettingValue("Provider=SQLNCLI11;Data Source=LIYONGNIAN;Persist Security Info=True;Password=sa123456;User ID=sa;Initial Catalog=toySQLTest")]
	public string ConnectionString1 => (string)this["ConnectionString1"];
}
