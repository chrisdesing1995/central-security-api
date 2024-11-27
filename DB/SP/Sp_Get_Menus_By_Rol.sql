USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus_By_Rol]
    @RolId UNIQUEIDENTIFIER
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id,m.MenuName, m.ParentId, p.MenuName as parentName, m.Url, m.Icon, m.SortOrder,m.IsActive
    FROM [Menu] M
		INNER JOIN RoleMenu rm ON RM.MenuId = M.Id
		INNER JOIN [Role] R ON R.Id = RM.RoleId
		LEFT JOIN [Menu] P ON M.ParentId = P.Id
    WHERE R.Id = @RolId
      AND m.IsActive = 'A'
    ORDER BY m.SortOrder;
END;
