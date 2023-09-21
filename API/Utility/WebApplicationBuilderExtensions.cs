﻿using API.Models.Fights;
using API.Models.Monsters;
using API.Models.Players;
using API.Models.Validations;
using API.Port.Database;
using API.Port.Repositories;
using API.Services;
using API.Services.Dice;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace API.Utility
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RPGContext>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("MySQL");
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
            builder.Services.AddTransient<DbInitialiser>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IDiceService, DiceService>(_ => new DiceService(Random.Shared, DiceValue.D20));
            builder.Services.AddScoped<IGenericService<Fight>, GenericService<Fight>>();
            builder.Services.AddScoped<IGenericService<Player>, GenericService<Player>>();
            builder.Services.AddScoped<IGenericService<Monster>, GenericService<Monster>>();
        }

        public static void AddMapper(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<FightMapper, FightMapper>();
            builder.Services.AddSingleton<PlayerMapper, PlayerMapper>();
            builder.Services.AddSingleton<MonsterMapper, MonsterMapper>();
            builder.Services.AddSingleton<ValidationFailedMapper, ValidationFailedMapper>();
        }

        public static void AddRabbitMqConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.SetInMemorySagaRepositoryProvider();

                var assembly = typeof(Program).Assembly;

                x.AddConsumers(assembly);
                x.AddSagaStateMachines(assembly);
                x.AddSagas(assembly);
                x.AddActivities(assembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    var connection = builder.Configuration.GetConnectionString("RabbitMQ");
                    cfg.Host(connection, "/", h =>
                    {
                        var rabbitMQConfiguration = builder.Configuration.GetSection("RabbitMQ.Configuration");
                        var user = rabbitMQConfiguration.GetValue<string>("User");
                        var password = rabbitMQConfiguration.GetValue<string>("Password");

                        h.Username(user);
                        h.Password(password);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}