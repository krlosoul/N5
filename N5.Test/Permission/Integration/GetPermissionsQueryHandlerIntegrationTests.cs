namespace N5.Test.Permission.Integration
{
    using Xunit;
    using System.Threading.Tasks;
    using Moq;
    using N5.Business.Features.Permission.Queries;
    using N5.Core.Dtos;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;

    public class GetPermissionsQueryHandlerIntegrationTests
    {
        [Fact]
        public async Task Handle_CallsKafkaService_WithCorrectParameters()
        {
            // Arrange
            var groupServiceMock = new Mock<IGroupService>();
            var kafkaServiceMock = new Mock<IKafkaServices>();

            groupServiceMock.Setup(x => x.PermissionElasticsearchService.GetAllAsync())
                            .ReturnsAsync(new List<PermissionDto>() { new PermissionDto() });

            var handler = new GetPermissionsQueryHandler(groupServiceMock.Object);
            var query = new GetPermissionsQuery();

            // Act
            await handler.Handle(query, CancellationToken.None);

            // Assert
            groupServiceMock.Verify(x => x.KafkaServices.SendServices(It.IsAny<KafkaDto>()), Times.Once);

        }
    }
}

