USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_GeneralParameters]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		GP.[Id],
		GP.[Code],
		GP.[Description],
		GP.[IsActive]
	FROM [dbo].[GeneralParameter] GP

END;
