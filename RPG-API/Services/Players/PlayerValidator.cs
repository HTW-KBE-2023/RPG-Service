﻿using API.Models.Players;
using FluentValidation;

namespace API.Services.Players;

public class PlayerValidator : AbstractValidator<Player>
{
    public PlayerValidator()
    {
        RuleFor(player => player.Id).NotEmpty();
        RuleFor(player => player.Attack).NotEmpty();
        RuleFor(player => player.Defence).NotEmpty();
        RuleFor(player => player.Health).NotEmpty();
    }
}