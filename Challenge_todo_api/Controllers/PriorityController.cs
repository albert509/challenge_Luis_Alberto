using Challenge_todo_list.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Challenge_todo_list.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly TodoDbContext _dbContext;

        public PriorityController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getPriorities")]
        public async Task<IActionResult> GetPriorities()
        {
            var priorities = await _dbContext.Priorities.ToListAsync();

            return Ok(priorities);
        }
    }
}
