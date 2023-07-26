using System.Reflection;
using CleanArchitectureBase.Domain.Interfaces;

namespace CleanArchitectureBase.Tests.Architecture.Tests;

public class InfrastructureTests
{
    [Test]
    public void RepositoryClassShouldImplementIRepository()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "CleanArchitectureBase.Infrastructure"))
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Infrastructure.Repositories")
            .And()
            .AreClasses()
            .Should()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }

    [Test]
    public void RepositoryClassesShouldBeSealed()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "CleanArchitectureBase.Infrastructure"))
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Infrastructure.Repositories")
            .And()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .And()
            .AreClasses()
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    
    [Test]
    public void RepositoryClassesShouldBeInternal()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "CleanArchitectureBase.Infrastructure"))
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Infrastructure.Repositories")
            .And()
            .ImplementInterface(interfaceType: typeof(IRepository<>))
            .And()
            .AreClasses()
            .Should()
            .NotBePublic()
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
    
    [Test]
    public void RepositoryClassesShouldNotImplementApplicationInterfaces()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "CleanArchitectureBase.Infrastructure"))
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Infrastructure.Repositories")
            .And()
            .AreClasses()
            .ShouldNot()
            .HaveDependencyOn(dependency: "CleanArchitectureBase.Application")
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
}