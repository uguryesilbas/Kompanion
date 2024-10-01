using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Queries;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Bank.Dtos;
using Kompanion.ECommerce.Application.Bank.Mappings;
using Kompanion.ECommerce.Application.Bank.Queries;
using Kompanion.ECommerce.Domain.Bank;

namespace Kompanion.ECommerce.Application.Bank.QueryHandlers;

public class BankQueryHandler : IQueryHandler<GetBankQuery, ApiResponse<BankDto>>
{
    private readonly IBankRepository _bankRepository;

    public BankQueryHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task<ApiResponse<BankDto>> Handle(GetBankQuery request, CancellationToken cancellationToken)
    {
        BankEntity bankEntity = await _bankRepository.FindByIdAsync(request.BankId, cancellationToken);

        return bankEntity is not null
            ? new ApiResponse<BankDto>(bankEntity.MapToDto()).Ok()
            : new ApiResponse<BankDto>(null).BadRequest().AddError("Banka bulunamadı!");
    }
}

