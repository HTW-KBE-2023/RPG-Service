using API.Models.Fights;
using API.Models.Fights.Responses;
using API.Models.Monsters;
using API.Models.Monsters.Requests;
using API.Models.Monsters.Responses;
using API.Models.Players;
using API.Services.Dice;
using Boxed.Mapping;
using MassTransit;
using MessagingContracts.RPG;
using Moq;
using System.Dynamic;
using System.Reflection.Emit;
using Xunit;

namespace API.Test.Models.Monsters
{
    public class FightMapperTest

    {
        [Fact]
        public void WhenFightIsMappedToFightResponseThenPropertiesShouldBeTheSame()
        {
            IMapper<Fight, FightResponse> mapper = new FightMapper();
            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Player = new(),
                Enemy = new(),
                Completed = true,
                Summary = new List<string>()
                {
                    "Test",
                    "Test2"
                }
            };

            var response = mapper.Map(fight);

            Assert.Equal(fight.Id, response.Id);
            Assert.Equal(fight.Player, response.Player);
            Assert.Equal(fight.Enemy, response.Enemy);
            Assert.Equal(fight.Completed, response.Completed);
            Assert.NotNull(response.Summary);
            Assert.NotEmpty(response.Summary);
            foreach (var summaryPart in fight.Summary)
            {
                Assert.Contains(summaryPart, response.Summary);
            }
        }

        [Fact]
        public void WhenFightIsMappedToFightConcludedThenPropertiesShouldBeTheSame()
        {
            IMapper<Fight, FightConcluded> mapper = new FightMapper();
            var fight = new Fight()
            {
                Id = Guid.NewGuid(),
                Player = new(),
                Enemy = new(),
                Completed = true,
                Summary = new List<string>()
                {
                    "Test",
                    "Test2"
                }
            };

            var fightConcluded = mapper.Map(fight);

            Assert.Equal(fight.Id, fightConcluded.Id);
            Assert.Equal(fight.Player.Id, fightConcluded.Player);
            Assert.NotNull(fightConcluded.Summary);
            Assert.NotEmpty(fightConcluded.Summary);
            foreach (var summaryPart in fight.Summary)
            {
                Assert.Contains(summaryPart, fightConcluded.Summary);
            }
        }
    }
}