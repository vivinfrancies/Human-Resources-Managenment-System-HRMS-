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

    public class LoginDetailsController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;

        public LoginDetailsController(ModelContext DbContext, IConfiguration configuration)
        {
            _context = DbContext;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginDetails>>> GetTodoItems(string user, string password)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<LoginDetails>();
                    var sql = @"SELECT * FROM LoginDetails WHERE UserName=@user AND [Password]=@password";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@user", user);
                        selectCommand.Parameters.AddWithValue("@password", password);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new LoginDetails
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    RoleId = Convert.ToInt32(reader["RoleId"]),
                                    UserName = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString(),
                                    Role = reader["Role"].ToString(),
                                    Firstname = reader["FirstName"].ToString(),
                                    Fullname = reader["FullName"].ToString(),
                                    Profileimage = reader["ProfileImage"].ToString()
                                };
                                projects.Add(project);
                            }
                        }
                    }
                    return Ok(projects);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
        }
        [HttpPut("{Id:int}")]
        public async Task<ActionResult<LoginDetails>> PutToItem(LoginDetails todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE EmployeeLogin SET [Password] = @NewPassword  WHERE EmpId = @Id";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@Id", todoitem.Id);
                        updatecmd.Parameters.AddWithValue("@NewPassword", todoitem.NewPassword);

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
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<ForgetPassword>>> Getmail(string email)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<ForgetPassword>();
                    var sql = @"SELECT ED.ID,ED.Email,EL.UserName,EL.Password FROM EmployeeDetails AS ED INNER JOIN EmployeeLogin AS EL ON ED.ID=EL.EmpId WHERE Email=@email";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@email", email);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new ForgetPassword
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    Email = reader["Email"].ToString(),
                                    EmpCode = reader["UserName"].ToString(),
                                    Password = reader["Password"].ToString()
                                };

                                projects.Add(project);
                            }
                        }
                    }
                    return Ok(projects);
                }
                catch (Exception ex)
                {
                    return Ok(ex);
                }
            }
        }

        [HttpPut("Changepassword")]
        public async Task<ActionResult<LoginDetails>> ChangePassword(LoginDetails todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {

                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE EmployeeLogin SET Password = @NewPassword WHERE Password = @Password AND ID=@Id";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@Id", todoitem.Id);
                        updatecmd.Parameters.AddWithValue("@NewPassword", todoitem.NewPassword);
                        updatecmd.Parameters.AddWithValue("@Password", todoitem.Password);

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

    }
}