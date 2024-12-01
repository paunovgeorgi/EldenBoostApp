EldenBoost Application
======================

This application was developed as a graduate project for SoftUni's C# Web Development career program.

About EldenBoostApp
-------------------

The **EldenBoostApp** is an Elden Ring game boosting application. Before diving into the technical details, let's first understand what a game boosting service is.

A game boosting service (in this case for Elden Ring) allows a client to pay a professional player (a booster) to log in to their account and achieve in-game goals that the client cannot or chooses not to achieve themselves.

These services vary widely depending on the game but often include:

-   Completing specific quests requiring advanced skills or additional party members.
-   Leveling up characters to save time for clients who prefer to enjoy the game during weekends but lack the time to grind for levels or items during the week.

While Elden Ring serves as the focus for this application, the underlying logic and architecture can be adapted to other games.

We will explore the app's functionalities in more detail in the following sections.

* * * * *

Technologies Used
-----------------

-   **Languages & Frameworks:**\
    C#, ASP.NET MVC, Entity Framework Core, MSSQL, JavaScript, SignalR, HTML, CSS, Bootstrap
-   **Additional Tools:**\
    SweetAlert2, Toastr, TinyMCE API
-   **Browser Compatibility:**\
    Mainly developed and tested on Mozilla Firefox.

* * * * *

Application Structure
---------------------

The application is organized into six layers:

1.  **EldenBoost:**\
    The web layer, containing controllers, views, areas, extensions, model binders, components, and static files within the `wwwroot` directory.

2.  **EldenBoost.Core:**\
    This layer handles the business logic of the application, including Contracts, Services, and all ViewModels.

3.  **EldenBoost.Infrastructure:**\
    This layer serves as the application's foundation, containing the Data folder, which includes the Repository, Data Models, Entity Configurations, and Seed Data.

4.  **EldenBoost.Common:**\
    Houses shared resources, such as constants, enumerations, and return messages, used throughout the application.

5.  **EldenBoost.WebApi:**\
    A Web API project primarily responsible for Admin area functionalities, implemented through API controllers and leveraging the business logic from the `.Core` layer.

6.  **EldenBoost.Tests:**\
    An NUnit test project used for testing services in the `.Core` layer.

* * * * *

Setup Instructions
------------------

Follow these steps to set up the application after cloning the repository:

1.  **Connection String:**\
    Update the connection string in the user secrets for both the `EldenBoost` and `EldenBoost.WebApi` layers to match your database settings.

2.  **Configure Web API:**

    -   Edit the `apiPort` constant in `wwwroot/js/config.js` to match your Web API port.
    -   Update the `PolicyAppURL` constant in `EldenBoost.Common.Constants.GeneralApplicationConstants` to allow interaction between the main app and the Web API.
3.  **Database Initialization:**\
    Run the `update-database` command to create and seed the database. The seed data includes:

    -   **Users:** ApplicationUsers, including Clients, Boosters, Authors, and an Admin.
    -   **Services:** Services, ServiceOptions, and Platforms.
    -   **Boosters:** Boosters and their associated platforms.
    -   **Content:** Articles and Reviews.
