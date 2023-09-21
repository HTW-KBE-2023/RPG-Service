using API.Models.Fights;
using API.Models.Fights.Responses;
using API.Services;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Route("fights")]
[ApiController]
public class FightsController : ControllerBase
{
    private readonly ILogger<FightsController> _logger;
    private readonly IGenericService<Fight> _fightService;
    private readonly IMapper<Fight, FightResponse> _fightMapper;

    public FightsController(ILogger<FightsController> logger,
                            IGenericService<Fight> fightService,
                            FightMapper fightMapper)
    {
        _logger = logger;
        _fightService = fightService;
        _fightMapper = fightMapper;
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        var fight = _fightService.GetById(id);
        if (fight is not null)
        {
            var response = _fightMapper.Map(fight);
            return Ok(response);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var fights = _fightService.GetAll();
        var fightsResponse = new FightsResponse()
        {
            Items = _fightMapper.MapList(fights)
        };

        return Ok(fightsResponse);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        _fightService.DeleteById(id);
        return Ok();
    }
}