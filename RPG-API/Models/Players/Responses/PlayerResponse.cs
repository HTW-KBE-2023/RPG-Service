namespace API.Models.Players.Responses;

public class PlayerResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public int Level { get; set; }
    public int Health { get; set; }
    public int Defence { get; set; }
    public int Attack { get; set; }
    public double Experience { get; set; }
}