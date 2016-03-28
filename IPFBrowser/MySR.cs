using System.Windows.Forms;

namespace IPFBrowser
{
	// http://stackoverflow.com/questions/1918247/how-to-disable-the-line-under-tool-strip-in-winform-c
	public class MySR : ToolStripSystemRenderer
	{
		public MySR() { }

		protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
		{
			if (e.ToolStrip is ToolStrip)
			{
				// skip render border
			}
			else
			{
				// do render border
				base.OnRenderToolStripBorder(e);
			}
		}
	}
}
