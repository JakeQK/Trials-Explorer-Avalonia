using System.IO;

namespace Trials_Explorer.Features.ArchiveExplorer.Models
{
    public class ArchiveFile : IArchiveNode
    {
        public ArchiveFileEntry ArchiveFileEntry { get; set; }
        public long ArchiveFileEntryOffset { get; set; }

        public bool IsDirectory => false;
        public string FullPath { get; set; }
        public string Name => Path.GetFileName(FullPath);

        public string SafeFilename => Name;
        public string DirectoryPath => Path.GetDirectoryName(FullPath) ?? string.Empty;
        public bool IsFilenameTable => ArchiveFileEntry.IsFilenameTableEntry();
        public string Extension => Path.GetExtension(FullPath)?.ToLowerInvariant() ?? string.Empty;

        public ArchiveFile()
        {

        }

        public ArchiveFile(ArchiveFileEntry archiveFileEntry, string filename)
        {
            ArchiveFileEntry = archiveFileEntry;
            FullPath = filename;
        }

        public int GetImageIndex()
        {
            if (string.IsNullOrEmpty(Extension))
            {
                return 0;
            }

            switch (Extension)
            {
                case ".xml":
                case ".gfx":
                case ".ct":
                case ".grp":
                case ".mdl":
                case ".csv":
                case ".pose":
                case ".fsb":
                case ".lst":
                case ".trk":
                case ".txt":
                case ".dat":
                case ".hdr":
                case ".world":
                case ".game":
                case ".raw":
                case ".swf":
                case ".ogv":
                case ".tex":
                default:
                    return 1;
            }
        }
    }
}
