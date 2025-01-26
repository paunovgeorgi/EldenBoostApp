![EldenBoost Logo](EldenBoost/wwwroot/images/hero2.jpg "EldenBoost Logo")
EldenBoost (Elden Ring Boosting Application)
======================

This application was developed as a graduate project for SoftUni's C# Web Development career program.

The application is built using C# on ASP.NET Core 8, with Entity Framework Core (EFCore) as the ORM and Microsoft SQL Server (MSSQL) as the database server. It features custom CSS and JavaScript for styling and interactivity.

To see a preview of the application in action, click on **SHOW MEDIA** located at the bottom of each type of the **User Types** section:  
[Anonymous User](#anonymous-user) | [Client](#client) | [Booster](#booster) | [Admin](#admin)

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
    Mainly developed and tested on Mozilla Firefox | 1920x1080 screen resolution.

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

    -   **Users:**\
        The database includes seeded `ApplicationUsers` for different roles:

        -   **Clients:** Seeded users with emails ending in `@client.com`.
        -   **Boosters:** Seeded users with emails ending in `@booster.com`.
        -   **Authors:** Seeded users with emails ending in `@author.com`.
        -   **Admin:** A dedicated admin user with the email `admin@admin.com`. This user is created specifically to manage the admin functionalities of the application.
        -   **Note:** The @client.com email suffixes indicate the intended roles of the users; however, users remain standard users by default. Unless you make a purchase they can still apply for Booster or Author position.
    -   **Services:**\
        Includes a variety of services, service options (for option-based services), and platforms (PC, PlayStation, Xbox).

    -   **Boosters:**\
        Seeded boosters include their respective platform associations (e.g., PC-only or multi-platform).

    -   **Content:**\
        Seeded articles and reviews showcase the application's features and serve as initial content.
  
USER TYPES AND APP FUNCTIONALITIES
==================================

USERS
-----

### Anonymous User

Anonymous users can access the following pages:

-   **Home Page:**\
    The default page of the application. It serves as an introduction to the website, showcasing what the site offers, customer reviews, the latest guides/news, and featured services. Note: Leaving a review is only possible for registered users (specifically clients who have purchased at least one service).

-   **Popular Services Page:**\
    Displays the 8 most-purchased services in a visually appealing diamond grid layout.

-   **All Services Page:**\
    Allows users to browse all available services. Users can:

    -   Customize the number of services displayed per page.
    -   Sort services by different criteria (e.g., price, date).
    -   Use a search field to find specific services.\
        By clicking the **Details** button, users can view detailed information about a service.
    - Choose from additional options like **Stream**, **Express**, and **Select Platform**. Depending on the service type, the page may also include a slider for level selection or a dropdown menu for choosing specific options. (Add to Cart works only for registered users).   
-   **All Articles Page:**\
    Users can browse and sort all articles by type (e.g., News, Guides) or use a search field to find articles. Clicking the **Read** button opens the selected article.

-   **Boosters Page:**\
    Lists all boosters, displaying their information and ratings.

-   **Themes:**\
    Users can choose from a variety of themes to customize their UX preferences.

<details>
<summary>$${\color{yellow}*SHOW MEDIA*}$$</summary>


#### $${\color{lightblue}Media \space for \space Anonymous \space Registered\space User}$$

**Home Page**
   
   ![Home Page](https://github.com/user-attachments/assets/e7da0eda-5077-404e-9d1f-e08c161affb9)

**Popular Services**
   
   ![Popular Services](https://github.com/user-attachments/assets/02009bec-a215-461b-a915-d2aa92c84f95)

**All Services**
   
   ![All Services](https://github.com/user-attachments/assets/73a5069b-a3b2-4e12-a0e5-fa5b041607aa)

**Articles**
   
   ![Articles](https://github.com/user-attachments/assets/040d366d-5661-4b15-b1f6-bf1b5d1e086c)

**Boosters**
   
   ![Boosters](https://github.com/user-attachments/assets/9544771a-0f83-4356-9832-cf116599849f)

**Register | Login**
   
   ![Register - Login](https://github.com/user-attachments/assets/c5dade74-153a-423a-ad6a-ec1a99c63fd7)

**Themes**
   
   ![Themes](https://github.com/user-attachments/assets/9660797f-ab86-4ebf-95c5-3c72ed995976)


**Service Types: Standard | Option | Slider**
   
   ![Service Types](https://github.com/user-attachments/assets/25f01b5a-5593-4485-9e97-f962624ddef3)

</details>

* * * * *

### Registered User

Registered users inherit all the capabilities of anonymous users. By default, registered users are treated as potential **clients** but may also apply for positions (Booster/Author). A registered user gains additional functionalities and restrictions once they make a purchase or their application is approved.

-   **My Profile Page:**\
    Registered users can change their profile picture here. After becoming a client, booster, or author, the profile page adapts to reflect their new status.

-   **Manage Account Page:**\
    Registered users can update their account details.

* * * * *

### CLIENT

Once a registered user purchases a service, they are treated as a **Client**. Clients can no longer apply for booster/author positions (and vice versa).

#### Client Functionalities:

1.  **Cart System:**

    -   Clients can add services to their cart, where each service becomes a **CartItem**.
    -   They can remove individual cart items or clear the entire cart (with confirmation prompts).
    -   Clicking the **Checkout** button creates a separate **Order** for each cart item.
        -   **Why separate orders?**\
            Each purchased service may be handled by a different booster. Additionally, each order includes a unique order chat between the client and assigned booster.
2.  **My Profile Page:**

    -   Displays **profile stats** such as the total number of orders, total spending, and account level progression.
    -   Includes a **Loyal Customer Program** visualization, showing progress toward the next level. This program rewards clients with badges and perks at spending thresholds (future perks include discounts and store credit).
3.  **Order Management:**\
    Orders are categorized into **Pending**, **Active**, and **Completed**:

    -   **Pending Orders:**\
        Show basic service details (platform, status, paid amount, additional options).
    -   **Active Orders:**\
        Display assigned booster details and include an **Order Chat** button for secure communication. Communication rules are enforced via on-site monitoring, preventing external communication (e.g., Discord/Skype).
    -   **Completed Orders:**\
        The order chat is disabled, but clients can now rate their booster via a **Rate** button. Booster ratings are displayed on the **Boosters Page**.

<details>
<summary>$${\color{yellow}*SHOW MEDIA*}$$</summary>

#### $${\color{lightblue}Media \space for \space Client \space User}$$

**Cart System (Remove Item | Clear Cart)**
   
   ![Cart System](https://github.com/user-attachments/assets/ded72620-5736-4baf-839c-5a8bdd8b5973)

**Cart System (Add to cart | Checkout)**
   
   ![Checkout](https://github.com/user-attachments/assets/2fd2d66f-22b8-489d-b6e8-9b2f7803ad63)

**Client Profile (Update Picture | Rate Booster)**
   
   ![Client Profile](https://github.com/user-attachments/assets/1606b0e6-5068-4995-9b62-81aa3d08c407)

**Client Profile (Order Chat)**
   
   ![Order Chat](https://github.com/user-attachments/assets/5ec4cf83-89c8-41a8-8b7c-46a478109ee8)

</details>

* * * * *

### BOOSTER

To become a booster, a registered user must submit an application detailing their experience, availability, and platforms they boost on.

#### Booster Functionalities:

1.  **Pending Orders Page:**

    -   Boosters see pending orders for the platforms they boost on (e.g., PC, PlayStation).
    -   Orders include details such as service type, booster pay, and client username.
    -   Boosters can take orders only if:
        -   They have no ongoing orders.
        -   Any ongoing orders are for the same client.
2.  **My Profile Page:**

    -   Displays **profile stats** such as completed orders, earnings, and payout requests.
    -   Boosters can request payment if they:
        -   Have no pending payment requests.
        -   Have completed orders not yet included in a previous payment.
3.  **Order Management:**

    -   **Active Orders:** Include an **Order Chat** button for client communication and a **Complete Order** button.
    -   **Completed Orders:** The booster can no longer modify these orders.

* * * * *

### AUTHOR

To become an author, a registered user must submit an application.

#### Author Functionalities:

1.  **Create Articles:**

    -   Authors can create articles using the **TinyMCE API**. Articles can be categorized as News or Guides.
2.  **Edit Articles:**

    -   Authors can edit their published articles.
3.  **My Profile Page:**

    -   Displays **profile stats** such as the number of articles written and their categories.

* * * * *

### ADMIN

The admin has access to a **Dashboard** with real-time updates (every 30 seconds) and the ability to manage all critical entities.

#### Admin Functionalities:

1.  **Orders Management:**

    -   Archive completed orders.
    -   Access order chats for ongoing/completed orders.
2.  **Services Management:**

    -   View, edit, or deactivate services.
    -   Switch between active and deactivated services.
3.  **Users Management:**

    -   View all registered users, including their roles and online/offline status.
    -   Promote/demote boosters and authors. Demoted users lose privileges immediately.
4.  **Articles Management:**

    -   View and edit all articles.
5.  **Payments Management:**

    -   View pending and paid payments.
    -   Access details for each order included in payments.
6.  **Applications Management:**

    -   Approve or decline booster/author applications.
7.  **Create Services:**\
    Admins can create 3 service types:

    -   **Basic Service:** A simple service requiring a title, description, image, and price.
    -   **Slider Service:** A service with a selectable range (e.g., coaching hours, item collection). Requires a max purchasable amount.
    -   **Option Service:** A service offering multiple options (e.g., boss kills, dungeon runs). Each option includes a name and price.

* * * * *

CONCLUSION
----------

While this README cannot cover every detail of the application, it provides a clear overview of its structure, features, and functionalities. This should serve as a comprehensive guide for understanding the purpose and capabilities of the application.
