using CustTrack.Models.EntityFramework;
using System.Linq;
using System.Web.Security;

namespace CustTrack.Security
{
    public class _RoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new System.NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new System.NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            SalesManContextEntities db = new SalesManContextEntities();
            switch (db.T_Employee.FirstOrDefault(x => x.employee_username == username).employee_authority_id)
            {
                case 1:
                    return new string[] { "Admin" };
                case 2:
                    return new string[] { "Manager" };
                case 3:
                    return new string[] { "SalesMan" };
                default:
                    throw new System.NotImplementedException();
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new System.NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new System.NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new System.NotImplementedException();
        }
    }
}