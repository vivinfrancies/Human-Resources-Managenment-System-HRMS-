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
using System.Net.Sockets;

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DashboardsController(ModelContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<Dashboard>> AllDetails()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new Dashboard();
                    var sql = @"EXEC AllCountDetails";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            // Read TotalProject
                            if (await reader.ReadAsync())
                            {
                                dashboard.TotalProject = reader.GetInt32(reader.GetOrdinal("TotalProject"));
                            }

                            // Move to the next result set (TotalClient)
                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                dashboard.TotalClient = reader.GetInt32(reader.GetOrdinal("TotalClient"));
                            }

                            // Move to the next result set (TotalEmployee)
                            await reader.NextResultAsync();
                            if (await reader.ReadAsync())
                            {
                                dashboard.TotalEmployee = reader.GetInt32(reader.GetOrdinal("TotalEmployee"));
                            }

                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }


        [HttpGet("GenderCount")]
        public async Task<ActionResult<Dashboard>> Gender()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new List<Dashboard>();
                    var sql = @"SELECT Gender, COUNT(Gender) AS Count FROM EmployeeDetails WHERE IsActive=1 GROUP BY Gender";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var dashboards = new Dashboard
                                {

                                    EmpGender = reader["Gender"].ToString(),
                                    GenderCount = Convert.ToInt32(reader["Count"]),
                                };
                                dashboard.Add(dashboards);
                            }
                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }

        [HttpGet("EmployeeOfMonth")]
        public async Task<ActionResult<Dashboard>> EmpOfMonth()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new List<Dashboard>();
                    var sql = @"SELECT * FROM EmpOfMonth";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var dashboards = new Dashboard
                                {

                                    EOMName = reader["FirstName"].ToString(),
                                    EOMDesignation = reader["Designation"].ToString(),
                                };
                                dashboard.Add(dashboards);
                            }
                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }


        [HttpGet("TotalProjectDetails")]
        public async Task<ActionResult<Dashboard>> TotalProjectDetails()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new List<Dashboard>();
                    var sql = @"SELECT COUNT(Status) AS ProjectCount, Status FROM ProjectTable WHERE IsActive=1 GROUP BY Status";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var dashboards = new Dashboard
                                {
                                    ProjectCompleteCount = Convert.ToInt32(reader["ProjectCount"]),
                                    ProjectStatus = reader["Status"].ToString(),
                                };
                                dashboard.Add(dashboards);
                            }
                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }

        [HttpGet("TodayLeaveCount")]
        public async Task<ActionResult<Dashboard>> TodayLeave()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new List<Dashboard>();
                    var sql = @"SELECT * FROM TodayLeaveSample";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var dashboards = new Dashboard
                                {
                                    TodayLeaveCount = Convert.ToInt32(reader["TodayPresent"]),
                                    TodayLeaveStatus = reader["Status"].ToString(),
                                };
                                dashboard.Add(dashboards);
                            }
                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }



        [HttpGet("EmpLeaveDetails/{id:int}")]
        public async Task<ActionResult<Dashboard>> EmpLeaveDetails(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var dashboard = new List<Dashboard>();
                    var sql = @"SELECT LeaveDetails, TakenLeave, LeaveLimit FROM LeaveRemaining WHERE ID=@id";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        selectCommand.Parameters.AddWithValue("@id", id);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var dashboards = new Dashboard
                                {
                                    Leavedetails = reader["LeaveDetails"].ToString(),
                                    EmpLeave = Convert.ToInt32(reader["TakenLeave"]),
                                    EmpTotalLeave = Convert.ToInt32(reader["LeaveLimit"]),
                                };
                                dashboard.Add(dashboards);
                            }
                        }
                    }
                    return Ok(dashboard);
                }
				catch (Exception ex)
				{
					return BadRequest(ex.Message);
				}
			}
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Profile>> Get(int id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                var attendanceList = new List<Profile>();
                var sql = @"SELECT [EmployeeCode], [FirstName], [ProfileImage] FROM EmployeeDetails WHERE ID=@id";
                using (var selectCommand = new SqlCommand(sql, appDbConnection))
                {
                    try
                    {
                        selectCommand.Parameters.AddWithValue("@id", id);
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {

                            while (await reader.ReadAsync())
                            {
                                var attendance = new Profile
                                {
                                    Name = reader["FirstName"].ToString(),
                                    EmpCode = reader["EmployeeCode"].ToString(),
                                    Image = reader["ProfileImage"].ToString()
                                };
                                attendanceList.Add(attendance);
                            }
                            return Ok(attendanceList);
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
}