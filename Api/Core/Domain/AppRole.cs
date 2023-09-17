namespace Api.Core.Domain
{
    public class AppRole : BaseEntity
    {
        public string Definition { get; set; }
        public List<AppUser>? AppUsers { get; set; }
    }
}
