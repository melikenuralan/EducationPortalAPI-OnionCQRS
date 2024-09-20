using EducationPortal.Application.Abstractions.IServices;
using EducationPortal.Application.Dtos.User;
using EducationPortal.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPortal.Persistence.Services
{
    public class RoleService : IRoleService
    {
        readonly RoleManager<Role> _roleManager;
        readonly UserManager<User> _userManager;

        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<bool> CreateRole(string name)
        {
            IdentityResult result = await _roleManager.CreateAsync(new() { Id = Guid.NewGuid(), Name = name });
            return result.Succeeded;
        }

        public async Task<bool> DeleteRole(Guid id)
        {
            Role? role = await _roleManager.FindByIdAsync(id.ToString());

            IdentityResult result = await _roleManager.DeleteAsync(role);
            return result.Succeeded;
        }
        public async Task<bool> UpdateRole(Guid id, string name)
        {
            Role? role = await _roleManager.FindByIdAsync(id.ToString());
            role.Name=name;
            IdentityResult result = await _roleManager.UpdateAsync(role);
            return result.Succeeded;
        }
        public IDictionary<Guid, string> GetAllRoles()
        {
            return _roleManager.Roles.ToDictionary(role => role.Id, role => role.Name);

        }
        public async Task<(Guid id, string name)> GetRoleById(Guid id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            return (id, role.Name);
        }        
    }
}
