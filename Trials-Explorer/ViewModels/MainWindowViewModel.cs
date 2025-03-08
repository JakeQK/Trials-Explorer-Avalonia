using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Trials_Explorer.Features.ArchiveExplorer.Services;
using Trials_Explorer.Features.ArchiveExplorer.ViewModels;
using Trials_Explorer.Features.Home.ViewModels;
using Trials_Explorer.Features.Settings.ViewModels;
using Trials_Explorer.Shared.Models;

namespace Trials_Explorer.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly ArchiveService _archiveService;

    [ObservableProperty]
    private NavigationItem? _selectedItem;

    public ObservableCollection<NavigationItem> NavigationItems { get; }

    public MainWindowViewModel()
    {
        _archiveService = new ArchiveService();
        
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem("Home", "home", new HomeViewModel()),
            new NavigationItem("Archive Explorer", "folder", new ArchiveExplorerViewModel(_archiveService)),
            new NavigationItem("Settings", "settings", new SettingsViewModel())
        };

        // Set default selected item
        SelectedItem = NavigationItems[0];
    }
}
