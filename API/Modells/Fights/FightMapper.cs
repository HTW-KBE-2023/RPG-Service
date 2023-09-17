using API.Modells.Fights.Responses;
using Boxed.Mapping;
using MessagingContracts.RPG;

namespace API.Modells.Fights;

public class FightMapper : IMapper<Fight, FightConcluded>, IMapper<Fight, FightResponse>
{
    public void Map(Fight source, FightConcluded destination)
    {
        destination.Id = source.Id;
        destination.Player = source.Player.Id;
        destination.Summary = source.Summary;
    }

    public void Map(Fight source, FightResponse destination)
    {
        destination.Id = source.Id;
        destination.Player = source.Player;
        destination.Enemy = source.Enemy;
        destination.Summary = source.Summary;
        destination.Completed = source.Completed;
    }
}