using Contacts.Application.Commands;
using Contacts.Application.ViewModels;

namespace Contacts.Server.Mappers;

public static class ManualMapper
{
    public static AddContact.Command MapToAddCommand(this AddOrUpdateContactViewModel viewModel)
    {
        return new AddContact.Command(
            viewModel.FirstName,
            viewModel.LastName, 
            viewModel.Email, 
            viewModel.Phone, 
            viewModel.Title, 
            viewModel.MiddleInitial);
    }

    public static UpdateContact.Command MapToUpdateCommand(this AddOrUpdateContactViewModel viewModel, int id)
    {
        return new UpdateContact.Command(id, 
            viewModel.FirstName,
            viewModel.LastName,
            viewModel.Email,
            viewModel.Phone,
            viewModel.Title,
            viewModel.MiddleInitial);
    }    
}
