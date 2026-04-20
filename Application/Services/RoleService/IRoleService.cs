using Application.Services.RoleService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.RoleService
{
    public interface IRoleService
    {
        List<GetRolesDto> GetRoles();
    }
}
