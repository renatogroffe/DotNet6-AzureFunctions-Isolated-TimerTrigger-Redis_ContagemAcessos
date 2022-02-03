using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace FunctionAppContadorRedis
{
    public static class RefreshContador
    {
        private static ConnectionMultiplexer _redisConnection =
            ConnectionMultiplexer.Connect(
               Environment.GetEnvironmentVariable("Redis"));

        [Function(nameof(RefreshContador))]
        public static void Run([TimerTrigger("*/5 * * * * *")] FunctionContext context)
        {
            var logger = context.GetLogger(nameof(RefreshContador));
            logger.LogInformation(
                $"Valor atual = {_redisConnection.GetDatabase().StringIncrement("ContadorAzureFunctions")}");
        }
    }
}