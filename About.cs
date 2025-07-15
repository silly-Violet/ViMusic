using System;
using System.Diagnostics;
using System.Security.Policy;

namespace ViMusic
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();

            pfp.Image = new Bitmap(Icons.umbre, pfp.Width, pfp.Height);
        }

        private void Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = link.Text, UseShellExecute = true });
        }
    }
}
