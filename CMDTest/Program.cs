/*
// 1. 测试音频参数 
using System.Diagnostics;

foreach (var i in Enumerable.Range(1, 30))
{
    Task.Run(() =>
    {
        var proc = new Process();
        try
        {
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.Start();
            var dosLine = $"SystemSpeechTest.exe test{i} 我们test{i}";
            proc.StandardInput.WriteLine(dosLine);
            proc.StandardInput.WriteLine("exit");
            while (!proc.HasExited)
            {
                proc.WaitForExit(1000);
            }

            var errorMsg = proc.StandardError.ReadToEnd();
            proc.StandardError.Close();
            if (string.IsNullOrEmpty(errorMsg) == false) throw new Exception(errorMsg);
        }
        finally
        {
            proc.Close();
            proc.Dispose();
        }

        Console.WriteLine($"ok {i}");
    });
}

Console.ReadKey();*/


// 2. 测试命令行程序 

using System.Diagnostics;


using var proc = new Process();
proc.StartInfo.FileName = "cmd.exe";
proc.StartInfo.UseShellExecute = false;
proc.StartInfo.RedirectStandardInput = true;
proc.StartInfo.RedirectStandardOutput = true;
proc.StartInfo.RedirectStandardError = true;
proc.StartInfo.CreateNoWindow = true;
proc.Start();
// var dosLine = @$"main.exe 1.57 Z:\CZ-UA3P-5#\2107314255\R1";
var dosLine = @$"tcping.exe google.com 443";
proc.StandardInput.WriteLine(dosLine);
proc.StandardInput.WriteLine("exit");
while (proc.HasExited == false) proc.WaitForExit(1000);

var errorMsg = proc.StandardError.ReadToEnd();
var msg = proc.StandardOutput.ReadToEnd();
proc.StandardError.Dispose();
proc.StandardOutput.Dispose();
Console.WriteLine(string.IsNullOrEmpty(errorMsg) == false ? errorMsg : msg);


/*
// 3.linux 关机 

using System.Diagnostics;

var startInfo = new ProcessStartInfo
{
    FileName = "/usr/bin/sudo",
    Arguments = "/sbin/shutdown -h now"
};
using var process = new Process { StartInfo = startInfo };
process.Start();*/