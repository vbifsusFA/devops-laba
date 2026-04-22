using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApp.Domain.Entities;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Features.Todos.Commands;

public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, int>
{
    private readonly ITodoRepository _todoRepository;

    public CreateTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<int> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = new TodoItem
        {
            Title = request.Title,
            Description = request.Description,
            UserId = request.UserId,
            CreatedAt = DateTime.UtcNow,
            IsCompleted = false
        };

        await _todoRepository.AddAsync(item);
        return item.Id;
    }
}
