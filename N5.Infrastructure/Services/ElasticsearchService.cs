namespace N5.Infrastructure.Services
{
    using Microsoft.Extensions.Configuration;
    using N5.Business.Interfaces.Services;
    using N5.Infrastructure.Common.Constants;
    using N5.Infrastructure.Common.Dtos;
    using Nest;

    public class ElasticsearchService<T> : IElasticsearchService<T> where T : class
    {
        #region Properties
        private readonly IConfiguration _configuration;
        private readonly IElasticClient _elasticClient;
        private ElasticsearchDto? _elasticsearchDto;
        #endregion

        public ElasticsearchService(IConfiguration configuration)
		{
            _configuration = configuration;
            _elasticClient = Instance();
            ChekIndex().ConfigureAwait(true);
        }

        #region Queries
        public async Task<IList<T>> GetAllAsync()
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            ISearchResponse<T> results = await _elasticClient.SearchAsync<T>(s => s
                 .Index(indexName)
                 .Query(q => q
                    .MatchAll()
                 )
                 .Size(50)
            );

            return results.Documents.ToList();
        }

        public async Task<T> FirstAsync(int id)
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            var response = await _elasticClient.GetAsync<T>(id, q => q.Index(indexName));

            return response.Source;
        }
        #endregion

        #region Commands
        public async Task InsertAsync(T data)
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            await _elasticClient.CreateAsync(data, q => q.Index(indexName));
        }

        public async Task UpdateAsync(T data, int id)
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            await _elasticClient.UpdateAsync<T>(id, a => a.Index(indexName).Doc(data));
        }

        public async Task DeleteAsync(int id)
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            await _elasticClient.DeleteAsync(DocumentPath<T>.Id(id).Index(indexName));
        }

        public async Task DeleteIndexAsync()
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            await _elasticClient.Indices.DeleteAsync(indexName);
        }
        #endregion

        #region Private Method
        private ElasticClient Instance()
        {
            ElasticsearchDto instance = _elasticsearchDto = new ElasticsearchDto();
            _configuration.Bind(ElasticsearchConstant.Elasticsearch, instance);

            string? host = _elasticsearchDto.Host;
            int port = _elasticsearchDto.Port;
            string? username = _elasticsearchDto.Username;
            string? password = _elasticsearchDto.Password;
            var settings = new ConnectionSettings(new Uri(host + ":" + port));
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password)) settings.BasicAuthentication(username, password);

            return new ElasticClient(settings);
        }

        private async Task ChekIndex()
        {
            string? indexName = _elasticsearchDto?.PermisionIndex;
            var anyy = await _elasticClient.Indices.ExistsAsync(indexName);
            if (!anyy.Exists)
            {
                var response = await _elasticClient.Indices.CreateAsync(indexName,
                ci => ci
                    .Index(indexName)
                    .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
                    );
            }
        }
        #endregion
    }
}

