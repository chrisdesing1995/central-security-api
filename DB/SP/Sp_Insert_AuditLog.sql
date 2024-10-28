USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_AuditLog]
(
    @Action NVARCHAR(100),
    @TableName NVARCHAR(100),
    @UserId UNIQUEIDENTIFIER,
    @Details NVARCHAR(MAX)
)
AS
BEGIN
    DECLARE @Id UNIQUEIDENTIFIER = NEWID();
    DECLARE @Timestamp DATETIME2(7) = SYSDATETIME();

    INSERT INTO [dbo].[AuditLog]
           ([Id], [Action], [TableName], [UserId], [Timestamp], [Details])
     VALUES
           (@Id, @Action, @TableName, @UserId, @Timestamp, @Details);
END
GO
