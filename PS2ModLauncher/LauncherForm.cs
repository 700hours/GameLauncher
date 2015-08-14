﻿using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using PSLauncher.Properties;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSLauncher
{

    public enum LaunchDomain
    {
        Live
    }

    public enum GameState
    {
        Stopped,
        Launching,
        Running
    }

    public partial class LauncherForm : Form
    {
        Process psProcess;
        string USER_AGENT = "Mozilla/5.0 (Windows NT 6.1; rv:31.0) Gecko/20100101 Firefox/31.0";
        static string PS_EXE_NAME = "planetside.exe";
        int DEFAULT_WEB_TIMEOUT = 5000;
        string planetsidePath = "";
        bool planetsidePathValid = false;
        GameState gameState = GameState.Stopped;

        LaunchDomain domain = LaunchDomain.Live;

        Dictionary<LaunchDomain, string> domains = new Dictionary<LaunchDomain, string>()
        {
            { LaunchDomain.Live, "https://lp.soe.com/ps/live" }
        };

        public LauncherForm()
        {
            InitializeComponent();
            
            string psDefault = getDefaultDirectory();
            
            // first run with no settings or invalid starting path
            if (Settings.Default.PSPath == "" || !checkDirForPlanetSide(Settings.Default.PSPath))
            {
                // try setting the path. It may not be right, but this initializes state
                setPlanetSidePath(Path.Combine(psDefault, PS_EXE_NAME), false);
                Settings.Default.PSPath = psDefault;
            }
            else
            {
                setPlanetSidePath(Path.Combine(Settings.Default.PSPath, PS_EXE_NAME), false);
            }

            planetside2PathTextField.Text = Settings.Default.PSPath;
            launchArgs.Text = Settings.Default.ExtraArgs;
            loggingCheckBox.Checked = Settings.Default.Logging;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // set the starting path for the dialog
            if (planetside2PathTextField.Text != "")
                findPTRDirDialogue.SelectedPath = planetside2PathTextField.Text;

            DialogResult r = findPTRDirDialogue.ShowDialog();

            if (r == DialogResult.OK)
            {
                // combine the folder name with the standard PS.exe name
                string psPath = Path.Combine(findPTRDirDialogue.SelectedPath, PS_EXE_NAME);

                planetside2PathTextField.Text = findPTRDirDialogue.SelectedPath;
                setPlanetSidePath(psPath);
                
                Settings.Default.PSPath = findPTRDirDialogue.SelectedPath;
            }
        }

        private bool setPlanetSidePath(string path, bool alert = true)
        {
            planetsidePath = path;
            planetsidePathValid = false;

            if(!File.Exists(path))
            {
                planetsideVersion.Text = "Not found";
                planetsideVersion.ForeColor = System.Drawing.Color.Red;

                if(alert)
                MessageBox.Show("Cannot open " + PS_EXE_NAME + " (check the selected directory)",
                       "Cannot Find Executable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return planetsidePathValid;
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(path);

            planetsidePathValid = true;

            if (versionInfo.FileVersion != "")
            {
                planetsideVersion.Text = "Version " + versionInfo.FileVersion;
                planetsideVersion.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                planetsideVersion.Text = "Unknown version";
                planetsideVersion.ForeColor = System.Drawing.Color.Yellow;
            }

            return planetsidePathValid;
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void stopLaunching()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.launchGame.Enabled = true;
                    this.launchGame.Text = "Launch";
                    this.launchProgress.Visible = false;
                });
            }
            else
            {
                this.launchGame.Enabled = true;
                this.launchGame.Text = "Launch";
                this.launchProgress.Visible = false;
            }

            setProgress(0);

            gameState = GameState.Stopped;
        }

        private void startLaunching()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.launchGame.Enabled = false;
                    this.launchGame.Text = "Launching...";
                    this.launchProgress.Visible = true;
                    setProgress(0);
                });
            }
            else
            {
                this.launchGame.Enabled = false;
                this.launchGame.Text = "Launching...";
                this.launchProgress.Visible = true;
                setProgress(0);
            }

            gameState = GameState.Launching;
        }

        private void setProgress(int prog)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.launchProgress.Value = prog;
                });
            }
            else
            {
                this.launchProgress.Value = prog;
            }
        }

        private void setErrorMessage(string error)
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (error == "")
                    {
                        this.launchMessage.Visible = false;
                        return;
                    }

                    this.launchMessage.Visible = true;
                    this.launchMessage.Text = error;
                });
            }
            else
            {
                if (error == "")
                {
                    this.launchMessage.Visible = false;
                    return;
                }

                this.launchMessage.Visible = true;
                this.launchMessage.Text = error;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startLaunching();
            setErrorMessage("");

            string path = planetside2PathTextField.Text;
            string psExe = Path.Combine(path, PS_EXE_NAME);

            if (!skipLauncher.Checked && (username.Text == String.Empty || password.Text == String.Empty))
            {
                setErrorMessage("Username or password blank");
                stopLaunching();
                return;
            }

            if (!planetsidePathValid)
            {
                setErrorMessage("Invalid planetside exe");
                stopLaunching();
                return;
            }

            if (String.IsNullOrWhiteSpace(launchArgs.Text))
                launchArgs.Text = "";

            //And the extra launch args
            if (Settings.Default.ExtraArgs != launchArgs.Text)
            {
                Settings.Default.ExtraArgs = this.launchArgs.Text;
            }

            if (skipLauncher.Checked)
            {
                // magic string to login to planetside from the actual game
                startPlanetSide(planetsidePath, Path.GetDirectoryName(planetsidePath), "/K:StagingTest " + launchArgs.Text);
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    this.doLogin();
                });
            }
        }

        void doLogin()
        {
            long ts = (long)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            string path = planetside2PathTextField.Text;
            string psExe = Path.Combine(path, PS_EXE_NAME);

            /////////////////////////////////////////////////////////////////
            // Step 1: Establish Session ID
            /////////////////////////////////////////////////////////////////

            String endpoint = domains[domain];
            CookieContainer reqCookies = new CookieContainer();
            HttpWebRequest req = WebRequest.Create(endpoint + "/?t=43323") as HttpWebRequest;
            req.CookieContainer = reqCookies;
            req.CookieContainer.MaxCookieSize = 4000;
            req.Method = "GET";
            req.UserAgent = USER_AGENT;
            req.Timeout = DEFAULT_WEB_TIMEOUT;

            HttpWebResponse r;
            try
            {
                r = req.GetResponse() as HttpWebResponse;
            }
            catch (WebException x)
            {
                addLine("Failed to gather initial session: " + x.Message);
                stopLaunching();
                return;
            }

            // Note: we must manually add secure cookies and CookieContainer is crap
            // See http://thomaskrehbiel.com/post/1690-cookiecontainer_httpwebrequest_and_secure_cookies/

            reqCookies.Add(new Uri(endpoint), r.Cookies);

            addLine("PSWeb: session started");
            r.Close();
            setProgress(25);

            /////////////////////////////////////////////////////////////////
            // Step 2: Try logging in
            /////////////////////////////////////////////////////////////////

            req = WebRequest.Create(endpoint + "/login?t=43323") as HttpWebRequest;
            req.CookieContainer = reqCookies;
            req.Method = "POST";
            req.UserAgent = USER_AGENT;
            req.Timeout = DEFAULT_WEB_TIMEOUT;
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Headers.Add("Origin", "https://lp.soe.com");

            req.ContentType = "application/x-www-form-urlencoded";
            req.Referer = endpoint + "/?t=43323";

            NameValueCollection query = new NameValueCollection();
            query.Add("username", username.Text);
            query.Add("password", password.Text);
            query.Add("rememberPassword", "false");
            query.Add("ts", ts.ToString());

            var postdata = Encoding.ASCII.GetBytes(query.ToQueryString());
            //addLine(query.ToQueryString());

            req.ContentLength = postdata.Length;

            using (var stream = req.GetRequestStream())
            {
                stream.Write(postdata, 0, postdata.Length);
            }

            try
            {
                r = req.GetResponse() as HttpWebResponse;
            }
            catch (WebException x)
            {
                string txt;

                using (HttpWebResponse respExcept = (HttpWebResponse)x.Response)
                { 
                    if (respExcept != null && respExcept.GetResponseStream().CanRead)
                    {
                        StreamReader r2 = new StreamReader(respExcept.GetResponseStream());
                        txt = r2.ReadToEnd();
                        respExcept.Close();
                    }
                    else
                    {
                        txt = "";
                        addLine("Login failed: " + x.Message);
                        return;
                    }
                }

                string errorDetail = "";

                try
                {
                    JObject obj2 = JObject.Parse(txt);
                    errorDetail = (string)obj2["errorDetail"];
                }
                catch (Newtonsoft.Json.JsonException x2)
                {
                    errorDetail = "Json parse error: " + x2.Message;
                }

                if (errorDetail == "INVALID_ACCOUNT_ID")
                {
                    setErrorMessage("Unknown username");
                }
                else if (errorDetail == "RESET_ACCOUNT_PASSWORD")
                {
                    setErrorMessage("Your account needs a password reset");
                }
                else if (errorDetail == "PASSWORD_MISMATCH")
                {
                    setErrorMessage("Bad password");
                }
                else // unrecognized!
                {
                    setErrorMessage("Unknown error - see output window");
                    addLine("Login failure: " + x.Status);
                    addLine("Error: " + errorDetail);
                    addLine(txt);
                }

                stopLaunching();
                return;
            }

            if (!r.GetResponseStream().CanRead)
            {
                setErrorMessage("Unknown error - see output window");
                addLine("No login response received");
                addLine("Status: " + r.StatusCode);
                stopLaunching();
                return;
            }

            StreamReader reader = new StreamReader(r.GetResponseStream());
            string text = reader.ReadToEnd();

            //addLine(r.Headers["Set-Cookie"]);
            reqCookies.Add(new Uri(endpoint), r.Cookies);

            string result = "";
            r.Close();
            addLine("PSWeb: logged in");
            setProgress(50);

            try
            {
                JObject obj = JObject.Parse(text);
                result = (string)obj["result"];
            }
            catch (Newtonsoft.Json.JsonException x2)
            {
                result = "Json parse error: " + x2.Message;
            }

            if (result != "SUCCESS")
            {
                setErrorMessage("Unknown error - see output window");
                addLine("Bad login response: " + result);
                addLine("Status: " + r.StatusCode);
                addLine(text);
                stopLaunching();
                return;
            }
            

            /////////////////////////////////////////////////////////////////
            // Step 3: Fetch the login token
            /////////////////////////////////////////////////////////////////

            req = WebRequest.Create(endpoint + "/get_play_session?t=43323") as HttpWebRequest;
            req.CookieContainer = reqCookies;
            req.Method = "GET";
            req.UserAgent = USER_AGENT;
            req.Timeout = DEFAULT_WEB_TIMEOUT;
            req.Headers.Add("Origin", "https://lp.soe.com");
            req.Referer = endpoint + "/login?t=43323";
            req.Headers.Add("X-Requested-With", "XMLHttpRequest");
            req.Accept = "*/*";

            try
            {
                r = req.GetResponse() as HttpWebResponse;
            }
            catch (WebException x)
            {
                string txt;

                using (HttpWebResponse respExcept = (HttpWebResponse)x.Response)
                {
                    if (respExcept != null && respExcept.GetResponseStream().CanRead)
                    {
                        StreamReader r2 = new StreamReader(respExcept.GetResponseStream());
                        txt = r2.ReadToEnd();
                    }
                    else
                    {
                        txt = "";
                    }
                }

                string errorDetail = "";

                try
                {
                    JObject obj2 = JObject.Parse(txt);
                    errorDetail = (string)obj2["result"];
                }
                catch (Newtonsoft.Json.JsonException x2)
                {
                    errorDetail = "Json parse error: " + x2.Message;
                }

                if (errorDetail == "RE_LOGIN")
                {
                    setErrorMessage("Failed to fetch token: bad login");
                }
                else // unrecognized!
                {
                    setErrorMessage("Unknown error - see output window");

                }

                addLine("Get token failure: " + x.Status);
                addLine("Error: " + errorDetail);
                addLine(txt);
                stopLaunching();
                return;
            }

            if (!r.GetResponseStream().CanRead)
            {
                setErrorMessage("Unknown error - see output window");
                addLine("No login response received");
                addLine("Status: " + r.StatusCode);
                stopLaunching();
                return;
            }

            reader = new StreamReader(r.GetResponseStream());
            text = reader.ReadToEnd();

            result = "";
            r.Close();
            setProgress(75);

            string token = "";

            try
            {
                JObject obj = JObject.Parse(text);
                result = (string)obj["result"];
                token = (string)obj["launch_args"];
            }
            catch (Newtonsoft.Json.JsonException x2)
            {
                result = "Json parse error: " + x2.Message;
                token = "";
            }

            if (result != "SUCCESS")
            {
                setErrorMessage("Failed to get token");
                addLine("Bad token response: " + result);
                addLine("Status: " + r.StatusCode);
                addLine(text);
                stopLaunching();
                return;
            }

            addLine("PSWeb: got launch args " + token);

            string launch_args = token;
            string ExtraLaunchArgs = launchArgs.Text;

            if (ExtraLaunchArgs != String.Empty)
                launch_args += " " + ExtraLaunchArgs;

            startPlanetSide(psExe, path, launch_args);

            setProgress(100);
            return;
        }

        void startPlanetSide(string exe, string workingDir, string args)
        {
            psProcess = new Process();

            psProcess.StartInfo.WorkingDirectory = workingDir; // TODO: should this be where the launcher is for logging?
            psProcess.StartInfo.FileName = exe;
            psProcess.StartInfo.Arguments = args;
            psProcess.StartInfo.RedirectStandardOutput = true;
            psProcess.StartInfo.RedirectStandardError = true;
            psProcess.StartInfo.UseShellExecute = false;
            psProcess.Exited += new EventHandler(ps_Exited);
            psProcess.OutputDataReceived += new DataReceivedEventHandler(ps_OutputDataReceived);
            psProcess.EnableRaisingEvents = true;

            addLine("ProcessStart: \"" + exe + "\" " + args);
            psProcess.Start();

            psProcess.BeginErrorReadLine();
            psProcess.BeginOutputReadLine();
            

            gameRunning();
        }

        void ps_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if(e != null)
            {
                addLine(e.Data);
            }
        }

        void addLine(String line)
        {
            if(this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    ps_consoleOutput.AppendText(line + Environment.NewLine);
                });
            }
            else
            {
                ps_consoleOutput.AppendText(line + Environment.NewLine);
            }
        }

        void gameRunning()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    this.launchGame.Enabled = false;
                    this.launchGame.Text = "Running";
                });
            }
            else
            {
                this.launchGame.Enabled = false;
                this.launchGame.Text = "Running";
            }

            gameState = GameState.Running;
        }

        void gameStopped()
        {
            this.stopLaunching();
        }

        void ps_Exited(object sender, EventArgs e)
        {
            gameStopped();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private static string getDefaultDirectory()
        {
            RegistryKey key = null;
            string psFolder = "";

            // non-steam install
            key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\App Paths\LaunchPad.exe");

            if (key != null && key.GetValue("") != null)
            {
                String defaultDirectory;
                defaultDirectory = key.GetValue("").ToString();
                defaultDirectory = Path.GetDirectoryName(defaultDirectory);

                // verify that we aren't mistakingly returning a PlanetSide 2 directory...
                if (Directory.Exists(defaultDirectory) && checkDirForPlanetSide(defaultDirectory))
                    return defaultDirectory;

                // try to go up a directory and find the PlanetSide folder
                string upOne = Directory.GetParent(defaultDirectory).FullName;
                psFolder = Path.Combine(upOne, "Planetside");

                if (Directory.Exists(psFolder) && checkDirForPlanetSide(psFolder))
                    return psFolder;
            }
            
            // worth a shot!
            psFolder = Path.Combine(ProgramFilesx86(), "Sony\\PlanetSide");

            if (Directory.Exists(psFolder) && checkDirForPlanetSide(psFolder))
                return psFolder;

            // HACK: our last attempt. Should work on Win7 and above with and updated launcher
            psFolder = "C:\\Users\\Public\\Sony Online Entertainment\\Installed Games\\Planetside";

            if (Directory.Exists(psFolder) && checkDirForPlanetSide(psFolder))
                return psFolder;

            // give up
            return "";
        }
        
        private static bool checkDirForPlanetSide(string dir)
        {
            return File.Exists(Path.Combine(dir, PS_EXE_NAME));
        }

        private static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        private void loggingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Logging = loggingCheckBox.Checked;
        }

        private void loginWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.ShowDialog(this);
        }

        private void loginFormChanged(object sender, EventArgs e)
        {
            if(gameState == GameState.Stopped)
            {
                if (username.Text.Length > 0 && password.Text.Length > 0 || skipLauncher.Checked)
                    launchGame.Enabled = true;
                else
                    launchGame.Enabled = false;
            }

            if (skipLauncher.Checked)
            {
                username.Enabled = false;
                password.Enabled = false;
            }
            else
            {
                username.Enabled = true;
                password.Enabled = true;
            }
        }
    }

    public static class QueryExtensions
    {
        public static string ToQueryString(this NameValueCollection nvc)
        {
            IEnumerable<string> segments = from key in nvc.AllKeys
                                           from value in nvc.GetValues(key)
                                           select string.Format("{0}={1}", key, value);
            return string.Join("&", segments);
        }
    }
}
