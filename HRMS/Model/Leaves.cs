namespace HRMS.Model
{
	public class Leave
	{
		public int Id { get; set; }
		public DateTime? Startdate { get; set; } = DateTime.Today;
		public DateTime? Enddate { get; set; } = DateTime.Today;
		public int? LeaveType { get; set; }
		public string Reason { get; set; }
		public string Detailedreason { get; set; }
		public int IsActive { get; set; }
		public bool IsComplete { get; set; }
	}
	public class Chartmodel
	{

		public int Id { get; set; }
		public string EmployeeCode { get; set; }
		public string Firstname { get; set; }
		public string Role { get; set; }
		public int Leave { get; set; }
		public int Leavetype { get; set; }
		public int LeaveLimit { get; set; }
		public int Remainingleave { get; set; }

		public string LeaveTypename { get; set; }

	}

	public class ListModel
	{
		public int ID { get; set; }
		public int EmpId { get; set; }
		public string EmployeeCode { get; set; }
		public string FirstName { get; set; }
		public string Designation { get; set; }
		public string Leavetypename { get; set; }
		public int leavetypeid { get; set; }
		public DateTime? StartDate { get; set; } = DateTime.Now;
		public DateTime? EndDate { get; set; } = DateTime.Now;
		public string Reason { get; set; }
		public string Status { get; set; }

		public int NoOfDays { get; set; }
		public bool Accept { get; set; }



	}
	public class CardModel
	{
		public int ID { get; set; }
		public int EmpId { get; set; }
		public string EmployeeCode { get; set; }
		public string FirstName { get; set; }
		public string Designation { get; set; }

		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public string Reason { get; set; }
		public string Detailedreason { get; set; }

		public string Leavetypename { get; set; }
		public int NoOfDays { get; set; }
		public string Status { get; set; }

		public int Role { get; set; }
		public string wordstartdate { get; set; }
		public string wordenddate { get; set; }
	}
	public class Barchart
	{
        
        public int absent { get; set; }
		public int present { get; set; }
		public string a { get; set; } = "";
	}
	public class LeaveTypeDetails
	{
		public int Id { get; set; }
		public string LeaveName { get; set; }
	}
}