using Amazon.DynamoDBv2.DataModel;
using KafkaMessage.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaMessage.App.Database
{
    public class ClienteRepository : DynamoAbstract<Cliente>
    {
        public ClienteRepository(IDynamoDBContext context) : base(context)
        {
        }
        public async Task<List<Cliente>> GetAll() => await Scan();

        public async Task<List<Cliente>> GetById(Guid id) => await this.QueryByHash(id);


    }
}
