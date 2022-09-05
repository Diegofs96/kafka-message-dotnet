using Confluent.Kafka;
using kafka_consumer_sqs.Domain.Interfaces.Repositories;
using kafka_consumer_sqs.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kafka_consumer_sqs.Domain.Worker
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly ILogger<ConsumerWorker> _logger;
        private readonly IKafkaTopicConsumer _kafkaTopicConsumer;
        private readonly IConfiguration _configuration;
        private readonly string _host;
        private readonly string _topic;
        private readonly KafkaOptions _kafkaOptions;

        public ConsumerWorker(ILogger<ConsumerWorker> logger, IConfiguration configuration, IKafkaTopicConsumer kafkaTopicConsumer)
        {
            _logger = logger;
            _configuration = configuration;
            _kafkaTopicConsumer = kafkaTopicConsumer;
            _host = _configuration.GetSection("KafkaOptions:Host").Value;
            _topic = _configuration.GetSection("KafkaOptions:KafkaBootstrapServers").Value;
            _kafkaOptions = new KafkaOptions();
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaOptions.Host,
                GroupId = $"{_topic}-group0",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _logger.LogInformation("Iniciou o consumidor");
            _kafkaTopicConsumer.StartConsuming(_topic, stoppingToken);

            return Task.CompletedTask;
        }
    }
}
