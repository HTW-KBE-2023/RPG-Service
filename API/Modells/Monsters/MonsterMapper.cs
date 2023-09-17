using API.Modells.Monsters.Requests;
using API.Modells.Monsters.Responses;
using Boxed.Mapping;
using MassTransit;

namespace API.Modells.Monsters;

public class MonsterMapper : IMapper<Monster, MonsterResponse>, IMapper<CreateMonsterRequest, Monster>, IMapper<UpdateMonsterRequest, Monster>
{
    public void Map(Monster source, MonsterResponse destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Level = source.Level;
        destination.BaseHealth = source.BaseHealth;
        destination.BaseDefence = source.BaseDefence;
        destination.BaseAttack = source.BaseAttack;
        destination.LevelMultiplierHealth = source.LevelMultiplierHealth;
        destination.LevelMultiplierDefence = source.LevelMultiplierDefence;
        destination.LevelMultiplierAttack = source.LevelMultiplierAttack;
        destination.MaxHealth = source.MaxHealth;
        destination.Health = source.Health;
        destination.Defence = source.Defence;
        destination.Attack = source.Attack;
        destination.Experience = source.Experience;
    }

    public void Map(CreateMonsterRequest source, Monster destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Level = source.Level;
        destination.BaseHealth = source.BaseHealth;
        destination.BaseDefence = source.BaseDefence;
        destination.BaseAttack = source.BaseAttack;
        destination.LevelMultiplierHealth = source.LevelMultiplierHealth;
        destination.LevelMultiplierDefence = source.LevelMultiplierDefence;
        destination.LevelMultiplierAttack = source.LevelMultiplierAttack;
    }

    public void Map(UpdateMonsterRequest source, Monster destination)
    {
        destination.Level = source.Level;
        destination.Name = source.Name;
        destination.BaseHealth = source.BaseHealth;
        destination.BaseDefence = source.BaseDefence;
        destination.BaseAttack = source.BaseAttack;
        destination.LevelMultiplierHealth = source.LevelMultiplierHealth;
        destination.LevelMultiplierDefence = source.LevelMultiplierDefence;
        destination.LevelMultiplierAttack = source.LevelMultiplierAttack;
    }
}