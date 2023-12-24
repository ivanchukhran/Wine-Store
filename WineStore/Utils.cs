using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using WineStore.Repository.Models;
using WineStore.ViewModels;
using WineStore.Views;

namespace WineStore;

public class Utils
{
    public static void OpenMainWindow(User user)
    {
        Window oldWindow = null;
        if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            oldWindow = desktop.MainWindow;
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(user),
            };
            desktop.MainWindow.Show();
            oldWindow?.Close();
        }
    }
}