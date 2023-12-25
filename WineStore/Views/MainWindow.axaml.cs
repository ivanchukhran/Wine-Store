using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using WineStore.Repository.Models;
using WineStore.Repository.Providers;
using WineStore.ViewModels;

namespace WineStore.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => 
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
        this.WhenActivated(action => 
            action(ViewModel!.ShowAddWineDialog.RegisterHandler(DoShowAddWineDialogAsync)));
    }

    private async Task DoShowAddWineDialogAsync(InteractionContext<AddWineWindowViewModel, WineViewModel> arg)
    {
        var dialog = new AddWineWindow();
        dialog.DataContext = arg.Input;
        var result = await dialog.ShowDialog<WineViewModel>(this);
        arg.SetOutput(result);
    }

    private async Task DoShowDialogAsync(InteractionContext<WineStoreViewModel, WineViewModel?> interaction)
    {
        var dialog = new WineStoreWindow();
        dialog.DataContext = interaction.Input;
        var result = await dialog.ShowDialog<WineViewModel?>(this);
        interaction.SetOutput(result);
    }
}