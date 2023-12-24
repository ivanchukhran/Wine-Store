using System.Reactive.Linq;
using System.Windows.Input;
using ReactiveUI;
using WineStore.Repository.Models;

namespace WineStore.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ICommand BuyWineCommand { get; }
    public Interaction<WineStoreViewModel, WineViewModel?> ShowDialog { get; }
    public User User { get; set; }

    public MainWindowViewModel(User user)
    {
        ShowDialog = new Interaction<WineStoreViewModel, WineViewModel?>();
        BuyWineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new WineStoreViewModel();
            var result = await ShowDialog.Handle(store);
        });
        User = user;
    }
}