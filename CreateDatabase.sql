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

-- Indexing
--CREATE NONCLUSTERED INDEX IX_LoanApplications_NicPassport 
--ON LoanApplications(NicPassport);

INSERT INTO LoanApplications(CustomerName, NicPassport, LoanType, InterestRate, LoanAmount, DurationMonths,Status)
VALUES ('Vishwa', '982803217V', 'Personal', 5.00, 500000.00, 12, 'New'),
('Kalyani', '892736149V', 'Housing', 3.00, 100000.00, 36, 'New'),
('Nimal', '973452816V', 'Personal', 5.00, 50000.00, 12, 'Rejected')

Select * From LoanApplications

--TRUNCATE TABLE LoanApplications

