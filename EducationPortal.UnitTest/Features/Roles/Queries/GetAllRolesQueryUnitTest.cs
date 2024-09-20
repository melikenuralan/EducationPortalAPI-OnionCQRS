using EducationPortal.Application.Abstractions.IServices;
using Moq;
using Shouldly;

namespace EducationPortal.UnitTest.Features.Roles.Queries
{
    public sealed class GetAllRolesQueryUnitTest
    {
        private readonly Mock<IRoleService> _roleServiceMock;
        public GetAllRolesQueryUnitTest()
        {
            _roleServiceMock = new();
        }
        [Fact]
        public async Task GetAllRolesQueryResponseShouldNotBeNull()
        {
            _roleServiceMock.Setup(x => x.GetAllRoles()).Returns(new Dictionary<Guid, string>());
            var result =  _roleServiceMock.Object.GetAllRoles();
            result.ShouldNotBeNull();
            result.ShouldBeOfType<Dictionary<Guid, string>>();
        }
    }
}
