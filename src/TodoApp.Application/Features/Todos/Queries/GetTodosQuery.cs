using System.Collections.Generic;
using MediatR;
using TodoApp.Application.DTOs;

namespace TodoApp.Application.Features.Todos.Queries;

public record GetTodosQuery(string UserId) : IRequest<IEnumerable<TodoDto>>;
