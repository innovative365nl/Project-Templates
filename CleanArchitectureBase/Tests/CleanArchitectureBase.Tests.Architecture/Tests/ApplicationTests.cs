using System.Reflection;

namespace CleanArchitectureBase.Tests.Architecture.Tests;
public class ApplicationTests
{
    [Test]
    public void ProjectShouldNotHaveDependenciesOnInfrastructureProject()
    {
        var result = Types
            .InAssembly(assembly: Assembly.Load(assemblyString: "CleanArchitectureBase.Application"))
            .That()
            .ResideInNamespace(name: "CleanArchitectureBase.Application")
            .ShouldNot()
            .HaveDependencyOn(dependency: "CleanArchitectureBase.Infrastructure")
            .GetResult();
        Assert.True(condition: result.IsSuccessful);
    }

}
