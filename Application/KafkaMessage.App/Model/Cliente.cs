using Amazon.DynamoDBv2.DataModel;
using System;

namespace KafkaMessage.App.Model
{
    [DynamoDBTable("ClienteTable")]
    public class Cliente
    {
        [DynamoDBHashKey]
        public Guid IdCliente { get; set; }

        [DynamoDBProperty]
        public string Nome { get; set; }

        [DynamoDBProperty]
        public string CPF { get; set; }

        [DynamoDBProperty]
        public DateTime DataNascimento { get; set; }
    }
}
