using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaladProject.Models;
using SaladProject.Services;

namespace SaladProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateUser() {
            User user = UserService.AddNewUser();
            return CreatedAtAction(nameof(CreateUser), new {userId = user.UserId}, user);
        }

        [HttpGet("{userId}")]
        public ActionResult<User> GetUser(int userId)
        {
            User user = UserService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("{userId}/games")]
        public async Task<IActionResult> AddGame(int userId, GameIdContainer gameId)
        {
            User user = UserService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            Game game = await GameService.GetGame(gameId.GameId);
            if (game == null)
            {
                return BadRequest();
            }
            foreach (Game g in user.Games)
            {
                if (g.GameId == game.GameId)
                {
                    return Conflict();
                }
            }
            user.Games.Add(game);
            return NoContent();
        }

        [HttpDelete("{userId}/games/{gameId}")]
        public IActionResult DeleteGame(int userId, int gameId)
        {
            User user = UserService.Get(userId);
            if (user == null)
            {
                return NotFound();
            }
            Game game = null;
            foreach (Game g in user.Games)
            {
                if (g.GameId == gameId)
                {
                    game = g;

                    break;
                }
            }
            if (game == null)
            {
                return NotFound();
            }
            user.Games.Remove(game);
            return NoContent();
        }
        
        [HttpPost("{userId}/comparison")]
        public ActionResult<Compare> CompareUsers(int userId, PartialCompare compare)
        {
            User user1 = UserService.Get(userId);
            if (user1 == null)
            {
                return NotFound();
            }
            User user2 = UserService.Get(compare.OtherUserId);
            if (user2 == null)
            {
                return BadRequest();
            }
            Compare comparison = CompareService.CompareUsers(user1, user2, compare.Comparison);
            if (comparison == null)
            {
                return BadRequest();
            }
            return comparison;
        }
    }
}
