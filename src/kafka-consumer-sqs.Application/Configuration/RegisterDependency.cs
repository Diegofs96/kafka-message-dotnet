using kafka_consumer_sqs.Domain.Interfaces.Repositories;
using kafka_consumer_sqs.Domain.Models;
using kafka_consumer_sqs.Infrastructure.Kafka.Builder;
using kafka_consumer_sqs.Infrastructure.Kafka.Consumer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace kafka_consumer_sqs.Application.Configuration
{
    public static class RegisterDependency
    {
        public static void RegisterInjectionDependency(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IKafkaConsumerBuilder, KafkaConsumerBuilder>();
            services.AddSingleton<IKafkaTopicConsumer, KafkaTopicConsumer>();

            var kafka = new KafkaOptions();
            configuration.Bind("KafkaOptions", kafka);

            services.AddSingleton(kafka);

        }
    }
}
