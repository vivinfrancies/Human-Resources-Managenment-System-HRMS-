namespace HRMS_API.Model
{
	public class EmployeeDetailsTable
	{
		// Personal Details
		public int Id { get; set; }
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
		public byte[] FileContent3 { get; set; }

		// Education Details
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

		// Account Details
		public string Salary { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		public string IFSCCode { get; set; }
		public string PFAccount { get; set; }
		public int RoleId { get; set; }
		public string RoleName { get; set; }
		public int DesignationId { get; set; }
		public string DesignationName { get; set; }
	}


	public class EmployeeList
	{
		public int EmpId { get; set; }
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
		public DateTime Dateofjoining { get; set; }
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

		public int EmpId { get; set; }
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
		public int EmpId { get; set; }
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
		public int EmpId { get; set; }
		// Account Details
		public string Salary { get; set; }
		public string BankName { get; set; }
		public string AccountNumber { get; set; }
		public string IFSCCode { get; set; }
		public string PFAccount { get; set; }
	}

}