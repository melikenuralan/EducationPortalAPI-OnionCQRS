using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Features.Roles.Command.DeleteRole;
using Moq;
using Shouldly;

namespace EducationPortal.UnitTest.Features.Roles.Command.DeleteRole
{
    public sealed class DeleteRoleCommandUnitTest
    {
        private readonly Mock<IRoleService> _roleServiceMock;

        public DeleteRoleCommandUnitTest()
        {
            _roleServiceMock = new();
        }
        [Fact]
        public async Task DeleteRoleShouldFail()
        {
            _roleServiceMock.Setup(x => x.DeleteRole(It.IsAny<Guid>())).ReturnsAsync(false);
            var result = await _roleServiceMock.Object.DeleteRole(Guid.Parse("e007c720-42ee-499e-9ef4-d6c95428bc0e"));

            result.ShouldBeFalse();
        }
        [Fact]
        public async Task DeleteRoleCommandResponseShouldNotBeNull()
        {
            var command = new DeleteRoleCommandRequest
            {
                Id = Guid.Parse("e007c720-42ee-499e-9ef4-d6c95428bc0e")
            };
            _roleServiceMock.Setup(x => x.DeleteRole(It.IsAny<Guid>())).ReturnsAsync(true);

            var handler = new DeleteRoleCommandHandler(_roleServiceMock.Object);

            DeleteRoleCommandResponse response = await handler.Handle(command, default);

            response.ShouldNotBeNull();
            response.Succeeded.ShouldBeTrue();

            _roleServiceMock.Verify(x=>x.DeleteRole(It.IsAny<Guid>()), Times.Once());       
        }
    }
}
