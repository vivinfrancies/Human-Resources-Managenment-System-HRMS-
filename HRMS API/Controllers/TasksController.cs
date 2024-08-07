using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRMS_API.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using NuGet.Protocol.Plugins;
using WebApplication1.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HRMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly IConfiguration _configuration;


        public TasksController(ModelContext dbcontext, IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tasks>>> GetTodoItems()
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var TaskList = new List<Tasks>();
                    var sql = @"SELECT * FROM TaskTable Where IsActive = 1 AND Status = 1;";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Task = new Tasks
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    EmpId = Convert.ToInt32(reader["EmployeeId"]),
                                    EmpCode = reader["EmployeeCode"].ToString(),
                                    TaskName = reader["TaskName"].ToString(),
                                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                                    Progress = Convert.ToInt32(reader["Progress"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    projectstatus = Convert.ToBoolean(reader["ProjectStatus"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                TaskList.Add(Task);
                            }

                            return Ok(TaskList);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tasks>> Get(string id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var TaskList = new List<Tasks>();
                    var sql = @"SELECT * FROM TaskTable WHERE ProjectID = @ID AND IsActive = 1 AND Status = 1;";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        SelectCommand.Parameters.AddWithValue("@ID", id);
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Task = new Tasks
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    EmpId = Convert.ToInt32(reader["EmployeeId"]),
                                    EmpCode = reader["EmployeeCode"].ToString(),
                                    TaskName = reader["TaskName"].ToString(),
                                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                                    Progress = Convert.ToInt32(reader["Progress"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    projectstatus = Convert.ToBoolean(reader["ProjectStatus"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                TaskList.Add(Task);
                            }

                            return Ok(TaskList);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        [HttpGet("ProjectMember/{id:int}")]
        public async Task<ActionResult<Tasks>> ProjectMember(string id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                try
                {
                    var TaskList = new List<Tasks>();
                    var sql = @"SELECT * FROM TaskTable WHERE EmployeeId = @ID AND IsActive = 1 AND Status = 1;";
                    using (var SelectCommand = new SqlCommand(sql, appDbConnection))
                    {
                        SelectCommand.Parameters.AddWithValue("@ID", id);
                        using (var reader = await SelectCommand.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var Task = new Tasks
                                {
                                    Id = Convert.ToInt32(reader["ID"]),
                                    EmpId = Convert.ToInt32(reader["EmployeeId"]),
                                    EmpCode = reader["EmployeeCode"].ToString(),
                                    TaskName = reader["TaskName"].ToString(),
                                    DueDate = Convert.ToDateTime(reader["DueDate"]),
                                    Progress = Convert.ToInt32(reader["Progress"]),
                                    Status = Convert.ToBoolean(reader["Status"]),
                                    ProjectID = Convert.ToInt32(reader["ProjectID"]),
                                    ProjectName = reader["ProjectName"].ToString(),
                                    projectstatus = Convert.ToBoolean(reader["ProjectStatus"]),
                                    IsActive = Convert.ToBoolean(reader["IsActive"])
                                };
                                TaskList.Add(Task);
                            }

                            return Ok(TaskList);
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        // POST: api/Tasks
        [HttpPost]
        public async Task<ActionResult<Tasks>> PostTodoItem(Tasks todoDTO)
        // public async Task<ActionResult<string>> viewstaff([FromBody] viewstaff todoDTO)

        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();

                try
                {
                    var sql = @"INSERT INTO Task (ProjectID, TaskName, DueDate, EmpId, Status) VALUES  (@ProjectID, @TaskName, @DueDate, @EmpId, @Status)";

                    using (var insertCommand = new SqlCommand(sql, appDbConnection))
                    {
                        insertCommand.Parameters.AddWithValue("@ProjectID", todoDTO.ProjectID);
                        insertCommand.Parameters.AddWithValue("@TaskName", todoDTO.TaskName);
                        insertCommand.Parameters.AddWithValue("@DueDate", todoDTO.DueDate);
                        insertCommand.Parameters.AddWithValue("@EmpId", todoDTO.EmpId);
                        insertCommand.Parameters.AddWithValue("@Status", todoDTO.Status);

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



        [HttpPut("{Id:int}")]
        public async Task<ActionResult<Tasks>> PutToItem(Tasks todoitem)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"  UPDATE Task  
                            SET ProjectID = @ProjectID,TaskName = @TaskName,DueDate = @DueDate, EmpId = @EmpId,[Status] = @Status 
                            WHERE Id =@ID ; ";

                using (var UpdateCommand = new SqlCommand(sql, appDbConnection))
                {
                    UpdateCommand.Parameters.AddWithValue("@ProjectID", todoitem.ProjectID);
                    UpdateCommand.Parameters.AddWithValue("@TaskName", todoitem.TaskName);
                    UpdateCommand.Parameters.AddWithValue("@DueDate", todoitem.DueDate);
                    UpdateCommand.Parameters.AddWithValue("@EmpId", todoitem.EmpId);
                    UpdateCommand.Parameters.AddWithValue("@Status", todoitem.Status);
                    UpdateCommand.Parameters.AddWithValue("@ID", todoitem.Id);

                    await UpdateCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }


        [HttpPut("TaskStatus/{Id:int}/")]
        public async Task<ActionResult<Tasks>> StatusComplete(int Id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"UPDATE Task Set [Status] = 0, CompletedAt = GETDATE() where ID = @Id";

                using (var UpdateCommand = new SqlCommand(sql, appDbConnection))
                {
                    UpdateCommand.Parameters.AddWithValue("@Id", Id);
                    await UpdateCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }

        [HttpPut("Delete/{Id:int}/")]
        public async Task<ActionResult<Tasks>> Delete(int Id)
        {
            var appDbConnectionString = _configuration.GetConnectionString("AppDbConnection");

            using (var appDbConnection = new SqlConnection(appDbConnectionString))
            {
                await appDbConnection.OpenAsync();
                var sql = @"UPDATE Task Set IsActive = 0 where id = @Id";

                using (var UpdateCommand = new SqlCommand(sql, appDbConnection))
                {
                    UpdateCommand.Parameters.AddWithValue("@id", Id);
                    await UpdateCommand.ExecuteNonQueryAsync();
                    return Ok(true);
                }
            }
        }
    }
}