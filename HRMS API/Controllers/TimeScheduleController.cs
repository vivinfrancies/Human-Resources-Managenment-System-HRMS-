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
    public class TimeScheduleController : ControllerBase
    {
        private readonly ModelContext _context;
        private readonly IConfiguration _configuration;

        public TimeScheduleController(ModelContext DbContext, IConfiguration configuration)
        {
            _context = DbContext;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult<Dashboard>> PostTodoItem(Dashboard todoDTO)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var sql = @"INSERT INTO TimeSchedule ([EventName], [Time], [TimeOut]) VALUES(@EventName,@TimeIn,@TimeOut)";
                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@EventName", todoDTO.EventName);
                        insertCommand.Parameters.AddWithValue("@TimeIn", todoDTO.TimeIn);
                        insertCommand.Parameters.AddWithValue("@TimeOut", todoDTO.TimeOut);

                        await insertCommand.ExecuteNonQueryAsync();
                    }
                    return Ok("true");
                }
                catch (Exception)
                {
                    return null;
                }

            }
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dashboard>>> GetTodoItems()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var projects = new List<Dashboard>();
                    var sql = @"SELECT ID, [EventName], CONVERT(NVARCHAR,[Time],0) AS [TimeIn], CONVERT(NVARCHAR,[TimeOut],0) AS [TimeOut] FROM TimeSchedule WHERE IsActive=1 ORDER BY [TimeOut]";
                    using (var selectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await selectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var project = new Dashboard
                                {
                                    EventId = Convert.ToInt32(reader["ID"]),
                                    EventName = reader["EventName"].ToString(),
                                    TimeIn = reader["TimeIn"].ToString(),
                                    TimeOut = reader["TimeOut"].ToString()
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



        [HttpPut("{EventId:int}")]
        public async Task<ActionResult<Dashboard>> PutToItem(Dashboard todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE TimeSchedule SET [IsActive] = 0 WHERE ID = @EventId";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@EventId", todoitem.EventId);

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

        [HttpPut("TimeUpdate/{EventId:int}")]
        public async Task<ActionResult<Dashboard>> PutUpdate(Dashboard todoitem)
        {
            var appdbconnectionstring = _configuration.GetConnectionString("AppDbConnection");
            using (var appconnection = new SqlConnection(appdbconnectionstring))
            {
                await appconnection.OpenAsync();
                try
                {
                    var sql = @"UPDATE TimeSchedule SET [EventName] = @EventName, [Time] = @TimeIn ,[TimeOut] = @TimeOut WHERE ID = @EventId";
                    using (var updatecmd = new SqlCommand(sql, appconnection))
                    {
                        updatecmd.Parameters.AddWithValue("@EventId", todoitem.EventId);
                        updatecmd.Parameters.AddWithValue("@EventName", todoitem.EventName);
                        updatecmd.Parameters.AddWithValue("@TimeIn", todoitem.TimeIn);
                        updatecmd.Parameters.AddWithValue("@TimeOut", todoitem.TimeOut);

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