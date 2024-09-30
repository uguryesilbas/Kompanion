using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace Kompanion.TestBase.Abstracts;

public abstract class BaseLayerTests
{
    protected abstract Assembly ApplicationLayerAssembly { get; }
    protected abstract Assembly DomainLayerAssembly { get; }
    protected abstract Assembly InfrastructureLayerAssembly { get; }

    protected virtual void AssertArchTestResult(TestResult results) => results.FailingTypes.Should().BeNullOrEmpty();

    [Fact]
    public void DomainLayer_DoesNotHaveDependency_ToApplicationLayer()
    {
        TestResult testResult = Types.InAssembly(DomainLayerAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationLayerAssembly.GetName().Name)
            .GetResult();

        AssertArchTestResult(testResult);
    }

    [Fact]
    public void DomainLayer_DoesNotHaveDependency_ToInfrastructureLayer()
    {
        TestResult testResult = Types.InAssembly(DomainLayerAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureLayerAssembly.GetName().Name)
            .GetResult();

        AssertArchTestResult(testResult);
    }

    [Fact]
    public void ApplicationLayer_DoesNotHaveDependency_ToInfrastructureLayer()
    {
        TestResult testResult = Types.InAssembly(ApplicationLayerAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureLayerAssembly.GetName().Name)
            .GetResult();

        AssertArchTestResult(testResult);
    }
}

