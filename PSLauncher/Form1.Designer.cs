using System.Drawing;
using System.Windows.Forms;

namespace PSLauncher.cotf
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.exit = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.log = new System.Windows.Forms.RichTextBox();
            this.label_status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.serverSelection = new System.Windows.Forms.ComboBox();
            this.settings = new System.Windows.Forms.Button();
            this.launch = new System.Windows.Forms.Button();
            this.add_server = new System.Windows.Forms.Button();
            this.about = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.DimGray;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.CausesValidation = false;
            this.richTextBox1.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox1.Location = new System.Drawing.Point(498, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox1.Size = new System.Drawing.Size(164, 288);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Transparent;
            this.exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.exit.Enabled = false;
            this.exit.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.exit.FlatAppearance.BorderSize = 2;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F);
            this.exit.ForeColor = System.Drawing.SystemColors.ControlText;
            this.exit.Location = new System.Drawing.Point(12, 464);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(39, 26);
            this.exit.TabIndex = 14;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(94, 26);
            this.contextMenuStrip1.Text = "Menu";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // log
            // 
            this.log.BackColor = System.Drawing.Color.Silver;
            this.log.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.log.CausesValidation = false;
            this.log.Font = new System.Drawing.Font("Cascadia Code", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.log.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.log.Location = new System.Drawing.Point(498, 306);
            this.log.Name = "log";
            this.log.ReadOnly = true;
            this.log.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.log.Size = new System.Drawing.Size(164, 134);
            this.log.TabIndex = 15;
            this.log.Text = "";
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.Location = new System.Drawing.Point(498, 471);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(37, 13);
            this.label_status.TabIndex = 16;
            this.label_status.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(498, 449);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 28;
            this.label1.Text = "Server";
            // 
            // serverSelection
            // 
            this.serverSelection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serverSelection.FormattingEnabled = true;
            this.serverSelection.Location = new System.Drawing.Point(541, 446);
            this.serverSelection.Name = "serverSelection";
            this.serverSelection.Size = new System.Drawing.Size(121, 21);
            this.serverSelection.TabIndex = 27;
            this.serverSelection.SelectedIndexChanged += new System.EventHandler(this.serverSelectionChanged);
            // 
            // settings
            // 
            this.settings.BackColor = System.Drawing.Color.DimGray;
            this.settings.BackgroundImage = global::PSLauncher.Properties.Resources.bluepane;
            this.settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.settings.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.settings.FlatAppearance.BorderSize = 2;
            this.settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settings.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.settings.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.settings.Image = ((System.Drawing.Image)(resources.GetObject("settings.Image")));
            this.settings.Location = new System.Drawing.Point(108, 300);
            this.settings.Name = "settings";
            this.settings.Size = new System.Drawing.Size(96, 96);
            this.settings.TabIndex = 12;
            this.settings.Text = "Settings";
            this.settings.UseVisualStyleBackColor = false;
            this.settings.Click += new System.EventHandler(this.settings_Click);
            // 
            // launch
            // 
            this.launch.BackColor = System.Drawing.Color.DimGray;
            this.launch.BackgroundImage = global::PSLauncher.Properties.Resources.bluepane;
            this.launch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.launch.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.launch.FlatAppearance.BorderSize = 2;
            this.launch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.launch.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.launch.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.launch.Image = ((System.Drawing.Image)(resources.GetObject("launch.Image")));
            this.launch.Location = new System.Drawing.Point(108, 107);
            this.launch.Name = "launch";
            this.launch.Size = new System.Drawing.Size(96, 96);
            this.launch.TabIndex = 11;
            this.launch.Text = "Launch";
            this.launch.UseVisualStyleBackColor = false;
            this.launch.Click += new System.EventHandler(this.launch_Click);
            // 
            // add_server
            // 
            this.add_server.BackColor = System.Drawing.Color.DimGray;
            this.add_server.BackgroundImage = global::PSLauncher.Properties.Resources.bluepane;
            this.add_server.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.add_server.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.add_server.FlatAppearance.BorderSize = 2;
            this.add_server.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_server.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.add_server.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.add_server.Image = ((System.Drawing.Image)(resources.GetObject("add_server.Image")));
            this.add_server.Location = new System.Drawing.Point(300, 107);
            this.add_server.Name = "add_server";
            this.add_server.Size = new System.Drawing.Size(96, 96);
            this.add_server.TabIndex = 10;
            this.add_server.Text = "Server List";
            this.add_server.UseVisualStyleBackColor = false;
            this.add_server.Click += new System.EventHandler(this.server_list_Click);
            // 
            // about
            // 
            this.about.BackColor = System.Drawing.Color.DimGray;
            this.about.BackgroundImage = global::PSLauncher.Properties.Resources.bluepane;
            this.about.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.about.FlatAppearance.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.about.FlatAppearance.BorderSize = 2;
            this.about.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.about.Font = new System.Drawing.Font("Franklin Gothic Medium", 12F);
            this.about.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.about.Image = ((System.Drawing.Image)(resources.GetObject("about.Image")));
            this.about.Location = new System.Drawing.Point(300, 300);
            this.about.Name = "about";
            this.about.Size = new System.Drawing.Size(96, 96);
            this.about.TabIndex = 7;
            this.about.Text = "About";
            this.about.UseVisualStyleBackColor = false;
            this.about.Click += new System.EventHandler(this.about_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(480, 480);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.CancelButton = this.exit;
            this.ClientSize = new System.Drawing.Size(673, 502);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.serverSelection);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.log);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.settings);
            this.Controls.Add(this.launch);
            this.Controls.Add(this.add_server);
            this.Controls.Add(this.about);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Cotf Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private RichTextBox richTextBox1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;
        internal RichTextBox log;
        internal Label label_status;
        private Label label1;
        internal ComboBox serverSelection;
        internal Button launch;
        internal Button about;
        internal Button add_server;
        internal Button settings;
        internal Button exit;
    }
}
