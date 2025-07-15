namespace ViMusic
{
    partial class About
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            pfp = new PictureBox();
            label1 = new Label();
            link = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pfp).BeginInit();
            SuspendLayout();
            // 
            // pfp
            // 
            pfp.InitialImage = (Image)resources.GetObject("pfp.InitialImage");
            pfp.Location = new Point(34, 12);
            pfp.Name = "pfp";
            pfp.Size = new Size(125, 125);
            pfp.TabIndex = 0;
            pfp.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.White;
            label1.Location = new Point(28, 140);
            label1.Name = "label1";
            label1.Size = new Size(138, 15);
            label1.TabIndex = 1;
            label1.Text = "Program made by Violet!";
            // 
            // link
            // 
            link.ActiveLinkColor = Color.FromArgb(235, 96, 244);
            link.AutoSize = true;
            link.DisabledLinkColor = Color.FromArgb(51, 51, 51);
            link.LinkColor = Color.FromArgb(96, 151, 244);
            link.Location = new Point(12, 163);
            link.Name = "link";
            link.Size = new Size(169, 15);
            link.TabIndex = 2;
            link.TabStop = true;
            link.Text = "https://github.com/silly-Violet";
            link.LinkClicked += Link_LinkClicked;
            // 
            // About
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(31, 31, 31);
            ClientSize = new Size(193, 187);
            Controls.Add(link);
            Controls.Add(label1);
            Controls.Add(pfp);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "About";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About";
            ((System.ComponentModel.ISupportInitialize)pfp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pfp;
        private Label label1;
        private LinkLabel link;
    }
}