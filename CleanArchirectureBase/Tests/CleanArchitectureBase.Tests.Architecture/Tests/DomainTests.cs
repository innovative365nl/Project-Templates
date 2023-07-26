using CleanArchitectureBase.Tests.Architecture.Rules;

namespace CleanArchitectureBase.Tests.Architecture.Tests;

public class DomainTests
{
    [Test]
    public void ShouldNotHaveDependenciesOnOtherProjects()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .ShouldNot()
            .HaveDependencyOnAll("CleanArchitectureBase.Application", "CleanArchitectureBase.Infrastructure")
            .GetResult();
        Assert.True(condition: result.IsSuccessful);
    }

    [Test]
    public void ClassValueObjectsShouldBeSealed()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Domain.ValueObjects")
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    

    [Test]
    public void EntityClassesShouldNotHavePublicConstructors()
    {
        var result = Types
            .InAssembly(assembly: typeof(AggregateRoot).Assembly)
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Domain.Entities")
            .ShouldNot()
            .MeetCustomRule(rule: new ContainsPublicConstructorParametersRule())
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
}