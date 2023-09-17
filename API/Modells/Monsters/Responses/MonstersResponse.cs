namespace API.Modells.Monsters.Responses;

public class MonstersResponse
{
    public IEnumerable<MonsterResponse> Items { get; set; } = Enumerable.Empty<MonsterResponse>();
}