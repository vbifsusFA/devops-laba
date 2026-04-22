using FluentValidation;
using TodoApp.Application.Features.Todos.Commands;

namespace TodoApp.Application.Features.Todos.Validators;

public class CreateTodoCommandValidator : AbstractValidator<CreateTodoCommand>
{
    public CreateTodoCommandValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters.");

        RuleFor(v => v.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
            
        RuleFor(v => v.UserId)
            .NotEmpty().WithMessage("UserId is required.");
    }
}
