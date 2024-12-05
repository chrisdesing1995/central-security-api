USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_ObjectFile_By_Entity]
(
	@EnityId UNIQUEIDENTIFIER = NULL,
	@EntityName NVARCHAR(200)
)
AS
BEGIN
    SET NOCOUNT ON;
	
	SELECT [Id]
		  ,[EntityId]
		  ,[EntityName]
		  ,[ObjectData]
	FROM [dbo].[ObjectFile]
	WHERE [EntityId] = @EnityId AND [EntityName] = @EntityName

END;
