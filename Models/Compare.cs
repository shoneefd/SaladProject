using System.Collections.Generic;

namespace SaladProject.Models
{
    public class Compare
    {
        public int UserId { get; set; }
        public int OtherUserId { get; set; }
        public string Comparison { get; set; }
        public List<Game> Games { get; set; }
    }
}