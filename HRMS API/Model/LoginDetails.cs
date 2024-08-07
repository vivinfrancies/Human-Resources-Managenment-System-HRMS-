namespace HRMS_API.Model
{
    public class ForgetPassword
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string EmpCode { get; set; }
        public string Password { get; set; }

    }


    public class LoginDetails
    {

        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string NewPassword { get; set; }
        public string Firstname { get; set; }
        public string Profileimage { get; set; }
        public string Fullname { get; set; }

    }
}