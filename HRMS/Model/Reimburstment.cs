namespace HRMS.Model
{
	public class Reimbursement
	{
		public int Id { get; set; }


		public int? EmpId { get; set; }

		public string EmpCode { get; set; }

		public string FirstName { get; set; }

		public int? Role { get; set; }

		public string Roles { get; set; }

		public int roleId { get; set; }

		public string roleName { get; set; }
		public string Designation { get; set; }
		public int? Expense { get; set; }

		public string Expensetypes { get; set; }

		public string BillNo { get; set; }

		public DateTime? BillDate { get; set; }

		public DateTime AppliedDate { get; set; }

		public int? BillAmount { get; set; }

		public string Status { get; set; }

		public string Bill { get; set; }
		public bool IsComplete { get; set; }


		//
		public int? LeaveLimit { get; set; }

		public int? InternetLimit { get; set; }

		public int? TravelLimit { get; set; }

		public int modifiedBy { get; set; }


		//
		public bool Accpt = false;
		public bool declin = false;

		public string color { get; set; }

		//
		public byte[] FileContent3 { get; set; }


	}
}