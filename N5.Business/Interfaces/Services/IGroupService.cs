namespace N5.Business.Interfaces.Services
{
    using N5.Core.Dtos.Permission;

    public interface IGroupService
	{
        /// <summary>
        /// Elasticsearch service for handling PermissionDto.
        /// </summary>
        public IElasticsearchService<PermissionDto> PermissionElasticsearchService { get; }

        /// <summary>
        /// Kafka services.
        /// </summary>
        public IKafkaServices KafkaServices { get; }
    }
}
