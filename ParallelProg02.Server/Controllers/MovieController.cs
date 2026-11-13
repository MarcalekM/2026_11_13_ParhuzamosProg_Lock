using Microsoft.AspNetCore.Mvc;
using ParallelProg02.Server.Models;

namespace ParallelProg02.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        private static readonly List<Movie> movies = Models.SeadData.Movies;

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetMovies()
        {
            await Task.Delay(Random.Shared.Next(100, 1000));
            return movies;
        }
    }
}
