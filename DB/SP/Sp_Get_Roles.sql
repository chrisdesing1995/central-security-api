USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Roles]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		Id
		,RoleName
		,Description
		,CreatedAt
		,UserCreated
		,UpdatedAt
		,UserUpdated
	FROM [dbo].[Role]

END;
