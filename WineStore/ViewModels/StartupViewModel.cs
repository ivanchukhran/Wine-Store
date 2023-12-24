using System.ComponentModel;
using System.Net.Mime;
using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;
using WineStore.Views;

namespace WineStore.ViewModels;

public class StartupViewModel : ViewModelBase
{
    private string _username;
    private string _password;

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }
    
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }

    public StartupViewModel()
    {
        LoginCommand = ReactiveCommand.Create(Login);
        RegisterCommand = ReactiveCommand.Create(Register);
    }
    
    private void Login()
    {
        
    }
    
    private void Register()
    {
        var user = UserService.CreateAsync(Username, Password);
        
    }

    private void OpenMainWindow(User user)
    {
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel { User = user }
            };
        }
    }
}