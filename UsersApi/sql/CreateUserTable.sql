CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

GO

CREATE PROCEDURE InsertUser
    @Name NVARCHAR(100),
    @Email NVARCHAR(100),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Users (Name, Email)
    VALUES (@Name, @Email);

    SET @Id = SCOPE_IDENTITY();
END

GO

CREATE PROCEDURE UpdateUser
    @Id INT,
    @Name NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Users
    SET Name = @Name, Email = @Email
    WHERE Id = @Id;
END

GO

CREATE PROCEDURE DeleteUser
    @Id INT
AS
BEGIN
    DELETE FROM Users
    WHERE Id = @Id;
END