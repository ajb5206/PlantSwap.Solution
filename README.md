# Plant Swap

#### _A web-based application allowing registered users to trade plants by posting offers and requests as well as see matching offers and requests from other traders. Entity is used to manage a many-to-many relationship in an SQL database and Identity is used to manage user Authentication._

#### By **Alex Bertotto, Shane Graff, Tim Roth and Jessica R. Williams**

## Table of Contents

1. [Technologies Used](#technologies)
2. [Description](#description)
3. [Setup/Installation Requirements](#setup)
4. [Future Stretch Goals](#goals)
5. [Known Bugs](#bugs)
6. [License](#license)
7. [Contact Information](#contact)

## Technologies Used <a id="technologies"></a>

* _C#_
* _.NET 5.0.100_
* _ASP.NET Core MVC_
* _dotnet.ef 5.0.2_
* _Microsoft.AspNetCore.Identity.EntityFrameworkCore 5.0.0_
* _Microsoft.EntityFrameworkCore 5.0.0_
* _Microsoft.EntityFrameworkCore.Design 5.0.0_
* _Microsoft.NET.Sdk.Web_
* _Microsoft.NET.Test.Sdk 15.0.0_
* _Pomelo.EntityFrameworkCore.MySql 5.0.0-alpha.2_

## Description <a id="description"></a>

The web-base Plant Swap application allows registered Users to post offers of and requests for plants to trade as well as see matching offers and requests from other Users. Users first create an account, log in and then create a Trader profile. Once logged in, Users may view the plant database and as well as other Traders profiles. Upon creating their own Trader profile, Users may list their own offers and requests as well as exploring their matches and adding plants to the database. Upon finding compatible offers or requests, the User contacts the other trader off the app.

Data is stored in a SQL database and users are authenticated with Identity.

## Setup/Installation Requirements <a id="setup"></a>

### Install C#, .NET, MySQL Community Server, MySQL Workbench and dotnet-ef
* Open the terminal on your local flavor
* If [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) and [.NET](https://docs.microsoft.com/en-us/dotnet/) are not installed on your local device, follow the instructions [here](https://www.learnhowtoprogram.com/c-and-net-part-time-c-and-react-track/getting-started-with-c/installing-c-and-net)
* If [MySQL Community Server](https://dev.mysql.com/downloads/mysql/) and [MySQL Workbench](https://www.mysql.com/products/workbench/) are not installed on your local device, follow the instructions [here](https://www.learnhowtoprogram.com/c-and-net-part-time-c-and-react-track/getting-started-with-c/installing-and-configuring-mysql)
* If [dotnet-ef](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) is not installed on your local device, install it with the terminal command `dotnet tool install --global dotnet-ef --version 5.0.2`

### Clone the project
* Navigate to the directory inside of which you wish to house this project
* Clone this project with the command `$ git clone https://github.com/ajb5206/PlantSwap.Solution`

### Scaffold and connect the database
* Launch the MySQL server with the command `mysql -uroot -p[YOUR-PASSWORD-HERE]`
* In your terminal, navigate to the production project directory with the command `$ cd PlantSwap.Solution/PlantSwap`
* In your terminal, create a file within the project in which to store your connection string for connecting the project to the database with the command `touch appsettings.json`
* In your text editor add the following code to the newly created appsettings.json file. *Note that uid and pwd may vary depending on your local MySql configurations.
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=plant_swap;uid=root;pwd=[YOUR-PASSWORD-HERE];"
  }
}
```
* Recreate project environment and install required dependencies with terminal command `$ dotnet restore`
* Scaffold the database with the command `$ dotnet ef database update`

### Run the project
* Run the program in the console with the command `$ dotnet run`

## Future Stretch Goals <a id="goals"></a>

* Add user authorization!

* Figure out how to link an IdentityUser and a Trader such that the app automatically grabs logged in IdentityUser’s UserName and assign it to the TraderHandle as well as limits each IdentityUser to being able to create a single Trader. A further stretch in this vein would be to automatically create a trader when an IdentityUser is created and then prompt the IdentityUser to fill in trader details upon first login.

* The user is able to not just list, but actually limit how far from their zip code they're willing to travel to complete the trade. Plant Swap only match users who have compatible plants (via a database search) and are geographically appropriate (via Google Maps Distance Matrix API and then a database search).

* Allow the users to add images

* Add a search/match View and route

## Known Bugs <a id="bugs"></a>
* No known bugs

## License <a id="license"></a>
*[MIT](https://choosealicense.com/licenses/mit/)*

Copyright (c) **2021 Alex Bertotto, Shane Graff, Tim Roth and Jessica R. Williams**

## Contact Information <a id="contact"></a>
**[Alex Bertotto](ajb5206@gmail.com), [Shane Graff](copellius@gmail.com), [Tim Roth](timdroth@gmail.com) and [Jessica R. Williams](mailto:jessicarubinwilliams@gmail.com)**