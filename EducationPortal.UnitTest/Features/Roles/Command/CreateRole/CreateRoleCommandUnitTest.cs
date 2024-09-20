using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Features.Roles.Command.CreateRole;
using Moq;
using Shouldly;

namespace EducationPortal.UnitTest.Features.Roles.Command.CreateRole
{
    public sealed class CreateRoleCommandUnitTest
    {
        private readonly Mock<IRoleService> _roleServiceMock;

        public CreateRoleCommandUnitTest()
        {
            _roleServiceMock = new();
        }

        [Fact]
        public async Task CreateRoleShouldFail()
        {
            _roleServiceMock.Setup(x => x.CreateRole(It.IsAny<string>())).ReturnsAsync(false); 
            _ = await _roleServiceMock.Object.CreateRole("TestRole");
        }
        [Fact]
        public async Task CreateRoleCommandReponseShouldNotBeNull()
        {
            var command = new CreateRoleCommandRequest
            {
                Name = "Role"
            };
            _roleServiceMock.Setup(x => x.CreateRole(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateRoleCommandHandler(_roleServiceMock.Object);

            CreateRoleCommandReponse response = await handler.Handle(command, default);
            response.ShouldNotBeNull();
            response.Succeeded.ShouldBeTrue();

            _roleServiceMock.Verify(x => x.CreateRole(command.Name), Times.Once);
        }
    }
}
