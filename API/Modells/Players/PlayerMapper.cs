using API.Modells.Monsters.Requests;
using API.Modells.Monsters.Responses;
using API.Modells.Players;
using API.Modells.Players.Responses;
using Boxed.Mapping;

namespace API.Modells.Monsters;

public class PlayerMapper : IMapper<Player, PlayerResponse>
{
    public void Map(Player source, PlayerResponse destination)
    {
        destination.Id = source.Id;
        destination.Name = source.Name;
        destination.Level = source.Level;
        destination.Health = source.Health;
        destination.Defence = source.Defence;
        destination.Attack = source.Attack;
        destination.Experience = source.Experience;
    }
}