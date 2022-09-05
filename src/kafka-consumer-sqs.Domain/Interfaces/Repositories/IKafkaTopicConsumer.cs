using System.Threading;

namespace kafka_consumer_sqs.Domain.Interfaces.Repositories
{
    public interface IKafkaTopicConsumer
    {
        void StartConsuming(string topic, CancellationToken stoppingToken);
    }
}
