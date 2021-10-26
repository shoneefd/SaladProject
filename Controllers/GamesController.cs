using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaladProject.Models;
using SaladProject.Services;
using SaladProject.Util;

namespace SaladProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Game>>> getGames(string q, string sort)
        {
            
            return await GameService.GetGames(q, sort);
        }
    }
}
