using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using Trials_Explorer.Features.ArchiveExplorer.Models;

namespace Trials_Explorer.Features.ArchiveExplorer.ViewModels;

public partial class ArchiveNodeViewModel : ObservableObject
{
    private readonly IArchiveNode _node;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private ObservableCollection<ArchiveNodeViewModel> _children;

    public IArchiveNode Node => _node;
    public string Name => _node.Name;
    public bool IsDirectory => _node.IsDirectory;
    public string FullPath => _node.FullPath;

    public ArchiveNodeViewModel(IArchiveNode node)
    {
        _node = node;
        _children = new ObservableCollection<ArchiveNodeViewModel>();

        if (IsDirectory)
        {
            var directory = (ArchiveDirectory)node;
            foreach (var child in directory.Children.OrderBy(c => !c.IsDirectory).ThenBy(c => c.Name))
            {
                _children.Add(new ArchiveNodeViewModel(child));
            }
        }
    }
} 