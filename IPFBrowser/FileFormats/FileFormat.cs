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

using ScintillaNET;

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
