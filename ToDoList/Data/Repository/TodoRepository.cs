using Microsoft.EntityFrameworkCore;
using ToDo_List.Models;

namespace ToDo_List.Data.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly AppDbContext _context;

        public TodoRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems.ToListAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }
        
        public async Task AddAsync(TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _context.TodoItems.FindAsync(id);

            if (item == null)
                return;

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(int id, TodoItem updated)
        {
            var todo = await GetByIdAsync(id);

            if (todo != null)
            {
                todo.Title = updated.Title;
                todo.IsCompleted = updated.IsCompleted;

                await _context.SaveChangesAsync();
            }
        }
    }
}
