using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;
using WineStore.Repository.Providers;

namespace WineStore.ViewModels;

public class WineStoreViewModel : ViewModelBase
{
    public WineStoreViewModel()
    {
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);
    }

    private async void DoSearch(string s)
    {
        IsBusy = true;
        SearchResults.Clear();
        
        var results = await WineService.GetStoreWinesAsync();
        
        if (string.IsNullOrWhiteSpace(s))
        {
            IsBusy = false;
            SearchResults.AddRange(results.Select(x => new WineViewModel(x)));
            return;
        }

        SearchResults.AddRange(results
            .Where(x =>
            x.Name.Contains(s, StringComparison.InvariantCultureIgnoreCase) ||
            x.Description.Contains(s, StringComparison.InvariantCultureIgnoreCase))
            .Select(x => new WineViewModel(x))
        );
        IsBusy = false;
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