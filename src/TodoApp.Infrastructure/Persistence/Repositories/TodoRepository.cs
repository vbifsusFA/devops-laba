using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Persistence.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly ApplicationDbContext _context;

    public TodoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllByUserIdAsync(string userId)
    {
        return await _context.TodoItems
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id, string userId)
    {
        return await _context.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);
    }

    public async Task AddAsync(TodoItem item)
    {
        await _context.TodoItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem item)
    {
        _context.TodoItems.Update(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id, string userId)
    {
        var item = await GetByIdAsync(id, userId);
        if (item != null)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
