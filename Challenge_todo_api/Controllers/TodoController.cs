using Challenge_todo_list.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_todo_list.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getMyTasks")]
        public async Task<IActionResult> GetMyTasks()
        {
            // todo: Filter by userId
            var myTasks = await _dbContext.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return Ok(myTasks);
        }

        public record TaskDTO(int id, string title, DateTime dueDate, int priorityId, bool isCompleted);

        [HttpPost("addTask")]
        public async Task<IActionResult> AddTask([FromBody] TaskDTO newTask)
        {


            try
            {

                var TaskToAdd = new Models.Task
                {
                    CreatedAt = DateTime.Now,
                    DueDate = newTask.dueDate,
                    IsCompleted = newTask.isCompleted,
                    PriorityId = newTask.priorityId,
                    Title = newTask.title
                };

                var AddedTask = await _dbContext.Tasks.AddAsync(TaskToAdd);

                await _dbContext.SaveChangesAsync();

                return Ok(AddedTask.Entity);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }


        }

        [HttpPut("updateTask")]
        public async Task<IActionResult> UpdateTask([FromBody] TaskDTO task)
        {
            try
            {
                var taskToUpdate = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == task.id);

                taskToUpdate.Title = task.title;
                taskToUpdate.DueDate = task.dueDate;
                taskToUpdate.IsCompleted = task.isCompleted;
                taskToUpdate.PriorityId = task.priorityId;

                await _dbContext.SaveChangesAsync();

                return Ok(taskToUpdate);
                

            }catch(Exception e)
            {

                return BadRequest(e.Message);

            }
        }

        [HttpPut("markCompleted")]
        public async Task<IActionResult> MarkCompleted([FromQuery] int taskId)
        {

            try
            {

                var taskToComplete = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == taskId);

                if (taskToComplete is null)
                    return NotFound();

                taskToComplete.IsCompleted = !taskToComplete.IsCompleted;

                await _dbContext.SaveChangesAsync();

                return Ok(taskToComplete);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpDelete("deleteTask")]
        public IActionResult DeteleTask([FromQuery] int taskId)
        {
            try 
            {
                var taskToDelete = _dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);

                if (taskToDelete is null)
                    return NotFound();

                _dbContext.Tasks.Remove(taskToDelete);

                _dbContext.SaveChanges();

                return Ok(taskToDelete);

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
