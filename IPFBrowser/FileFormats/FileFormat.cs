using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPFBrowser.FileFormats
{
	public class FileFormat
	{
		public string Icon { get; private set; }
		public PreviewType PreviewType { get; private set; }
		public Lexer Lexer { get; private set; }

		public FileFormat(string icon, PreviewType type, Lexer lexer = Lexer.Null)
		{
			Icon = icon;
			PreviewType = type;
			Lexer = lexer;
		}
	}

	public enum PreviewType
	{
		None,
		Text,
		Image,
		DdsImage,
		TgaImage,
		IesTable,
		TtfFont,
	}
}
