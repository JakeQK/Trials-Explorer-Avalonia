namespace Trials_Explorer.Features.ArchiveExplorer.Models
{
    public interface IArchiveNode
    {
        string Name { get; }
        bool IsDirectory { get; }
        string FullPath { get; set; }
    }
}
