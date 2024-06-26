using Contacts.Domain.Interfaces;
using MediatR;

namespace Contacts.Application.Commands;

public class DeleteContact
{
    public record Command(
         int Id) : IRequest<int>;

    public class Handler(IContactRepository contactRepository) : IRequestHandler<Command, int>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<int> Handle(Command request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository.GetContactByIdAsync(request.Id, cancellationToken);
            if (contact is null) return 0;

            return await _contactRepository.DeleteContactAsync(contact, cancellationToken);
        }
    }
}
