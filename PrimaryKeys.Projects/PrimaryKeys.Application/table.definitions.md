-- Create the [gssTables] schema if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = 'gssTables')
BEGIN
    EXEC('CREATE SCHEMA [gssTables]');
END

-- Create the ColumnRow table
CREATE TABLE [gssTables].[ColumnRow] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ColumnIndex] INT NOT NULL,
    [TableName] NVARCHAR(200) NOT NULL,
    [ColumnName] NVARCHAR(200) NOT NULL,
    [IsNone] BIT NOT NULL,
    [IsMasterKey] BIT NOT NULL,
    [IsPrimaryKey] BIT NOT NULL,
    [IsForeignKey] BIT NOT NULL,
    [IsRemoved] BIT NOT NULL,
    [ForeignKeyTable] NVARCHAR(200) NULL,
    [ForeignKeyField] NVARCHAR(200) NULL
);

-- Create the AuditEntry table
CREATE TABLE [gssTables].[AuditEntry] (
    [Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    [ChangedOn] DATETIME NOT NULL,
    [ChangedBy] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(MAX) NULL,
    [ColumnName] NVARCHAR(200) NULL,
    [PreviousValue] NVARCHAR(200) NULL,
    [NewValue] NVARCHAR(200) NULL,
    [ColumnRowId] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT FK_AuditEntry_ColumnRow FOREIGN KEY ([ColumnRowId])
        REFERENCES [gssTables].[ColumnRow] ([Id])
        ON DELETE CASCADE
);
