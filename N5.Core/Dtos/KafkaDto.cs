namespace N5.Core.Dtos
{
    using N5.Core.Enums;

    public class KafkaDto
	{
        public Guid Id { get; set; }
        public OperationEnum NameOperation { get; set; }
    }
}