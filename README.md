# TickerQ Playground

An educational introduction project demonstrating the capabilities of [TickerQ](https://github.com/Arcenox-co/TickerQ), a fast, reflection-free background task scheduler for .NET.

## 🎯 About This Project

This playground project serves as a hands-on introduction to TickerQ, showcasing its core features including:

- **Time-based and Cron scheduling** - Schedule jobs to run at specific times or using cron expressions
- **Entity Framework Core integration** - Persistent job storage with SQL Server
- **Live Dashboard UI** - Real-time monitoring and management of scheduled jobs
- **Retry policies** - Automatic retry mechanisms for failed jobs
- **Dependency injection support** - Seamless integration with .NET's DI container

## 🚀 Features Demonstrated

- ✅ Basic TickerQ setup and configuration
- ✅ Simple job definition with `[TickerFunction]` attribute
- ✅ Entity Framework Core operational store
- ✅ Dashboard UI with basic authentication
- ✅ SQL Server LocalDB integration
- ✅ Automatic database migrations

## 📋 Prerequisites

Before running this project, ensure you have:

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) (included with Visual Studio)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional but recommended)

## 🛠️ Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/fkucukkara/tickerQPlayground.git
cd TickerQPlayground
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Database Setup

The project uses Entity Framework Core with SQL Server LocalDB. The database will be created automatically when you first run the application.

**Connection String** (in `appsettings.json`):
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TickerQ;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Entity Framework Migrations (Optional)

While the application automatically handles database creation and migrations, you can also manage them manually using Entity Framework Core CLI commands.

#### Prerequisites for Manual Migrations

Ensure you have the EF Core tools installed globally:

```bash
dotnet tool install --global dotnet-ef
```

#### Common Migration Commands

**Add a new migration:**
```bash
dotnet ef migrations add <MigrationName> --project TickerQPlayground
```

**Update the database to the latest migration:**
```bash
dotnet ef database update --project TickerQPlayground
```

#### Example Migration Workflow

1. **Create a new migration after model changes:**
   ```bash
   dotnet ef migrations add UpdateTickerSchema --project TickerQPlayground
   ```

2. **Apply the migration to the database:**
   ```bash
   dotnet ef database update --project TickerQPlayground
   ```

### 5. Run the Application

```bash
dotnet run --project TickerQPlayground
```

The application will start on:
- HTTP: `http://localhost:5264`
- HTTPS: `https://localhost:7170`

## 🎮 Usage

### Accessing the Dashboard

Once the application is running, you can access the TickerQ Dashboard at:

```
http://localhost:5264/tickerq-dashboard
```

**Default Credentials:**
- Username: `admin`
- Password: `admin`

### Sample Job

The project includes a simple cleanup job defined in [`Jobs.cs`](TickerQPlayground/Jobs.cs):

```csharp
public class Jobs
{
    [TickerFunction(functionName: "CleanerJob")]
    public void CleanupLogs()
    {
        Console.WriteLine("Cleaner...");
    }
}
```

This job can be scheduled through the dashboard or programmatically.

### Testing the API

Use the included HTTP file [`TickerQPlayground.http`](TickerQPlayground/TickerQPlayground.http) to test the basic endpoint:

```http
GET http://localhost:5264/hello/
Accept: application/json
```

## ⚙️ Configuration

### TickerQ Configuration

The main TickerQ configuration is in [`Program.cs`](TickerQPlayground/Program.cs):

```csharp
builder.Services.AddTickerQ(options =>
{
    options.SetMaxConcurrency(4); // Optional: Set max concurrent jobs
    options.AddOperationalStore<MyDbContext>(efOpt =>
    {
        efOpt.UseModelCustomizerForMigrations();
        efOpt.CancelMissedTickersOnApplicationRestart();
    });
    options.AddDashboard(basePath: "/tickerq-dashboard");
    options.AddDashboardBasicAuth();
});
```

### Key Configuration Options

| Option | Description | Default |
|--------|-------------|---------|
| `SetMaxConcurrency(int)` | Maximum number of concurrent job executions | 4 |
| `UseModelCustomizerForMigrations()` | Enables automatic EF migrations for TickerQ tables | - |
| `CancelMissedTickersOnApplicationRestart()` | Cancels missed jobs on app restart | - |
| `AddDashboard(basePath)` | Enables dashboard UI at specified path | `/tickerq-dashboard` |
| `AddDashboardBasicAuth()` | Enables basic authentication for dashboard | - |

