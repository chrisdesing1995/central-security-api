USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id, m.MenuName, m.ParentId, m.Url, m.Icon, m.SortOrder
    FROM [Menu] m
    WHERE m.IsActive = 'A'
    ORDER BY m.SortOrder;
END;
