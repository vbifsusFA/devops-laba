using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Features.Todos.Commands;

public record DeleteTodoCommand(int Id, string UserId) : IRequest<bool>;

public class DeleteTodoCommandHandler : IRequestHandler<DeleteTodoCommand, bool>
{
    private readonly ITodoRepository _todoRepository;

    public DeleteTodoCommandHandler(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<bool> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _todoRepository.GetByIdAsync(request.Id, request.UserId);
        if (item == null) return false;

        await _todoRepository.DeleteAsync(request.Id, request.UserId);
        return true;
    }
}
