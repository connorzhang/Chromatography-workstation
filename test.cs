using System;
using System.IO;
using System.Text.RegularExpressions;

class Program {
    static void Main() {
        string text = File.ReadAllText(@"IBrainChrom2018\InsDeviceCtrl.cs", System.Text.Encoding.GetEncoding("gbk"));
        int idx = text.IndexOf("misMgr.devManager.eventCtrl0[j].fRowList[0]");
        if(idx > 0) {
            int start = Math.Max(0, idx - 200);
            Console.WriteLine(text.Substring(start, Math.Min(1000, text.Length - start)));
        }
    }
}
