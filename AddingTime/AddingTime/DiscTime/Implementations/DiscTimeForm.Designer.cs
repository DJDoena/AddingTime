namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    partial class DiscTimeForm
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
            if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiscTimeForm));
            this.DriveComboBox = new System.Windows.Forms.ComboBox();
            this.ScanButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DiscTreeView = new System.Windows.Forms.TreeView();
            this.MinimumTrackLengthUpDown = new System.Windows.Forms.NumericUpDown();
            this.SitcomButton = new System.Windows.Forms.Button();
            this.DramaButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectAllButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.MovieButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumTrackLengthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // DriveComboBox
            // 
            this.DriveComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DriveComboBox.FormattingEnabled = true;
            this.DriveComboBox.Location = new System.Drawing.Point(53, 14);
            this.DriveComboBox.Name = "DriveComboBox";
            this.DriveComboBox.Size = new System.Drawing.Size(281, 21);
            this.DriveComboBox.TabIndex = 1;
            // 
            // ScanButton
            // 
            this.ScanButton.Enabled = false;
            this.ScanButton.Location = new System.Drawing.Point(340, 70);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(75, 23);
            this.ScanButton.TabIndex = 7;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;            
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Drive:";
            // 
            // DiscTreeView
            // 
            this.DiscTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DiscTreeView.CheckBoxes = true;
            this.DiscTreeView.Location = new System.Drawing.Point(15, 70);
            this.DiscTreeView.Name = "DiscTreeView";
            this.DiscTreeView.Size = new System.Drawing.Size(319, 289);
            this.DiscTreeView.TabIndex = 8;
            // 
            // MinimumTrackLengthUpDown
            // 
            this.MinimumTrackLengthUpDown.Location = new System.Drawing.Point(187, 44);
            this.MinimumTrackLengthUpDown.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.MinimumTrackLengthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinimumTrackLengthUpDown.Name = "MinimumTrackLengthUpDown";
            this.MinimumTrackLengthUpDown.Size = new System.Drawing.Size(66, 20);
            this.MinimumTrackLengthUpDown.TabIndex = 3;
            this.MinimumTrackLengthUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // SitcomButton
            // 
            this.SitcomButton.Location = new System.Drawing.Point(259, 41);
            this.SitcomButton.Name = "SitcomButton";
            this.SitcomButton.Size = new System.Drawing.Size(75, 23);
            this.SitcomButton.TabIndex = 4;
            this.SitcomButton.Text = "Sitcom";
            this.SitcomButton.UseVisualStyleBackColor = true;            
            // 
            // DramaButton
            // 
            this.DramaButton.Location = new System.Drawing.Point(340, 12);
            this.DramaButton.Name = "DramaButton";
            this.DramaButton.Size = new System.Drawing.Size(75, 23);
            this.DramaButton.TabIndex = 5;
            this.DramaButton.Text = "Drama";
            this.DramaButton.UseVisualStyleBackColor = true;            
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Minimum Track Length in Minutes:";
            // 
            // SelectAllButton
            // 
            this.SelectAllButton.Location = new System.Drawing.Point(340, 336);
            this.SelectAllButton.Name = "SelectAllButton";
            this.SelectAllButton.Size = new System.Drawing.Size(75, 23);
            this.SelectAllButton.TabIndex = 9;
            this.SelectAllButton.Text = "Select All";
            this.SelectAllButton.UseVisualStyleBackColor = true;            
            // 
            // OKButton
            // 
            this.OKButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OKButton.Location = new System.Drawing.Point(259, 365);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 10;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;            
            // 
            // AbortButton
            // 
            this.AbortButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AbortButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.AbortButton.Location = new System.Drawing.Point(340, 365);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(75, 23);
            this.AbortButton.TabIndex = 11;
            this.AbortButton.Text = "Cancel";
            this.AbortButton.UseVisualStyleBackColor = true;            
            // 
            // MovieButton
            // 
            this.MovieButton.Location = new System.Drawing.Point(340, 41);
            this.MovieButton.Name = "MovieButton";
            this.MovieButton.Size = new System.Drawing.Size(75, 23);
            this.MovieButton.TabIndex = 6;
            this.MovieButton.Text = "Movie";
            this.MovieButton.UseVisualStyleBackColor = true;            
            // 
            // DiscTimeForm
            // 
            this.AcceptButton = this.OKButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.AbortButton;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.MovieButton);
            this.Controls.Add(this.AbortButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.SelectAllButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DramaButton);
            this.Controls.Add(this.SitcomButton);
            this.Controls.Add(this.MinimumTrackLengthUpDown);
            this.Controls.Add(this.DiscTreeView);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.DriveComboBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiscTimeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Read from Drive";            
            ((System.ComponentModel.ISupportInitialize)(this.MinimumTrackLengthUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox DriveComboBox;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView DiscTreeView;
        private System.Windows.Forms.NumericUpDown MinimumTrackLengthUpDown;
        private System.Windows.Forms.Button SitcomButton;
        private System.Windows.Forms.Button DramaButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SelectAllButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button AbortButton;
        private System.Windows.Forms.Button MovieButton;
    }
}