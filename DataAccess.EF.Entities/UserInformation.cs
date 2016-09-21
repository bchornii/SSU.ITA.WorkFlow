using System;
using System.Collections;
using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserInformation
    {
        public int UserId { get; set; } 
        public int RoleId { get; set; } 
        public int CompanyId { get; set; } 
        public int ManagerId { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Salt { get; set; } 
        public string Name { get; set; } 
        public string SurName { get; set; } 
        public string Address { get; set; } 
        public string PhoneNumber { get; set; } 
        public string Photo { get; set; }
        public bool IsConfirmed { get; set; }


        public ICollection<RegistrationToken> RegistrationToken { get; set; }
        public ICollection<SessionToken> SessionToken { get; set; }
        public ICollection<UserProjectRelation> UserProjectRelation { get; set; }
        public ICollection<UserReport> UserReport { get; set; } 
        public ICollection<UserTask> UserTask { get; set; } 

        public UserRole UserRole { get; set; } 
        public CompanyInformation CompanyInformation { get; set; } 
       
        public UserInformation()
        {
            RegistrationToken = new List<RegistrationToken>();
            SessionToken = new List<SessionToken>();
            UserProjectRelation = new List<UserProjectRelation>();
            UserReport = new List<UserReport>();
            UserTask = new List<UserTask>();
        }
    }
}
