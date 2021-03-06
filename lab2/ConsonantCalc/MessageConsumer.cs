﻿using System;
using System.Threading.Tasks;
using MassTransit;
using System.Configuration;

using PoemFilterContract;

namespace ConsonantCalc
{
    public class MessageConsumer : IConsumer<CalculateConsonants>
    {
        public async Task Consume(ConsumeContext<CalculateConsonants> context)
        {
            Uri sendEndpointUri = new Uri(string.Concat(ConfigurationManager.AppSettings["RabbitMQHost"], ConfigurationManager.AppSettings["BestLineSelectorQueueName"]));
            var endpoint = await context.GetSendEndpoint(sendEndpointUri);
            await endpoint.Send<ExtractBestLines>(new
            {
                UserId = context.Message.UserId,
                CorrId = context.Message.CorrId,
                Text = context.Message.Text,
                VowelCounts = context.Message.VowelCounts,
                ConsonantCount = ConsonantCalculator.GetConsonantCountPerLine(context.Message.Text)
            });
        }
    }
}
