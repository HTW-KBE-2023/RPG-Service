namespace API.Models.Monsters.Requests;

public class UpdateMonsterRequest
{
    public int Level { get; set; }
    public string? Name { get; set; }
    public int BaseHealth { get; set; }
    public int BaseDefence { get; set; }
    public int BaseAttack { get; set; }
    public int LevelMultiplierHealth { get; set; }
    public int LevelMultiplierDefence { get; set; }
    public int LevelMultiplierAttack { get; set; }
}