# .NET 8 SLN Template with Clean Architecture

This is a C# solution template following the Clean Architecture approach. This template is designed to serve as a starting point for new C# projects, emphasizing a well-organized, scalable, and maintainable codebase. It is based on .NET 8 and includes architectural tests.

## Introduction

This solution template adheres to the Clean Architecture concept, as described by Robert C. Martin (Uncle Bob). The primary goal of this architecture is to keep business logic isolated from technical details, making the code flexible, extensible, and easily testable.

## Structure

The solution is divided into several projects to keep responsibilities separated. Here's an overview of the project structure:

1. **ProjectName.Domain**: This project contains the core logic of the application. It includes domain entities, business rules, and interfaces for services.

2. **ProjectName.Infrastructure**: This project contains implementations of the interfaces defined in the Domain project. It handles technical details such as databases, external APIs, and logging.

3. **ProjectName.Application**: This project contains the application logic. It uses the interfaces from the Domain project and acts as an intermediate layer between the presentation layer and the infrastructure layer.

4. **ProjectName.ArchitectureTests**: This project contains architecture tests. It uses rules to validate if the project is in line with Architecture Rules.

## Architecture Rules
- Repository Class Should Implement IRepository
- Repository Classes Should Be Internal
- Repository Classes Should Be Sealed
- Repository Classes Should Not Implement Application Interfaces
- Domain Should Not Have Dependencies On Other Projects
- Value Objects Should Be Sealed
- Entity Classes Should Not Have Public Constructors

## Naming Conventions
 - Interfaces Should Start With I



## Template Features: 
1. ***Interfaces of Repositories in Domain Layer:***
   In this template, the interfaces of repositories are defined and stored in the Domain Layer. The Domain Layer represents the core business logic and contains the domain models and business rules. By placing the repository interfaces in the Domain Layer, we enforce the principle of separation of concerns, ensuring that the domain remains independent of the data access implementation details in the Infrastructure Layer.<br/><br/>
2. ***Prepared ValueObjects:***
   To combat Primitive Obsessions and promote a rich domain model, this template includes the use of Value Objects. Value Objects are small, immutable objects that encapsulate a specific domain concept. They are designed to be used as whole values, and their equality is determined by their attribute values. By including prepared ValueObjects in the template, we encourage developers to create dedicated domain-specific abstractions, leading to a more expressive and type-safe codebase.<br/><br/>
3. ***Entities and Aggregate Roots:***
   In this template, we distinguish between Entities and Aggregate Roots, both of which are essential concepts in Domain-Driven Design.
   Entities: Entities represent domain objects with a unique identity and a mutable state. They encapsulate business logic and behavior and are integral to the core domain model. In the context of the template, entities are defined within the Domain Layer. Examples of entities in an e-commerce application could be "Customer," "Product," or "Order." Each entity has a globally unique identifier (ID) that distinguishes it from other entities if this is setup in the object.
   Aggregate Roots: Aggregate Roots are special types of entities that act as the entry points to the aggregate - a cluster of associated entities that are treated as a single unit. Aggregate Roots ensure consistency and transactional boundaries within the aggregate. The template recognizes the significance of Aggregate Roots and designates them as the primary points of access to interact with the Domain Layer. In the e-commerce application example, the "Order" entity could be the Aggregate Root, encapsulating "OrderItem" entities within it. <br/><br/>By distinguishing between Entities and Aggregate Roots, the template encourages developers to design domain models that maintain a clear and consistent representation of business concepts while enforcing encapsulation and transactional consistency within aggregates. This promotes a cleaner and more maintainable domain model in line with Domain-Driven Design principles.<br/><br/>
4. ***Dependency Injection Splitting between Application and Infrastructure:***
   In this template, the Dependency Injection (DI) is split between the Application Layer and the Infrastructure Layer. The Application Layer is responsible for orchestrating the interactions between the Domain Layer and the Infrastructure Layer. It contains application services that utilize the interfaces of repositories defined in the Domain Layer to perform domain-specific operations. These services are registered in the DI container.<br/><br/>
   On the other hand, the Infrastructure Layer is responsible for implementing the interfaces defined in the Domain Layer, such as repositories and other technical concerns like data access. These implementations are registered in the DI container separately. This separation ensures a clear boundary between the application's core logic and technical details, promoting better maintainability and testability.<br/><br/>
