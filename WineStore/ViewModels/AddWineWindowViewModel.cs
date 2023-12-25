using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;

namespace WineStore.ViewModels;

public class AddWineWindowViewModel : ViewModelBase
{
    private string? _name;
    private string? _description;
    private string? _selectedCountry;
    private readonly MainWindowViewModel _parentWindow;
    private List<string> _countries;

    public User Owner { get; set; }

    public AddWineWindowViewModel( MainWindowViewModel parentWindow)
    {
        _parentWindow = parentWindow;
        AddWineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var country = await CountryService.GetCountryAsync(SelectedCountry!);
            if (country == null)
            {
                Console.WriteLine($"Country not found {country}");
                country = new Country
                {
                    Name = SelectedCountry!
                };
                CountryService.CreateAsync(country);
            }
            var wine = new Wine
            {
                Name = Name!,
                Description = Description!,
                Country = country,
                // User = Owner
            };
            // var user = await UserService.GetUserAsync(_parentWindow.User.Id);
            // wine.User = user;
            WineService.AddWineAsync(wine);
            // _parentWindow.SearchResults.Add(new WineViewModel(wine));
        });
    }
    
    

    public string? SelectedCountry 
    {
        get => _selectedCountry;
        set => this.RaiseAndSetIfChanged(ref _selectedCountry, value);
    }
    
    public string? Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    public string? Description
    {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }
    
    public ReactiveCommand<Unit, Unit> AddWineCommand { get; }
}