using API.Modells.Fights;
using API.Modells.Monsters;
using API.Modells.Players;
using API.Services.Dice;
using Moq;
using System.Dynamic;
using Xunit;

namespace API.Test.Models.Fights
{
    public class FightTest
    {
        [Fact]
        public void WhenFightIsCreatedThenIdShouldNotBeEmpty()
        {
            var fight = new Fight();

            Assert.NotEqual(Guid.Empty, fight.Id);
        }

        [Fact]
        public void WhenFightIsCreatedThenPlayerShouldBeDefault()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Player);
        }

        [Fact]
        public void WhenFightIsCreatedThenEnemyShouldBeDefault()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Enemy);
        }

        [Fact]
        public void WhenFightIsCreatedThenSummaryShouldNotBeNullButEmpty()
        {
            var fight = new Fight();

            Assert.NotNull(fight.Summary);
            Assert.Empty(fight.Summary);
        }

        [Fact]
        public void WhenFightIsCreatedThenCompletedShouldNotBeFalse()
        {
            var fight = new Fight();

            Assert.False(fight.Completed);
        }

        [Fact]
        public void WhenFightWasSimulatedThenCompletedShouldBeTrueAndSummaryFilled()
        {
            var fakeDiceService = new Mock<IDiceService>();
            fakeDiceService.Setup(dice => dice.Roll()).Returns(10);
            fakeDiceService.Setup(dice => dice.RollAgainst(It.IsAny<int>())).Returns(true);

            var fight = new Fight()
            {
                Player = new Player()
                {
                    Name = "Player",
                    Attack = 5,
                    Defence = 5,
                    Health = 5
                },
                Enemy = new Monster()
                {
                    Name = "Monster"
                }
            };

            fight.Simulate(fakeDiceService.Object);

            Assert.True(fight.Completed);
            Assert.NotEmpty(fight.Summary);
        }
    }
}