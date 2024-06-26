using Contacts.Application.ViewModels;
using Contacts.Domain.Entities;
using Contacts.Server;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;

namespace IntegrationTests
{
    public class ContactsEndpointTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ContactsEndpointTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]
        public async Task GetContacts_ReturnsCount10WithDefaultPaginationParams()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("/api/contacts");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();
            var viewModel = JsonConvert.DeserializeObject<ContactResponseViewModel>(content);
            viewModel.Should().NotBeNull();

            viewModel?.Contacts.Should().HaveCount(10); //seeded 15 contacts but pagination default logic returns 10
        }
    }
}