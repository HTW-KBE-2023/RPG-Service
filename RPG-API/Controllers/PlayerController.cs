using API.Models.Monsters;
using API.Models.Players;
using API.Models.Players.Responses;
using API.Services;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Route("players")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly IGenericService<Player> _playerService;
    private readonly IMapper<Player, PlayerResponse> _playerToPlayerResponseMapper;

    public PlayerController(ILogger<PlayerController> logger,
                            IGenericService<Player> playerService,
                            PlayerMapper playerMapper)
    {
        _logger = logger;
        _playerService = playerService;
        _playerToPlayerResponseMapper = playerMapper;
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        var player = _playerService.GetById(id);
        if (player is not null)
        {
            var response = _playerToPlayerResponseMapper.Map(player);
            return Ok(response);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var players = _playerService.GetAll();
        var playersResponse = new PlayersResponse()
        {
            Items = _playerToPlayerResponseMapper.MapList(players)
        };

        return Ok(playersResponse);
    }
}