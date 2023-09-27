using API.Models.Monsters.Requests;
using API.Models.Monsters.Responses;
using API.Models.Players;
using API.Models.Players.Responses;
using Boxed.Mapping;

namespace API.Services.Players;

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