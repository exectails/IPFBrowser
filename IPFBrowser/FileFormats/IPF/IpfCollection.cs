using System;
using System.Collections.Generic;
using System.IO;

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
				foreach (var ipfName in Directory.EnumerateFiles(folder, "*.ipf", SearchOption.TopDirectoryOnly))
				{
					var ipf = new Ipf(ipfName);
					foreach (var ipfFile in ipf.Files)
					{
						if (this.Files.ContainsKey(ipfFile.FullPath))
						{
							//if (this.Files[f.fullpath].ipf.Footer.NewVersion > f.ipf.Footer.NewVersion)
							//	throw new Exception("Version lower?");

							//if (this.Files[f.fullpath].ipf.Footer.NewVersion > ipf.Footer.VersionToPatch)
							//	throw new Exception("Version mismatch?");

							//if (ipf.Footer.VersionToPatch != this.Files[f.fullpath].ipf.Footer.NewVersion + 1)
							//	throw new Exception("Version mismatch?");
						}

						this.Files[ipfFile.FullPath] = ipfFile;
					}
				}
			}
		}
	}
}
