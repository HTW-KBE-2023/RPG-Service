using API.Modells.Monsters;
using API.Modells.Players;

namespace API.Modells.Fights.Responses;

public class FightResponse
{
    public Guid Id { get; set; }
    public Player? Player { get; set; }
    public Monster? Enemy { get; set; }
    public IList<string>? Summary { get; set; }
    public bool Completed { get; set; }
}