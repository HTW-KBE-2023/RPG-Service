﻿namespace API.Models.Fights.Responses;

public class FightsResponse
{
    public IEnumerable<FightResponse> Items { get; set; } = Enumerable.Empty<FightResponse>();
}