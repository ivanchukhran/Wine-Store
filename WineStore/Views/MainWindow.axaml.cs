using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;
using WineStore.ViewModels;

namespace WineStore.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        this.WhenActivated(action => 
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<WineStoreViewModel, WineViewModel?> interaction)
    {
        var dialog = new WineStoreWindow();
        dialog.DataContext = interaction.Input;
        var result = await dialog.ShowDialog<WineViewModel?>(this);
        interaction.SetOutput(result);
    }
}