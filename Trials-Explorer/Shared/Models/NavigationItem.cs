using CommunityToolkit.Mvvm.ComponentModel;

namespace Trials_Explorer.Shared.Models;

public partial class NavigationItem : ObservableObject
{
    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _icon = string.Empty;

    [ObservableProperty]
    private object? _content;

    public NavigationItem(string title, string icon, object? content = null)
    {
        Title = title;
        Icon = icon;
        Content = content;
    }
} 