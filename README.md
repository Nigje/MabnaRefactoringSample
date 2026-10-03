# Mabna Refactoring Sample

A small ASP.NET MVC application I developed in 2015 as part of a technical interview. The exercise was to refactor a controller action that looked up a user, sent an email, and recorded the operation in a log file.

I keep this repository as part of my early development portfolio: a snapshot of how I approached C#, application structure, and separation of responsibilities at that stage of my career. It preserves the original implementation and its design decisions, including the areas I would approach differently today.

## The exercise

The starting implementation combined request handling, inline SQL, email handling, and file logging in a single controller action. You can find that version in [OriginalCode/EMailController.cs](OriginalCode/EMailController.cs).

My implementation moves those responsibilities into an MVC application, a component library, and a SQL Server data access library. The sample explores:

- Separating controller logic from application behavior and persistence.
- Modeling and validating request parameters.
- Looking up users through a repository abstraction and parameterized stored procedures.
- Delegating logging through an interface.
- Calling the application workflow from an asynchronous controller action.
- Checking the successful workflow with an MSTest controller test.

## How it works

1. A request to `/EMail/Index` supplies an `id` and a `UserName`.
2. `EMailController` checks model validation and calls `EmailManager.SendEmailByIdAndUserName`.
3. The repository loads its configured SQL Server provider. When that provider is initialized, it runs the embedded database setup scripts.
4. The `GetUserByIdAndUserName` stored procedure retrieves a user whose ID and username both match.
5. `EmailManager` passes the user's email address to the sample send method, which records the operation through `LogManager` and `EmailLoger`.
6. The controller returns a view with a `Successful` or `Failed` model value for valid requests.

Email delivery is a placeholder: the sample writes a log entry but does not connect to an SMTP server or deliver a real message. The email view is also minimal and does not render the controller's result message; inspect the log file or controller result to observe the outcome.

## Repository structure

| Location | Purpose |
| --- | --- |
| [MabnaRefactoringSample/](MabnaRefactoringSample/) | ASP.NET MVC application, controllers, input model, views, and configuration. |
| [Component/](Component/) | User model, application managers, repository abstraction, and logging interface and implementation. |
| [DataAccess/](DataAccess/) | SQL Server repository, ADO.NET helpers, object mapping, and embedded database scripts. |
| [MabnaRefactoringSample.Tests/](MabnaRefactoringSample.Tests/) | MSTest test that calls the email controller and checks its successful result. |
| [OriginalCode/](OriginalCode/) | The original controller supplied for the refactoring exercise. |

## Technology

C#, .NET Framework 4.5, ASP.NET MVC 5.2.3, SQL Server Express, ADO.NET, Razor, and MSTest. Dependencies are managed through NuGet `packages.config` files. The solution was saved with Visual Studio 2015.

## Running locally

Use a Windows development environment with Visual Studio and the ASP.NET web tooling and .NET Framework 4.5 targeting support required by this legacy solution. SQL Server Express is the configured database provider.

**Use a disposable database instance.** The embedded setup script drops an existing database named `MabnaRefactoringSample` and recreates it when the SQL repository is initialized. This also applies when running the test in a new process.

1. Open `MabnaRefactoringSample.sln` in Visual Studio and restore its NuGet packages.
2. In [MabnaRefactoringSample/Web.config](MabnaRefactoringSample/Web.config), check the `MabnaDataBase` connection string. It defaults to `.\sqlexpress` with Windows authentication. The account running the application needs permission to create and drop the sample database.
3. Set `EmailLogPath` to a file location writable by the application account. The default points to `C:\EmailLog.txt`; choose a suitable local path and ensure its parent directory exists.
4. Set `MabnaRefactoringSample` as the startup project, build the solution, and run it with IIS Express.
5. Visit the following path on the application's local address:

   ```text
   /EMail/Index?id=1&UserName=user_1
   ```

The setup scripts seed four sample users, including `user_1` with ID `1`. A successful request appends an entry to the configured email log file.

## Test

The included MSTest test calls `EMailController.Index` with the seeded `user_1` and checks that the returned view model is `Successful`.

It exercises the database and file logging rather than using mocked dependencies. Before running it through Visual Studio Test Explorer, review the database connection and log path in [MabnaRefactoringSample.Tests/App.config](MabnaRefactoringSample.Tests/App.config). The same database recreation behavior applies.

## Portfolio context

This project documents an early step in my development as a software engineer. It shows my work on turning a single action with several responsibilities into a more structured application, while keeping the original exercise available for comparison.

The code and dependencies are retained as a historical sample. Today, I would revisit dependency injection, asynchronous database access, error handling, non-destructive database migrations, and isolated tests. The repository remains useful as a record of that earlier work and the progression of my engineering approach.
