using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;

namespace WineStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand BuyWineCommand { get; }
    public Interaction<WineStoreViewModel, WineViewModel?> ShowDialog { get; }
    
    public MainWindowViewModel()
    {
        ShowDialog = new Interaction<WineStoreViewModel, WineViewModel?>();
        BuyWineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new WineStoreViewModel();
            var result = await ShowDialog.Handle(store);
        });
    }
}