## __Hotel Booking App Backend__
### __Introduction__
This repository contains the backend part of the Hotel Booking App, developed as the final assignment for the Coding Factory course. The backend is built using C# and .NET MVC, providing a RESTful API for managing hotel data and bookings.

### __Features__
User Authentication and Authorization
CRUD operations for hotels and bookings
Search and filter functionalities
RESTful API endpoints
Logging and error handling
Technologies Used
C#
.NET MVC
Entity Framework Core
SQL Server
JWT for authentication

### __Installation__
To run the project locally, follow these steps:

#### Clone the repository
git clone https://github.com/mariatemp/hotel-booking-app-backend.git
cd hotel-booking-app-backend

#### Install dependencies
dotnet restore

#### Set up environment variables Configure the appsettings.json file with your database connection strings and other settings. You can use appsettings.Development.json for development settings.

#### Run database migrations
dotnet ef database update

#### Run the application
dotnet run

#### Access the API
The API will be available at http://localhost:5139/api.

### __Project Structure__

HotelBookingApp/
├── Controllers/
├── Data/
├── DTO/
├── Exceptions/
├── Models/
├── Repositories/
├── Services/
├── Util/
├── Views/
├── wwwroot/
├── appsettings.json
├── Program.cs
├── HotelBookingApp.csproj
└── README.md

### ___API Endpoints__
GET /api/hotels - Retrieve all hotels
GET /api/hotels/{id} - Retrieve a hotel by ID
POST /api/hotels - Create a new hotel
PUT /api/hotels/{id} - Update an existing hotel
DELETE /api/hotels/{id} - Delete a hotel by ID
GET /api/bookings - Retrieve all bookings
GET /api/bookings/{id} - Retrieve a booking by ID
POST /api/bookings - Create a new booking
PUT /api/bookings/{id} - Update an existing booking
DELETE /api/bookings/{id} - Delete a booking by ID

### __Future Improvements__
Implement unit and integration tests
Add more endpoints for additional functionalities
Improve error handling and validation

### __License__
This project is licensed under the MIT License - see the LICENSE file for details.

