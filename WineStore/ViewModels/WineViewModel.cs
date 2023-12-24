using WineStore.Models;

namespace WineStore.ViewModels;

public class WineViewModel : ViewModelBase
{
    public WineViewModel(WineStore.Repository.Models.Wine wine)
    {
        _wine = wine;
    }
    
    private readonly WineStore.Repository.Models.Wine _wine;
    
    public string Name => _wine.Name;
    public string Description => _wine.Description;
    
}