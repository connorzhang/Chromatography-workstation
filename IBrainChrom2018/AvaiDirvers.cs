namespace IBrainChrom2018;

public class AvaiDirvers
{
	public CtrlModules controlModules = new CtrlModules();

	public void SysAvaiDirvers()
	{
		controlModules.ClearAll();
		controlModules.AddControlModule(new AutoSamplerControl1());
		controlModules.AddControlModule(new LiquidPump1());
		controlModules.AddControlModule(new LiquidPump2());
		controlModules.AddControlModule(new GasChromControl1());
		controlModules.AddControlModule(new GasChromControl2());
		controlModules.AddControlModule(new DetectorControl1());
		controlModules.AddControlModule(new SZ_Dt());
		controlModules.AddControlModule(new Control1());
		controlModules.AddControlModule(new Control0());
	}
}
