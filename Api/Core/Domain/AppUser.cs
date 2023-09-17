namespace Api.Core.Domain
{
    public class AppUser : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int? AppRoleId { get; set; }
        public AppRole? AppRole { get; set; }
    }
}
