
CREATE TABLE [User] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    Username NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(800) NOT NULL UNIQUE,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    IsActive NVARCHAR(1) NOT NULL DEFAULT 'A',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME2 NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [Role] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    RoleName NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(500) NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME2 NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE [Menu] (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
    MenuName NVARCHAR(100) NOT NULL,
    ParentId UNIQUEIDENTIFIER NULL,
    Url NVARCHAR(256) NULL,
    Icon NVARCHAR(100) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive NVARCHAR(1) NOT NULL DEFAULT 'A',
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserCreated NVARCHAR(100) NOT NULL,
    UpdatedAt DATETIME2 NULL,
    UserUpdated NVARCHAR(100) NULL
);

CREATE TABLE UserRole (
    UserId UNIQUEIDENTIFIER NOT NULL,
    RoleId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (UserId, RoleId)
);

CREATE TABLE RoleMenu (
    RoleId UNIQUEIDENTIFIER NOT NULL,
    MenuId UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (RoleId, MenuId)
);


CREATE TABLE AuditLog (
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
    Action NVARCHAR(100) NOT NULL, -- (Create, Update, Delete)
    TableName NVARCHAR(100) NOT NULL, -- Nombre de la tabla
    UserId UNIQUEIDENTIFIER NULL, -- ID del usuario que realiz� la acci�n
    Timestamp DATETIME2 NOT NULL DEFAULT GETDATE(), -- Fecha y hora de la acci�n
    Details NVARCHAR(MAX) NULL -- Detalles adicionales sobre la acci�n
);
