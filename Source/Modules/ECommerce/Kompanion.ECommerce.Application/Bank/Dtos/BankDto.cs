using Kompanion.ECommerce.Domain.Bank;

namespace Kompanion.ECommerce.Application.Bank.Dtos;

public class BankDto
{
    public int Id { get; set; }
    public string BankName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime? UpdatedDateTime { get; set; }
    public int CreatedUserId { get; set; }
    public int? UpdatedUserId { get; set; }

    public static explicit operator BankDto(BankEntity entity)
    {
        return new BankDto
        {
            Id = entity.Id,
            BankName = entity.BankName,
            CreatedDateTime = entity.CreatedDateTime,
            UpdatedDateTime = entity.UpdatedDateTime,
            CreatedUserId = entity.CreatedUserId,
            UpdatedUserId = entity.UpdatedUserId,
        };
    }
}

