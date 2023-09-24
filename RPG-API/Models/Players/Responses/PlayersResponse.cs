namespace API.Models.Players.Responses;

public class PlayersResponse
{
    public IEnumerable<PlayerResponse> Items { get; set; } = Enumerable.Empty<PlayerResponse>();
}