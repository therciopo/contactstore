using Contacts.Domain.Entities;
using Contacts.Domain.Interfaces;
using Contacts.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Repositories;

public class ContactRepository(ApplicationDbContext dbContext) : IContactRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;

    public Task<int> CountAllContactsAsync(string searchTerm, CancellationToken cancellationToken)
    {
        var query = _dbContext.Contacts;

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query.Where(x => x.Email.Contains(searchTerm) ||
            x.FirstName.Contains(searchTerm));
        }
        return query.CountAsync(cancellationToken);        
    }

    public Task<List<Contact>> GetAllContactsAsync(string searchTerm, int pageNum, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Contacts.AsQueryable();        

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(x => 
            x.Email.Contains(searchTerm) ||
            x.FirstName.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);

        var contacts = query
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return contacts;
    }
    public async Task<Contact?> GetContactByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _dbContext.Contacts.FindAsync(id, cancellationToken);
    }

    public async Task<Contact?> GetContactByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Contacts.SingleOrDefaultAsync(contact => contact.Email == email, cancellationToken);
    }

    public async Task<int> UpdateContactAsync(Contact contact, CancellationToken cancellationToken)
    {
        contact.ModifiedDate = DateTime.Now;
        _dbContext.Contacts.Update(contact);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return contact.Id;
    }

    public async Task<int> AddContactAsync(Contact contact, CancellationToken cancellationToken)
    {
        contact.CreatedDate = DateTime.Now;
        _dbContext.Contacts.Add(contact);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return contact.Id;
    }

    public async Task<int> DeleteContactAsync(Contact contact, CancellationToken cancellationToken)
    {
        // hard delete. Option for soft delete
        _dbContext.Remove(contact);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return contact.Id;
    }
}