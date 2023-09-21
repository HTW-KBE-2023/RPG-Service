using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Services;
using API.Services.Dice;
using API.Services.Fights;
using MassTransit;
using MessagingContracts.RPG;

namespace API.Port.MessageQueue.Consumers;

public class FightRequestedConsumer : IConsumer<FightRequested>
{
    private readonly ILogger<FightRequestedConsumer> _logger;
    private readonly FightSimulator _fightSimulator;

    public FightRequestedConsumer(ILogger<FightRequestedConsumer> logger, FightSimulator fightSimulator)
    {
        _logger = logger;
        _fightSimulator = fightSimulator;
    }

    public Task Consume(ConsumeContext<FightRequested> context)
    {
        var requestedFight = context.Message;
        _logger.LogInformation("Request a fight for Player {player}", requestedFight.Player);

        _fightSimulator.SimulationFor(requestedFight);

        return Task.CompletedTask;
    }
}