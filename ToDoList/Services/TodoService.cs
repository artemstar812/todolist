using ToDo_List.Data.Repository;
using ToDo_List.Models;

namespace ToDo_List.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Title))
            {
                throw new ArgumentException("Task title cannot be empty");
            }

            if (item.Title.Length > 100)
                throw new ArgumentException("Title is too long");

            var count = (await _repository.GetAllAsync()).Count;

            await _repository.AddAsync(item);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task UpdateAsync(int id, TodoItem updatedItem)
        {
            await _repository.UpdateItemAsync(id, updatedItem);
        }

        public async Task ToggleAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
                throw new Exception("Task not found");

            item.IsCompleted = !item.IsCompleted;

            await _repository.UpdateItemAsync(id, item);
        }
    }
}
