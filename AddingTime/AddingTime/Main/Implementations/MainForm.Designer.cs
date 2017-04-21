namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.InputTextBox = new System.Windows.Forms.TextBox();
            this.EpisodesListBox = new System.Windows.Forms.ListBox();
            this.EpisodesFullTimeTextBox = new System.Windows.Forms.TextBox();
            this.EpisodesMiddleTimeTextBox = new System.Windows.Forms.TextBox();
            this.EpisodesShortTimeTextBox = new System.Windows.Forms.TextBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.ClearEpisodesButton = new System.Windows.Forms.Button();
            this.CopyEpisodesButton = new System.Windows.Forms.Button();
            this.CopyAllEpisodesButton = new System.Windows.Forms.Button();
            this.AddFromClipboardButton = new System.Windows.Forms.Button();
            this.RemoveEpisodeButton = new System.Windows.Forms.Button();
            this.MoveEpisodeButton = new System.Windows.Forms.Button();
            this.DiscsListBox = new System.Windows.Forms.ListBox();
            this.DiscsShortTimeTextBox = new System.Windows.Forms.TextBox();
            this.DiscsMiddleTimeTextBox = new System.Windows.Forms.TextBox();
            this.DiscsFullTimeTextBox = new System.Windows.Forms.TextBox();
            this.CopyAllDiscsButton = new System.Windows.Forms.Button();
            this.CopyDiscsButton = new System.Windows.Forms.Button();
            this.ClearDiscsButton = new System.Windows.Forms.Button();
            this.RemoveDiscButton = new System.Windows.Forms.Button();
            this.ClearAllButton = new System.Windows.Forms.Button();
            this.CopyFullDiscsButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ReadFromDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ReadMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CheckForUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputTextBox
            // 
            this.InputTextBox.Location = new System.Drawing.Point(12, 29);
            this.InputTextBox.Name = "InputTextBox";
            this.InputTextBox.Size = new System.Drawing.Size(150, 20);
            this.InputTextBox.TabIndex = 0;
            // 
            // EpisodesListBox
            // 
            this.EpisodesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.EpisodesListBox.FormattingEnabled = true;
            this.EpisodesListBox.Location = new System.Drawing.Point(12, 55);
            this.EpisodesListBox.Name = "EpisodesListBox";
            this.EpisodesListBox.Size = new System.Drawing.Size(150, 108);
            this.EpisodesListBox.TabIndex = 1;
            // 
            // EpisodesFullTimeTextBox
            // 
            this.EpisodesFullTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EpisodesFullTimeTextBox.Location = new System.Drawing.Point(12, 176);
            this.EpisodesFullTimeTextBox.Name = "EpisodesFullTimeTextBox";
            this.EpisodesFullTimeTextBox.ReadOnly = true;
            this.EpisodesFullTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.EpisodesFullTimeTextBox.TabIndex = 2;
            // 
            // EpisodesMiddleTimeTextBox
            // 
            this.EpisodesMiddleTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EpisodesMiddleTimeTextBox.Location = new System.Drawing.Point(12, 205);
            this.EpisodesMiddleTimeTextBox.Name = "EpisodesMiddleTimeTextBox";
            this.EpisodesMiddleTimeTextBox.ReadOnly = true;
            this.EpisodesMiddleTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.EpisodesMiddleTimeTextBox.TabIndex = 3;
            // 
            // EpisodesShortTimeTextBox
            // 
            this.EpisodesShortTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.EpisodesShortTimeTextBox.Location = new System.Drawing.Point(12, 234);
            this.EpisodesShortTimeTextBox.Name = "EpisodesShortTimeTextBox";
            this.EpisodesShortTimeTextBox.ReadOnly = true;
            this.EpisodesShortTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.EpisodesShortTimeTextBox.TabIndex = 4;
            // 
            // AddButton
            // 
            this.AddButton.Enabled = false;
            this.AddButton.Location = new System.Drawing.Point(168, 29);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(116, 23);
            this.AddButton.TabIndex = 5;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            // 
            // ClearEpisodesButton
            // 
            this.ClearEpisodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearEpisodesButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ClearEpisodesButton.Enabled = false;
            this.ClearEpisodesButton.Location = new System.Drawing.Point(168, 140);
            this.ClearEpisodesButton.Name = "ClearEpisodesButton";
            this.ClearEpisodesButton.Size = new System.Drawing.Size(116, 23);
            this.ClearEpisodesButton.TabIndex = 7;
            this.ClearEpisodesButton.Text = "Clear";
            this.ClearEpisodesButton.UseVisualStyleBackColor = true;
            // 
            // CopyEpisodesButton
            // 
            this.CopyEpisodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyEpisodesButton.Enabled = false;
            this.CopyEpisodesButton.Location = new System.Drawing.Point(168, 203);
            this.CopyEpisodesButton.Name = "CopyEpisodesButton";
            this.CopyEpisodesButton.Size = new System.Drawing.Size(116, 23);
            this.CopyEpisodesButton.TabIndex = 8;
            this.CopyEpisodesButton.Text = "Copy";
            this.CopyEpisodesButton.UseVisualStyleBackColor = true;
            // 
            // CopyAllEpisodesButton
            // 
            this.CopyAllEpisodesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyAllEpisodesButton.Enabled = false;
            this.CopyAllEpisodesButton.Location = new System.Drawing.Point(168, 232);
            this.CopyAllEpisodesButton.Name = "CopyAllEpisodesButton";
            this.CopyAllEpisodesButton.Size = new System.Drawing.Size(116, 23);
            this.CopyAllEpisodesButton.TabIndex = 9;
            this.CopyAllEpisodesButton.Text = "Copy All";
            this.CopyAllEpisodesButton.UseVisualStyleBackColor = true;
            // 
            // AddFromClipboardButton
            // 
            this.AddFromClipboardButton.Location = new System.Drawing.Point(168, 58);
            this.AddFromClipboardButton.Name = "AddFromClipboardButton";
            this.AddFromClipboardButton.Size = new System.Drawing.Size(116, 23);
            this.AddFromClipboardButton.TabIndex = 10;
            this.AddFromClipboardButton.Text = "Add from Clipboard";
            this.AddFromClipboardButton.UseVisualStyleBackColor = true;
            // 
            // RemoveEpisodeButton
            // 
            this.RemoveEpisodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveEpisodeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.RemoveEpisodeButton.Enabled = false;
            this.RemoveEpisodeButton.Location = new System.Drawing.Point(168, 111);
            this.RemoveEpisodeButton.Name = "RemoveEpisodeButton";
            this.RemoveEpisodeButton.Size = new System.Drawing.Size(116, 23);
            this.RemoveEpisodeButton.TabIndex = 11;
            this.RemoveEpisodeButton.Text = "Remove";
            this.RemoveEpisodeButton.UseVisualStyleBackColor = true;
            // 
            // MoveEpisodeButton
            // 
            this.MoveEpisodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MoveEpisodeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.MoveEpisodeButton.Enabled = false;
            this.MoveEpisodeButton.Location = new System.Drawing.Point(168, 174);
            this.MoveEpisodeButton.Name = "MoveEpisodeButton";
            this.MoveEpisodeButton.Size = new System.Drawing.Size(116, 23);
            this.MoveEpisodeButton.TabIndex = 12;
            this.MoveEpisodeButton.Text = "Move to Right List";
            this.MoveEpisodeButton.UseVisualStyleBackColor = true;
            // 
            // DiscsListBox
            // 
            this.DiscsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscsListBox.FormattingEnabled = true;
            this.DiscsListBox.Location = new System.Drawing.Point(290, 55);
            this.DiscsListBox.Name = "DiscsListBox";
            this.DiscsListBox.Size = new System.Drawing.Size(150, 108);
            this.DiscsListBox.TabIndex = 13;
            // 
            // DiscsShortTimeTextBox
            // 
            this.DiscsShortTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscsShortTimeTextBox.Location = new System.Drawing.Point(290, 234);
            this.DiscsShortTimeTextBox.Name = "DiscsShortTimeTextBox";
            this.DiscsShortTimeTextBox.ReadOnly = true;
            this.DiscsShortTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.DiscsShortTimeTextBox.TabIndex = 16;
            // 
            // DiscsMiddleTimeTextBox
            // 
            this.DiscsMiddleTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscsMiddleTimeTextBox.Location = new System.Drawing.Point(290, 205);
            this.DiscsMiddleTimeTextBox.Name = "DiscsMiddleTimeTextBox";
            this.DiscsMiddleTimeTextBox.ReadOnly = true;
            this.DiscsMiddleTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.DiscsMiddleTimeTextBox.TabIndex = 15;
            // 
            // DiscsFullTimeTextBox
            // 
            this.DiscsFullTimeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscsFullTimeTextBox.Location = new System.Drawing.Point(290, 176);
            this.DiscsFullTimeTextBox.Name = "DiscsFullTimeTextBox";
            this.DiscsFullTimeTextBox.ReadOnly = true;
            this.DiscsFullTimeTextBox.Size = new System.Drawing.Size(150, 20);
            this.DiscsFullTimeTextBox.TabIndex = 14;
            // 
            // CopyAllDiscsButton
            // 
            this.CopyAllDiscsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyAllDiscsButton.Enabled = false;
            this.CopyAllDiscsButton.Location = new System.Drawing.Point(446, 232);
            this.CopyAllDiscsButton.Name = "CopyAllDiscsButton";
            this.CopyAllDiscsButton.Size = new System.Drawing.Size(116, 23);
            this.CopyAllDiscsButton.TabIndex = 18;
            this.CopyAllDiscsButton.Text = "Copy All";
            this.CopyAllDiscsButton.UseVisualStyleBackColor = true;
            // 
            // CopyDiscsButton
            // 
            this.CopyDiscsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyDiscsButton.Enabled = false;
            this.CopyDiscsButton.Location = new System.Drawing.Point(446, 203);
            this.CopyDiscsButton.Name = "CopyDiscsButton";
            this.CopyDiscsButton.Size = new System.Drawing.Size(116, 23);
            this.CopyDiscsButton.TabIndex = 17;
            this.CopyDiscsButton.Text = "Copy";
            this.CopyDiscsButton.UseVisualStyleBackColor = true;
            // 
            // ClearDiscsButton
            // 
            this.ClearDiscsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearDiscsButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ClearDiscsButton.Enabled = false;
            this.ClearDiscsButton.Location = new System.Drawing.Point(446, 140);
            this.ClearDiscsButton.Name = "ClearDiscsButton";
            this.ClearDiscsButton.Size = new System.Drawing.Size(116, 23);
            this.ClearDiscsButton.TabIndex = 19;
            this.ClearDiscsButton.Text = "Clear";
            this.ClearDiscsButton.UseVisualStyleBackColor = true;
            // 
            // RemoveDiscButton
            // 
            this.RemoveDiscButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RemoveDiscButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.RemoveDiscButton.Enabled = false;
            this.RemoveDiscButton.Location = new System.Drawing.Point(446, 111);
            this.RemoveDiscButton.Name = "RemoveDiscButton";
            this.RemoveDiscButton.Size = new System.Drawing.Size(116, 23);
            this.RemoveDiscButton.TabIndex = 20;
            this.RemoveDiscButton.Text = "Remove";
            this.RemoveDiscButton.UseVisualStyleBackColor = true;
            // 
            // ClearAllButton
            // 
            this.ClearAllButton.Enabled = false;
            this.ClearAllButton.Location = new System.Drawing.Point(446, 27);
            this.ClearAllButton.Name = "ClearAllButton";
            this.ClearAllButton.Size = new System.Drawing.Size(116, 23);
            this.ClearAllButton.TabIndex = 22;
            this.ClearAllButton.Text = "Clear All";
            this.ClearAllButton.UseVisualStyleBackColor = true;
            // 
            // CopyFullDiscsButton
            // 
            this.CopyFullDiscsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CopyFullDiscsButton.Enabled = false;
            this.CopyFullDiscsButton.Location = new System.Drawing.Point(446, 261);
            this.CopyFullDiscsButton.Name = "CopyFullDiscsButton";
            this.CopyFullDiscsButton.Size = new System.Drawing.Size(116, 23);
            this.CopyFullDiscsButton.TabIndex = 23;
            this.CopyFullDiscsButton.Text = "Copy Full";
            this.CopyFullDiscsButton.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadFromDriveToolStripMenuItem,
            this.HelpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(572, 24);
            this.menuStrip1.TabIndex = 24;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ReadFromDriveToolStripMenuItem
            // 
            this.ReadFromDriveToolStripMenuItem.Name = "ReadFromDriveToolStripMenuItem";
            this.ReadFromDriveToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.ReadFromDriveToolStripMenuItem.Text = "Read from Drive";
            // 
            // helpToolStripMenuItem
            // 
            this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReadMeToolStripMenuItem,
            this.CheckForUpdateToolStripMenuItem,
            this.AboutToolStripMenuItem});
            this.HelpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.HelpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpToolStripMenuItem.Text = "&Help";
            // 
            // ReadMeToolStripMenuItem
            // 
            this.ReadMeToolStripMenuItem.Name = "ReadMeToolStripMenuItem";
            this.ReadMeToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.ReadMeToolStripMenuItem.Text = "&Read Me";
            // 
            // CheckForUpdateToolStripMenuItem
            // 
            this.CheckForUpdateToolStripMenuItem.Name = "CheckForUpdateToolStripMenuItem";
            this.CheckForUpdateToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.CheckForUpdateToolStripMenuItem.Text = "&Check for Update";
            // 
            // AboutToolStripMenuItem
            // 
            this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
            this.AboutToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.AboutToolStripMenuItem.Text = "&About";
            // 
            // MainForm
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ClearEpisodesButton;
            this.ClientSize = new System.Drawing.Size(572, 295);
            this.Controls.Add(this.CopyFullDiscsButton);
            this.Controls.Add(this.ClearAllButton);
            this.Controls.Add(this.RemoveDiscButton);
            this.Controls.Add(this.ClearDiscsButton);
            this.Controls.Add(this.CopyAllDiscsButton);
            this.Controls.Add(this.CopyDiscsButton);
            this.Controls.Add(this.DiscsShortTimeTextBox);
            this.Controls.Add(this.DiscsMiddleTimeTextBox);
            this.Controls.Add(this.DiscsFullTimeTextBox);
            this.Controls.Add(this.DiscsListBox);
            this.Controls.Add(this.MoveEpisodeButton);
            this.Controls.Add(this.RemoveEpisodeButton);
            this.Controls.Add(this.AddFromClipboardButton);
            this.Controls.Add(this.CopyAllEpisodesButton);
            this.Controls.Add(this.CopyEpisodesButton);
            this.Controls.Add(this.ClearEpisodesButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.EpisodesShortTimeTextBox);
            this.Controls.Add(this.EpisodesMiddleTimeTextBox);
            this.Controls.Add(this.EpisodesFullTimeTextBox);
            this.Controls.Add(this.EpisodesListBox);
            this.Controls.Add(this.InputTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(588, 960);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(588, 334);
            this.Name = "MainForm";
            this.Text = "AddingTime";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InputTextBox;
        private System.Windows.Forms.ListBox EpisodesListBox;
        private System.Windows.Forms.TextBox EpisodesFullTimeTextBox;
        private System.Windows.Forms.TextBox EpisodesMiddleTimeTextBox;
        private System.Windows.Forms.TextBox EpisodesShortTimeTextBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button ClearEpisodesButton;
        private System.Windows.Forms.Button CopyEpisodesButton;
        private System.Windows.Forms.Button CopyAllEpisodesButton;
        private System.Windows.Forms.Button AddFromClipboardButton;
        private System.Windows.Forms.Button RemoveEpisodeButton;
        private System.Windows.Forms.Button MoveEpisodeButton;
        private System.Windows.Forms.ListBox DiscsListBox;
        private System.Windows.Forms.TextBox DiscsShortTimeTextBox;
        private System.Windows.Forms.TextBox DiscsMiddleTimeTextBox;
        private System.Windows.Forms.TextBox DiscsFullTimeTextBox;
        private System.Windows.Forms.Button CopyAllDiscsButton;
        private System.Windows.Forms.Button CopyDiscsButton;
        private System.Windows.Forms.Button ClearDiscsButton;
        private System.Windows.Forms.Button RemoveDiscButton;
        private System.Windows.Forms.Button ClearAllButton;
        private System.Windows.Forms.Button CopyFullDiscsButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReadMeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CheckForUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ReadFromDriveToolStripMenuItem;
    }
}

