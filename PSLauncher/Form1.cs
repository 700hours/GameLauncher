using cotf;
using cotf.Assets;
using cotf.Base;
using System;
using System.Drawing;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Numerics;
using System.Windows.Forms;
using PSLauncher.Properties;
namespace PSLauncher.cotf
{
    public partial class Form1 : Form
    {
        int size = 96;
        int num = 480;
        int index = 0;
        bool render = false;
        internal static Form1 Instance;

        public Form1()
        {
            InitializeComponent();
            Instance = this;
            if (Settings.Default.PSPath == string.Empty)
            { 
                if (MessageBox.Show("Please locate the Planetside directory.", "Directory Notice", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    Close();
                }
                else
                {
                    FolderBrowserDialog dialog = new FolderBrowserDialog();
                    dialog.ShowDialog(this);
                    Settings.Default.PSPath = dialog.SelectedPath;
                    Settings.Default.Save();
                }
            }
            new LauncherForm();
            LauncherForm.Instance.setErrorMessage("Awaiting input", Status.None);
            LauncherForm.Instance.loadServerSelection();
            launch.ForeColor = Color.Green;
            Initialize();
            Render();
            richTextBox1.Lines = LoadNews();
        }
        private string[] LoadNews()
        {
            return new string[]
            {
                "Welcome to the Cotf Launcher",
                "",
                "News",
                "January 22nd",
                "This is an alternative GUI launcher.",
                "",
                "Enjoy your stay."
            };
        }
    
        private void Render()
        {
            Lamp.NewLamp(20, num / 2, 300, Lamp.RandomLight(), true);
            Lamp.NewLamp(num - 20, 80, 300, Lamp.RandomLight(), true);
            Lamp.NewLamp(num - 20, 400, 300, Lamp.RandomLight(), true);
            Tile.GetTile(size, size, new Size(size, size)).active = true;
            Tile.GetTile(size, size * 3, new Size(size, size)).active = true;
            Tile.GetTile(size * 3, size, new Size(size, size)).active = true;
            Tile.GetTile(size * 3, size * 3, new Size(size, size)).active = true;
            Image input = pictureBox1.Image = new Bitmap(num, num);
            Lib.Render(ref input);
            pictureBox1.Image = input;
        }

        private void Initialize()
        {
            Lib.SetDimensions(num, num);
            Texture.SplitImage(Asset<Bitmap>.Request(".\\Textures\\JF_14thtex", ".png"), new Size(size, size), "background");
            Texture.GenerateColorTextureFiles("tile", Color.DarkGray, new Size(size, size));
            Lib.Initialize(4, new Size(size, size));
            Lib.InitArray();
            foreach (var item in Lib.tile)
            {
                item.active = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Lib.UnloadAll();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void launch_Click(object sender, EventArgs e)
        {
            LauncherForm.Instance.launchGame_Click(sender, e);
        }

        private void about_Click(object sender, EventArgs e)
        {
            About a = new About();
            a.StartPosition = FormStartPosition.CenterParent;
            a.ShowDialog(this);
        }

        private void settings_Click(object sender, EventArgs e)
        {
            SettingsForm a = new SettingsForm();
            a.StartPosition = FormStartPosition.CenterParent;
            a.ShowDialog(this);
        }

        private void server_list_Click(object sender, EventArgs e)
        {
            ServerList a = new ServerList();
            a.StartPosition = FormStartPosition.CenterParent;
            a.ShowDialog(this);
            LauncherForm.Instance.loadServerSelection();
        }

        private void serverSelectionChanged(object sender, EventArgs e)
        {
            LauncherForm.Instance.serverSelection_SelectedIndexChanged(sender, e);
        }

        private void formClosing(object sender, FormClosingEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
