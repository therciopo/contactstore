using Contacts.Application.Commands;
using Contacts.Application.Queries;
using Contacts.Application.ViewModels;
using Contacts.Server.Mappers;
using FluentValidation;
using MediatR;

namespace Contacts.Server.Endpoints;

public static class ContactEndpointsGroup
{
    public static void MapContactEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/contacts")
            .WithOpenApi();

        group.MapGet("/", async ([AsParameters]GetAllContacts.Query query, IMediator mediator) =>
        {
            // validate query
            var response = await mediator.Send(query);
            return Results.Ok(response);
        })
        .WithName("GetContacts")        
        .WithSummary("Get all contacts");

        group.MapGet("/{id}", async (
            ILoggerFactory loggerFactory,
            IMediator mediator, int id) =>
        {
            var logger = loggerFactory.CreateLogger("Contact");

            var contact = await mediator.Send(new GetContactById.Query(id));

            if (contact is null)
            {
                logger.LogError("Contact Id {0} not found", id);
                return Results.NotFound();
            }
            return Results.Ok(contact);
        })
        .WithName("GetContactById")
        .WithSummary("Get contact by Id");

        group.MapPut("/{id}", async (
            ILoggerFactory loggerFactory,
            IValidator <AddOrUpdateContactViewModel> validator,
            int id, AddOrUpdateContactViewModel viewModel,
            IMediator mediator) =>
        {
            var logger = loggerFactory.CreateLogger("Contact");

            var validationResult = await validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var command = ManualMapper.MapToUpdateCommand(viewModel, id);
            var contactId = await mediator.Send(command);

            if (contactId == 0)
            {
                logger.LogError("Contact Id {0} not found", contactId);
                return Results.NotFound();
            }

            logger.LogInformation("Contact Id {0} updated", contactId);

            return Results.NoContent();
        })
        .WithName("UpdateContactById")
        .WithSummary("Update contact by Id");

        group.MapPost("/", async (
            ILoggerFactory loggerFactory,
            IValidator<AddOrUpdateContactViewModel> validator,
            AddOrUpdateContactViewModel viewModel,
            IMediator mediator) =>
        {
            var logger = loggerFactory.CreateLogger("Contact");

            var validationResult = await validator.ValidateAsync(viewModel);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var contactId = await mediator.Send(ManualMapper.MapToAddCommand(viewModel));

            if (contactId == 0)
            {
                var errorMessage = $"There is already a contact with email {viewModel.Email}";
                logger.LogError(errorMessage);
                return Results.Conflict(errorMessage);
            }

            logger.LogInformation("Contact Id {0} created", contactId);

            return Results.Ok(contactId);
        })
        .WithName("AddContact")
        .WithSummary("Create contact");

        group.MapDelete("/{id}",
            async(int id, 
            ILoggerFactory loggerFactory,
            IMediator mediator) =>
            {
                var logger = loggerFactory.CreateLogger("Contact");

                var contactId = await mediator.Send(new DeleteContact.Command(id));

                if (contactId == 0)
                {
                    logger.LogError("Contact Id {0} not found", contactId);
                    return Results.NotFound();
                }

                logger.LogInformation("Contact Id {0} deleted", contactId);
                return Results.NoContent();
            })
            .WithName("DeleteContactById")
            .WithSummary("Delete contact by Id");
    }
}