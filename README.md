Task Manager Backend (ASP.NET Core Web API)
📌 Overview

This is the backend of the Task Manager App, built with ASP.NET Core Web API.
It provides APIs for user authentication and task management, and connects to a database (SQL Server).

🚀 Features

🔑 User Authentication (Register / Login) with JWT tokens

📝 Task Management (CRUD: Add, Edit, Delete, Mark as Done)

🔎 Task Filtering (All, Pending, Completed)

🗄️ Entity Framework Core for database operations

🔐 Secure endpoints (only authenticated users can manage their tasks)

📂 Prerequisites

Make sure you have installed:

Visual Studio 2022
 or VS Code

.NET SDK 8.0

SQL Server

⚙️ Installation & Running

Clone the repository:

git clone https://github.com/madonna-hany-boils/taskManagerBack.git
cd taskManagerBack


Open the project in Visual Studio or VS Code.

Update the database connection string inside appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=TaskManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true"
}


Apply migrations & update the database:

dotnet ef database update


Run the API:

dotnet run


The API will be available at:

https://localhost:5001/api

🔗 Frontend

Make sure the Angular frontend is running and points to the backend API URL.
By default, it is configured to call:

http://taskymanager.runasp.net/api

📄 Notes

Use /api/Auth/register to register a user.

Use /api/Auth/login to login and get a JWT token.

Use the JWT token in the Authorization header (Bearer Token) to access secured task endpoints.
