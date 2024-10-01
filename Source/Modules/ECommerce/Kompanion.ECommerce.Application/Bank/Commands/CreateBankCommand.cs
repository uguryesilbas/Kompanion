using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Bank.Commands;

public record CreateBankCommand : BaseCommand<ApiResponse<int>>
{
    public string BankName { get; init; }
}
