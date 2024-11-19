USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_GeneralParameters_By_Id]
(
	@Id UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		GP.[Id],
		GP.[Code],
		GP.[Description],
		GP.[IsActive]
	FROM [dbo].[GeneralParameter] GP
	WHERE GP.[Id] = @Id

END;
