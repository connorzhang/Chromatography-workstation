using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;

namespace IBrainChrom2018.Unit;

public class CalculateExpression
{
	private static CalculateExpression myself = null;

	private Assembly assembly = null;

	private object classInstance;

	private int m_nparam;

	public static object Calculate(string exp)
	{
		string text = "Calc";
		string text2 = "Run";
		exp = exp.Replace("/", "*1.0/");
		CodeDomProvider codeDomProvider = new CSharpCodeProvider();
		CompilerParameters compilerParameters = new CompilerParameters();
		compilerParameters.GenerateExecutable = false;
		compilerParameters.GenerateInMemory = true;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("public   class   " + text + "\n ");
		stringBuilder.Append("{\n ");
		stringBuilder.Append("         public   object   " + text2 + "()\n ");
		stringBuilder.Append("         {\n ");
		stringBuilder.Append("                 return   " + exp + ";\n ");
		stringBuilder.Append("         }\n ");
		stringBuilder.Append("} ");
		CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
		Assembly assembly = null;
		try
		{
			assembly = compilerResults.CompiledAssembly;
		}
		catch (FileNotFoundException)
		{
			return 0f;
		}
		object obj = assembly.CreateInstance(text);
		MethodInfo method = obj.GetType().GetMethod(text2);
		object result = method.Invoke(obj, null);
		GC.Collect();
		return result;
	}

	public static CalculateExpression Create()
	{
		if (myself == null)
		{
			myself = new CalculateExpression();
		}
		return myself;
	}

	private CalculateExpression()
	{
	}

	public void AddCalculate(string[] keyList, string[] expList, int nParam)
	{
		if (keyList.Length != expList.Length)
		{
			throw new Exception(" 公式数量不匹配");
		}
		if (nParam > 6)
		{
			throw new Exception("参数数量超出设定范围");
		}
		string text = "Calc2";
		string text2 = "Run";
		string[] array = new string[6] { "XX", "YY", "ZZ", "UU", "VV", "WW" };
		m_nparam = nParam;
		CodeDomProvider codeDomProvider = new CSharpCodeProvider();
		CompilerParameters compilerParameters = new CompilerParameters();
		compilerParameters.GenerateExecutable = false;
		compilerParameters.GenerateInMemory = true;
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("public   class   " + text + "\n ");
		stringBuilder.Append("{\n ");
		for (int i = 0; i < expList.Length; i++)
		{
			string text3 = expList[i];
			string text4 = text2 + keyList[i];
			string text5 = "";
			for (int j = 0; j < m_nparam; j++)
			{
				if (text5 != "")
				{
					text5 += ",";
				}
				text5 = text5 + " double " + array[j];
			}
			text3 = text3.Replace("/", "*1.0/");
			stringBuilder.Append("         public   object   " + text4 + "(" + text5 + ")\n ");
			stringBuilder.Append("         {\n ");
			stringBuilder.Append("                 return   " + text3 + ";\n ");
			stringBuilder.Append("         }\n ");
		}
		stringBuilder.Append("} ");
		CompilerResults compilerResults = codeDomProvider.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
		try
		{
			assembly = compilerResults.CompiledAssembly;
			classInstance = assembly.CreateInstance(text);
		}
		catch (FileNotFoundException)
		{
		}
	}

	public object RunExpression(string strkey, params double[] value)
	{
		if (assembly == null || classInstance == null)
		{
			throw new Exception("必须先调用AddCalculate初始化公式，才能进行解析");
		}
		string name = "Run" + strkey;
		object[] array = new object[value.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = value[i];
		}
		MethodInfo method = classInstance.GetType().GetMethod(name);
		return method.Invoke(classInstance, array);
	}
}
