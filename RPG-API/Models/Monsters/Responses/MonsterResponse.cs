namespace API.Models.Monsters.Responses;

public class MonsterResponse
{
    public Guid Id { get; set; }
    public int Level { get; set; }
    public string? Name { get; set; }
    public int BaseHealth { get; set; }
    public int BaseDefence { get; set; }
    public int BaseAttack { get; set; }
    public int LevelMultiplierHealth { get; set; }
    public int LevelMultiplierDefence { get; set; }
    public int LevelMultiplierAttack { get; set; }
    public int MaxHealth { get; set; }
    public int Health { get; set; }
    public int Defence { get; set; }
    public int Attack { get; set; }
    public double Experience { get; set; }
}