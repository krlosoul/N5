namespace N5.Test.Permission.Unit
{
    using Xunit;
    using Moq;
    using System.Threading;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using N5.Business.Features.Permission.Queries;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;

    public class GetPermissionsQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsPermissions_WhenDataExists()
        {
            // Arrange
            var mockGroupService = new Mock<IGroupService>();
            mockGroupService.Setup(x => x.PermissionElasticsearchService.GetAllAsync())
                            .ReturnsAsync(new List<PermissionDto>() { new PermissionDto() });

            var handler = new GetPermissionsQueryHandler(mockGroupService.Object);
            var query = new GetPermissionsQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotEmpty(result);
        }
    }
}