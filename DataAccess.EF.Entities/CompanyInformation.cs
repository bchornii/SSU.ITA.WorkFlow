using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class CompanyInformation
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Desciption { get; set; }

        public ICollection<UserInformation> UserInformation { get; set; }

        public CompanyInformation()
        {
            UserInformation = new List<UserInformation>();
        }
    }
}
