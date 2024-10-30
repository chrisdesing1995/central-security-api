USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Users]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		U.[Id],
		[Username],
		[Password],
		[Email],
		[IsActive],
		STRING_AGG(CONVERT(NVARCHAR(36), R.[Id]), ',') AS RoleIds,
		STRING_AGG(R.[RoleName], ',') AS RoleNames
	FROM [dbo].[User] U
	INNER JOIN [dbo].[UserRole] UR ON U.Id = UR.UserId
	INNER JOIN [dbo].[Role] R ON UR.RoleId = R.Id
	GROUP BY U.[Id], [Username], [Password], [Email], [IsActive]

END;
