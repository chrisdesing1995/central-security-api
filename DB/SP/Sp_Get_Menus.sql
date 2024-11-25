USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id,m.MenuName, m.ParentId, m.Url, m.Icon, m.SortOrder,m.IsActive
    FROM [Menu] m
    ORDER BY m.SortOrder;

END;
