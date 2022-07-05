using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AssessmentTest
{
    public partial class UserModel
    {
        [Required(ErrorMessage = "Email is required")] 
        public string email { set; get; }
        [Required(ErrorMessage = "Password is required")]
        public string password { set; get; }
    }

    public class LoginDto
    {
        public string Token { set; get; }
    }

    public class PaginationDto
    {
        public int page { set; get; }
        public int per_page { set; get; }
        public int total { set; get; }
        public int total_pages { set; get; }

    }
    public class ResponseDto : PaginationDto
    {
        public List<UserDto> data { set; get; }
        public SupportDto support { set; get; }
    }
    public class UserDto
    {
        public int id { set; get; }
        public string email { set; get; }
        public string first_name { set; get; }
        public string last_name { set; get; }
        public string avatar { set; get; }
    }
    public class SupportDto
    {
        public string url { set; get; }
        public string text { set; get; }

    }
    public class UserDetailDto
    {
        public UserDto data { set; get; }
        public SupportDto support { set; get; }
    }
}
