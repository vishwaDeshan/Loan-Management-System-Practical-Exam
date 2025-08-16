# Loan Management System

This project is a simple Loan Management System built using ASP.NET Core MVC 8 and SQL Server. It demonstrates adding, editing, and viewing loan applications using ADO.NET.

---

## Prerequisites

- SQL Server (LocalDB or full instance)
- .NET 8 SDK
- Visual Studio 2022 or later
- Git (optional for version control)

---

## Setup Instructions

### 1. Create Database

1. Open SQL Server Management Studio (SSMS) or any SQL query tool.
2. Open the file `CreateDatabase.sql`.
3. Execute the script to create the database and seed initial data.

Ensure the database `LoanManagementSystemDB` and table `LoanApplications` are created successfully.

---

### 2. Configure Connection String

1. Open the project in Visual Studio.
2. Open `appsettings.json`.
3. Set the connection string to your SQL Server instance:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=LoanManagementSystemDB;Trusted_Connection=True;"
}
```

Replace `YOUR_SERVER_NAME` with your SQL Server instance name.

---

### 3. Run the Application

1. Build the solution in Visual Studio.
2. Press `F5` or click **Run** to start the application.
3. Navigate to `https://localhost:{PORT}`.
4. Login using the hardcoded credentials:

- Username: `Admin`
- Password: `Admin@123`

---

### 4. Using the Application

- **Dashboard**: View all loan applications.
- **Add/Edit Loan**: Create a new loan or update existing loan details.
- **View Details**: Check loan information.
- **Edit Status**: Change loan status (New, Approved, Rejected).

---

