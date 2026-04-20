using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SystemRole Code { get; set; }
        public ICollection<User> Users { get; set; }
    }
    public enum SystemRole 
    { 
        Admin = 1,
        Employee = 2,
        User = 3,
    }
}
