using System.Collections.ObjectModel;
using ReactiveUI;

namespace WineStore.ViewModels;

public class WineStoreViewModel : ViewModelBase
{
    public WineStoreViewModel()
    {
        SearchResults.Add(new WineViewModel());
        SearchResults.Add(new WineViewModel());
        SearchResults.Add(new WineViewModel());
    }
    
    private string? _searchText;
    private bool _isBusy;
    private WineViewModel? _selectedWine;
    
    public ObservableCollection<WineViewModel> SearchResults { get; } = new();
    
    public WineViewModel? SelectedWine
    {
        get => _selectedWine;
        set => this.RaiseAndSetIfChanged(ref _selectedWine, value);
    }

    public string? SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set => this.RaiseAndSetIfChanged(ref _isBusy, value);
    }
}