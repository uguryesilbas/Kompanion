using Kompanion.ECommerce.Application.Bank.Dtos;
using Kompanion.ECommerce.Domain.Bank;

namespace Kompanion.ECommerce.Application.Bank.Mappings;

public static class BankDtoMapper
{
    public static BankDto MapToDto(this BankEntity entity) => (BankDto)entity;
    public static List<BankDto> MapToDto(this IEnumerable<BankEntity> source) => source.Select(x => x.MapToDto()).ToList();
}
