using FluentValidation;

namespace API.Models.Monsters;

public class MonsterValidator : AbstractValidator<Monster>
{
    public MonsterValidator()
    {
        RuleFor(monster => monster.Id).NotEmpty();
    }
}