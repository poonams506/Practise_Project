namespace MebershipApI.Moduels.Domain
{
    public class Members
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SocietyName { get; set; } = string.Empty;
        public string GenderName { get; set; } = string.Empty;

        public string MemberCategoryName { get; set; } = string.Empty;

        public List<int> HobbyList { get; set; } = new List<int>(); 

        public bool IsActive { get; set; } 
       
    }
}
