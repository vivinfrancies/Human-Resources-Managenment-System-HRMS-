using System.ComponentModel.DataAnnotations;

namespace HRMS.Model
{
	public class EmployeeDetailsTable
	{
		// Personal Details
		public int Id { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = "Length should be less and equal to 15")]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string FirstName { get; set; }

		[StringLength(15, ErrorMessage = "Length should be less and equal to 15")]
		[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only Alphabets is required")]
		public string MiddleName { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = "Length should be less and equal to 15")]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string LastName { get; set; }
		[Required]
		[RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Enter valid email")]
		public string Email { get; set; }
		[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Only 10 digit number is required")]
		public string MobileNumber { get; set; }
		public string Address { get; set; }
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string City { get; set; }
		[RegularExpression(@"[0-9]{6}$", ErrorMessage = "Only Number is required")]
		public string PinCode { get; set; }
		[RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "Only Alphabets is required")]
		public string State { get; set; }
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string Country { get; set; }
		[RegularExpression(@"[0-9]$", ErrorMessage = "Only Number is required")]
		public int Experience { get; set; }
		public int Role { get; set; }
		public int Designation { get; set; }

		[Required(ErrorMessage = "Enter the Gender")]
		public string Gender { get; set; } = string.Empty;
		public DateTime? DateOfBirth { get; set; } = DateTime.Now;
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string Religion { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string Nationality { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string MaritalStatus { get; set; }
		public string ProfileImage { get; set; }

		// Education Details
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string PgCollegeName { get; set; }
		public DateTime? PgStart { get; set; }
		public DateTime? PgEnd { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string PgCourse { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string UgCollageName { get; set; }
		public DateTime? UgStart { get; set; }
		public DateTime? UgEnd { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string UgCourse { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string HSCSchoolName { get; set; }
		public DateTime? HSCStart { get; set; }
		public DateTime? HSCEnd { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string HSCCourse { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string SSLCSchoolName { get; set; }
		public DateTime? SSLCStart { get; set; }
		public DateTime? SSLCEnd { get; set; }
		[RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Only 10 digit number is required")]
		public string EmergencyContact { get; set; }
		/*[Required]
		[RegularExpression(@"^[A-Za-z\s]$", ErrorMessage = "Only Alphabets is required")]*/
		public string Relationship { get; set; }

		// Account Details
		[Required]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Only number is required")]
		public string Salary { get; set; }
		[Required]
		[RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Only Alphabets is required")]
		public string BankName { get; set; }
		[Required]
		[RegularExpression(@"^[0-9]{12}$", ErrorMessage = "Only 12 number is required")]
		public string AccountNumber { get; set; }
		[Required]
		public string IFSCCode { get; set; }
		public string PFAccount { get; set; }
		public byte[] FileContent3 { get; set; }
	}

	public class roles
	{
		public int RoleName { get; set; }
		public int DesignationName { get; set; }
	}

	/*	public class roles
		{
			public int RoleName { get; set; }
			public int DesignationName { get; set; }
		}*/

	public class EmployeeList
	{
		public int EmpId { get; set; }
		public int Sno { get; set; }
		public string Employeecode { get; set; }
		public string Firstname1 { get; set; }
		public string Middlename1 { get; set; }
		public string Lastname1 { get; set; }
		public string Email1 { get; set; }
		public string Mobilenumber1 { get; set; }
		public string Address1 { get; set; }
		public string City1 { get; set; }
		public string Pincode1 { get; set; }
		public string State1 { get; set; }
		public string Country1 { get; set; }
		public string Role1 { get; set; }
		public string Designation1 { get; set; }
		public int DesignationId { get; set; }
		public int Experience { get; set; }
		public string Gender1 { get; set; }
		public DateTime Dateofbirth1 { get; set; }
		public DateTime? Dateofjoining { get; set; } = DateTime.Now;
		public string Religion1 { get; set; }
		public string Nationality1 { get; set; }
		public string Maritalstatus1 { get; set; }
		public string Profileimage1 { get; set; }
		public string Pgcollegename1 { get; set; }
		public DateTime Pgstartdate1 { get; set; }
		public DateTime Pgenddate1 { get; set; }
		public string Pgcoursename1 { get; set; }
		public string Ugcollagename1 { get; set; }
		public DateTime Ugstartdate1 { get; set; }
		public DateTime Ugenddate1 { get; set; }
		public string Ugcoursename1 { get; set; }
		public string HSCschoolname1 { get; set; }
		public DateTime HSCstartdate1 { get; set; }
		public DateTime HSCenddate1 { get; set; }
		public string HSCcoursename1 { get; set; }
		public string SSLCschoolname1 { get; set; }
		public DateTime SSLCstartdate1 { get; set; }
		public DateTime SSLCenddate1 { get; set; }
		public string Emergencycontact1 { get; set; }
		public string Relationship1 { get; set; }
		public string Salary1 { get; set; }
		public string Bankname1 { get; set; }
		public string Accountnumber1 { get; set; }
		public string Ifsccode1 { get; set; }
		public string Pfaccount1 { get; set; }
		public int isactive1 { get; set; }
		public string status1 { get; set; }
	}

	public class EditProfile
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string MobileNumber { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string PinCode { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
		public int Role { get; set; }
		public int Designation { get; set; }
		public int Experience { get; set; }
		public string Gender { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Religion { get; set; }
		public string Nationality { get; set; }
		public string MaritalStatus { get; set; }
		public string ProfileImage { get; set; }
	}

	public class EditProfile2
	{
		public int ID { get; set; }
		public string PgCollegeName { get; set; }
		public DateTime PgStart { get; set; }
		public DateTime PgEnd { get; set; }
		public string PgCourse { get; set; }
		public string UgCollageName { get; set; }
		public DateTime UgStart { get; set; }
		public DateTime UgEnd { get; set; }
		public string UgCourse { get; set; }
		public string HSCSchoolName { get; set; }
		public DateTime HSCStart { get; set; }
		public DateTime HSCEnd { get; set; }
		public string HSCCourse { get; set; }
		public string SSLCSchoolName { get; set; }
		public DateTime SSLCStart { get; set; }
		public DateTime SSLCEnd { get; set; }
		public string EmergencyContact { get; set; }
		public string Relationship { get; set; }
	}
	public class EditProfile3
	{
		public int ID { get; set; }
		// Account Details
		public string Salary { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		public string IFSCCode { get; set; }
		public string PFAccount { get; set; }
	}

}