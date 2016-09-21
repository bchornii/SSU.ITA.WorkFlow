using System;
using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class SessionToken
    {
        public int SessionTokenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
        
        public UserInformation UserInformation { get; set; } 
    }
}