5. ***IRepository Structure:***
   The template follows the common IRepository structure, where each domain entity has its own repository interface. For instance, if we have a domain entity "Product," the IRepository structure would typically include an interface named "IProductRepository" in the Domain Layer. This interface declares methods for basic CRUD operations related to the "Product" entity.
   By adhering to this IRepository structure, the code remains organized, and it's easier to manage different domain entities' data access operations. The IRepository structure facilitates the decoupling of the Domain Layer from the data access implementation, making it possible to switch out different database technologies without affecting the core business logic.
   <br/><br/>
6. ***Central Package Version Management (Directory.Packages.Props):*** In larger projects, managing package versions across multiple projects can become challenging. To address this, we utilize a Directory.Packages.Props file that centralizes the management of package versions. This file lists all the required packages and their corresponding versions in one location, allowing developers to easily update or modify package versions across the entire solution. Centralized package version management ensures that all projects within the solution use the same package versions, reducing the risk of version conflicts and ensuring consistent behavior throughout the application.
   <br/><br/>
7. ***Central Property Management (Directory.Build.Props):*** Similar to central package version management, we employ a Directory.Build.Props file to centralize property management for the solution. This file includes common build properties that are shared across projects, such as compiler options, build configurations, or paths to external tools. Centralized property management ensures that the same build settings and configurations are applied consistently across all projects within the solution. This simplifies maintenance and promotes standardization, making it easier for developers to understand and navigate the project's build process.
   <br/><br/>By incorporating central package version management and property management, our evolving template promotes consistency and maintainability across the entire solution. These practices make it easier for developers to manage dependencies and build configurations, allowing them to focus more on writing business logic and less on dealing with configuration-related issues. As the solution grows and more templates are introduced, these practices will facilitate smoother integration and collaboration among the different parts of the application.

## Examples   
### Example 1: Creating a Value Object
``` 
public class ProductName : ValueObject<string, ProductName>
{
    private const int NameMinLength = 3;
    private const int NameMaxLength = 255;
    protected override void Validate()
    {
        if (string.IsNullOrWhiteSpace(Value))
            throw new NameIsEmptyException();
        if (Value.Length > NameMaxLength)
            throw new NameIsTooLongException(count: Value.Length);
        if (Value.Length < NameMinLength)
            throw new NameIsTooShortException(count: Value.Length);
        
        base.Validate();
    }
}
```
### Example 2: Creating an Entity
```
public class Product : Entity
{
    public ProductName ProductName { get; private set; }
    public bool IsAvailable { get; private set; }
    
    private Product(string productName)
    {
        ProductName = ProductName.From(productName);
        IsAvailable = false;
    }

    public static Product Create(string productName)
    {
        
        return new Product(productName);
        
    }

    public void Available()
    {
        IsAvailable = true;
    }
}
```
### Example 3: Creating an Aggregate Root
```
public class ProductGroup : AggregateRoot
{
    public List<Product> Products { get; private set; } 
}
```
### Example 4: Creating a Repository
```
public class ProductGroupRepository : IRepository<Product>
{
    private ProductGroupContext _context;
    public ProductGroupRepository(ProductGroupContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task<ProductGroup> GetByIdAsync(Guid id)
    {
        return _context.ProductsGroups.FindAsync(id);
    }
}
```


## Getting started

1. The easy way :
   ``` 
      dotnet new install Innovative.CleanArchitectureTemplates::1.0.2
   ``` 
2. The hard way :
   ``` 
      git clone
      dotnet new --install <path to the template>
    ``` 

## Missing Domain Events?
In this project we are not using Domain Events. If you want to use Domain Events, Most of the projects are dependent on the MediatR and this template has no dependency on any third party library in the domain layer. You can use Domain Events by adding MediatR to the project.
if you want to use Domain Events based on medatr, you can use the following code for example:
```
//create in the Primites folder
public interface IDomainEvent : INotification
{
}

//Create a Domain Events Folder
public sealed record ProductCreatedDomainEvent(Int Id) : IDomainEvent;
{
}


```
## Contributing

Contributions to the project are welcome! If you notice a bug, want to add a feature, or fix an issue, please create a pull request with your changes. Or submit a new issue.

Don't forget to add or update tests for new features and bug fixes to maintain code quality.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more information.

---
