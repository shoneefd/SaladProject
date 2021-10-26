using SaladProject.Models;
using System.Linq;

namespace SaladProject.Services
{
    public static class CompareService
    {
        public static Compare CompareUsers(User user1, User user2, string comparison)
        {
            if (comparison == "union")
            {
                return UserUnion(user1, user2);
            }
            else if (comparison == "intersection")
            {
                return UserIntersection(user1, user2);
            }
            else if (comparison == "difference")
            {
                return UserDifference(user1, user2);
            }
            return null;
        }
        private static Compare UserDifference(User user1, User user2)
        {
            Compare comparison = new Compare
            {
                UserId = user1.UserId,
                OtherUserId = user2.UserId,
                Comparison = "Difference",
                Games = user1.Games.Except(user2.Games).ToList()
            };
            return comparison;
        }
        private static Compare UserIntersection(User user1, User user2)
        {
            Compare comparison = new Compare
            {
                UserId = user1.UserId,
                OtherUserId = user2.UserId,
                Comparison = "Intersection",
                Games = user1.Games.Intersect(user2.Games).ToList()
            };
            return comparison;
        }
        private static Compare UserUnion(User user1, User user2)
        {
            Compare comparison = new Compare
            {
                UserId = user1.UserId,
                OtherUserId = user2.UserId,
                Comparison = "Union",
                Games = user1.Games.Union(user2.Games).ToList()
            };
            return comparison;
        }
    }
}