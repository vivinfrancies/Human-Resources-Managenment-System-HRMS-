namespace HRMS_API.Model
{
	public class ResignationTable
	{
		public int EmpId { get; set; }
		public DateTime LastDateOfWorking { get; set; }
		public string ReasonForResign { get; set; }

		public string AdditionalReasonForResign { get; set; }
	}

	public class ResignationTableview
	{

		public int EmpId { get; set; }
		public string EmployeeCode { get; set; }
		public string EmployeeName { get; set; }
		public DateTime Dateofjoining { get; set; }
		public string Designation { get; set; }
		public DateTime LastDateOfWorking { get; set; }
		public string ReasonForResign { get; set; }
		public string AdditionalReasonForResign { get; set; }


	}
	public class Reason
	{
		public int id { get; set; }
		public string problem { get; set; }
	}
}