using System;
using System.ComponentModel;
using System.Net.Mime;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;
using WineStore.Views;

namespace WineStore.ViewModels;

public class StartupViewModel : ViewModelBase
{
    private string _loginUsername;
    private string _loginPassword;
    
    private string _registerUsername;
    private string _registerPassword;
    
    public string LoginUsername
    {
        get => _loginUsername;
        set => this.RaiseAndSetIfChanged(ref _loginUsername, value);
    }
    
    public string LoginPassword
    {
        get => _loginPassword;
        set => this.RaiseAndSetIfChanged(ref _loginPassword, value);
    }
    
    public string RegisterUsername
    {
        get => _registerUsername;
        set => this.RaiseAndSetIfChanged(ref _registerUsername, value);
    }
    
    public string RegisterPassword
    {
        get => _registerPassword;
        set => this.RaiseAndSetIfChanged(ref _registerPassword, value);
    }
    
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    
    public StartupViewModel()
    {
        LoginCommand = ReactiveCommand.Create(Login);
        RegisterCommand = ReactiveCommand.Create(Register);
    }
    
    public async void Login()
    {
        Console.WriteLine("Login");
        var user = await UserService.GetUserAsync(LoginUsername);
        if (user == null)
        {
            Console.WriteLine("User not found");
            LoginUsername = "";
            LoginPassword = "";
            return;
        }
        Console.WriteLine("User found " + user.Username);
        if (user.Password != LoginPassword)
        {
            Console.WriteLine("Wrong password");
            return;
        }
        Utils.OpenMainWindow(user);
    }
    
    public async void Register()
    {
        var user = new User {Username = RegisterUsername, Password = RegisterPassword};
        UserService.CreateAsync(user);
        Utils.OpenMainWindow(user);
    }
}