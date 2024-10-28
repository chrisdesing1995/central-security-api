USE MarketMaxDB
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Users_Login]
    @UserName NVARCHAR(100)
AS
BEGIN
    SELECT 
			U.Id,
			U.Password,
			U.IsActive,
			U.Email,
			U.UserName,
			R.Id AS RolId,
			R.RoleName,
			NULL AS Token
	FROM [dbo].[User] U
	INNER JOIN [dbo].[UserRole] UR ON UR.UserId = U.Id 
	INNER JOIN [dbo].[Role] R ON UR.RoleId = R.Id
	WHERE U.IsActive = 'A' AND (U.UserName = @UserName OR U.Email = @UserName)
END
