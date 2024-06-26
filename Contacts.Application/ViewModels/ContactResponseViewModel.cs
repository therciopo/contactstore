namespace Contacts.Application.ViewModels;

public class ContactResponseViewModel
{
    public int TotalItems { get; set; }
    public List<ContactViewModel>? Contacts { get; set; }
}
