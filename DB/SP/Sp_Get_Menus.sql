USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus]
AS
BEGIN
    SET NOCOUNT ON;

	SELECT 
		m.Id,
		m.MenuName, 
		m.ParentId, 
		p.MenuName AS parentName, 
		m.Url, 
		m.Icon, 
		m.SortOrder,
		m.IsActive
	FROM [Menu] m
	LEFT JOIN [Menu] p ON m.ParentId = p.Id
	ORDER BY ISNULL(m.ParentId, m.Id), m.ParentId, m.SortOrder;


END;
