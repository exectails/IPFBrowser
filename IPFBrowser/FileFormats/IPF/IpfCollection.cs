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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IPFBrowser.FileFormats.IPF
{
	public class IpfCollection
	{
		public Dictionary<string, IpfFile> Files { get; private set; }

		public IpfCollection(string folderPath)
		{
			var dataFolder = Path.Combine(folderPath, "data");
			var patchFolder = Path.Combine(folderPath, "patch");
			var releaseFolder = Path.Combine(folderPath, "release");

			if (!Directory.Exists(dataFolder) || !Directory.Exists(patchFolder) || !Directory.Exists(releaseFolder))
				throw new ArgumentException("Not a TOS folder.");

			this.Files = new Dictionary<string, IpfFile>();

			foreach (var folder in new string[] { dataFolder, patchFolder })
			{
				var files = Directory.EnumerateFiles(folder, "*.ipf", SearchOption.TopDirectoryOnly);
				if (folder == patchFolder)
				{
					files = files.OrderBy(a =>
					{
						var fileName = Path.GetFileNameWithoutExtension(a);
						var index = fileName.IndexOf('_');
						return int.Parse(fileName.Substring(0, index));
					});
				}

				foreach (var ipfName in files)
				{
					var ipf = new Ipf(ipfName);
					foreach (var ipfFile in ipf.Files)
					{
						//if (this.Files.ContainsKey(ipfFile.FullPath))
						//{
						//	if (this.Files[f.fullpath].ipf.Footer.NewVersion > f.ipf.Footer.NewVersion)
						//		throw new Exception("Version lower?");

						//	if (this.Files[f.fullpath].ipf.Footer.NewVersion > ipf.Footer.VersionToPatch)
						//		throw new Exception("Version mismatch?");

						//	if (ipf.Footer.VersionToPatch != this.Files[f.fullpath].ipf.Footer.NewVersion + 1)
						//		throw new Exception("Version mismatch?");
						//}

						this.Files[ipfFile.FullPath] = ipfFile;
					}
				}
			}
		}
	}
}
