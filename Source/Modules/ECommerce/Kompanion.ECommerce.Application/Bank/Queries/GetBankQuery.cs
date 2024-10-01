using Kompanion.Application.MediatR.Queries;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Bank.Dtos;

namespace Kompanion.ECommerce.Application.Bank.Queries;

public sealed record GetBankQuery(int BankId) : BaseQuery<ApiResponse<BankDto>>;
