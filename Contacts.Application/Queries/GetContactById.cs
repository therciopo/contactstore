using Contacts.Application.ViewModels;
using Contacts.Domain.Interfaces;
using MediatR;

namespace Contacts.Application.Queries;

public class GetContactById
{
    public record Query(
         int Id) : IRequest<ContactViewModel>;

    public class Handler(IContactRepository contactRepository) : IRequestHandler<Query, ContactViewModel?>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<ContactViewModel?> Handle(Query request, CancellationToken cancellationToken)
        {
            var contact = await _contactRepository
                .GetContactByIdAsync(request.Id, cancellationToken);

            if (contact is null) return null;

            // mapping
            return new ContactViewModel
            {
                Id = contact.Id,
                CreatedDate = contact.CreatedDate,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleInitial=contact.MiddleInitial,
                ModifiedDate = contact.ModifiedDate,
                Phone= contact.Phone,
                Title = contact.Title
            };
        }
    }
}