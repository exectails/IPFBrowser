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

using IPFBrowser.FileFormats;
using IPFBrowser.FileFormats.DDS;
using IPFBrowser.FileFormats.IES;
using IPFBrowser.FileFormats.IPF;
using IPFBrowser.FileFormats.TGA;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPFBrowser
{
	public partial class FrmMain : Form
	{
		private Ipf _openedIpf;

		private Dictionary<string, List<string>> _folders = new Dictionary<string, List<string>>();
		private Dictionary<string, IpfFile> _files = new Dictionary<string, IpfFile>();

		private Dictionary<string, FileFormat> _fileTypes = new Dictionary<string, FileFormat>();

		/// <summary>
		/// Initializes form.
		/// </summary>
		/// <param name="args"></param>
		public FrmMain(string[] args)
		{
			InitializeComponent();

			// Initialize file types
			_fileTypes[".ies"] = new FileFormat("table.png", PreviewType.IesTable);

			_fileTypes[".lua"] = new FileFormat("page_white_code.png", PreviewType.Text, Lexer.Lua);
			_fileTypes[".txt"] = new FileFormat("page_white_text.png", PreviewType.Text, Lexer.Null);
			_fileTypes[".lst"] = new FileFormat("page_white_text.png", PreviewType.Text, Lexer.Null);
			_fileTypes[".fx"] = new FileFormat("page_white_code.png", PreviewType.Text, Lexer.Cpp);

			_fileTypes[".dds"] = new FileFormat("image.png", PreviewType.DdsImage);
			_fileTypes[".tga"] = new FileFormat("image.png", PreviewType.TgaImage);
			_fileTypes[".ttf"] = new FileFormat("image.png", PreviewType.TtfFont);

			_fileTypes[".xml"] = new FileFormat("page_white_code.png", PreviewType.Text, Lexer.Xml);
			_fileTypes[".effect"] = _fileTypes[".xml"];
			_fileTypes[".skn"] = _fileTypes[".xml"];
			_fileTypes[".xsd"] = _fileTypes[".xml"];

			_fileTypes[".jpg"] = new FileFormat("image.png", PreviewType.Image);
			_fileTypes[".bmp"] = _fileTypes[".jpg"];
			_fileTypes[".png"] = _fileTypes[".jpg"];

			// Prepare code preview
			TxtPreview.Dock = DockStyle.Fill;
			TxtPreview.Visible = false;
			TxtPreview.Margins[0].Width = 40;

			// Dock preview elements
			PnlImagePreview.Dock = DockStyle.Fill;
			LblPreview.Dock = DockStyle.Fill;
			GridPreview.Dock = DockStyle.Fill;

			// Disable extract buttons by default
			BtnExtractPack.Enabled = false;
			BtnExtractFile.Enabled = false;

			// Hide empty lists
			SplMain.Visible = false;

			// Fix white border of tool strip
			toolStrip1.Renderer = new MySR();

			// Load settings
			BtnPreview.Checked = Properties.Settings.Default.Preview;
			SplFiles.Panel2Collapsed = !Properties.Settings.Default.Preview;

			// Reset/initialize preview elements
			ResetPreview();

			// Load files passed as arguments
			if (args.Length != 0)
			{
				var filePath = args[0];
				if (File.Exists(filePath))
					Open(filePath);
				else
					MessageBox.Show("File not found.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Called when dragging something on top of the form,
		/// checks if it's a dropable IPF file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmMain_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				var files = (string[])e.Data.GetData(DataFormats.FileDrop);
				var file = files[0];
				if (Path.GetExtension(file) == ".ipf")
					e.Effect = DragDropEffects.Copy;
			}
		}

		/// <summary>
		/// Called when file is dropped on the form, opens it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void FrmMain_DragDrop(object sender, DragEventArgs e)
		{
			var files = (string[])e.Data.GetData(DataFormats.FileDrop);
			var file = files[0];
			Open(file);
		}

		/// <summary>
		/// Called when clicking Open, shows open dialog and opens the
		/// selected file.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnOpen_Click(object sender, EventArgs e)
		{
			if (OfdIpfFile.ShowDialog() != DialogResult.OK)
				return;

			var filePath = OfdIpfFile.FileName;
			Open(filePath);
		}

		/// <summary>
		/// Opens given IPF file.
		/// </summary>
		/// <param name="filePath"></param>
		private void Open(string filePath)
		{
			// Reset everything
			TreeFolders.Nodes.Clear();
			LstFiles.Items.Clear();
			ResetPreview();

			_folders.Clear();
			_files.Clear();

			LblVersion.Text = "";
			LblFileName.Text = "";

			// Open IPF
			try
			{
				_openedIpf = new Ipf(filePath);
				_openedIpf.Load();
			}
			catch (IOException)
			{
				_openedIpf = null;
				MessageBox.Show("Failed to open file, it's already in use.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			// Create file list
			var paths = new List<string>();
			foreach (var ipfFile in _openedIpf.Files)
			{
				paths.Add(ipfFile.FullPath);
				_files.Add(ipfFile.FullPath, ipfFile);
			}

			// Create fil tree
			PopulateTreeView(TreeFolders, paths, '/');

			// Status info
			LblVersion.Text = "Version " + _openedIpf.Footer.NewVersion;
			LblFileName.Text = filePath;

			// Open first node if there only is one
			if (TreeFolders.Nodes.Count == 1)
			{
				TreeFolders.SelectedNode = TreeFolders.Nodes[0];
				TreeFolders.SelectedNode.Toggle();
			}

			// Show lists and enabled pack extract button
			BtnExtractPack.Enabled = true;
			SplMain.Visible = true;
		}

		/// <summary>
		/// Creates nodes in tree view, based on given paths.
		/// </summary>
		/// <param name="treeView"></param>
		/// <param name="paths"></param>
		/// <param name="pathSeparator"></param>
		private void PopulateTreeView(TreeView treeView, IEnumerable<string> paths, char pathSeparator)
		{
			var insertedPaths = new Dictionary<string, TreeNode>();

			treeView.BeginUpdate();
			treeView.Nodes.Clear();
			foreach (string path in paths)
			{
				var subPaths = path.Split(pathSeparator);
				var subPathAgg = "";

				for (int i = 0; i < subPaths.Length; ++i)
				{
					var subPath = subPaths[i];
					var parentPath = subPathAgg;

					subPathAgg += subPath + pathSeparator;

					if (i == subPaths.Length - 1)
					{
						if (!_folders.ContainsKey(parentPath))
							_folders.Add(parentPath, new List<string>());
						_folders[parentPath].Add(subPathAgg.Trim(pathSeparator));
						break;
					}

					if (!insertedPaths.ContainsKey(subPathAgg))
					{
						TreeNode node;
						if (!insertedPaths.TryGetValue(parentPath, out node))
						{
							node = treeView.Nodes.Add(subPathAgg, subPath);
							insertedPaths.Add(subPathAgg, node);
						}
						else
						{
							node = node.Nodes.Add(subPathAgg, subPath);
							insertedPaths.Add(subPathAgg, node);
						}
					}
				}
			}
			treeView.EndUpdate();
		}

		/// <summary>
		/// Called when (de)selecting in files list, shows preview.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LstFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			ResetPreview();

			if (LstFiles.SelectedIndices.Count == 0)
			{
				BtnExtractFile.Enabled = false;
				return;
			}

			BtnExtractFile.Enabled = true;

			if (BtnPreview.Checked)
				Preview();
		}

		/// <summary>
		/// Called when selected a node in the tree view,
		/// lists files in node's folder in file list.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TreeFolders_AfterSelect(object sender, TreeViewEventArgs e)
		{
			var path = e.Node.FullPath.Replace('\\', '/') + '/';

			LstFiles.BeginUpdate();
			LstFiles.Items.Clear();

			List<string> paths;
			if (_folders.TryGetValue(path, out paths))
			{
				foreach (var filePath in paths)
				{
					var fileName = Path.GetFileName(filePath);
					var ext = Path.GetExtension(fileName);

					var lvi = LstFiles.Items.Add(fileName);
					//lvi.SubItems.Add("0 Byte");
					lvi.Tag = filePath;

					FileFormat fileType;
					if (_fileTypes.TryGetValue(ext, out fileType))
						lvi.ImageKey = fileType.Icon;
					else
						lvi.ImageKey = "page_white.png";
				}
			}

			LstFiles.EndUpdate();
		}

		/// <summary>
		/// Shows preview for selected file.
		/// </summary>
		private void Preview()
		{
			if (LstFiles.SelectedIndices.Count == 0)
				return;

			var selected = LstFiles.SelectedItems[0];
			var fileName = (string)selected.Tag;
			var ipfFile = _files[fileName];
			var ext = Path.GetExtension(fileName);

			var previewType = PreviewType.None;
			var lexer = Lexer.Null;

			FileFormat fileType;
			if (_fileTypes.TryGetValue(ext, out fileType))
			{
				previewType = fileType.PreviewType;
				lexer = fileType.Lexer;
			}

			ThreadPool.QueueUserWorkItem(state =>
			{
				switch (previewType)
				{
					case PreviewType.Text:
						var txtData = ipfFile.GetData();
						var text = Encoding.UTF8.GetString(txtData);

						SetTextPreviewStyle(lexer);

						Invoke((MethodInvoker)delegate
						{
							TxtPreview.ReadOnly = false;
							TxtPreview.Text = text;
							TxtPreview.ReadOnly = true;
							TxtPreview.Visible = true;
						});
						break;

					case PreviewType.Image:
						var imgData = ipfFile.GetData();

						Invoke((MethodInvoker)delegate
						{
							using (var ms = new MemoryStream(imgData))
								ImgPreview.Image = Image.FromStream(ms);
							ImgPreview.Size = ImgPreview.Image.Size;
							PnlImagePreview.Visible = true;
						});
						break;

					case PreviewType.DdsImage:
						var ddsData = ipfFile.GetData();

						DDSImage ddsImage = null;
						try
						{
							ddsImage = new DDSImage(ddsData);
						}
						catch (NullReferenceException)
						{
							Invoke((MethodInvoker)delegate
							{
								LblPreview.Text = "Preview failed";
							});
							break;
						}

						Invoke((MethodInvoker)delegate
						{
							ImgPreview.Image = ddsImage.BitmapImage;
							ImgPreview.Size = ImgPreview.Image.Size;
							PnlImagePreview.Visible = true;
						});
						break;

					case PreviewType.TgaImage:
						var tgaData = ipfFile.GetData();

						TargaImage tgaImage = null;
						try
						{
							using (var ms = new MemoryStream(tgaData))
								tgaImage = new TargaImage(ms);
						}
						catch (NullReferenceException)
						{
							Invoke((MethodInvoker)delegate
							{
								LblPreview.Text = "Preview failed";
							});
							break;
						}

						Invoke((MethodInvoker)delegate
						{
							ImgPreview.Image = tgaImage.Image;
							ImgPreview.Size = ImgPreview.Image.Size;
							PnlImagePreview.Visible = true;
						});
						break;

					case PreviewType.IesTable:
						var iesData = ipfFile.GetData();
						var iesFile = new IesFile(iesData);

						Invoke((MethodInvoker)delegate
						{
							GridPreview.SuspendDrawing();

							foreach (var iesColumn in iesFile.Columns)
								GridPreview.Columns.Add(iesColumn.Name, iesColumn.Name);

							foreach (var iesRow in iesFile.Rows)
							{
								var row = new DataGridViewRow();
								row.CreateCells(GridPreview);

								var i = 0;
								foreach (var iesColumn in iesFile.Columns)
									row.Cells[i++].Value = iesRow[iesColumn.Name];

								GridPreview.Rows.Add(row);
							}

							GridPreview.ResumeDrawing();

							GridPreview.Visible = true;
						});
						break;

					case PreviewType.TtfFont:
						var pfc = new PrivateFontCollection();

						try
						{
							var ttfData = ipfFile.GetData();
							using (var ms = new MemoryStream(ttfData))
							{
								var fontdata = new byte[ms.Length];
								ms.Read(fontdata, 0, (int)ms.Length);

								unsafe
								{
									fixed (byte* pFontData = fontdata)
										pfc.AddMemoryFont((IntPtr)pFontData, fontdata.Length);
								}
							}
						}
						catch (Exception)
						{
							Invoke((MethodInvoker)delegate
							{
								LblPreview.Text = "Preview failed";
							});
							break;
						}

						var fontFamily = pfc.Families.First();
						var font = new Font(fontFamily, 18, FontStyle.Regular, GraphicsUnit.Pixel);
						var arialFont = new Font("Arial", 12);

						var fontInfo = "Name: " + fontFamily.Name;
						var example1 = "abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ\n1234567890.:,;'\" (!?) +-*/=";
						var example2 = "Lorem ipsum dolor sit amet.";

						var bmp = new Bitmap(600, 500);
						using (var graphics = Graphics.FromImage(bmp))
						{
							var infoHeight = graphics.MeasureString(fontInfo, arialFont).Height;
							var example1Height = graphics.MeasureString(example1, font).Height;

							graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
							graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

							graphics.DrawString(fontInfo, arialFont, Brushes.Black, new Point(0, 0));
							graphics.DrawString(example1, font, Brushes.Black, new PointF(0, infoHeight + 10));

							var point = new PointF(10, infoHeight + 10 + example1Height + 20);
							foreach (var size in new int[] { 12, 18, 24, 36, 48, 60, 72 })
							{
								font = new Font(fontFamily, size, FontStyle.Regular, GraphicsUnit.Pixel);
								graphics.DrawString(example2, font, Brushes.Black, point);
								point.Y += font.Height + 5;
							}
						}

						Invoke((MethodInvoker)delegate
						{
							ImgPreview.Image = bmp;
							ImgPreview.Size = ImgPreview.Image.Size;
							PnlImagePreview.Visible = true;
						});
						break;

					default:
						Invoke((MethodInvoker)delegate
						{
							LblPreview.Text = "No Preview";
						});
						break;
				}
			});
		}

		/// <summary>
		/// Called when exit option is clicked, closes program.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExit_Click(object sender, EventArgs e)
		{
			Close();
		}

		/// <summary>
		/// Sets lexer and styles for text preview.
		/// </summary>
		/// <param name="lexer"></param>
		private void SetTextPreviewStyle(Lexer lexer)
		{
			Invoke((MethodInvoker)delegate
			{
				TxtPreview.StyleResetDefault();
				TxtPreview.Styles[Style.Default].Font = "Courier New";
				TxtPreview.Styles[Style.Default].Size = 10;
				TxtPreview.StyleClearAll();

				TxtPreview.Lexer = lexer;

				switch (lexer)
				{
					case Lexer.Xml:
						TxtPreview.Styles[Style.Xml.XmlStart].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.XmlEnd].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.Tag].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.TagEnd].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
						TxtPreview.Styles[Style.Xml.DoubleString].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.SingleString].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Xml.Comment].ForeColor = Color.Green;
						break;

					case Lexer.Lua:
						TxtPreview.SetKeywords(0, "and break do else elseif end false for function goto if in local nil not or repeat return then true until while");
						TxtPreview.SetKeywords(1, "_ENV _G _VERSION assert collectgarbage dofile error getfenv getmetatable ipairs load loadfile loadstring module next pairs pcall print rawequal rawget rawlen rawset require select setfenv setmetatable tonumber tostring type unpack xpcall string table math bit32 coroutine io os debug package __index __newindex __call __add __sub __mul __div __mod __pow __unm __concat __len __eq __lt __le __gc __mode");
						TxtPreview.SetKeywords(2, "byte char dump find format gmatch gsub len lower match rep reverse sub upper abs acos asin atan atan2 ceil cos cosh deg exp floor fmod frexp ldexp log log10 max min modf pow rad random randomseed sin sinh sqrt tan tanh arshift band bnot bor btest bxor extract lrotate lshift replace rrotate rshift shift string.byte string.char string.dump string.find string.format string.gmatch string.gsub string.len string.lower string.match string.rep string.reverse string.sub string.upper table.concat table.insert table.maxn table.pack table.remove table.sort table.unpack math.abs math.acos math.asin math.atan math.atan2 math.ceil math.cos math.cosh math.deg math.exp math.floor math.fmod math.frexp math.huge math.ldexp math.log math.log10 math.max math.min math.modf math.pi math.pow math.rad math.random math.randomseed math.sin math.sinh math.sqrt math.tan math.tanh bit32.arshift bit32.band bit32.bnot bit32.bor bit32.btest bit32.bxor bit32.extract bit32.lrotate bit32.lshift bit32.replace bit32.rrotate bit32.rshift");
						TxtPreview.SetKeywords(3, "close flush lines read seek setvbuf write clock date difftime execute exit getenv remove rename setlocale time tmpname coroutine.create coroutine.resume coroutine.running coroutine.status coroutine.wrap coroutine.yield io.close io.flush io.input io.lines io.open io.output io.popen io.read io.tmpfile io.type io.write io.stderr io.stdin io.stdout os.clock os.date os.difftime os.execute os.exit os.getenv os.remove os.rename os.setlocale os.time os.tmpname debug.debug debug.getfenv debug.gethook debug.getinfo debug.getlocal debug.getmetatable debug.getregistry debug.getupvalue debug.getuservalue debug.setfenv debug.sethook debug.setlocal debug.setmetatable debug.setupvalue debug.setuservalue debug.traceback debug.upvalueid debug.upvaluejoin package.cpath package.loaded package.loaders package.loadlib package.path package.preload package.seeall");

						TxtPreview.Styles[Style.Lua.Default].ForeColor = Color.Black;
						TxtPreview.Styles[Style.Lua.Comment].ForeColor = Color.Green;
						TxtPreview.Styles[Style.Lua.CommentLine].ForeColor = Color.Green;
						TxtPreview.Styles[Style.Lua.CommentDoc].ForeColor = Color.DarkSeaGreen;
						TxtPreview.Styles[Style.Lua.LiteralString].ForeColor = Color.Purple;
						TxtPreview.Styles[Style.Lua.Preprocessor].ForeColor = Color.Brown;
						TxtPreview.Styles[Style.Lua.Number].ForeColor = Color.Orange;
						TxtPreview.Styles[Style.Lua.String].ForeColor = Color.Gray;
						TxtPreview.Styles[Style.Lua.StringEol].ForeColor = Color.Gray;
						TxtPreview.Styles[Style.Lua.Character].ForeColor = Color.Gray;
						TxtPreview.Styles[Style.Lua.Operator].ForeColor = Color.DarkBlue;
						TxtPreview.Styles[Style.Lua.Word].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Lua.Word2].ForeColor = Color.CornflowerBlue;
						TxtPreview.Styles[Style.Lua.Word3].ForeColor = Color.Purple;
						TxtPreview.Styles[Style.Lua.Word4].ForeColor = Color.DarkBlue;
						break;

					case Lexer.Cpp:
						TxtPreview.SetKeywords(0, "alignof and and_eq bitand bitor break case catch compl const_cast continue default delete do dynamic_cast else false for goto if namespace new not not_eq nullptr operator or or_eq reinterpret_cast return sizeof static_assert static_cast switch this throw true try typedef typeid using while xor xor_eq NULL");
						TxtPreview.SetKeywords(1, "alignas asm auto bool char char16_t char32_t class const constexpr decltype double enum explicit export extern final float friend inline int long mutable noexcept override private protected public register short signed static struct template thread_local typename union unsigned virtual void volatile wchar_t");

						TxtPreview.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
						TxtPreview.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
						TxtPreview.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
						TxtPreview.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
						TxtPreview.Styles[Style.Cpp.Number].ForeColor = Color.Olive;
						TxtPreview.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Cpp.Word2].ForeColor = Color.Blue;
						TxtPreview.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
						TxtPreview.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
						TxtPreview.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
						TxtPreview.Styles[Style.Cpp.StringEol].BackColor = Color.Pink;
						TxtPreview.Styles[Style.Cpp.Operator].ForeColor = Color.Purple;
						TxtPreview.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Maroon;
						break;
				}
			});
		}

		/// <summary>
		/// Called when clicking About, shows About window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnAbout_Click(object sender, EventArgs e)
		{
			new FrmAbout().ShowDialog();
		}

		/// <summary>
		/// Resets preview, clearing all preview elements.
		/// </summary>
		private void ResetPreview()
		{
			TxtPreview.Visible = false;
			TxtPreview.Text = "";

			PnlImagePreview.Visible = false;
			ImgPreview.Image = null;

			GridPreview.Visible = false;
			GridPreview.Rows.Clear();
			GridPreview.Columns.Clear();

			LblPreview.Text = "Preview";
		}

		/// <summary>
		/// Called when clicking Extrack Pack button, extracts current IPF
		/// file to selected destination.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExtractPack_Click(object sender, EventArgs e)
		{
			FbdExtractPack.Description = "Select folder to extract pack to.";
			FbdExtractPack.ShowNewFolderButton = true;

			if (FbdExtractPack.ShowDialog() != DialogResult.OK)
				return;

			var extractPath = FbdExtractPack.SelectedPath;

			if (!Directory.Exists(extractPath))
			{
				MessageBox.Show("Directory not found.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			ExtractFiles(_openedIpf.Files, extractPath);
		}

		/// <summary>
		/// Called when clicking Extract File button, extracts selected
		/// file from IPF to selected destination.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExtractFile_Click(object sender, EventArgs e)
		{
			if (LstFiles.SelectedIndices.Count == 0)
				return;

			var selected = LstFiles.SelectedItems[0];
			var filePath = (string)selected.Tag;
			var ipfFile = _files[filePath];
			var fileName = Path.GetFileName(filePath);
			var ext = Path.GetExtension(filePath);

			SavExtractFile.FileName = fileName;

			if (SavExtractFile.ShowDialog() != DialogResult.OK)
				return;

			filePath = SavExtractFile.FileName;

			var file = ipfFile.GetData();
			File.WriteAllBytes(filePath, file);
		}

		/// <summary>
		/// Called when clicking Extract Client button, extracts selected
		/// TOS client to selected destination.
		/// </summary>
		/// <remarks>
		/// Loads data first, followed by patch, to get the latest version
		/// of all files found.
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnExtractClient_Click(object sender, EventArgs e)
		{
			FbdExtractPack.Description = "Select TOS folder.";
			FbdExtractPack.ShowNewFolderButton = false;

			if (FbdExtractPack.ShowDialog() != DialogResult.OK)
				return;

			var tosPath = FbdExtractPack.SelectedPath;
			var dataPath = Path.Combine(tosPath, "data");
			var patchPath = Path.Combine(tosPath, "patch");
			var releasePath = Path.Combine(tosPath, "release");

			if (!Directory.Exists(dataPath) || !Directory.Exists(patchPath) || !Directory.Exists(releasePath))
			{
				MessageBox.Show("Please select the TOS folder that contains 'data', 'patch', and 'release'.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			FbdExtractPack.Description = "Select folder to extract to.";
			FbdExtractPack.ShowNewFolderButton = true;

			if (FbdExtractPack.ShowDialog() != DialogResult.OK)
				return;

			var extractPath = FbdExtractPack.SelectedPath;

			if (!Directory.Exists(tosPath))
			{
				MessageBox.Show("Directory not found.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			var col = new IpfCollection(tosPath);
			ExtractFiles(col.Files.Values, extractPath);
		}

		/// <summary>
		/// Called when clicking preview button, toggles preview panel.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BtnPreview_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.Preview = BtnPreview.Checked;
			SplFiles.Panel2Collapsed = !Properties.Settings.Default.Preview;

			if (BtnPreview.Checked)
				Preview();
		}

		/// <summary>
		/// Called when program is closed, saves settings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		/// <summary>
		/// Extracts given files to extractPath.
		/// </summary>
		/// <param name="ipfFiles"></param>
		/// <param name="extractPath"></param>
		private void ExtractFiles(IEnumerable<IpfFile> ipfFiles, string extractPath)
		{
			// Warm up
			var timer = Stopwatch.StartNew();

			var count = ipfFiles.Count();
			var frmProgress = new FrmProgress(count);

			// Run in thread, so it doesn't block the UI thread
			ThreadPool.QueueUserWorkItem(state =>
			{
				// Actually start timer
				timer.Restart();

				var canceled = false;

				// Extract files in parallel for performance
				var i = 0;
				Parallel.ForEach(ipfFiles, ipfFile =>
				{
					// Just return if cancel was clicked, this way we get
					// to after the ForEach in an instant, even if it
					// technically doesn't `break;`.
					if (canceled = frmProgress.Cancel)
						return;

					var filePath = Path.Combine(extractPath, ipfFile.FullPath);

					// Create folder if it doesn't exist yet
					var parent = Path.GetDirectoryName(filePath);
					if (!Directory.Exists(parent))
						Directory.CreateDirectory(parent);

					// Extract file
					var data = ipfFile.GetData();
					File.WriteAllBytes(filePath, data);

					// Update progress bar
					Invoke((MethodInvoker)delegate
					{
						if (frmProgress.Handle != IntPtr.Zero)
							frmProgress.UpdateProgress(++i);
					});
				});

				// Stop timer
				timer.Stop();

				// Close progress window and show result
				Invoke((MethodInvoker)delegate
				{
					frmProgress.Close();
					MessageBox.Show(canceled ? "Canceled." : "Done (" + timer.Elapsed + ").", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				});
			});

			// Show progress window after the thread was started, as it
			// blocks the main window.
			frmProgress.ShowDialog();
		}
	}
}
