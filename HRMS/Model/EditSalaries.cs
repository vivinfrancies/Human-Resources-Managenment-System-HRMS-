namespace HRMS.Model
{
    public class SalaryDetails
    {
        public int ID { get; set; }
        public int EmpId { get; set; }
        public string EmployeeCode { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public decimal Reimbursement { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class MonthSalCalc
    {
        public int ID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Role { get; set; }
        public int Experience { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Reimbursement { get; set; }
        public int Leave { get; set; }

    }
    public class AccountDetails
    {
        public string Salary { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PFAccount { get; set; }
    }
    public class AllEmployeeDetails
    {
        public int ID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public decimal Salary { get; set; }
    }

    public class PayslipDetails
    {
        public int ID { get; set; }
        public int EmpId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PFAccount { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal HouseRent { get; set; }
        public decimal Conveyance { get; set; }
        public decimal OtherAllowance { get; set; }
        public decimal ESI { get; set; }
        public decimal Tax { get; set; }
        public decimal PF { get; set; }
        public decimal Others { get; set; }
        public decimal Reimbursement { get; set; }
        public decimal TotalEarnings => BasicSalary + HouseRent + Conveyance + OtherAllowance + Reimbursement;
        public decimal TotalDeductions => (Tax + ESI + Others + PF);
        public decimal NetSalary { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class IndividualPayrollHistory
    {
        public int EmpId { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public string BasicSalary { get; set; }
        public DateTime CreatedAt { get; set; }


    }

    public class EmployeeDetails
    {
        public int EmpId { get; set; }
        // Other properties as needed
    }

}