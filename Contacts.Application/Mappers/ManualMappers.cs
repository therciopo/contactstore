using Contacts.Application.ViewModels;
using Contacts.Domain.Entities;

namespace Contacts.Application.Mappers;

public static class ManualMappers
{
    public static List<ContactViewModel>? MapToViewModel(List<Contact>? contacts)
    {
        if (contacts == null) return null;

        var list = new List<ContactViewModel>();

        foreach (var contact in contacts)
        {
            var contactViewModel = new ContactViewModel
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                MiddleInitial = contact.MiddleInitial,
                Phone = contact.Phone,
                Title = contact.Title,
                CreatedDate = contact.CreatedDate,
                ModifiedDate = contact.ModifiedDate
            };

            list.Add(contactViewModel);
        }
        return list;
    }
}