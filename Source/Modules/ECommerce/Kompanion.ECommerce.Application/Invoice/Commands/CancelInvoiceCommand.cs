using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompanion.ECommerce.Application.Invoice.Commands;

public sealed record CancelInvoiceCommand : BaseCommand<ApiResponse>
{
    //Fatura bilgileri alınır...
}

