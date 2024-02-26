namespace N5.Infrastructure.Common.Dtos
{
	public class KafkaDto
	{
        public string? Topic { get; set; }
        public string? BootstrapServers { get; set; }
    }
}