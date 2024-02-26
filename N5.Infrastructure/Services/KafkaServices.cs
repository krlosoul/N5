namespace N5.Infrastructure.Services
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Confluent.Kafka;
    using Microsoft.Extensions.Configuration;
    using N5.Business.Interfaces.Services;
    using N5.Infrastructure.Common.Constants;
    using ConfigKafka = Common.Dtos.KafkaDto;
    using DtoKafka = Core.Dtos.KafkaDto;

    public class KafkaServices : IKafkaServices
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private ConfigKafka? _configKafka;
        #endregion

        public KafkaServices(IConfiguration configuration)
		{
            _configuration = configuration;
            Configuration();
        }

        public async Task<bool> SendServices(DtoKafka dtoKafka)
        {
            string? bootstrapServers = _configKafka?.BootstrapServers;
            string? topic = _configKafka?.Topic;
            string message = JsonSerializer.Serialize(dtoKafka);
            ProducerConfig config = new()
            {
                BootstrapServers = bootstrapServers,
                ClientId = Dns.GetHostName()
            };
            try
            {
                using (var producer = new ProducerBuilder<Null, string>(config).Build())
                {
                    var result = await producer.ProduceAsync(topic, new Message<Null, string>
                    {
                        Value = message
                    });

                    Debug.WriteLine($"Delivery Timestamp:{result.Timestamp.UtcDateTime}");
                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
            }

            return await Task.FromResult(false);
        }

        #region Private Method
        private void Configuration()
        {
            ConfigKafka instance = _configKafka = new ConfigKafka();
            _configuration.Bind(KafkaConstant.Kafka, instance);
        }
        #endregion
    }
}

