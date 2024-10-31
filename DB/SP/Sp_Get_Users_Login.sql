USE MarketMaxDB
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Users_Login]
    @UserName NVARCHAR(100)
AS
BEGIN
    SELECT 
			U.Id,
			U.UserName,
			U.Email,
			U.Password,
			U.IsActive,
			STRING_AGG(CONVERT(NVARCHAR(36), R.[Id]), ',') AS RoleIds,
			STRING_AGG(R.[RoleName], ',') AS RoleNames,
			NULL AS Token
	FROM [dbo].[User] U
	INNER JOIN [dbo].[UserRole] UR ON UR.UserId = U.Id 
	INNER JOIN [dbo].[Role] R ON UR.RoleId = R.Id
	WHERE U.IsActive = 'A' AND (U.UserName = @UserName OR U.Email = @UserName)
	GROUP BY U.[Id], [Username], [Password], [Email], [IsActive]

END
