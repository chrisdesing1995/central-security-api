USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_GeneralParameterDetail_By_Code]
(
	@Code NVARCHAR(200) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT
		GPD.[Id],
		GPD.[Code],
		GPD.[Value1],
		GPD.[Value2],
		GPD.[Value3],
		GPD.[Value4],
		GPD.[Value5]
	FROM [dbo].[GeneralParameterDetail] GPD
	INNER JOIN [dbo].[GeneralParameter] GP ON GP.[Id] = GPD.[GeneralParameterId]
	WHERE GP.[Code] = @Code

END;
