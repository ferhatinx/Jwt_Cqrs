namespace Api.Core.Dto
{
    public class CheckUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string? Role { get; set; }

        public bool isExist { get; set; }
    }
}
