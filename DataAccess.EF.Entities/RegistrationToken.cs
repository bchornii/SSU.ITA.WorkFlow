namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class RegistrationToken
    {
        public int RegistrationTokenId { get; set; } 
        public string Token { get; set; }
        public int UserId { get; set; }

        public UserInformation UserInformation { get; set; }
    }
}
