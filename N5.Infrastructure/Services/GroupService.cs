namespace N5.Infrastructure.Services
{
    using Microsoft.Extensions.Configuration;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;

    public class GroupService : IGroupService
    {
        private readonly IConfiguration _configuration;
        private IKafkaServices? _kafkaServices;
        private IElasticsearchService<PermissionDto>? _permissionElasticsearchService;

        public GroupService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #region Services
        public IKafkaServices KafkaServices
        {
            get
            {
                return _kafkaServices ??= new KafkaServices(_configuration);
            }
        }
        #endregion

        #region ElasticSearsh
        public IElasticsearchService<PermissionDto> PermissionElasticsearchService
        {
            get
            {
                return _permissionElasticsearchService ??= new ElasticsearchService<PermissionDto>(_configuration);
            }
        }
        #endregion
    }
}

