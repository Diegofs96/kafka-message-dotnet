using System;
using System.Collections.Generic;
using System.Text;

namespace kafka_consumer_sqs.Domain.Models
{
    public class KafkaOptions
    {
        public string KafkaBootstrapServers { get; set; }
        public string ConsumerGroupId { get; set; }
        public string Host { get; set; }
    }
}
