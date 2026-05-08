using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.CurrentUser
{
    public interface ICurrentUserService
    {
        public int? UserId { get; }
        public string? Name { get;}
        public string? Email { get;}
        public string? MobilePhone { get;}
        public string? Role { get; }
    }
}
