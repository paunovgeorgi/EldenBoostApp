Application Structure

The application is organized into six layers:

    EldenBoost:
    The web layer, containing controllers, views, areas, extensions, model binders, components, and static files within the wwwroot directory.

    EldenBoost.Core:
    This layer handles the business logic of the application, including Contracts, Services, and all ViewModels.

    EldenBoost.Infrastructure:
    This layer serves as the application's foundation, containing the Data folder, which includes the Repository, Data Models, Entity Configurations, and Seed Data.

    EldenBoost.Common:
    Houses shared resources, such as constants, enumerations, and return messages, used throughout the application.

    EldenBoost.WebApi:
    A Web API project primarily responsible for Admin area functionalities, implemented through API controllers and leveraging the business logic from the .Core layer.

    EldenBoost.Tests:
    An NUnit test project used for testing services in the .Core layer.

Setup Instructions

Follow these steps to set up the application after cloning the repository:

    Connection String:
    Update the connection string in the user secrets for both the EldenBoost and EldenBoost.WebApi layers to match your database settings.

    Configure Web API:
        Edit the apiPort constant in wwwroot/js/config.js to match your Web API port.
        Update the PolicyAppURL constant in EldenBoost.Common.Constants.GeneralApplicationConstants to allow interaction between the main app and the Web API.

    Database Initialization:
    Run the update-database command to create and seed the database. The seed data includes:
        Users: ApplicationUsers, including Clients, Boosters, Authors, and an Admin.
        Services: Services, ServiceOptions, and Platforms.
        Boosters: Boosters and their associated platforms.
        Content: Articles and Reviews.
