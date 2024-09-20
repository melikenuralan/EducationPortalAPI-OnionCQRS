using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Features.Roles.Command.UpdateRole;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.UnitTest.Features.Roles.Command.UpdateRole
{
    public sealed class UpdateRoleCommandUnitTest
    {
        private readonly Mock<IRoleService> _roleServiceMock;
        public UpdateRoleCommandUnitTest()
        {
            _roleServiceMock = new();
        }
        [Fact]
        public async Task UpdateRoleShouldFail()
        {
            _roleServiceMock.Setup(x => x.UpdateRole(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(false);
            var result = await _roleServiceMock.Object.UpdateRole(Guid.Parse("e007c720-42ee-499e-9ef4-d6c95428bc0e"), "TestRole");
            result.ShouldBeFalse();
        }
        [Fact]
        public async Task UpdateRoleCommandReponseShouldNotBeNull()
        {
            var command = new UpdateRoleCommandRequest
            {
                Id = Guid.Parse("e007c720-42ee-499e-9ef4-d6c95428bc0e"),
                Name = "Role",
            };
            _roleServiceMock.Setup(x => x.UpdateRole(It.IsAny<Guid>(), It.IsAny<string>())).ReturnsAsync(true);
            var handler = new UpdateRoleCommandHandler(_roleServiceMock.Object);
            UpdateRoleCommandResponse response = await handler.Handle(command, default);
            response.ShouldNotBeNull();
            response.Succeeded.ShouldBeTrue();
            _roleServiceMock.Verify(x => x.UpdateRole(command.Id, command.Name), Times.Once());
        }
    }
}
