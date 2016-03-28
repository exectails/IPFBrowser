using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPFBrowser
{
	public partial class FrmProgress : Form
	{
		public bool Cancel { get; set; }

		private int _max;

		public FrmProgress(int max)
		{
			InitializeComponent();

			_max = max;

			PrbProgress.Minimum = 0;
			PrbProgress.Maximum = max;
			PrbProgress.Value = 0;
			UpdateProgress(0);
		}

		public void UpdateProgress(int current)
		{
			LblProgress.Text = string.Format(CultureInfo.InvariantCulture, "{0:n0}/{1:n0}", current, _max);
			PrbProgress.Value = current;
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			Cancel = true;
		}

		private void FrmProgress_FormClosing(object sender, FormClosingEventArgs e)
		{
			// Let close when finished
			if (PrbProgress.Value >= PrbProgress.Maximum)
				return;

			e.Cancel = !Cancel;
			Cancel = true;
		}
	}
}
