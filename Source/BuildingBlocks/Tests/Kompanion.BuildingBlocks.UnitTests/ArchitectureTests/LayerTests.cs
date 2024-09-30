using System.Reflection;
using Kompanion.Application.Wrappers;
using Kompanion.Domain.Abstracts;
using Kompanion.Infrastructure.Database.Abstracts;
using Kompanion.TestBase.Abstracts;

namespace Kompanion.BuildingBlocks.UnitTests.ArchitectureTests;

public class LayerTests : BaseLayerTests
{
    protected override Assembly ApplicationLayerAssembly => typeof(ApiResponse).Assembly;
    protected override Assembly DomainLayerAssembly => typeof(BaseEntity).Assembly;
    protected override Assembly InfrastructureLayerAssembly => typeof(BaseDbContext).Assembly;
}

