namespace SSU.ITA.WorkFlow.DataAccess.EF.Entities
{
    public class UserProjectRelation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }

        public UserInformation UserInformation { get; set; }
        public UserProject UserProject { get; set; }
    }
}
