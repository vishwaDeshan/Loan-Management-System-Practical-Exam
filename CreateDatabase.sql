Use LoanManagementSystemDB

CREATE TABLE LoanApplications (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100) NOT NULL,
    NicPassport NVARCHAR(50) NOT NULL,
    LoanType NVARCHAR(50) NOT NULL,
    InterestRate DECIMAL(5,2) NOT NULL,
    LoanAmount DECIMAL(18,2) NOT NULL,
    DurationMonths INT NOT NULL,
    Status NVARCHAR(50) NOT NULL DEFAULT 'New'
);

Select * From LoanApplications