using Contacts.Application.Queries;
using Contacts.Domain.Entities;
using FluentAssertions;

namespace ApplicationTests;

public class ContactHandlerTests
{
    private readonly TestFixture _fixture = new();
    [Fact]
    public async Task Should_Get_All_Contacts()
    {
        // Arrange
        var contact = new Contact { Id = 1, FirstName = "First1", LastName = "Last1", Email = "first1.last1@email.com", Phone = "123456789", Title = "Mr", CreatedDate = DateTime.Now };
        var contactsMock = new List<Contact> { contact };
        
        _fixture.MockContact(contactsMock);
        int pageSize = 10;

        // Act
        var result = await _fixture.GetAllContactsHandler.Handle(
           new GetAllContacts.Query(1, pageSize, ""), default);
        
        // Assert
        result.TotalItems.Should().Be(1);        
        result.Contacts.Should().NotBeNull();
        result.Contacts.FirstOrDefault()!.Id.Should().Be(contact.Id);
    }
}