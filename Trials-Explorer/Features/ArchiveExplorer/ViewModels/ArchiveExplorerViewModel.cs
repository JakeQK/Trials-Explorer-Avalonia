using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Trials_Explorer.Features.ArchiveExplorer.Models;
using Trials_Explorer.Features.ArchiveExplorer.Services;
using Trials_Explorer.ViewModels;

namespace Trials_Explorer.Features.ArchiveExplorer.ViewModels;

public partial class ArchiveExplorerViewModel : ViewModelBase
{
    private readonly ArchiveService _archiveService;
    private readonly IStorageProvider _storageProvider;

    [ObservableProperty]
    private ArchiveNodeViewModel? _rootNode;

    [ObservableProperty]
    private ArchiveNodeViewModel? _selectedNode;

    [ObservableProperty]
    private bool _isArchiveLoaded;

    public ArchiveExplorerViewModel(ArchiveService archiveService, IStorageProvider storageProvider)
    {
        _archiveService = archiveService;
        _storageProvider = storageProvider;
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
    private async Task OpenArchiveAsync()
    {
        var options = new FilePickerOpenOptions
        {
            Title = "Open Archive File",
            AllowMultiple = false,
            FileTypeFilter = new FilePickerFileType[]
            {
                new("Archive Files")
                {
                    Patterns = new[] { "*.pak" }
                }
            }
        };

        var result = await _storageProvider.OpenFilePickerAsync(options);
        var file = result.FirstOrDefault();
        
        if (file != null)
        {
            _archiveService.OpenArchive(file.Path.LocalPath);
        }
    }
} 