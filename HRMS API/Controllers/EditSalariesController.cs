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

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditSalaryController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EditSalaryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        // POST: api/Todoitems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EditSalary>> PostTodoitem(EditSalary todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"insert into SalaryDetails ([EmpId],[EmployeeCode],[BasicPay],[HouseRent],[Conveyance],[OtherAllowance],[ESI],[Tax],[PF],[Others],[CreatedBy])values(@EmpId,@EmployeeCode,@Basic,@HouseRent,@Conveyance,@OtherAllowance,@ESI,@Tax,@PF,@Others,@CreatedBy)";
                    using (var insertcmd = new SqlCommand(sql, appconnection))
                    {
                        insertcmd.Parameters.AddWithValue("@EmpId", todoitem.EmpId);
                        insertcmd.Parameters.AddWithValue("@EmployeeCode", todoitem.EmployeeCode);
                        insertcmd.Parameters.AddWithValue("@Basic", todoitem.Basic);
                        insertcmd.Parameters.AddWithValue("@HouseRent", todoitem.HouseRent);
                        insertcmd.Parameters.AddWithValue("@Conveyance", todoitem.Conveyance);
                        insertcmd.Parameters.AddWithValue("@OtherAllowance", todoitem.OtherAllowance);
                        insertcmd.Parameters.AddWithValue("@ESI", todoitem.ESI);
                        insertcmd.Parameters.AddWithValue("@Tax", todoitem.Tax);
                        insertcmd.Parameters.AddWithValue("@PF", todoitem.PF);
                        insertcmd.Parameters.AddWithValue("@Others", todoitem.Others);
                        insertcmd.Parameters.AddWithValue("@CreatedBy", todoitem.CreatedBy);
                        await insertcmd.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        /*update salary*/

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Payslipdetails>> PutToItem(Payslipdetails todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"update SalaryDetails set BasicPay=@BasicPay where EmpId=@Id";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@BasicPay", todoitem.BasicPay);
                        updatecmd.Parameters.AddWithValue("@Id", todoitem.Id);
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

        /* getsalary using id*/

        [HttpGet("{EmpId:int}")]
        public async Task<ActionResult<EditSalary>> Get(int EmpId)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var salaryinfo = new List<EditSalary>();
                    var sql = @"select * from SalaryDetails where EmpId=@EmpId";
                    using (var selectcmd = new SqlCommand(sql, appconnection))
                    {
                        selectcmd.Parameters.AddWithValue("@EmpId", EmpId);
                        using (var reader = await selectcmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var editsalary = new EditSalary
                                {
                                    EmpId = Convert.ToInt32(reader["EmpId"]),
                                    Basic = Convert.ToDecimal(reader["BasicPay"]),
                                    HouseRent = Convert.ToDecimal(reader["HouseRent"]),
                                    Conveyance = Convert.ToDecimal(reader["Conveyance"]),
                                    OtherAllowance = Convert.ToDecimal(reader["OtherAllowance"]),
                                    ESI = Convert.ToDecimal(reader["ESI"]),
                                    Tax = Convert.ToDecimal(reader["Tax"]),
                                    PF = Convert.ToDecimal(reader["PF"]),
                                    Others = Convert.ToDecimal(reader["Others"]),
                                    createdDate = DateTime.Parse((reader["createdDate"]).ToString())
                                };
                                /*foreach (var property in typeof(EditSalary).GetProperties())
                                {
                                    if (reader[property.Name] != DBNull.Value)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(editsalary, value);
                                    }
                                }*/
                                salaryinfo.Add(editsalary);
                            }
                            else
                            {
                                return NotFound();
                            }
                            return Ok(salaryinfo);

                        }

                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        /*get all the employee salary*/

        [HttpGet("Employeesalarydetails")]
        public async Task<ActionResult<Salarydetails>> Get()
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var salary = new List<Salarydetails>();
                    var sql = "select * from EmployeeSalaryDetails ";
                    using (var selectcmd = new SqlCommand(sql, appconnection))
                    {
                        using (var reader = await selectcmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var salarys = new Salarydetails();

                                foreach (var property in typeof(Salarydetails).GetProperties())
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

        /*payslipdetails procedure*/

        [HttpGet("GetPayslip/{Id:}")]
        public async Task<ActionResult<Payslipdetails>> GetPayslip(int Id)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var salary = new List<Payslipdetails>();
                    var sql = @"select * from payslipdetailsview where Id=@Id";
                    using (var selectcmd = new SqlCommand(sql, appconnection))
                    {
                        selectcmd.Parameters.AddWithValue("@Id", Id);
                        using (var reader = await selectcmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var salarys = new Payslipdetails();

                                foreach (var property in typeof(Payslipdetails).GetProperties())
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
        /*monthlysalary view*/

        [HttpGet("GetMonthlySalary")]
        public async Task<ActionResult<Monthlysalary>> GetMonthlySalary()
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var monthlysalary = new List<Monthlysalary>();
                    var sql = "select * from MonthlySalary";
                    using (var selectcmd = new SqlCommand(sql, appconnection))
                    {
                        using (var reader = await selectcmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var monthlysalarys = new Monthlysalary();

                                foreach (var property in typeof(Monthlysalary).GetProperties())
                                {
                                    if (reader[property.Name] != DBNull.Value)
                                    {
                                        var value = Convert.ChangeType(reader[property.Name], property.PropertyType);
                                        property.SetValue(monthlysalarys, value);
                                    }
                                }
                                monthlysalary.Add(monthlysalarys);
                            }
                            return Ok(monthlysalary);
                        }

                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpGet("GetShowSalaryDetails")]
        public async Task<ActionResult<Showsalarydetails>> GetShowSalaryDetails()
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var salary = new List<Showsalarydetails>();
                    var sql = "select * from SHOWSALARYDETAILS";
                    using (var selectcmd = new SqlCommand(sql, appconnection))
                    {
                        using (var reader = await selectcmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var salarys = new Showsalarydetails();

                                foreach (var property in typeof(Showsalarydetails).GetProperties())
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

    }
}


