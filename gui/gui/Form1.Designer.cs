
namespace gui
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.ImageBox = new System.Windows.Forms.PictureBox();
            this.LoadButton = new System.Windows.Forms.Button();
            this.PathBox = new System.Windows.Forms.TextBox();
            this.LanguageSelect = new System.Windows.Forms.ComboBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.BackButton = new System.Windows.Forms.Button();
            this.ForthButton = new System.Windows.Forms.Button();
            this.CurrentXLabel = new System.Windows.Forms.Label();
            this.CurrentYLabel = new System.Windows.Forms.Label();
            this.CurrentXBox = new System.Windows.Forms.TextBox();
            this.CurrentYBox = new System.Windows.Forms.TextBox();
            this.CurrentLabel = new System.Windows.Forms.Label();
            this.StartLabel = new System.Windows.Forms.Label();
            this.StartYBox = new System.Windows.Forms.TextBox();
            this.StartXBox = new System.Windows.Forms.TextBox();
            this.StartYLabel = new System.Windows.Forms.Label();
            this.StartXLabel = new System.Windows.Forms.Label();
            this.EndLabel = new System.Windows.Forms.Label();
            this.EndYBox = new System.Windows.Forms.TextBox();
            this.EndXBox = new System.Windows.Forms.TextBox();
            this.EndYLabel = new System.Windows.Forms.Label();
            this.EndXLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ImageBox
            // 
            this.ImageBox.AllowDrop = true;
            this.ImageBox.BackColor = System.Drawing.Color.Cornsilk;
            this.ImageBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImageBox.Enabled = false;
            this.ImageBox.Location = new System.Drawing.Point(0, 0);
            this.ImageBox.Name = "ImageBox";
            this.ImageBox.Size = new System.Drawing.Size(866, 644);
            this.ImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.ImageBox.TabIndex = 0;
            this.ImageBox.TabStop = false;
            this.ImageBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseDown);
            this.ImageBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseMove);
            this.ImageBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ImageBox_MouseUp);
            // 
            // LoadButton
            // 
            this.LoadButton.BackColor = System.Drawing.Color.Cornsilk;
            this.LoadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LoadButton.Location = new System.Drawing.Point(993, 292);
            this.LoadButton.Name = "LoadButton";
            this.LoadButton.Size = new System.Drawing.Size(104, 30);
            this.LoadButton.TabIndex = 1;
            this.LoadButton.Text = "Load";
            this.LoadButton.UseVisualStyleBackColor = false;
            this.LoadButton.Click += new System.EventHandler(this.load_Click);
            // 
            // PathBox
            // 
            this.PathBox.AllowDrop = true;
            this.PathBox.BackColor = System.Drawing.Color.Cornsilk;
            this.PathBox.Location = new System.Drawing.Point(903, 263);
            this.PathBox.Name = "PathBox";
            this.PathBox.ReadOnly = true;
            this.PathBox.Size = new System.Drawing.Size(276, 23);
            this.PathBox.TabIndex = 2;
            this.PathBox.Text = "C:";
            // 
            // LanguageSelect
            // 
            this.LanguageSelect.BackColor = System.Drawing.Color.Cornsilk;
            this.LanguageSelect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageSelect.FormattingEnabled = true;
            this.LanguageSelect.Items.AddRange(new object[] {
            "English"});
            this.LanguageSelect.Location = new System.Drawing.Point(903, 382);
            this.LanguageSelect.Name = "LanguageSelect";
            this.LanguageSelect.Size = new System.Drawing.Size(274, 24);
            this.LanguageSelect.SelectedIndex = 0;
            this.LanguageSelect.TabIndex = 3;
            // 
            // SaveButton
            // 
            this.SaveButton.BackColor = System.Drawing.Color.Cornsilk;
            this.SaveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SaveButton.Enabled = false;
            this.SaveButton.Location = new System.Drawing.Point(993, 536);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(104, 30);
            this.SaveButton.TabIndex = 4;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = false;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // BackButton
            // 
            this.BackButton.BackColor = System.Drawing.Color.Cornsilk;
            this.BackButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BackButton.Image = global::gui.Properties.Resources.Back;
            this.BackButton.Location = new System.Drawing.Point(876, 6);
            this.BackButton.Name = "BackButton";
            this.BackButton.Size = new System.Drawing.Size(36, 30);
            this.BackButton.TabIndex = 5;
            this.BackButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BackButton.UseVisualStyleBackColor = false;
            this.BackButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // ForthButton
            // 
            this.ForthButton.BackColor = System.Drawing.Color.Cornsilk;
            this.ForthButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ForthButton.Image = global::gui.Properties.Resources.Forth;
            this.ForthButton.Location = new System.Drawing.Point(928, 6);
            this.ForthButton.Name = "ForthButton";
            this.ForthButton.Size = new System.Drawing.Size(36, 30);
            this.ForthButton.TabIndex = 6;
            this.ForthButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.ForthButton.UseVisualStyleBackColor = false;
            this.ForthButton.Click += new System.EventHandler(this.ForthButton_Click);
            // 
            // CurrentXLabel
            // 
            this.CurrentXLabel.Location = new System.Drawing.Point(876, 606);
            this.CurrentXLabel.Name = "CurrentXLabel";
            this.CurrentXLabel.Size = new System.Drawing.Size(24, 16);
            this.CurrentXLabel.TabIndex = 7;
            this.CurrentXLabel.Text = "X:";
            // 
            // CurrentYLabel
            // 
            this.CurrentYLabel.Location = new System.Drawing.Point(876, 622);
            this.CurrentYLabel.Name = "CurrentYLabel";
            this.CurrentYLabel.Size = new System.Drawing.Size(24, 16);
            this.CurrentYLabel.TabIndex = 8;
            this.CurrentYLabel.Text = "Y:";
            // 
            // CurrentXBox
            // 
            this.CurrentXBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentXBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.CurrentXBox.Location = new System.Drawing.Point(900, 606);
            this.CurrentXBox.Name = "CurrentXBox";
            this.CurrentXBox.ReadOnly = true;
            this.CurrentXBox.Size = new System.Drawing.Size(40, 16);
            this.CurrentXBox.TabIndex = 9;
            this.CurrentXBox.Text = "0";
            // 
            // CurrentYBox
            // 
            this.CurrentYBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentYBox.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.CurrentYBox.Location = new System.Drawing.Point(900, 622);
            this.CurrentYBox.Name = "CurrentYBox";
            this.CurrentYBox.ReadOnly = true;
            this.CurrentYBox.Size = new System.Drawing.Size(40, 16);
            this.CurrentYBox.TabIndex = 10;
            this.CurrentYBox.Text = "0";
            // 
            // CurrentLabel
            // 
            this.CurrentLabel.Location = new System.Drawing.Point(876, 587);
            this.CurrentLabel.Name = "CurrentLabel";
            this.CurrentLabel.Size = new System.Drawing.Size(64, 21);
            this.CurrentLabel.TabIndex = 11;
            this.CurrentLabel.Text = "Current";
            // 
            // StartLabel
            // 
            this.StartLabel.Location = new System.Drawing.Point(1055, 587);
            this.StartLabel.Name = "StartLabel";
            this.StartLabel.Size = new System.Drawing.Size(64, 21);
            this.StartLabel.TabIndex = 16;
            this.StartLabel.Text = "Start";
            // 
            // StartYBox
            // 
            this.StartYBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartYBox.Location = new System.Drawing.Point(1079, 622);
            this.StartYBox.Name = "StartYBox";
            this.StartYBox.Size = new System.Drawing.Size(40, 16);
            this.StartYBox.TabIndex = 15;
            this.StartYBox.Text = "0";
            // 
            // StartXBox
            // 
            this.StartXBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.StartXBox.Location = new System.Drawing.Point(1079, 606);
            this.StartXBox.Name = "StartXBox";
            this.StartXBox.Size = new System.Drawing.Size(40, 16);
            this.StartXBox.TabIndex = 14;
            this.StartXBox.Text = "0";
            // 
            // StartYLabel
            // 
            this.StartYLabel.Location = new System.Drawing.Point(1055, 622);
            this.StartYLabel.Name = "StartYLabel";
            this.StartYLabel.Size = new System.Drawing.Size(24, 16);
            this.StartYLabel.TabIndex = 13;
            this.StartYLabel.Text = "Y:";
            // 
            // StartXLabel
            // 
            this.StartXLabel.Location = new System.Drawing.Point(1055, 606);
            this.StartXLabel.Name = "StartXLabel";
            this.StartXLabel.Size = new System.Drawing.Size(24, 16);
            this.StartXLabel.TabIndex = 12;
            this.StartXLabel.Text = "X:";
            // 
            // EndLabel
            // 
            this.EndLabel.Location = new System.Drawing.Point(1132, 587);
            this.EndLabel.Name = "EndLabel";
            this.EndLabel.Size = new System.Drawing.Size(64, 21);
            this.EndLabel.TabIndex = 21;
            this.EndLabel.Text = "End";
            // 
            // EndYBox
            // 
            this.EndYBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndYBox.Location = new System.Drawing.Point(1156, 622);
            this.EndYBox.Name = "EndYBox";
            this.EndYBox.Size = new System.Drawing.Size(40, 16);
            this.EndYBox.TabIndex = 20;
            this.EndYBox.Text = "0";
            // 
            // EndXBox
            // 
            this.EndXBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.EndXBox.Location = new System.Drawing.Point(1156, 606);
            this.EndXBox.Name = "EndXBox";
            this.EndXBox.Size = new System.Drawing.Size(40, 16);
            this.EndXBox.TabIndex = 19;
            this.EndXBox.Text = "0";
            // 
            // EndYLabel
            // 
            this.EndYLabel.Location = new System.Drawing.Point(1132, 622);
            this.EndYLabel.Name = "EndYLabel";
            this.EndYLabel.Size = new System.Drawing.Size(24, 16);
            this.EndYLabel.TabIndex = 18;
            this.EndYLabel.Text = "Y:";
            // 
            // EndXLabel
            // 
            this.EndXLabel.Location = new System.Drawing.Point(1132, 606);
            this.EndXLabel.Name = "EndXLabel";
            this.EndXLabel.Size = new System.Drawing.Size(24, 16);
            this.EndXLabel.TabIndex = 17;
            this.EndXLabel.Text = "X:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Cornsilk;
            this.panel1.Controls.Add(this.ImageBox);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 644);
            this.panel1.TabIndex = 22;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::gui.Properties.Resources.Background;
            this.ClientSize = new System.Drawing.Size(1208, 650);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.EndLabel);
            this.Controls.Add(this.EndYBox);
            this.Controls.Add(this.EndXBox);
            this.Controls.Add(this.EndYLabel);
            this.Controls.Add(this.EndXLabel);
            this.Controls.Add(this.StartLabel);
            this.Controls.Add(this.StartYBox);
            this.Controls.Add(this.StartXBox);
            this.Controls.Add(this.StartYLabel);
            this.Controls.Add(this.StartXLabel);
            this.Controls.Add(this.CurrentLabel);
            this.Controls.Add(this.CurrentYBox);
            this.Controls.Add(this.CurrentXBox);
            this.Controls.Add(this.CurrentYLabel);
            this.Controls.Add(this.CurrentXLabel);
            this.Controls.Add(this.ForthButton);
            this.Controls.Add(this.BackButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.LanguageSelect);
            this.Controls.Add(this.PathBox);
            this.Controls.Add(this.LoadButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWindow";
            this.Text = "Shut-Your-Bubble";
            ((System.ComponentModel.ISupportInitialize)(this.ImageBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox ImageBox;
        private System.Windows.Forms.Button LoadButton;
        private System.Windows.Forms.TextBox PathBox;
        private System.Windows.Forms.ComboBox LanguageSelect;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button BackButton;
        private System.Windows.Forms.Button ForthButton;
        private System.Windows.Forms.Label CurrentXLabel;
        private System.Windows.Forms.Label CurrentYLabel;
        private System.Windows.Forms.TextBox CurrentXBox;
        private System.Windows.Forms.TextBox CurrentYBox;
        private System.Windows.Forms.Label CurrentLabel;
        private System.Windows.Forms.Label StartLabel;
        private System.Windows.Forms.TextBox StartYBox;
        private System.Windows.Forms.TextBox StartXBox;
        private System.Windows.Forms.Label StartYLabel;
        private System.Windows.Forms.Label StartXLabel;
        private System.Windows.Forms.Label EndLabel;
        private System.Windows.Forms.TextBox EndYBox;
        private System.Windows.Forms.TextBox EndXBox;
        private System.Windows.Forms.Label EndYLabel;
        private System.Windows.Forms.Label EndXLabel;
        private System.Windows.Forms.Panel panel1;
    }
}

