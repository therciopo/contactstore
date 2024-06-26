using Contacts.Domain.Interfaces;
using MediatR;

namespace Contacts.Application.Commands;

public class UpdateContact
{
    public record Command(
        int Id,
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
            var contact = await _contactRepository.GetContactByIdAsync(request.Id, cancellationToken);
            if (contact is null) return 0;

            // mapping
            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.Email = request.Email;
            contact.Phone = request.Phone;
            contact.Title = request.Title;
            contact.MiddleInitial = request.MiddleInitial;

            return await _contactRepository.UpdateContactAsync(contact, cancellationToken);
        }
    }
}