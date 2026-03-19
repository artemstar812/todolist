using Microsoft.AspNetCore.Mvc;
using ToDo_List.Models;
using ToDo_List.Services;

namespace ToDo_List.Controllers
{
    [ApiController]
    [Route("api/todos")]
    public class TodoController : Controller
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
        {
            var items = _todoService.GetAllAsync();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            var item = _todoService.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> Create(TodoItem item)
        {
            await _todoService.AddAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _todoService.DeleteAsync(id);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TodoItem item)
        {
            await _todoService.UpdateAsync(id, item);

            return NoContent();
        }

        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _todoService.ToggleAsync(id);

            return NoContent();
        }
    }
}
