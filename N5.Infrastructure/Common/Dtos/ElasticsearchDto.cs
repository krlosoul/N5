namespace N5.Infrastructure.Common.Dtos
{
	public class ElasticsearchDto
	{
		public string? Host { get; set; }
        public int Port { get; set; }
        public string? PermisionIndex { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}