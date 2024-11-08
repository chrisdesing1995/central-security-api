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
		GP.[IsActive],
		GPD.[Id] AS DetailId,
		GPD.[Code] AS DetailCode,
		GPD.[Value1],
		GPD.[Value2],
		GPD.[Value3],
		GPD.[Value4],
		GPD.[Value5]
	FROM [dbo].[GeneralParameter] GP
	INNER JOIN [dbo].[GeneralParameterDetail] GPD ON GP.[Id] = GPD.[GeneralParameterId]
	WHERE GP.[Id] = @Id

END;
