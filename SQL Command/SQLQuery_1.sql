CREATE TABLE Users(
    Id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50),
    Email NVARCHAR(50),
    Password NVARCHAR(50),
    IsLibrarian BIT DEFAULT 0
);

CREATE TABLE Books(
    Id INT PRIMARY KEY,
    Title NVARCHAR(50),
    Author NVARCHAR(50),
    Category NVARCHAR(50),
    IsBorrowed BIT DEFAULT 0,
    Path NVARCHAR(50)
);

CREATE TABLE Notifications(
    Id INT PRIMARY KEY,
    Content NVARCHAR(200),
    CreatedAt DATETIME,
);

CREATE TABLE Loans(
    Id INT PRIMARY KEY,
    UserId INT,
    BookId INT,
    LoanDate DATETIME,
    ReturnDate DATETIME,
    DueDate DATETIME
);

