using Contacts.Domain.Entities;
using Contacts.Infrastructure.Database;

namespace IntegrationTests;

public static class TestDataSeeder
{
    public static async Task SeedTestDataAsync(ApplicationDbContext context)
    {
        await context.Database.EnsureCreatedAsync();
        //await context.Database.ExecuteSqlRawAsync("DELETE FROM Products");

        // improve use faker
        var contacts = new List<Contact>();
        for (int i = 1; i <= 15; i++)
        {
            var contact = new Contact { FirstName = $"First{i}", LastName = $"Last{i}", Email = $"first{i}.last{i}@email.com", Id = i, Phone = "1234567890", Title = "Mr", CreatedDate = DateTime.Now };
            contacts.Add(contact);
        }

        await context.Contacts.AddRangeAsync(contacts);
        await context.SaveChangesAsync();
    }
}