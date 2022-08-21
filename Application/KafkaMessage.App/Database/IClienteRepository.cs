using KafkaMessage.App.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KafkaMessage.App.Database
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetAll();
        Task<List<Cliente>> GetById(Guid id);
    }
}
