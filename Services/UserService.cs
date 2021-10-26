using SaladProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace SaladProject.Services
{
    public static class UserService
    {
        static List<User> Users { get; }
        static int nextId = 0;
        public static List<User> GetAll() => Users;
        public static User Get(int id) => Users.FirstOrDefault(u => u.UserId == id);
        public static User Add(User user)
        {
            user.UserId = nextId++;
            Users.Add(user);
            return user;
        }
        public static void AddGame(int userId, int gameId) {
            User user = Get(userId);
            // Add gameId game to user.
        }
        public static void RemoveGame(int userId, int gameId) {
            User user = Get(userId);
            user.Games.Remove(user.Games.FirstOrDefault(g => g.GameId == gameId));
        }
    }
}