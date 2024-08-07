using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HRMS_API.Model;
using WebApplication1.Model;
using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Reflection.Emit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : Controller
    {
        private readonly ILogger<PayrollController> _logger;
        private readonly IConfiguration _configuration;

        public PayrollController(ModelContext dbContext, ILogger<PayrollController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        #region Monthlysalary
        [HttpGet("employeedetails/{EmployeeCode}")]
        public async Task<ActionResult<MonthSalCalc>> employeedetails(string EmployeeCode)
        {
            try
            {
                var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
                using (var appconnection = new SqlConnection(appdbconnectionstring))
                {
                    await appconnection.OpenAsync();
                    try
                    {
                        var salary = new List<MonthSalCalc>();
                        var sql = "select * from MonthlySalaryCalc where EmployeeCode=@EmployeeCode";
                        using (var selectcmd = new SqlCommand(sql, appconnection))
                        {
                            selectcmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);

                            using (var reader = await selectcmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var salarys = new MonthSalCalc();

                                    foreach (var property in typeof(MonthSalCalc).GetProperties())
                                    {
                                        if (reader[property.Name] != DBNull.Value)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(salarys, value);
                                        }
                                    }
                                    salary.Add(salarys);
                                }
                                return Ok(salary);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Insert
        [HttpPost]
        public async Task<ActionResult<SalaryDetails>> PostTodoItem(SalaryDetails todoItem)
        {
            try
            {
                var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");
                using (var appConnection = new SqlConnection(appDbConnectionString))
                {
                    await appConnection.OpenAsync();
                    try
                    {
/*                        decimal adjustedSalary = CalculateAdjustedSalary(todoItem);
*/                        var sql = @"INSERT INTO DisplaySalaryDetails (EmpId, EmployeeCode,BasicSalary, HouseRent, Conveyance, OtherAllowance, ESI, Tax, PF, Others, Reimbursement, NetSalary, CreatedAt, UpdatedAt) 
                VALUES (@EmpId, @EmployeeCode,@BasicSalary, @HouseRent, @Conveyance, @OtherAllowance, @Others, @ESI, @Tax, @PF, @Reimbursement, @NetSalary, @CreatedAt, @UpdatedAt)";
                        using (var insertcmd = new SqlCommand(sql, appConnection))
                        {
                            insertcmd.Parameters.AddWithValue("@EmpId", todoItem.EmpId);
                            insertcmd.Parameters.AddWithValue("@EmployeeCode", todoItem.EmployeeCode);
                            insertcmd.Parameters.AddWithValue("@BasicSalary", todoItem.BasicSalary);
                            insertcmd.Parameters.AddWithValue("@HouseRent", todoItem.HouseRent);
                            insertcmd.Parameters.AddWithValue("@Conveyance", todoItem.Conveyance);
                            insertcmd.Parameters.AddWithValue("@OtherAllowance", todoItem.OtherAllowance);
                            insertcmd.Parameters.AddWithValue("@Others", todoItem.Others);
                            insertcmd.Parameters.AddWithValue("@ESI", todoItem.ESI);
                            insertcmd.Parameters.AddWithValue("@Tax", todoItem.Tax);
                            insertcmd.Parameters.AddWithValue("@PF", todoItem.PF);
                            insertcmd.Parameters.AddWithValue("@Reimbursement", todoItem.Reimbursement);
                            insertcmd.Parameters.AddWithValue("@NetSalary", todoItem.NetSalary);
                            insertcmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
                            insertcmd.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                            await insertcmd.ExecuteNonQueryAsync();
                        };
                        return Ok(todoItem);
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        _logger.LogError(ex, "An error occurred while inserting data");
                        return StatusCode(500, "Internal server error");
                    }
                    finally
                    {
                        await appConnection.CloseAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Update
        [HttpPut("{EmployeeCode}")]
        public async Task<ActionResult<SalaryDetails>> PutToItem(string EmployeeCode, [FromBody] SalaryDetails todoitem)
        {
            if (EmployeeCode != todoitem.EmployeeCode)
            {
                return BadRequest("Employee Code mismatch.");
            }

            try
            {
                var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
                using (var appconnection = new SqlConnection(appdbconnectionstring))
                {
                    await appconnection.OpenAsync();
                    try
                    {
                        var sql = @"update EmployeeAccountDetails set Salary=@BasicSalary where EmployeeCode=@EmployeeCode";
                        using (var updatecmd = new SqlCommand(sql, appconnection))
                        {
                            updatecmd.Parameters.AddWithValue("@BasicSalary", todoitem.BasicSalary);
                            updatecmd.Parameters.AddWithValue("@EmployeeCode", todoitem.EmployeeCode);
                            await updatecmd.ExecuteNonQueryAsync();
                        }
                        return Ok("true");
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region AllEmployeeDetails
        /*       /hrpayrollpage/*/
        [HttpGet("AllEmployeeDetails{EmployeeCode}")]
        public async Task<ActionResult<AllEmployeeDetails>> AllEmployeeDetails(string EmployeeCode)
        {
            try
            {
                var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
                using (var appconnection = new SqlConnection(appdbconnectionstring))
                {
                    await appconnection.OpenAsync();
                    try
                    {
                        var salary = new List<AllEmployeeDetails>();
                        var sql = "select * from DisplayEmployeeDetails where NOT EmployeeCode=@EmployeeCode";
                        using (var selectcmd = new SqlCommand(sql, appconnection))
                        {
                            selectcmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);

                            using (var reader = await selectcmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var salarys = new AllEmployeeDetails();

                                    foreach (var property in typeof(AllEmployeeDetails).GetProperties())
                                    {
                                        if (reader[property.Name] != DBNull.Value)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(salarys, value);
                                        }
                                    }
                                    salary.Add(salarys);
                                }
                                return Ok(salary);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region IndividualPayroll

        [HttpGet("IndividualPayroll{ID}")]
        public async Task<ActionResult<PayslipDetails>> EmployeeDetails(int ID)
        {
            try
            {
                var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
                using (var appconnection = new SqlConnection(appdbconnectionstring))
                {
                    await appconnection.OpenAsync();
                    try
                    {
                        var salary = new List<PayslipDetails>();
                        var sql = "select * from  payslipdetailsview where ID=@ID ";
                        using (var selectcmd = new SqlCommand(sql, appconnection))
                        {
                            selectcmd.Parameters.AddWithValue("@ID", ID);
                            using (var reader = await selectcmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var salarys = new PayslipDetails();

                                    foreach (var property in typeof(PayslipDetails).GetProperties())
                                    {
                                        if (reader[property.Name] != DBNull.Value)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(salarys, value);
                                        }
                                    }
                                    salary.Add(salarys);
                                }
                                return Ok(salary);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

        #region Payslip
        /*        /Payslipdetails generateslip page/
        */
        [HttpGet("PayslipDetails/{EmployeeCode}")]
        public async Task<ActionResult<PayslipDetails>> PayslipDetails(string EmployeeCode)
        {
            try
            {
                var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
                using (var appconnection = new SqlConnection(appdbconnectionstring))
                {
                    await appconnection.OpenAsync();
                    try
                    {
                        var salary = new List<PayslipDetails>();
                        var sql = "select * from  payslipdetailsview where EmployeeCode=@EmployeeCode order by CreatedAt Desc";
                        using (var selectcmd = new SqlCommand(sql, appconnection))
                        {
                            selectcmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
                            using (var reader = await selectcmd.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var salarys = new PayslipDetails();

                                    foreach (var property in typeof(PayslipDetails).GetProperties())
                                    {
                                        if (reader[property.Name] != DBNull.Value)
                                        {
                                            var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                            property.SetValue(salarys, value);
                                        }
                                    }
                                    salary.Add(salarys);
                                }
                                return Ok(salary);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        #endregion

    }
}