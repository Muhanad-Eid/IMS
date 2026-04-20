using Application.Repositories;
using Application.Services.RoleService.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.RoleService
{
    public class RoleService: IRoleService
    {
        private readonly IGenericRepository<Role> _roleRepository;
        public RoleService(IGenericRepository<Role> roleRepository) 
        {
            _roleRepository = roleRepository;
        }
        public List<GetRolesDto> GetRoles()
        {
            var data = _roleRepository.GetAll();
            var roles = data.Select(r => new GetRolesDto
            {
                Id = r.Id,
                Name = r.Name,
            }).ToList();
            return roles;
        }

       
    }
}
