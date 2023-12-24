namespace WineStore.Repository.Models;

public class Wine
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Country Country { get; set; }
    public int? UserId { get; set; }
}