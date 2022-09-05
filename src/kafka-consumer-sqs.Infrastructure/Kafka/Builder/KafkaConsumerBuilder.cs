using Confluent.Kafka;
using kafka_consumer_sqs.Domain.Interfaces.Repositories;
using kafka_consumer_sqs.Domain.Models;
using Microsoft.Extensions.Options;
using System;

namespace kafka_consumer_sqs.Infrastructure.Kafka.Builder
{
    public class KafkaConsumerBuilder : IKafkaConsumerBuilder
    {
        private readonly KafkaOptions _kafkaOptions;
        public KafkaConsumerBuilder(IOptions<KafkaOptions> kafkaOptions)
        {
            _kafkaOptions = kafkaOptions?.Value
                            ?? throw new ArgumentNullException(nameof(kafkaOptions));
        }


        public IConsumer<string, string> Build()
        {
            var config = new ClientConfig
            {
                BootstrapServers = "localhost:9092",
            };

            var consumerConfig = new ConsumerConfig(config)
            {
                GroupId = $"subscribe-group0",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            var consumerBuilder = new ConsumerBuilder<string, string>(consumerConfig);

            return consumerBuilder.Build();
        }
    }
}
