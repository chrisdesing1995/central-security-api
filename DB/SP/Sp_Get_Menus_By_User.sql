USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus_By_User]
    @UserId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id,m.MenuName, m.ParentId, p.MenuName as parentName, m.Url, m.Icon, m.SortOrder,m.IsActive
    FROM [Menu] M
		INNER JOIN RoleMenu rm ON RM.MenuId = M.Id
		INNER JOIN UserRole ur ON UR.RoleId = RM.RoleId
		INNER JOIN [User] U ON U.Id = UR.UserId
		LEFT JOIN [Menu] P ON M.ParentId = P.Id
    WHERE u.Id = @UserId
      AND m.IsActive = 'A'
    ORDER BY m.SortOrder;
END;
