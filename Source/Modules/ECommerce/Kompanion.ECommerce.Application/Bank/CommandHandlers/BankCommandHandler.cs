using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.Domain.Extensions;
using Kompanion.ECommerce.Application.Bank.Commands;
using Kompanion.ECommerce.Domain.Bank;

namespace Kompanion.ECommerce.Application.Bank.CommandHandlers;

public class BankCommandHandler : ICommandHandler<CreateBankCommand, ApiResponse<int>>
{
    private readonly IBankRepository _bankRepository;

    public BankCommandHandler(IBankRepository bankRepository)
    {
        _bankRepository = bankRepository;
    }

    public async Task<ApiResponse<int>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
    {
        BankEntity newEntity = new(request.BankName);

        bool result = await _bankRepository.InsertAsync(newEntity, cancellationToken);

        return result 
            ? new ApiResponse<int>(newEntity.Id).Created() 
            : new ApiResponse<int>(0).BadRequest().AddError("Banka kaydedilemedi!");
    }
}

