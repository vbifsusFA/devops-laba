using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Common.Interfaces;
using TodoApp.Application.DTOs;
using TodoApp.Application.Features.Todos.Commands;
using TodoApp.Application.Features.Todos.Queries;

namespace TodoApp.Web.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class TodosController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public TodosController(IMediator mediator, ICurrentUserService currentUserService)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodos()
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var query = new GetTodosQuery(userId);
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateTodo(CreateTodoRequest request)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var command = new CreateTodoCommand(request.Title, request.Description, userId);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTodo(int id, UpdateTodoRequest request)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var command = new UpdateTodoCommand(id, request.Title, request.Description, request.IsCompleted, userId);
        var result = await _mediator.Send(command);

        if (!result) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTodo(int id)
    {
        var userId = _currentUserService.UserId;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var command = new DeleteTodoCommand(id, userId);
        var result = await _mediator.Send(command);

        if (!result) return NotFound();
        return NoContent();
    }
}

public record CreateTodoRequest(string Title, string? Description);
public record UpdateTodoRequest(string Title, string? Description, bool IsCompleted);
