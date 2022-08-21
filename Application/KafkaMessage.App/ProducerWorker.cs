using Confluent.Kafka;
using KafkaMessage.App.Database;
using KafkaMessage.App.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaMessage.App
{
    public class ProducerWorker : BackgroundService
    {
        private readonly ILogger<ProducerWorker> _logger;
        private readonly ClienteRepository _clienteRepository;
        private readonly IConfiguration _config;
        private readonly string _host;
        private readonly string _topic;

        public ProducerWorker(ILogger<ProducerWorker> logger, IConfiguration configuration, ClienteRepository clienteRepository)
        {
            _logger = logger;
            _config = configuration;
            _host = _config.GetSection("Kafka:Host").Value;
            _topic = _config.GetSection("Kafka:Topic").Value;
            _clienteRepository = clienteRepository;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation($"Configuração Iniciada para o topico {_topic}");
                var config = new ProducerConfig
                {
                    BootstrapServers = _host
                };


                _logger.LogInformation($"Producer iniciado");
                using ( var producer = new ProducerBuilder<string, string>(config).Build())
                {
                    int i = 0;
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var cliente = new Cliente()
                        {
                            IdCliente = Guid.NewGuid(),
                            Nome = $"Diego Teste {i}",
                            CPF = $"{i}{i}{i}.{i}{i}{i}.{i}{i}{i}-{i}{i}",
                            DataNascimento = DateTime.Now
                        };
                        var result = producer.ProduceAsync(_topic, new Message<string, string>
                        {
                            Key = $"KEY+{i}",
                            Value = JsonConvert.SerializeObject(cliente),
                        });

                        i++;

                        Task.WaitAll(new Task[] {
                            _clienteRepository.Save(cliente)
                        }, cancellationToken: stoppingToken) ;

                        _logger.LogInformation($"Producer incluiu mensagem {i}");

                        
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocorreu um erro na mensageria: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
