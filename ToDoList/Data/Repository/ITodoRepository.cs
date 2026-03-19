using ToDo_List.Models;

namespace ToDo_List.Data.Repository
{
    public interface ITodoRepository
    {
        Task<List<TodoItem>> GetAllAsync();

        Task<TodoItem?> GetByIdAsync(int id);

        Task AddAsync(TodoItem item);

        Task DeleteAsync(int id);

        Task UpdateItemAsync(int id, TodoItem updated);
    }
}
