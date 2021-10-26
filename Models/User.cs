using System.Collections.Generic;

namespace SaladProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public List<Game> Games {get; set; }
    }
}