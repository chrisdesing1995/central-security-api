USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id, m.MenuName, m.ParentId, m.Url, m.Icon, m.SortOrder
    FROM [User] u
		INNER JOIN UserRole ur ON u.Id = ur.UserId
		INNER JOIN RoleMenu rm ON ur.RoleId = rm.RoleId
		INNER JOIN [Menu] m ON rm.MenuId = m.Id
    WHERE m.IsActive = 'A'
    ORDER BY m.SortOrder;
END;
