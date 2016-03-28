// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Globalization;
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
