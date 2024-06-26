using Contacts.Domain.Entities;

namespace Contacts.Domain.Interfaces;

public interface IContactRepository
{
    Task<List<Contact>> GetAllContactsAsync(string searchTerm, int pageNum, int pageSize, CancellationToken cancellationToken);
    Task<int> CountAllContactsAsync(string searchTerm, CancellationToken cancellationToken);
    Task<Contact?> GetContactByIdAsync(int id, CancellationToken cancellationToken);
    Task<int> UpdateContactAsync(Contact contact, CancellationToken cancellationToken);
    Task<int> AddContactAsync(Contact contact, CancellationToken cancellationToken);
    Task<int> DeleteContactAsync(Contact contact, CancellationToken cancellationToken);
    Task<Contact?> GetContactByEmailAsync(string email, CancellationToken cancellationToken);
}
