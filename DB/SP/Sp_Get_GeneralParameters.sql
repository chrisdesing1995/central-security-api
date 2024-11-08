USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_GeneralParameters]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		[Id],
		[Code],
		[Description],
		[IsActive]
	FROM [dbo].[GeneralParameter]

END;
