using Microsoft.AspNetCore.Mvc;
using MyBGList.DTO.v1;

namespace MyBGList.Controllers.v1
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BoardGamesController : ControllerBase
    {
        private readonly ILogger<BoardGamesController> _logger;

        public BoardGamesController(ILogger<BoardGamesController> logger)
        {
            _logger = logger;
        }

        [HttpGet (Name = "GetBoardGames")]
        [HttpGet("/")]
        [ResponseCache(NoStore = true)]
        //public IEnumerable<BoardGame> Get()
        public RestDTO<BoardGame[]> Get()
        {
            //return new[]
            return new RestDTO<BoardGame[]>()
            {
                Data = new BoardGame[] {
                    new BoardGame() {
                        Id = 1,
                        Name = "Axis & Allies",
                        Year = 1981,
                        MinPlayers = 10,
                        MaxPlayers = 20
                    },
                    new BoardGame() {
                        Id = 2,
                        Name = "Citadels",
                        Year = 2000,
                        MinPlayers = 1,
                        MaxPlayers = 10
                    },
                    new BoardGame() {
                        Id = 3,
                        Name = "Transforming Mars",
                        Year = 2016,
                        MinPlayers = 2,
                        MaxPlayers = 15
                    }
                },
                Links = new List<LinkDTO>
                {
                    new LinkDTO(
                        Url.Action(null, "BoardGames", null, Request.Scheme)!,
                        "self",
                        "GET"),
                }
            };
        }
    }
}
