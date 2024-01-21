# ASP.NET Core 8.0 Web API Project

## `Overview`
This project is a .NET Core 8.0 Web API application that demonstrates a simple backend service. It uses PostgreSQL for the main database and Entity Framework Core for ORM. The application also includes an example of using in-memory databases for unit testing.

## `Features`
**1. .NET Core 8.0:** Utilizes the latest .NET Core framework.

**2. PostgreSQL Database:** Implements PostgreSQL as the database solution.

**3. Entity Framework Core:** Uses EF Core for database operations.

**4. In-Memory Database for Testing:** Includes examples of unit tests using an in-memory database.

**5. OMDb API Integration:** Demonstrates external API calls to the OMDb API.

**6. Dependency Injection:** Showcases the use of dependency injection in .NET Core.

**8. Configurable Settings:** Uses appsettings.json for configuration, demonstrating best practices for ASP.NET Core applications.


## `Prerequisites`
[1. .NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

[2. PostgreSQL](https://www.postgresql.org/download/)

3. [Visual Studio](https://visualstudio.microsoft.com/downloads/) (recommended for Windows users or use **CLI**) or [Visual Studio Code](https://code.visualstudio.com/) (for Windows, Linux, and macOS)


## `Instructions on usage`
1. clone the project on [Github](https://github.com/proffnick/dotnet-movie-search.git) 
2. Obtaine your API Key at [omdbapi](http://www.omdbapi.com/)
3. Open the **appsettings.json** in your editor and add your apikey under the ```"OmdbApiSettings": {...}``` 
4. Open the **appsettings.json** in your editor and update your PostgreSQL connecting string under the ```"ConnectionStrings": {...}``` to march your PostGreSQL username, password, and DB

5. Open the **backend.csproj** file and check under **ItemGroup** for all the dependences or run ```dotnet list package``` to see all packages. 

## Getting Started
### `Clone the Repository as above`
To get started with this project, first clone the repository to your local machine:

```git clone [https://github.com/proffnick/dotnet-movie-search.git](https://github.com/proffnick/dotnet-movie-search.git) \ cd dotnet-movie-search```

### `Setup the Database`
Ensure you have PostgreSQL installed and running. Create a new database and update the connection string in `appsettings.json.`

### `Install Dependencies`
Navigate to the project directory and install the necessary packages: prolly ```cd dotnet-movie-search```

run ```dotnet restore```

You can also view the list of packages used in the project by checking the .csproj file or by running:

```dotnet list package```

### `Running the Application`
Run the application using the following command:
```dotnet run```

The API should now be up and running on **http://localhost:5095.**

### `Sample Search by ID`
localhost:5095/api/movies/search?id=tt2334879

### `Sample Search by Title`
localhost:5095/api/movies/search?title=Rush+Hour

### `Sample Get all recent search queries`
localhost:5095/api/movies/recent-searches

### `Running the Tests`
Execute the following command to run the unit tests:
```dotnet test```

## `Contributing`
Contributions to this project are welcome. Please ensure to follow the coding standards and guidelines.

## `License`
This project is licensed under the [MIT License](https://opensource.org/license/mit/).



