namespace MessagingContracts.RPG;

public class FightConcluded
{
    public Guid Id { get; set; }
    public Guid Player { get; set; }
    public IList<string>? Summary { get; set; }
}