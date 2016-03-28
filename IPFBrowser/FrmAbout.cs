using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPFBrowser
{
	public partial class FrmAbout : Form
	{
		public FrmAbout()
		{
			InitializeComponent();
		}

		private void LblLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(LblLink.Text);
		}

		private void BtnOK_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
