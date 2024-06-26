using MediatR;
using Contacts.Domain.Interfaces;
using Contacts.Application.ViewModels;
using Contacts.Application.Mappers;

namespace Contacts.Application.Queries;

public class GetAllContacts
{
    public sealed record Query(
        int? PageNum,
        int? PageSize,
        string? SearchTerm) : IRequest<ContactResponseViewModel>;

    public class Handler(IContactRepository contactRepository) : IRequestHandler<Query, ContactResponseViewModel>
    {
        private readonly IContactRepository _contactRepository = contactRepository;

        public async Task<ContactResponseViewModel> Handle(Query query, CancellationToken cancellationToken)
        {
            var contacts = await _contactRepository
                .GetAllContactsAsync(query.SearchTerm ?? "", query.PageNum ?? 0, query.PageSize ?? 10, cancellationToken);

            var totalCount = await _contactRepository.CountAllContactsAsync(query.SearchTerm ?? "", cancellationToken);

            // mapping
            return new ContactResponseViewModel {
                TotalItems = totalCount,
                Contacts = ManualMappers.MapToViewModel(contacts)
            };
        }
    }    
}