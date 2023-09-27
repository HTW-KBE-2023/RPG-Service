using API.Models.Fights;
using FluentValidation;

namespace API.Services.Fights;

public class FightValidator : AbstractValidator<Fight>
{
    public FightValidator()
    {
        RuleFor(fight => fight.Id).NotEmpty();
        RuleFor(fight => fight.Player).NotEmpty();
    }
}