namespace Contacts.Application.ViewModels;

public sealed record ContactRequestViewModel(string? SearchTerm, int? PageNum, int? PageSize)
{
}

//public sealed class ContactRequestViewModel
//{
//    public string? SearchTerm { get; set; }
//    public int? PageNum { get; set; }
//    public int? PageSize { get; set; }
//}
