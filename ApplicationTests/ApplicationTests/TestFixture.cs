using Contacts.Application.Queries;
using Contacts.Domain.Entities;
using Contacts.Domain.Interfaces;
using NSubstitute;

namespace ApplicationTests;

public class TestFixture
{
    private IContactRepository ContactRepository { get; set; }
    public GetAllContacts.Handler GetAllContactsHandler { get; }

    public TestFixture()
    {
        ContactRepository = Substitute.For<IContactRepository>();

        GetAllContactsHandler = new GetAllContacts.Handler(ContactRepository);

    }
    public void MockContact(List<Contact> contacts)
    {
        ContactRepository.GetAllContactsAsync(Arg.Any<string>(), Arg.Any<int>(), Arg.Any<int>(), default).Returns(contacts);

        ContactRepository.CountAllContactsAsync(Arg.Any<string>(), default).Returns(1);        
    }
}