using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Features.Todos.Commands;

public record UpdateTodoCommand(int Id, string Title, string? Description, bool IsCompleted, string UserId) : IRequest<bool>;

public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;

    public UpdateTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _todoRepository.GetByIdAsync(request.Id, request.UserId);
        if (item == null) return false;

        item.Title = request.Title;
        item.Description = request.Description;
        item.IsCompleted = request.IsCompleted;

        await _todoRepository.UpdateAsync(item);
        return true;
    }
}
