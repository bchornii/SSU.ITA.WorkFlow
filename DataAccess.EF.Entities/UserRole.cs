using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public ICollection<UserInformation> UserInformation { get; set; }

        public UserRole()
        {
            UserInformation = new List<UserInformation>();
        }
    }
}