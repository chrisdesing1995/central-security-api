USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Users_By_Id]
(
	@Id UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		U.[Id],
		U.[Name],
		[SurName],
		[Username],
		[Password],
		[Email],
		[Phone],
		[IsActive],
		STRING_AGG(CONVERT(NVARCHAR(36), R.[Id]), ',') AS RoleIds,
		STRING_AGG(R.[RoleName], ',') AS RoleNames
	FROM [dbo].[User] U
	INNER JOIN [dbo].[UserRole] UR ON U.Id = UR.UserId
	INNER JOIN [dbo].[Role] R ON UR.RoleId = R.Id
	WHERE U.[Id] = @Id
	GROUP BY U.[Id], [Username], [Password], [Email], [IsActive],
	U.[Name], [SurName],[Phone]

END;
