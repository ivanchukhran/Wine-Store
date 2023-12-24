namespace WineStore.Repository.Models;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CountryId { get; set; }
    public int? UserId { get; set; }
    public string? ImageUrl { get; set; }
}