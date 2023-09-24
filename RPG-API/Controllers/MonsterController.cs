using API.Models.Monsters;
using API.Models.Monsters.Requests;
using API.Models.Monsters.Responses;
using API.Models.Validations;
using API.Models.Validations.Responses;
using API.Services;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller;

[Route("api/monsters")]
[ApiController]
public class MonsterController : ControllerBase
{
    private readonly ILogger<MonsterController> _logger;
    private readonly IGenericService<Monster> _monsterService;
    private readonly IMapper<CreateMonsterRequest, Monster> _createMonsterRequestToMonsterMapper;
    private readonly IMapper<UpdateMonsterRequest, Monster> _updateMonsterRequestToMonsterMapper;
    private readonly IMapper<Monster, MonsterResponse> _monsterToMonsterResponseMapper;
    private readonly IMapper<ValidationFailed, ValidationFailureResponse> _validationMapper;

    public MonsterController(ILogger<MonsterController> logger,
                             IGenericService<Monster> monsterService,
                             MonsterMapper monsterMapper,
                             ValidationFailedMapper validationMapper)
    {
        _logger = logger;
        _monsterService = monsterService;
        _monsterToMonsterResponseMapper = monsterMapper;
        _createMonsterRequestToMonsterMapper = monsterMapper;
        _updateMonsterRequestToMonsterMapper = monsterMapper;
        _validationMapper = validationMapper;
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMonsterRequest request)
    {
        var monster = _createMonsterRequestToMonsterMapper.Map(request);

        var result = _monsterService.Create(monster);
        return result.Match<IActionResult>(
            _ => CreatedAtAction(nameof(Get), new { id = monster.Id }, _monsterToMonsterResponseMapper.Map(monster)),
            failed => BadRequest(_validationMapper.Map(failed)));
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateMonsterRequest request)
    {
        var monster = _updateMonsterRequestToMonsterMapper.Map(request);
        monster.Id = id;

        var result = _monsterService.Update(monster);

        return result.Match<IActionResult>(
            monster => monster is not null ? Ok(_monsterToMonsterResponseMapper.Map(monster)) : NotFound(),
            failed => BadRequest(_validationMapper.Map(failed)));
    }

    [HttpGet("{id:guid}")]
    public IActionResult Get([FromRoute] Guid id)
    {
        var monster = _monsterService.GetById(id);
        if (monster is not null)
        {
            var response = _monsterToMonsterResponseMapper.Map(monster);
            return Ok(response);
        }

        return NotFound();
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var monsters = _monsterService.GetAll();
        var monstersResponse = new MonstersResponse()
        {
            Items = _monsterToMonsterResponseMapper.MapList(monsters)
        };

        return Ok(monstersResponse);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        _monsterService.DeleteById(id);
        return Ok();
    }
}