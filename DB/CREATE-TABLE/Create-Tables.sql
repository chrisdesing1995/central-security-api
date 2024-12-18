
CREATE TABLE [User] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(200) NOT NULL,
	SurName NVARCHAR(200) NOT NULL,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    [Password] NVARCHAR(800) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
	Phone NVARCHAR(100) NULL,
    IsActive NVARCHAR(1) NOT NULL DEFAULT 'A',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [Role] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL UNIQUE,
    [Description] NVARCHAR(500) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [Menu] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    MenuName NVARCHAR(100) NOT NULL,
    ParentId UNIQUEIDENTIFIER NULL,
    [Url] NVARCHAR(256) NULL,
    Icon NVARCHAR(100) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive NVARCHAR(1) NOT NULL DEFAULT 'A',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [UserRole] (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (UserId, RoleId)
);

CREATE TABLE [RoleMenu] (
    RoleId UNIQUEIDENTIFIER NOT NULL,
    MenuId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (RoleId, MenuId)
);

CREATE TABLE [GeneralParameter] (
    Id UNIQUEIDENTIFIER NOT NULL  PRIMARY KEY,
    Code NVARCHAR(200) NOT NULL UNIQUE,
    [Description] NVARCHAR(255) NULL,
    IsActive NVARCHAR(1) NOT NULL DEFAULT 'A',
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [GeneralParameterDetail] (
    Id UNIQUEIDENTIFIER NOT NULL  PRIMARY KEY,
    GeneralParameterId UNIQUEIDENTIFIER NOT NULL,
    Code NVARCHAR(10) NOT NULL,
	Value1 NVARCHAR(100) NOT NULL,
	Value2 NVARCHAR(100) NULL,
	Value3 NVARCHAR(100) NULL,
	Value4 NVARCHAR(100) NULL,
	Value5 NVARCHAR(100) NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [ObjectFile] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	EntityId UNIQUEIDENTIFIER NOT NULL,
    EntityName NVARCHAR(100) NOT NULL,
    ObjectData NVARCHAR(MAX) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [AuditLog] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    [Action] NVARCHAR(100) NOT NULL,
    TableName NVARCHAR(100) NOT NULL,
    [User] NVARCHAR(100) NULL,
    [Timestamp] DATETIME NOT NULL DEFAULT GETDATE(),
    Details NVARCHAR(MAX) NULL
);
