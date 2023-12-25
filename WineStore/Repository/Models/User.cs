using System.Collections.Generic;

namespace WineStore.Repository.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public List<Wine> Wines { get; set; }
    }
}