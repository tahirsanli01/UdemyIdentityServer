IF COL_LENGTH(N'dbo.Department', N'Organization') IS NULL
BEGIN
    ALTER TABLE dbo.Department ADD Organization nvarchar(100) NULL;

    UPDATE dbo.Department
    SET Organization = N'Adana Sanayi Odası'
    WHERE Organization IS NULL;

    ALTER TABLE dbo.Department ALTER COLUMN Organization nvarchar(100) NOT NULL;
END;
GO

IF NOT EXISTS
(
    SELECT 1
    FROM sys.check_constraints
    WHERE name = N'CK_Department_Organization'
)
BEGIN
    ALTER TABLE dbo.Department WITH CHECK ADD CONSTRAINT CK_Department_Organization
        CHECK (Organization IN (N'Adana Sanayi Odası', N'Adana Sanayi Kampüsü'));
END;
GO
