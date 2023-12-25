using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;

namespace WineStore.ViewModels;

public class WineStoreViewModel : ViewModelBase
{
    public WineStoreViewModel(MainWindowViewModel parentWindow)
    {
        _parentWindow = parentWindow;
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);
    }
    
    
    private MainWindowViewModel? _parentWindow;
    public User User { get; set; }

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
    
    public async void BuyWineCommand()
    {
        if (SelectedWine == null)
        {
            return;
        }

        var wine = await WineService.GetStoreWineAsync(SelectedWine.Name);
        if (wine == null)
        {
            return;
        }
        
        WineService.SetWineOwner(wine, User.Id);
        SearchResults.Remove(SelectedWine);
        _parentWindow?.AddToObservable(wine);
    }
    
    
}