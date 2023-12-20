using WineStore.Models;

namespace WineStore.ViewModels;

public class WineViewModel : ViewModelBase
{
    public WineViewModel(Wine wine)
    {
        _wine = wine;
    }
    
    private readonly Wine _wine;
    
    public string Name => _wine.Name;
    public string Description => _wine.Description;
    
}