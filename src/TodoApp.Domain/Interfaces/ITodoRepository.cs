using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Domain.Interfaces;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(string userId);
    Task<TodoItem?> GetByIdAsync(int id, string userId);
    Task AddAsync(TodoItem item);
    Task UpdateAsync(TodoItem item);
    Task DeleteAsync(int id, string userId);
}
