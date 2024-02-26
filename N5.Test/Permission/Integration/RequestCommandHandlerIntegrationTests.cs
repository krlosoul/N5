﻿namespace N5.Test.Permission.Integration
{
    using Xunit;
    using System.Threading.Tasks;
    using AutoMapper;
    using Moq;
    using N5.Business.Features.Permission.Commands;
    using N5.Business.Interfaces.DataAccess;
    using N5.Business.Interfaces.Services;
    using N5.Core.Dtos.Permission;
    using N5.Core.Dtos;

    public class RequestCommandHandlerIntegrationTests
    {
        [Fact]
        public async Task Handle_CallsUnitOfWorkAndGroupServiceMethods_WithCorrectParameters()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var mapperMock = new Mock<IMapper>();
            var groupServiceMock = new Mock<IGroupService>();

            var handler = new RequestCommandHandler(unitOfWorkMock.Object, mapperMock.Object, groupServiceMock.Object);
            var command = new RequestCommand();

            // Act
            await handler.Handle(command, CancellationToken.None);

            // Assert
            unitOfWorkMock.Verify(x => x.BeginTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(x => x.CommitTransactionAsync(), Times.Once);
            unitOfWorkMock.Verify(x => x.RollbackTransactionAsync(), Times.Never);
            unitOfWorkMock.Verify(x => x.CloseTransactionAsync(), Times.Once);

            groupServiceMock.Verify(x => x.PermissionElasticsearchService.InsertAsync(It.IsAny<PermissionDto>()), Times.Once);
            groupServiceMock.Verify(x => x.KafkaServices.SendServices(It.IsAny<KafkaDto>()), Times.Once);
        }
    }

}

