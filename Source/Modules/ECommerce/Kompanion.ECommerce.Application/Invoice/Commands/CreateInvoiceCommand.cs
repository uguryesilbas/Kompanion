using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;

namespace Kompanion.ECommerce.Application.Invoice.Commands;

public sealed record CreateInvoiceCommand : BaseCommand<ApiResponse>
{
    //Fatura bilgileri alınır...
}

