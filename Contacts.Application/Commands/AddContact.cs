using Contacts.Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Contacts.Application.Commands;

public class AddContact
{
    public record Command(        
        string FirstName,
        string LastName,
        string Email,
        string Phone,
        string Title,
        string MiddleInitial) : IRequest<int>;

    public class Handler(IContactRepository contactRepository) : IRequestHandler<Command, int>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var existingContact = await _contactRepository.GetContactByEmailAsync(request.Email, cancellationToken);

            if (existingContact is not null) return 0;

            // mapping
            var contact = new Domain.Entities.Contact
            {
                
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Title = request.Title,
                MiddleInitial = request.MiddleInitial
            };

            return await _contactRepository.AddContactAsync(contact, cancellationToken);
        }
    }

    public class CreateAddContactValidator : AbstractValidator<Command>
    {
        public CreateAddContactValidator()
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
}