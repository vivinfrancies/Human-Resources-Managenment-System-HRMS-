namespace HRMS_API.Model
{
    public class EditSalary
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public decimal Basic { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public decimal NetSalary { get; set; }
        public int CreatedBy { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class Payslipdetails
    {
        public int Id { get; set; }

        public string Designation { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }
        public DateTime DateOfJoining { get; set; }

        public string BankName { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;
        public string IFSCCode { get; set; } = string.Empty;
        public decimal BasicPay { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public decimal NetSalary { get; set; }
        public int CreatedBy { get; set; }
        public string PFAccount { get; set; } = string.Empty;
    }

    public class Salarydetails
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string Designation { get; set; }
        public string Salary { get; set; }
    }

    public class Monthlysalary
    {

        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public string role { get; set; }
        public int experience { get; set; }
        public DateTime Dateofjoining { get; set; }
        public decimal BasicPay { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public decimal NetSalary { get; set; }
        public decimal AdjustedBasicPay { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

    }

    public class Showsalarydetails
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicPay { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
