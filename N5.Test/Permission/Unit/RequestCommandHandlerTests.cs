namespace N5.Test.Permission.Unit
{
    using Xunit;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using N5.Business.Features.Permission.Commands;
    using N5.Business.Interfaces.DataAccess;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;
    using N5.Core.Entities;
    using N5.Core.Dtos;

    public class RequestCommandHandlerTests
    {
        [Fact]
        public async Task Handle_InsertsPermission_AndCallsServices_WhenInsertSucceeds()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();
            var groupServiceMock = new Mock<IGroupService>();

            var handler = new RequestCommandHandler(unitOfWorkMock.Object, mapperMock.Object, groupServiceMock.Object);
            var request = new RequestCommand();

            unitOfWorkMock.Setup(x => x.BeginTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.CommitTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.CloseTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.PermissionRepository.InsertAsync(It.IsAny<Permission>())).ReturnsAsync(true);

            groupServiceMock.Setup(x => x.PermissionElasticsearchService.InsertAsync(It.IsAny<PermissionDto>())).Returns(Task.CompletedTask);
            groupServiceMock.Setup(x => x.KafkaServices.SendServices(It.IsAny<KafkaDto>())).Returns((Task<bool>)Task.CompletedTask);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify();
            groupServiceMock.Verify();
        }

        [Fact]
        public async Task Handle_RollsBackTransaction_AndThrowsException_WhenInsertFails()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();
            var groupServiceMock = new Mock<IGroupService>();

            var handler = new RequestCommandHandler(unitOfWorkMock.Object, mapperMock.Object, groupServiceMock.Object);
            var request = new RequestCommand();

            unitOfWorkMock.Setup(x => x.BeginTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.RollbackTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.CloseTransactionAsync()).Verifiable();
            unitOfWorkMock.Setup(x => x.PermissionRepository.InsertAsync(It.IsAny<Permission>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await handler.Handle(request, CancellationToken.None));

            // Assert
            unitOfWorkMock.Verify();
        }
    }

}

