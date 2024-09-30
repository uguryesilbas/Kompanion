using NetArchTest.Rules;
using Kompanion.Application.MediatR.Commands;
using Kompanion.Application.MediatR.Queries;
using Kompanion.TestBase.Rules;

namespace Kompanion.TestBase.Abstracts;

public abstract class BaseApplicationTests : BaseLayerTests
{
    [Fact]
    public void Query_Should_Have_Name_EndingWith_Query()
    {
        TestResult result = Types.InAssembly(ApplicationLayerAssembly)
            .That()
            .Inherit(typeof(BaseQuery<>))
            .Should()
            .MeetCustomRule(new GenericQueryNameRule())
            .GetResult();

        AssertArchTestResult(result);
    }

    [Fact]
    public void Command_Should_Have_Name_EndingWith_Command()
    {
        TestResult result = Types.InAssembly(ApplicationLayerAssembly)
            .That()
            .Inherit(typeof(BaseCommand<>))
            .Should()
            .MeetCustomRule(new GenericCommandNameRule())
            .GetResult();

        AssertArchTestResult(result);
    }
}

