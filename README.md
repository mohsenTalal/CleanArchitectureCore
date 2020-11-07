# Clean Architecture Solution Template


**Over the past two years, I’ve read the teaching programmers how to build enterprise applications interface using Clean Architecture with .NET Core. Recently, I’ve developed a many Clean Architecture Solution Template for .NET Core.
And today I will share my new designed with EAI "Enterprise Application Integration" is a Solution Template as a Clean Architecture Solution,
This is a solution template for creating an Application Programming Interface (API) and Microservices with ASP.NET Core 3.1 LTS following the principles of Clean Architecture.**


# Technologies
 * .NET Core 3.1
 * ASP .NET Core 3.1
 * Entity Framework Core 3.1
 * AutoMapper
 * FluentValidation
 * Security ,JWT Authentication(Json Web Token), and Basic authentication
 * Logger
 * OpenAPI | Swagger 3.1 ( basic Auth, baarer Auth, and OpenApiSecurityScheme)
 * Operation Filter
 * Redis Cache
 * Caching Response
 * Docker
 
# Overview 
 # Common
 This will contain all entities, enums, exceptions, interfaces, types and logic specific to the Common layer.

 # Application
This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

 # Infrastructure
 This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within     the application layer.
 
 # API
 This layer is a Maps to the layers that hold the Web, UI and Presenter concerns. In the context of our API, this means it accepts input in the form of HTTP requests over the   network (e.g., GET/POST/etc.) and returns its output as content formatted as JSON/HTML/XML, etc. The Presenters contain .NET framework-specific references and types, so they also live here as per The Dependency Rule we don't want to pass any of them inward.
 
 # Docker
 Docker is Supported on this Template

 # Getting Started

Create a new project based on this template by following 
 * Download the zip file
 * Extract the zip file to this path
C:\Users\[Your User]\Documents\Visual Studio 2019\Templates
 * Open visual studio 2019 editor and select create a new project find the EAI. 
 * To naming the project, please follow that "EAI.CompanyName."Name of the project".
  
  For Example "EAI.CompanyName.Microservices1"

> Note: **CleanArchitectureCore**.
## Support

**Need help or wanna share your thoughts?** Don't hesitate to join us on Gitter or ask your question on StackOverflow:

- **StackOverflow: [https://stackexchange.com/users/13936221/abdul-mohsen-al-enazi](https://stackexchange.com/users/13936221/abdul-mohsen-al-enazi)**

## Contributors

**CleanArchitectureCore** is actively maintained by **[Mohsen Talal](https://github.com/mohsenTalal)**. Contributions are welcome and can be submitted using pull requests.

https://twitter.com/mohsenenazi?lang=en
