using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;
using WineStore.Views;

namespace WineStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand BuyWineCommand { get; }
    public Interaction<WineStoreViewModel, WineViewModel?> ShowDialog { get; }
    public Interaction<AddWineWindowViewModel, WineViewModel?> ShowAddWineDialog { get; }
    public User User { get; set; }

    public MainWindowViewModel(User user)
    {
        
        ShowDialog = new Interaction<WineStoreViewModel, WineViewModel?>();
        ShowAddWineDialog = new Interaction<AddWineWindowViewModel, WineViewModel?>();
        BuyWineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new WineStoreViewModel(this) { User = user };
            var result = await ShowDialog.Handle(store);
        });
        AddWineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var addWine = new AddWineWindowViewModel(this) { Owner = user };
            var result = await ShowAddWineDialog.Handle(addWine);
        }); 
        User = user;
        this.WhenAnyValue(x => x.SearchText)
            .Throttle(TimeSpan.FromMilliseconds(400))
            .ObserveOn(RxApp.MainThreadScheduler)
            .Subscribe(DoSearch!);
    }
    
    public ICommand AddWineCommand { get; }

    // public void AddWineCommand()
    // {
    //     // var addWine = new AddWineWindowViewModel(this) { User = User };
    //     // ShowAddWineDialog.Handle(addWine);
    //     var addWineView = new AddWineWindow
    //     {
    //         DataContext = new AddWineWindowViewModel(this) { User = User }
    //     };
    //     var result = await Dialog.Show<WineViewModel?>(addWineView);
    // }
    

    private async void DoSearch(string s)
    {
        IsBusy = true;
        SearchResults.Clear();

        var results = await WineService.GetUserWinesAsync(User.Id);

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
    
    public void AddToObservable(Wine wine)
    {
        SearchResults.Add(new WineViewModel(wine));
    }
    
}