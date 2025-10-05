# 📚 BookingAPI - Flight Booking & Fare Search API

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)
![Status](https://img.shields.io/badge/status-Active-success)

A professional **ASP.NET Core 9.0** RESTful API for flight booking and fare search, built with **Clean Architecture** principles. Perfect for learning modern .NET development practices.

---

## 📋 Table of Contents

- [✨ Features](#-features)
- [🏗️ Architecture](#️-architecture)
- [🚀 Quick Start](#-quick-start)
- [📦 Prerequisites](#-prerequisites)
- [⚙️ Installation](#️-installation)
- [🎯 API Endpoints](#-api-endpoints)
- [📖 Detailed Method Explanations](#-detailed-method-explanations)
- [🗂️ Project Structure](#️-project-structure)
- [🔧 Configuration](#-configuration)
- [🧪 Testing the API](#-testing-the-api)
- [📊 Logging & Monitoring](#-logging--monitoring)
- [🛠️ Development Guide](#️-development-guide)
- [📚 Learning Resources](#-learning-resources)
- [❓ FAQ](#-faq)
- [🤝 Contributing](#-contributing)
- [📄 License](#-license)

---

## ✨ Features

### Core Functionality
- 🔍 **Flight Fare Search** - Search for available flights by route, date, and cabin class
- 📊 **Analytics Dashboard** - Track search history and popular routes
- 🎯 **Cabin Class Pricing** - Dynamic pricing based on Economy, Premium Economy, Business, and First class
- 📈 **Search Statistics** - Real-time analytics on search patterns

### Technical Features
- 🏛️ **Clean Architecture** - Separation of concerns with 4 distinct layers
- 🔒 **Immutable Domain Models** - C# 9+ records for data integrity
- ✅ **FluentValidation** - Comprehensive input validation
- 🗺️ **AutoMapper** - Automatic object-to-object mapping
- 📝 **Serilog Logging** - Structured logging with correlation IDs
- 💾 **EF Core In-Memory** - Mock database for development (easily switchable to SQL Server)
- 📖 **Swagger/OpenAPI** - Interactive API documentation
- 🔄 **Correlation ID Tracking** - Request tracking across the entire pipeline

---

## 🏗️ Architecture

This project follows **Clean Architecture** (also known as Onion Architecture or Hexagonal Architecture) with clear separation of concerns.

```
┌─────────────────────────────────────────────────┐
│         Presentation Layer (API)                │
│  Controllers, Swagger, JSON Configuration       │
└────────────────┬────────────────────────────────┘
                 │ Depends on ↓
┌────────────────▼────────────────────────────────┐
│         Application Layer                       │
│  Business Logic, Services, DTOs, Validators     │
└────────────────┬────────────────────────────────┘
                 │ Depends on ↓
┌────────────────▼────────────────────────────────┐
│         Domain Layer (Core)                     │
│  Entities, Enums, Repository Interfaces         │
└─────────────────────────────────────────────────┘
                 ▲ Implemented by
┌────────────────┴────────────────────────────────┐
│         Infrastructure Layer                    │
│  Database, Repositories, External Services      │
└─────────────────────────────────────────────────┘
```

### Layer Responsibilities

#### 1. **Domain Layer** (`Domain/`)
- **Pure Business Logic** - No dependencies on frameworks
- **Entities** - Immutable records representing business objects
- **Enums** - Type-safe constants with explicit values
- **Repository Interfaces** - Contracts for data access (no implementation)

**Key Files:**
- `Entities/FlightFare.cs` - Flight fare information
- `Entities/SearchHistory.cs` - Search tracking
- `Enums/CabinClass.cs` - Cabin class types

#### 2. **Application Layer** (`Application/`)
- **Business Logic** - Use cases and services
- **Validation** - FluentValidation rules
- **DTOs** - Data Transfer Objects for API contracts
- **Mapping** - AutoMapper profiles

**Key Files:**
- `Services/FlightFareService.cs` - Core business logic
- `Validators/FlightSearchRequestValidator.cs` - Input validation
- `Mappings/FlightFareMappingProfile.cs` - Object mapping

#### 3. **Infrastructure Layer** (`Infrastructure/`)
- **Data Access** - Repository implementations
- **Database** - EF Core DbContext
- **External Services** - Third-party integrations
- **Middleware** - Cross-cutting concerns

**Key Files:**
- `Repositories/FlightFareRepository.cs` - Data access implementation
- `DataAccess/BookingDbContext.cs` - Database context
- `Middleware/CorrelationIdMiddleware.cs` - Request tracking

#### 4. **Presentation Layer** (`Presentation/`)
- **API Controllers** - HTTP endpoints
- **Swagger** - API documentation
- **JSON Configuration** - Serialization settings

**Key Files:**
- `Controllers/FlightFareController.cs` - Flight search endpoints
- `Controllers/AnalyticsController.cs` - Analytics endpoints

---

## 🚀 Quick Start

### For Complete Beginners

1. **Clone or Download** the project
2. **Open in VS Code** or Visual Studio
3. **Press F5** to run and debug
4. **Browser opens automatically** at `http://localhost:5107/swagger`
5. **Try the API** using Swagger UI (no coding required!)

### For Developers

```powershell
# Navigate to project directory
cd BookingAPI/BookingAPI

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

Access Swagger UI at: **`http://localhost:5107/swagger`**

---

## 📦 Prerequisites

### Required Software

- **.NET 9.0 SDK** - [Download here](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Visual Studio Code** or **Visual Studio 2022** - [VS Code](https://code.visualstudio.com/) | [Visual Studio](https://visualstudio.microsoft.com/)
- **C# Extension** (for VS Code) - Install from VS Code marketplace

### Optional Tools

- **Postman** - For API testing ([Download](https://www.postman.com/downloads/))
- **Git** - For version control ([Download](https://git-scm.com/downloads))

### Verify Installation

```powershell
# Check .NET version (should show 9.0.x or higher)
dotnet --version

# Check SDK installation
dotnet --list-sdks
```

---

## ⚙️ Installation

### Step 1: Install .NET 9.0 SDK

1. Go to [https://dotnet.microsoft.com/download/dotnet/9.0](https://dotnet.microsoft.com/download/dotnet/9.0)
2. Download the installer for your operating system
3. Run the installer and follow the prompts
4. Verify installation: `dotnet --version`

### Step 2: Install an IDE

**Option A: Visual Studio Code (Recommended for beginners)**
1. Download from [https://code.visualstudio.com/](https://code.visualstudio.com/)
2. Install the **C# Extension** from the Extensions marketplace
3. Install the **C# Dev Kit** (optional but helpful)

**Option B: Visual Studio 2022**
1. Download from [https://visualstudio.microsoft.com/](https://visualstudio.microsoft.com/)
2. Select **ASP.NET and web development** workload during installation

### Step 3: Clone/Download the Project

```powershell
# Using Git
git clone <repository-url>
cd SimpleApp/BookingAPI/BookingAPI

# Or download ZIP and extract, then navigate to BookingAPI/BookingAPI folder
```

### Step 4: Restore Dependencies

```powershell
# This downloads all required NuGet packages
dotnet restore
```

### Step 5: Build the Project

```powershell
# Compile the application
dotnet build
```

If successful, you'll see: ✅ `Build succeeded in X.Xs`

### Step 6: Run the Application

```powershell
# Start the API server
dotnet run
```

You should see:
```
[22:11:56 INF] Starting BookingAPI application
[22:11:56 INF] BookingAPI application started successfully on Development
```

### Step 7: Access Swagger UI

Open your browser and navigate to: **`http://localhost:5107/swagger`**

---

## 🎯 API Endpoints

### Flight Fare Search

#### **POST** `/api/FlightFare/search`
Search for available flight fares based on criteria.

**Request Body:**
```json
{
  "origin": "JFK",
  "destination": "LAX",
  "departureDate": "2025-12-01",
  "cabinClass": "Economy",
  "passengerCount": 2
}
```

**Response:**
```json
[
  {
    "flightNumber": "BA101",
    "origin": "JFK",
    "destination": "LAX",
    "baseFare": 450.00,
    "tax": 75.50,
    "totalFare": 525.50,
    "currency": "USD",
    "departureDate": "2025-12-01T10:00:00Z",
    "cabinClass": "Economy"
  }
]
```

#### **GET** `/api/FlightFare/health`
Health check for flight fare service.

**Response:**
```json
{
  "status": "healthy",
  "service": "FlightFareService"
}
```

---

### Analytics Endpoints

#### **GET** `/api/Analytics/recent-searches?limit=10`
Get recent search history.

**Parameters:**
- `limit` (optional) - Number of results to return (default: 10)

**Response:**
```json
{
  "totalCount": 10,
  "searches": [
    {
      "id": "guid",
      "route": "JFK → LAX",
      "origin": "JFK",
      "destination": "LAX",
      "departureDate": "2025-12-01",
      "cabinClass": "Economy",
      "passengerCount": 2,
      "totalFaresFound": 3,
      "searchedAt": "2025-10-05 14:30:00"
    }
  ]
}
```

#### **GET** `/api/Analytics/popular-routes?topCount=10`
Get most frequently searched routes.

**Response:**
```json
{
  "totalRoutes": 5,
  "routes": [
    {
      "route": "JFK → LAX",
      "origin": "JFK",
      "destination": "LAX",
      "searchCount": 25
    }
  ]
}
```

#### **GET** `/api/Analytics/statistics`
Get comprehensive search statistics.

**Response:**
```json
{
  "totalSearches": 150,
  "totalFaresReturned": 450,
  "averageFaresPerSearch": 3.0,
  "mostPopularCabinClass": {
    "cabinClass": "Economy",
    "count": 100
  },
  "topRoutes": [...],
  "searchesByDate": [...]
}
```

#### **GET** `/api/Analytics/health`
Health check for analytics service.

---

## 📖 Detailed Method Explanations

### Flight Fare Search Method

**File:** `Application/Services/FlightFareService.cs`

```csharp
public async Task<IEnumerable<FlightFareResponseDto>> SearchFlightFaresAsync(
    FlightSearchRequestDto request)
```

**What it does:**
1. **Receives** a search request from the controller
2. **Validates** the request automatically (FluentValidation)
3. **Maps** DTO to domain entity using AutoMapper
4. **Calls** the repository to get flight fares from the database
5. **Saves** search history for analytics
6. **Maps** domain entities back to response DTOs
7. **Returns** the results to the controller

**Step-by-Step Explanation:**

```csharp
// Step 1: Log the incoming request
_logger.LogInformation(
    "Starting flight fare search - Origin: {Origin}, Destination: {Destination}",
    request.Origin, request.Destination);

// Step 2: Convert DTO to Domain Entity using AutoMapper
// This transforms the API request into a format the domain layer understands
var criteria = _mapper.Map<FlightSearchCriteria>(request);

// Step 3: Query the database through the repository
// The repository handles all database operations
var flightFares = await _repository.SearchFlightFaresAsync(criteria);

// Step 4: Save search history for analytics (wrapped in try-catch)
// This tracks user searches without failing the main operation
var searchHistory = _mapper.Map<SearchHistory>(criteria);
searchHistory = searchHistory with
{
    TotalFaresFound = flightFares.Count(),
    CorrelationId = GetCorrelationId()
};
await _searchHistoryRepository.SaveSearchHistoryAsync(searchHistory);

// Step 5: Convert domain entities to response DTOs
// This prepares the data for the API response
var response = _mapper.Map<IEnumerable<FlightFareResponseDto>>(flightFares);

// Step 6: Return the results
return response;
```

---

### Repository Search Method

**File:** `Infrastructure/Repositories/FlightFareRepository.cs`

```csharp
public async Task<IEnumerable<FlightFare>> SearchFlightFaresAsync(
    FlightSearchCriteria criteria)
```

**What it does:**
1. **Logs** the database operation
2. **Simulates** database delay (500ms) for realistic behavior
3. **Generates** sample flight fares with pricing based on cabin class
4. **Applies** pricing multipliers:
   - Economy: 1.0x (base price)
   - Premium Economy: 1.5x
   - Business: 2.5x
   - First: 4.0x
5. **Returns** adjusted flight fares

**Pricing Logic Explained:**

```csharp
// Base fare from "database"
var baseFare = 450.00m;

// Apply cabin class multiplier using switch expression
var multiplier = criteria.CabinClass switch
{
    CabinClass.Economy => 1.0m,          // $450 x 1.0 = $450
    CabinClass.PremiumEconomy => 1.5m,   // $450 x 1.5 = $675
    CabinClass.Business => 2.5m,         // $450 x 2.5 = $1,125
    CabinClass.First => 4.0m,            // $450 x 4.0 = $1,800
    _ => 1.0m                            // Default to Economy
};

// Calculate adjusted price
var adjustedBaseFare = baseFare * multiplier;

// Create immutable record with 'with' expression
var adjustedFlightFares = flightFares.Select(fare => fare with
{
    BaseFare = fare.BaseFare * multiplier,
    Tax = fare.Tax * multiplier
}).ToList();
```

---

### Validation Process

**File:** `Application/Validators/FlightSearchRequestValidator.cs`

**What it validates:**

```csharp
// 1. Origin airport code validation
RuleFor(x => x.Origin)
    .NotEmpty()                           // Must not be empty
    .WithMessage("Origin is required")
    .Length(3)                            // Must be exactly 3 characters
    .WithMessage("Must be 3-letter code")
    .Matches("^[A-Z]{3}$")               // Must be uppercase letters only
    .WithMessage("Uppercase only");

// 2. Destination validation (same as origin)
// 3. Date validation
RuleFor(x => x.DepartureDate)
    .GreaterThanOrEqualTo(DateTime.UtcNow.Date)  // Can't be in the past
    .LessThanOrEqualTo(DateTime.UtcNow.Date.AddYears(1)); // Max 1 year ahead

// 4. Cabin class validation
RuleFor(x => x.CabinClass)
    .IsInEnum();  // Must be valid CabinClass enum value

// 5. Passenger count validation
RuleFor(x => x.PassengerCount)
    .InclusiveBetween(1, 9);  // Between 1 and 9 passengers
```

**How it works:**
- Validation runs **automatically** before the controller method executes
- If validation fails, the request is rejected with a `400 Bad Request`
- Error messages are returned in the response

---

### Correlation ID Middleware

**File:** `Infrastructure/Middleware/CorrelationIdMiddleware.cs`

**What it does:**
1. **Extracts** correlation ID from request header `X-Correlation-ID`
2. **Generates** new GUID if header is missing
3. **Adds** correlation ID to response headers
4. **Enriches** all log entries with the correlation ID
5. **Enables** request tracking across the entire application

**Usage in Logs:**
```
[22:11:56 INF] a1b2c3d4-e5f6-7890 FlightFareService Search - Origin: JFK
[22:11:56 INF] a1b2c3d4-e5f6-7890 FlightFareRepository Database search
```

The same ID (`a1b2c3d4-e5f6-7890`) appears in all logs for that request.

---

### AutoMapper Configuration

**File:** `Application/Mappings/FlightFareMappingProfile.cs`

**What it does:**
Automatically converts between objects without manual property copying.

```csharp
// Define mapping once
CreateMap<FlightSearchRequestDto, FlightSearchCriteria>();

// Use anywhere by injecting IMapper
var criteria = _mapper.Map<FlightSearchCriteria>(requestDto);

// Instead of manual mapping:
// var criteria = new FlightSearchCriteria
// {
//     Origin = requestDto.Origin,
//     Destination = requestDto.Destination,
//     // ... repeat for every property
// };
```

**Benefits:**
- ✅ Less code to write
- ✅ Fewer bugs (no typos in property names)
- ✅ Easier to maintain
- ✅ Automatically handles null values

---

## 🗂️ Project Structure

```
SimpleApp/
└── BookingAPI/
    └── BookingAPI/                      # Main project folder
        ├── Program.cs                   # Application entry point
        ├── appsettings.json            # Configuration
        ├── BookingAPI.csproj           # Project file
        │
        ├── Domain/                      # Core business layer
        │   ├── Entities/
        │   │   ├── FlightFare.cs       # Flight fare entity (immutable record)
        │   │   ├── FlightSearchCriteria.cs
        │   │   └── SearchHistory.cs
        │   ├── Enums/
        │   │   └── CabinClass.cs       # Economy, Business, First, etc.
        │   └── Interfaces/
        │       ├── IFlightFareRepository.cs
        │       └── ISearchHistoryRepository.cs
        │
        ├── Application/                 # Business logic layer
        │   ├── Services/
        │   │   └── FlightFareService.cs # Main business logic
        │   ├── Interfaces/
        │   │   └── IFlightFareService.cs
        │   ├── DTOs/                    # Data Transfer Objects
        │   │   ├── FlightSearchRequestDto.cs
        │   │   └── FlightFareResponseDto.cs
        │   ├── Validators/              # FluentValidation rules
        │   │   ├── FlightSearchRequestValidator.cs
        │   │   └── FlightFareValidator.cs
        │   ├── Mappings/                # AutoMapper profiles
        │   │   ├── FlightFareMappingProfile.cs
        │   │   └── SearchHistoryMappingProfile.cs
        │   └── DependencyInjection/
        │       └── ApplicationServiceExtensions.cs
        │
        ├── Infrastructure/              # Data access & external services
        │   ├── DataAccess/
        │   │   └── BookingDbContext.cs  # EF Core database context
        │   ├── Repositories/
        │   │   ├── FlightFareRepository.cs
        │   │   └── SearchHistoryRepository.cs
        │   ├── Middleware/
        │   │   └── CorrelationIdMiddleware.cs
        │   ├── Logging/
        │   │   └── SerilogConfiguration.cs
        │   └── DependencyInjection/
        │       └── InfrastructureServiceExtensions.cs
        │
        ├── Presentation/                # API layer
        │   ├── Controllers/
        │   │   ├── FlightFareController.cs
        │   │   └── AnalyticsController.cs
        │   └── DependencyInjection/
        │       └── PresentationServiceExtensions.cs
        │
        ├── logs/                        # Log files (auto-generated)
        │   └── booking-api-YYYYMMDD.log
        │
        └── Properties/
            └── launchSettings.json      # Development settings
```

---

## 🔧 Configuration

### appsettings.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information"
    },
    "WriteTo": [
      {
        "Name": "Console"  // Logs to console
      },
      {
        "Name": "Async",   // Logs to file asynchronously
        "Args": {
          "configure": [{
            "Name": "File",
            "Args": {
              "path": "logs/booking-api-.log",
              "rollingInterval": "Day",
              "fileSizeLimitBytes": 10485760,      // 10 MB
              "retainedFileCountLimit": 10,        // Max 10 files
              "retainedFileTimeLimit": "7.00:00:00" // 7 days
            }
          }]
        }
      }
    ]
  }
}
```

### Environment Variables

Create `appsettings.Development.json` for development-specific settings:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",  // More verbose logging in development
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

---

## 🧪 Testing the API

### Using Swagger UI (Easiest for Beginners)

1. **Start the application**: `dotnet run`
2. **Open Swagger**: `http://localhost:5107/swagger`
3. **Expand** an endpoint (e.g., `POST /api/FlightFare/search`)
4. **Click** "Try it out"
5. **Edit** the request body:
   ```json
   {
     "origin": "JFK",
     "destination": "LAX",
     "departureDate": "2025-12-01",
     "cabinClass": "Economy",
     "passengerCount": 2
   }
   ```
6. **Click** "Execute"
7. **View** the response below

### Using PowerShell (Windows)

```powershell
# Search for flights
$body = @{
    origin = "JFK"
    destination = "LAX"
    departureDate = "2025-12-01"
    cabinClass = "Economy"
    passengerCount = 2
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5107/api/FlightFare/search" `
    -Method POST `
    -Body $body `
    -ContentType "application/json"
```

### Using curl (Linux/Mac/Git Bash)

```bash
curl -X POST "http://localhost:5107/api/FlightFare/search" \
  -H "Content-Type: application/json" \
  -d '{
    "origin": "JFK",
    "destination": "LAX",
    "departureDate": "2025-12-01",
    "cabinClass": "Economy",
    "passengerCount": 2
  }'
```

### Using the included test file

The project includes `test-requests.http` file:

```http
### Search for flight fares
POST http://localhost:5107/api/FlightFare/search
Content-Type: application/json

{
  "origin": "JFK",
  "destination": "LAX",
  "departureDate": "2025-12-01",
  "cabinClass": "Economy",
  "passengerCount": 2
}

### Get analytics
GET http://localhost:5107/api/Analytics/statistics
```

**To use in VS Code:**
1. Install "REST Client" extension
2. Open `test-requests.http`
3. Click "Send Request" above each request

---

## 📊 Logging & Monitoring

### Viewing Logs

**Console Logs:**
Logs appear automatically in the terminal where you ran `dotnet run`.

**File Logs:**
```powershell
# View recent logs
Get-Content logs/booking-api-*.log -Tail 50

# Monitor logs in real-time
Get-Content logs/booking-api-*.log -Wait

# Search logs for errors
Select-String -Path logs/*.log -Pattern "Error"
```

### Log Format

**Console:**
```
[22:11:56 INF] Starting BookingAPI application
[22:11:57 INF] Search - Origin: JFK, Dest: LAX
```

**File:**
```
[2025-10-05 22:11:56.123 +07:00] [INF] a1b2c3d4 FlightFareService Search - Origin: JFK
```

### Correlation ID Tracking

Every request gets a unique correlation ID:

```
# Request 1
[22:11:56 INF] abc123 Controller received request
[22:11:56 INF] abc123 Service processing search
[22:11:56 INF] abc123 Repository querying database

# Request 2 (different ID)
[22:11:57 INF] def456 Controller received request
[22:11:57 INF] def456 Service processing search
```

You can track a single request through the entire system using its correlation ID.

---

## 🛠️ Development Guide

### Adding a New Endpoint

**Example: Add a "Get Flight by ID" endpoint**

#### Step 1: Add Repository Method

**File:** `Domain/Interfaces/IFlightFareRepository.cs`
```csharp
Task<FlightFare?> GetByIdAsync(Guid id);
```

**File:** `Infrastructure/Repositories/FlightFareRepository.cs`
```csharp
public async Task<FlightFare?> GetByIdAsync(Guid id)
{
    // Implementation here
    return await Task.FromResult<FlightFare?>(null);
}
```

#### Step 2: Add Service Method

**File:** `Application/Interfaces/IFlightFareService.cs`
```csharp
Task<FlightFareResponseDto?> GetFlightFareByIdAsync(Guid id);
```

**File:** `Application/Services/FlightFareService.cs`
```csharp
public async Task<FlightFareResponseDto?> GetFlightFareByIdAsync(Guid id)
{
    var flightFare = await _repository.GetByIdAsync(id);
    if (flightFare == null) return null;
    
    return _mapper.Map<FlightFareResponseDto>(flightFare);
}
```

#### Step 3: Add Controller Endpoint

**File:** `Presentation/Controllers/FlightFareController.cs`
```csharp
[HttpGet("{id}")]
[ProducesResponseType(typeof(FlightFareResponseDto), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<IActionResult> GetById(Guid id)
{
    var result = await _flightFareService.GetFlightFareByIdAsync(id);
    if (result == null)
        return NotFound();
    
    return Ok(result);
}
```

#### Step 4: Test
1. Run the application
2. Go to Swagger
3. Find your new endpoint: `GET /api/FlightFare/{id}`
4. Try it out!

---

### Adding a New Entity

**Example: Add a "Booking" entity**

#### Step 1: Create Domain Entity

**File:** `Domain/Entities/Booking.cs`
```csharp
namespace BookingAPI.Domain.Entities;

public record Booking
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required string PassengerName { get; init; }
    public required string FlightNumber { get; init; }
    public required DateTime BookingDate { get; init; }
    public decimal TotalPrice { get; init; }
}
```

#### Step 2: Create DTOs

**File:** `Application/DTOs/BookingRequestDto.cs`
```csharp
public record BookingRequestDto
{
    public required string PassengerName { get; init; }
    public required string FlightNumber { get; init; }
}
```

#### Step 3: Create Validator

**File:** `Application/Validators/BookingRequestValidator.cs`
```csharp
public class BookingRequestValidator : AbstractValidator<BookingRequestDto>
{
    public BookingRequestValidator()
    {
        RuleFor(x => x.PassengerName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.FlightNumber)
            .NotEmpty()
            .Matches("^[A-Z]{2}\\d{1,4}$");
    }
}
```

#### Step 4: Create AutoMapper Profile

**File:** `Application/Mappings/BookingMappingProfile.cs`
```csharp
public class BookingMappingProfile : Profile
{
    public BookingMappingProfile()
    {
        CreateMap<BookingRequestDto, Booking>();
    }
}
```

#### Step 5: Add to DbContext

**File:** `Infrastructure/DataAccess/BookingDbContext.cs`
```csharp
public DbSet<Booking> Bookings { get; set; }
```

#### Step 6: Create Repository Interface & Implementation

Follow the same pattern as `FlightFareRepository`.

---

### Debugging Tips

#### Enable Detailed Errors

**File:** `Infrastructure/DependencyInjection/InfrastructureServiceExtensions.cs`
```csharp
services.AddDbContext<BookingDbContext>(options =>
    options.UseInMemoryDatabase("BookingApiDb")
           .EnableSensitiveDataLogging()  // Shows parameter values
           .EnableDetailedErrors());       // Shows detailed error messages
```

#### Set Breakpoints in VS Code

1. Click in the left margin next to a line number (red dot appears)
2. Press F5 to start debugging
3. Make an API request
4. Execution pauses at your breakpoint
5. Inspect variables in the Debug panel

#### Common Issues

**Issue:** "Port 5107 is already in use"
```powershell
# Find and kill the process
Get-Process -Name "BookingAPI" | Stop-Process
```

**Issue:** "Build failed"
```powershell
# Clean and rebuild
dotnet clean
dotnet build
```

**Issue:** "Validation errors"
- Check Swagger UI response for detailed validation messages
- Verify your request matches the DTO requirements

---

## 📚 Learning Resources

### C# & .NET Fundamentals
- [Microsoft Learn - C# Fundamentals](https://docs.microsoft.com/en-us/learn/paths/csharp-first-steps/)
- [.NET 9.0 Documentation](https://docs.microsoft.com/en-us/dotnet/core/)
- [ASP.NET Core Tutorial](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api)

### Clean Architecture
- [Clean Architecture by Robert C. Martin](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Microsoft Clean Architecture Guide](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)

### Technologies Used
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [FluentValidation](https://docs.fluentvalidation.net/)
- [AutoMapper](https://automapper.org/)
- [Serilog](https://serilog.net/)

### API Design
- [REST API Tutorial](https://restfulapi.net/)
- [Swagger/OpenAPI Specification](https://swagger.io/specification/)

---

## ❓ FAQ

### How do I switch from In-Memory to SQL Server?

**Step 1:** Install SQL Server package
```powershell
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

**Step 2:** Update connection string in `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=BookingDB;Trusted_Connection=True;"
  }
}
```

**Step 3:** Update `InfrastructureServiceExtensions.cs`
```csharp
services.AddDbContext<BookingDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection")
    )
);
```

**Step 4:** Create database migration
```powershell
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

### Why use immutable records instead of classes?

**Immutable records** (`record` with `init`) provide several benefits:

1. **Thread Safety** - Can't be modified, so safe to share across threads
2. **Predictability** - Data can't change unexpectedly
3. **Value Equality** - Two records with same values are considered equal
4. **Less Bugs** - Can't accidentally modify data

**Example:**
```csharp
// Mutable class (BAD)
public class FlightFare
{
    public decimal Price { get; set; }  // Can be changed anywhere!
}

var fare = new FlightFare { Price = 100 };
SomeMethod(fare);  // ⚠️ SomeMethod might change the price!

// Immutable record (GOOD)
public record FlightFare
{
    public required decimal Price { get; init; }  // Can only be set during creation
}

var fare = new FlightFare { Price = 100 };
SomeMethod(fare);  // ✅ Price cannot be changed
```

---

### What is Dependency Injection?

**Dependency Injection (DI)** is a way to provide dependencies to a class instead of creating them inside the class.

**Without DI (BAD):**
```csharp
public class FlightFareController
{
    private readonly FlightFareService _service;
    
    public FlightFareController()
    {
        _service = new FlightFareService();  // ❌ Hard-coded dependency
    }
}
```

**With DI (GOOD):**
```csharp
public class FlightFareController
{
    private readonly IFlightFareService _service;
    
    public FlightFareController(IFlightFareService service)  // ✅ Injected
    {
        _service = service;
    }
}
```

**Benefits:**
- ✅ Easy to test (inject mock service)
- ✅ Easy to swap implementations
- ✅ Loose coupling between classes

---

### How does validation work automatically?

FluentValidation integrates with ASP.NET Core's model validation:

1. **Request comes in** → ASP.NET Core deserializes JSON to DTO
2. **Before controller method** → FluentValidation runs automatically
3. **If validation fails** → Returns `400 Bad Request` with error details
4. **If validation passes** → Controller method executes

**You don't need to call validation manually!**

```csharp
// This is automatic - no code needed in controller!
[HttpPost("search")]
public async Task<IActionResult> SearchFlightFares(
    [FromBody] FlightSearchRequestDto request)  // ← Validated automatically
{
    // If we reach here, validation passed!
}
```

---

### What is AutoMapper and why use it?

**AutoMapper** automatically copies data between objects.

**Without AutoMapper:**
```csharp
// Manual mapping (tedious and error-prone)
var response = new FlightFareResponseDto
{
    FlightNumber = flightFare.FlightNumber,
    Origin = flightFare.Origin,
    Destination = flightFare.Destination,
    BaseFare = flightFare.BaseFare,
    Tax = flightFare.Tax,
    TotalFare = flightFare.TotalFare,
    Currency = flightFare.Currency,
    DepartureDate = flightFare.DepartureDate,
    CabinClass = flightFare.CabinClass
    // ... repeat for every property
};
```

**With AutoMapper:**
```csharp
// One line! AutoMapper copies all matching properties
var response = _mapper.Map<FlightFareResponseDto>(flightFare);
```

---

### How do I add authentication?

Authentication will be added in a future version. The architecture is ready for it:

```csharp
// In Program.cs (future)
app.UseAuthentication();  // ← Add before UseAuthorization()
app.UseAuthorization();

// In controller (future)
[Authorize]  // ← Require authentication
[HttpPost("search")]
public async Task<IActionResult> SearchFlightFares(...)
```

---

## 🤝 Contributing

We welcome contributions! Here's how to get started:

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/YourFeature`
3. **Make** your changes
4. **Test** thoroughly
5. **Commit**: `git commit -m "Add YourFeature"`
6. **Push**: `git push origin feature/YourFeature`
7. **Open** a Pull Request

### Code Style Guidelines

- Use **C# naming conventions** (PascalCase for classes, camelCase for variables)
- Add **XML documentation** for public methods
- Follow **Clean Architecture** layer separation
- Write **descriptive commit messages**
- Keep **methods small** and focused (Single Responsibility)

---

## 📄 License

This project is licensed under the MIT License.

```
MIT License

Copyright (c) 2025 BookingAPI

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
```

---

## 🎓 Next Steps for Learning

### Beginner Path
1. ✅ Run the project and explore Swagger UI
2. ✅ Make API requests and observe the responses
3. ✅ Read through `Program.cs` to understand startup
4. ✅ Explore one controller to see how endpoints work
5. ✅ Try modifying validation rules and see what happens

### Intermediate Path
1. Add a new endpoint following the guide
2. Create a new entity with full CRUD operations
3. Modify the pricing logic in the repository
4. Add custom AutoMapper mappings
5. Switch from In-Memory to SQL Server database

### Advanced Path
1. Implement authentication and authorization
2. Add unit and integration tests
3. Implement CQRS pattern with MediatR
4. Add caching with Redis
5. Deploy to Azure/AWS

---

## 📞 Support

- **Documentation**: Check this README and code comments
- **Issues**: Open an issue on GitHub
- **Questions**: Use GitHub Discussions

---

**Happy Coding! 🚀**

Built with ❤️ using .NET 9.0 and Clean Architecture principles.
