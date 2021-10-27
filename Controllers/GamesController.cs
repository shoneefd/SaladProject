using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SaladProject.Models;
using SaladProject.Services;

namespace SaladProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Game>>> getGames(string q, string sort)
        {
            List<string> legalSorts = new List<string>
            {
                "name", "released", "added", "created", "updated", "rating", "metacritic"
            };
            if (!String.IsNullOrEmpty(sort) && !legalSorts.Contains(sort))
            {
                return BadRequest();
            }
            List<Game> lst = await GameService.GetGames(q, sort);
            return lst;
        }
    }
}
