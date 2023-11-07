using CleanArchitectureBase.Application.Common.Composers;
using Microsoft.Extensions.DependencyInjection;
using CleanArchitectureBase.Infrastructure.Common.Composer;

namespace CleanArchitectureBase.Tests.UnitTests;

public class RegisterDepenciesTests
{
    [TestFixture]
    public class RegisterDependenciesTests
    {
        [Test]
        public void RegisterInfrastructure_WhenCalled_ReturnsServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = services.RegisterInfrastructure();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IServiceCollection>(result);
        }
        
        [Test]
        public void RegisterApplication_WhenCalled_ReturnsServiceCollection()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            var result = services.RegisterApplication();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IServiceCollection>(result);
        }
    }
}