USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_RoleMenu]
(
    @RoleId UNIQUEIDENTIFIER,
    @MenuIds NVARCHAR(MAX) = NULL,
	@UserCreated NVARCHAR(100)
)
AS
BEGIN
    DECLARE @MESSAGES NVARCHAR(500);
    DECLARE @Status BIT;
    DECLARE @AuditDetails NVARCHAR(MAX);

    BEGIN TRANSACTION;

    BEGIN TRY
        DELETE FROM [dbo].[RoleMenu] WHERE [RoleId] = @RoleId;

        DECLARE @MenuId NVARCHAR(36);
        DECLARE @Pos INT;
        DECLARE @Delimiter CHAR(1) = ',';

        SET @MenuIds = LTRIM(RTRIM(ISNULL(@MenuIds,''))) + @Delimiter;
        SET @Pos = CHARINDEX(@Delimiter, @MenuIds, 1);

        WHILE @Pos > 0
        BEGIN
            SET @MenuId = LTRIM(RTRIM(SUBSTRING(@MenuIds, 1, @Pos - 1)));

            INSERT INTO [dbo].[RoleMenu] ([RoleId], [MenuId])
            VALUES (@RoleId, CAST(@MenuId AS UNIQUEIDENTIFIER));

            SET @MenuIds = SUBSTRING(@MenuIds, @Pos + 1, LEN(@MenuIds));
            SET @Pos = CHARINDEX(@Delimiter, @MenuIds, 1);
        END

        SET @AuditDetails = CONCAT('Acción: INSERT, RoleId: ', @RoleId, ', MenuIds: ', @MenuIds);

        EXEC [dbo].[Sp_Insert_AuditLog]
										@Action = 'INSERT',
										@TableName = 'RoleMenu',
										@User = @UserCreated,
										@Details = @AuditDetails;

        SET @MESSAGES = 'Permisos de menú actualizados exitosamente para el rol.';
        SET @Status = 1;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @MESSAGES = ERROR_MESSAGE();
        SET @Status = 0;
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, NULL AS Data;
END
GO
