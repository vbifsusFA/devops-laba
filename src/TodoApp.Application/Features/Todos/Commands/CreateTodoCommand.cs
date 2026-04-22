using MediatR;

namespace TodoApp.Application.Features.Todos.Commands;

public record CreateTodoCommand(string Title, string? Description, string UserId) : IRequest<int>;
