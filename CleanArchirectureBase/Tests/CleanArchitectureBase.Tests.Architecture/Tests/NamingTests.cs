using System.Reflection;

namespace CleanArchitectureBase.Tests.Architecture.Tests;

public class GlobalTests
{
    [Test]
    public void InterfaceShouldStartWithI()
    {
        var result = Types
            .InCurrentDomain()
            .That()
            .AreInterfaces()
            .Should()
            .HaveNameStartingWith(start: "I", comparer: StringComparison.InvariantCulture)
            .GetResult();

        Assert.True(condition: result.IsSuccessful);
    }
}