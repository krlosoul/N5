namespace N5.Business.Interfaces.Services
{
    using N5.Core.Dtos;

    public interface IKafkaServices
	{
        /// <summary>
        /// Sends services using a Kafka service.
        /// </summary>
        /// <param name="kafkaDto">The service data to be sent.</param>
        /// <returns>Result indicates if the operation was successful or not.</returns>
        Task<bool> SendServices(KafkaDto kafkaDto);
    }
}

