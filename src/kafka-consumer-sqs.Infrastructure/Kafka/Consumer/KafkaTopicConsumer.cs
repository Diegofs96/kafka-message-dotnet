using kafka_consumer_sqs.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace kafka_consumer_sqs.Infrastructure.Kafka.Consumer
{
    public class KafkaTopicConsumer : IKafkaTopicConsumer
    {
        private readonly IKafkaConsumerBuilder _kafkaConsumerBuilder;
        private readonly ILogger<KafkaTopicConsumer> _logger;


        public KafkaTopicConsumer(IKafkaConsumerBuilder kafkaConsumerBuilder, ILogger<KafkaTopicConsumer> logger)
        {
            _logger = logger;
            _kafkaConsumerBuilder = kafkaConsumerBuilder;
        }

        public void StartConsuming(string topic, CancellationToken stoppingToken)
        {
            using (var consumer = _kafkaConsumerBuilder.Build())
            {
                consumer.Subscribe(topic);
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var consume = consumer.Consume(stoppingToken);
                        _logger.LogInformation($"Key: {consume.Message.Key} | Value: {consume.Message.Value}");
                    }
                    catch (OperationCanceledException)
                    {
                        // do nothing on cancellation
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        consumer.Close();
                        consumer.Dispose();
                    }
                }
            };
        }
    }
}
