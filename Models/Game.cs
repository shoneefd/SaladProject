namespace SaladProject.Models
{
    public class Game
    {
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Added { get; set; }
        public int Metacritic { get; set; }
        public double Rating { get; set; }
        public string Released { get; set; }
        public string Updated { get; set; }
    }
}