using SaladProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace SaladProject.Services
{
    public static class UserService
    {
        static List<User> Users { get; }
        static int nextId = 1;
        static UserService() {
            Users = new List<User>();
            nextId = 1;
        }
        public static List<User> GetAll() => Users;
        public static User Get(int id) => Users.FirstOrDefault(u => u.UserId == id);
        public static User AddNewUser()
        {
            User user = new User { UserId = nextId++, Games = new List<Game>()};
            Users.Add(user);
            return user;
        }
        public static void AddGame(int userId, Game game) {
            User user = Get(userId);
            user.Games.Add(game);
        }
        public static void RemoveGame(int userId, int gameId) {
            User user = Get(userId);
            user.Games.Remove(user.Games.FirstOrDefault(g => g.GameId == gameId));
        }
    }
}