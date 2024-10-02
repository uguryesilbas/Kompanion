using Kompanion.Application.Extensions;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using Kompanion.ECommerce.Application.Invoice.Commands;
using Kompanion.Infrastructure.Invoicing.Abstractions;
using Microsoft.Extensions.Logging;

namespace Kompanion.ECommerce.Application.Invoice.CommandHandlers;

public sealed class InvoiceCommandHandler : ICommandHandler<CreateInvoiceCommand, ApiResponse>, ICommandHandler<CancelInvoiceCommand, ApiResponse>
{
    private readonly IInvoiceService _invoiceService;
    private readonly ILogger<InvoiceCommandHandler> _logger;

    public InvoiceCommandHandler(IInvoiceService invoiceService, ILogger<InvoiceCommandHandler> logger)
    {
        _invoiceService = invoiceService;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _invoiceService.CreateInvoice(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Fatura oluşturulamadı!");

            return new ApiResponse().BadRequest();
        }

        return new ApiResponse().Ok();
    }

    public async Task<ApiResponse> Handle(CancelInvoiceCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _invoiceService.CancelInvoice(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Fatura iptal edilemedi!");

            return new ApiResponse().BadRequest();
        }

        return new ApiResponse().Ok();
    }
}

