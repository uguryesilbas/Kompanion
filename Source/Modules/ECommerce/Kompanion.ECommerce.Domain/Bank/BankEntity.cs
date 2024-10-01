using Kompanion.Domain.Abstracts;
using Kompanion.Domain.Interfaces;

namespace Kompanion.ECommerce.Domain.Bank;

public sealed class BankEntity : BaseEntity, ITrackableEntity
{
    public BankEntity()
    {

    }

    public BankEntity(string bankName)
    {
        BankName = bankName;
    }

    public override int Id { get; set; }
    public string BankName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public int CreatedUserId { get; set; }
    public int? UpdatedUserId { get; set; }

}
