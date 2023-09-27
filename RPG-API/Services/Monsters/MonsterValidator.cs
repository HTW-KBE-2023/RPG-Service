using API.Models.Monsters;
using FluentValidation;

namespace API.Services.Monsters;

public class MonsterValidator : AbstractValidator<Monster>
{
    public MonsterValidator()
    {
        RuleFor(monster => monster.Id).NotEmpty();
        RuleFor(monster => monster.Name).NotEmpty();
        RuleFor(monster => monster.BaseAttack).NotEmpty();
        RuleFor(monster => monster.BaseDefence).NotEmpty();
        RuleFor(monster => monster.BaseHealth).NotEmpty();
    }
}