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

Console.ReadKey();