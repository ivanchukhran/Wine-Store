using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WineStore.Repository;
using WineStore.ViewModels;
using WineStore.Views;

namespace WineStore;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // desktop.MainWindow = new MainWindow
            // {
            //     DataContext = new MainWindowViewModel(),
            // };
            desktop.MainWindow = new StartupView
            {
                DataContext = new StartupViewModel(),
            };
            
        }

        base.OnFrameworkInitializationCompleted();
    }
}