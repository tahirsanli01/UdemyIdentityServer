SET XACT_ABORT ON;
BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.UserType', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.UserType
    (
        Id int IDENTITY(1,1) NOT NULL CONSTRAINT PK_UserType PRIMARY KEY,
        Name nvarchar(50) NOT NULL,
        Code varchar(50) NOT NULL,
        SortOrder int NOT NULL CONSTRAINT DF_UserType_SortOrder DEFAULT (0),
        IsActive bit NOT NULL CONSTRAINT DF_UserType_IsActive DEFAULT (1),
        CONSTRAINT UQ_UserType_Code UNIQUE (Code)
    );
END;

IF NOT EXISTS (SELECT 1 FROM dbo.UserType WHERE Code = 'PERSONEL')
    INSERT dbo.UserType (Name, Code, SortOrder) VALUES (N'Personel', 'PERSONEL', 10);
IF NOT EXISTS (SELECT 1 FROM dbo.UserType WHERE Code = 'UYE')
    INSERT dbo.UserType (Name, Code, SortOrder) VALUES (N'Üye', 'UYE', 20);
IF NOT EXISTS (SELECT 1 FROM dbo.UserType WHERE Code = 'DIS_KULLANICI')
    INSERT dbo.UserType (Name, Code, SortOrder) VALUES (N'Dış Kullanıcı', 'DIS_KULLANICI', 30);

-- Idempotent seed corrections also repair text after an earlier import with a wrong code page.
UPDATE dbo.UserType SET Name = N'Personel', SortOrder = 10 WHERE Code = 'PERSONEL';
UPDATE dbo.UserType SET Name = N'Üye', SortOrder = 20 WHERE Code = 'UYE';
UPDATE dbo.UserType SET Name = N'Dış Kullanıcı', SortOrder = 30 WHERE Code = 'DIS_KULLANICI';

IF COL_LENGTH(N'dbo.Users', N'UserTypeId') IS NULL
    ALTER TABLE dbo.Users ADD UserTypeId int NULL;

IF COL_LENGTH(N'dbo.Users', N'IsActive') IS NULL
    ALTER TABLE dbo.Users ADD IsActive bit NULL;

IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = N'FK_Users_UserType')
    ALTER TABLE dbo.Users WITH CHECK ADD CONSTRAINT FK_Users_UserType
        FOREIGN KEY (UserTypeId) REFERENCES dbo.UserType(Id);

COMMIT TRANSACTION;