### Database Configuration

The project uses a custom DbContext ([`MyDbContext.cs`](TickerQPlayground/MyDbContext.cs)) that integrates with TickerQ's operational store:

```csharp
public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options) { }
    
    // TickerQ configurations are applied automatically
    // when using UseModelCustomizerForMigrations()
}
```

## 📊 Database Schema

TickerQ creates the following tables in the `ticker` schema:

- **TimeTickers** - Stores one-time scheduled jobs
- **CronTickers** - Stores recurring cron-based jobs  
- **CronTickerOccurrences** - Stores individual executions of cron jobs

## 🔧 Project Structure

```
TickerQPlayground/
├── Program.cs                 # Application entry point and TickerQ configuration
├── Jobs.cs                    # Sample job definitions
├── MyDbContext.cs            # Entity Framework DbContext
├── appsettings.json          # Application configuration
├── TickerQPlayground.http    # HTTP test requests
├── Migrations/               # Entity Framework migrations
│   ├── 20250803101422_InitialCreate.cs
│   └── ...
└── Properties/
    └── launchSettings.json   # Launch profiles
```

## 🎯 Learning Objectives

By exploring this playground project, you'll learn:

1. **Basic Setup** - How to integrate TickerQ into a .NET application
2. **Job Definition** - How to create jobs using the `[TickerFunction]` attribute
3. **Persistence** - How to configure Entity Framework Core for job storage
4. **Monitoring** - How to use the dashboard for job management
5. **Configuration** - Understanding various TickerQ configuration options

## 🚀 Next Steps

After exploring this playground, consider:

1. **Adding More Jobs** - Create additional job classes with different scheduling patterns
2. **Cron Scheduling** - Experiment with cron expressions for recurring jobs
3. **Error Handling** - Implement custom exception handlers
4. **Advanced Features** - Explore batch jobs, retry policies, and multi-node coordination
5. **Production Setup** - Learn about production deployment considerations

## 📚 Additional Resources

- [TickerQ Official Repository](https://github.com/Arcenox-co/TickerQ)
- [TickerQ Documentation](https://tickerq.arcenox.com)
- [TickerQ NuGet Packages](https://www.nuget.org/packages/TickerQ/)

### NuGet Packages Used

- `TickerQ` (v2.4.1) - Core scheduling engine
- `TickerQ.EntityFrameworkCore` (v2.4.1) - EF Core integration
- `TickerQ.Dashboard` (v2.4.1) - Web dashboard UI

## 🐛 Troubleshooting

### Common Issues

**Database Connection Issues:**
- Ensure SQL Server LocalDB is installed
- Check if the connection string matches your LocalDB instance
- Try running `sqllocaldb info` to verify LocalDB installation

**Migration Issues:**
- Delete the `Migrations` folder and run `dotnet ef migrations add InitialCreate --project TickerQPlayground`
- Ensure the database is not in use by other applications
- If you get "No migrations configuration type was found" error, make sure you're running commands from the solution root directory
- For "Unable to create an object of type 'MyDbContext'" error, ensure the connection string is properly configured in `appsettings.json`
- If migrations fail due to schema conflicts, try dropping the database first: `dotnet ef database drop --project TickerQPlayground`

**Entity Framework CLI Issues:**
- Install EF Core tools globally if commands are not recognized: `dotnet tool install --global dotnet-ef`
- Update EF Core tools to the latest version: `dotnet tool update --global dotnet-ef`
- Verify EF tools installation: `dotnet ef --version`
- If you encounter permission issues, try running the command prompt as administrator

**Dashboard Access Issues:**
- Verify the application is running on the correct port
- Check that basic auth credentials are correct (admin/admin)
- Ensure the dashboard path is `/tickerq-dashboard`

### Getting Help

If you encounter issues:

1. Check the [TickerQ Issues](https://github.com/Arcenox-co/TickerQ/issues) page
2. Review the [TickerQ Documentation](https://tickerq.arcenox.com)
3. Examine the console output for error messages

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.