using Amazon.DynamoDBv2.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaMessage.App.Database
{
    public abstract class DynamoAbstract<T> where T : class, new ()
    {
        private readonly IDynamoDBContext _context;

        protected DynamoAbstract(IDynamoDBContext context)
        {
            _context = context;
        }

        protected async Task<List<T>> Scan()
        {
            // Selecionando todos os itens da tabela
            return await _context.ScanAsync<T>(new List<ScanCondition>()).GetRemainingAsync();
        }

        protected async Task<List<T>> QueryByHash(object hashKey)
        {
            // Buscando todos os dados com base no hash
            return await _context.QueryAsync<T>(hashKey).GetRemainingAsync();
        }
        
        public async Task Save(T obj)
        {
            await _context.SaveAsync(obj);
        }
    }
}
