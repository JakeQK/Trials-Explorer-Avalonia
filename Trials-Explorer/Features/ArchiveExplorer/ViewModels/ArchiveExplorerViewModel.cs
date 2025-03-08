using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trials_Explorer.Features.ArchiveExplorer.Models;
using Trials_Explorer.Features.ArchiveExplorer.Services;
using Trials_Explorer.ViewModels;

namespace Trials_Explorer.Features.ArchiveExplorer.ViewModels;

public partial class ArchiveExplorerViewModel : ViewModelBase
{
    private readonly ArchiveService _archiveService;

    [ObservableProperty]
    private ArchiveNodeViewModel? _rootNode;

    [ObservableProperty]
    private ArchiveNodeViewModel? _selectedNode;

    [ObservableProperty]
    private bool _isArchiveLoaded;

    public ArchiveExplorerViewModel(ArchiveService archiveService)
    {
        _archiveService = archiveService;
        _archiveService.ArchiveLoaded += OnArchiveLoaded;
        _archiveService.ArchiveUnloaded += OnArchiveUnloaded;
    }

    private void OnArchiveLoaded(object? sender, ArchiveService.ArchiveLoadedEventArgs e)
    {
        if (e.Successfully)
        {
            IsArchiveLoaded = true;
            RootNode = new ArchiveNodeViewModel(_archiveService.GetRootDirectory);
        }
    }

    private void OnArchiveUnloaded(object? sender, EventArgs e)
    {
        IsArchiveLoaded = false;
        RootNode = null;
        SelectedNode = null;
    }

    [RelayCommand]
    private async Task ExtractSelectedAsync()
    {
        if (SelectedNode == null) return;

        if (SelectedNode.IsDirectory)
        {
            await _archiveService.QuickExtractDirectoryAsync((ArchiveDirectory)SelectedNode.Node);
        }
        else
        {
            _archiveService.QuickExtractFile((ArchiveFile)SelectedNode.Node);
        }
    }

    [RelayCommand]
    private void OpenArchive(string path)
    {
        _archiveService.OpenArchive(path);
    }
} 