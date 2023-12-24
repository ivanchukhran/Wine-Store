using WineStore.Repository.Models;

namespace WineStore.Models;

public class Wine
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    public string ImageUrl { get; set; } 
    
    public Country Country { get; set; }
    
    public Wine(string name, string description, string imageUrl, Country country)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        Country = country;
    }
}