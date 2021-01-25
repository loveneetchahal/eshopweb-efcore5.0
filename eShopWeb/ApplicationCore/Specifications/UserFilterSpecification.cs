using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Specifications
{
    public class UserFilterSpecification: Specification<User>
    {
        public UserFilterSpecification( int? roleId)
        {
            Query.Where(i => (!roleId.HasValue || i.RoleId == roleId));
        }
        public UserFilterSpecification(string userName, string password)
        {
            Query.Where(b => b.Email == userName && b.Password == password)
                .Include(b => b.UserRoles);
        }
    }
}
