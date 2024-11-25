USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Get_Menus_By_Id]
(
	@Id UNIQUEIDENTIFIER = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT DISTINCT m.Id, m.MenuName, m.ParentId, m.Url, m.Icon, m.SortOrder,m.IsActive
    FROM [Menu] m
    WHERE m.IsActive = 'A' AND m.Id = @Id
    ORDER BY m.SortOrder;
END;
