using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Interfaces;

namespace TodoApp.Application.Features.Todos.Queries;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, IEnumerable<TodoDto>>
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(ITodoRepository todoRepository, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TodoDto>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var items = await _todoRepository.GetAllByUserIdAsync(request.UserId);
        return _mapper.Map<IEnumerable<TodoDto>>(items);
    }
}
