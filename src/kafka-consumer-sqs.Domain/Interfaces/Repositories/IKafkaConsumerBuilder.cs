using Confluent.Kafka;

namespace kafka_consumer_sqs.Domain.Interfaces.Repositories
{
    public interface IKafkaConsumerBuilder
    {
        IConsumer<string, string> Build();
    }
}
