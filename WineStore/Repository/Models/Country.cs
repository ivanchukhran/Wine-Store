using System.Collections.Generic;

namespace WineStore.Repository.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<Wine> Wines { get; set; }
}