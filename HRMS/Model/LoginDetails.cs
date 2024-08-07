using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
	public class ForgetPassword
	{
		public int Id { get; set; }
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter the valid email address")]
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
		public string FirstName { get; set; }
		public string Profileimage { get; set; }
		public string FullName { get; set; }

	}



}