using FluentValidation;

namespace Contacts.Application.ViewModels;

public class AddOrUpdateContactViewModel
{    
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string Email { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Title { get; set; } = "";
    public string MiddleInitial { get; set; } = "";
}


public class AddOrUpdateContactValidator : AbstractValidator<AddOrUpdateContactViewModel>
{
    public AddOrUpdateContactValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty().WithMessage("First Name is required.")
            .MaximumLength(20).WithMessage("First Name must not exceed 20 characters.")
            .MinimumLength(3).WithMessage("First Name must be at least 3 characters.");

        RuleFor(c => c.LastName)
            .NotEmpty().WithMessage("Last Name is required.")
            .MaximumLength(20).WithMessage("Last Name must not exceed 20 characters.")
            .MinimumLength(3).WithMessage("Last Name must be at least 3 characters.");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(c => c.MiddleInitial)
            .MaximumLength(1).WithMessage("Middle Initial must not exceed 1 character.");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MinimumLength(2).WithMessage("Title must be at least 2 characters.")
            .NotNull();

        RuleFor(c => c.Phone)
            .NotEmpty()
            .MinimumLength(10)
            .Matches(@"^\d+$")
            .WithMessage("Phone must contain only numbers.");
    }
}