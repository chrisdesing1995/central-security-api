USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_UserRole]
(
	@UserId UNIQUEIDENTIFIER = NULL,
    @RoleIds NVARCHAR(MAX),
	@UserCreated NVARCHAR(100) = NULL,
    @UserUpdated NVARCHAR(100) = NULL,
    @Accion NVARCHAR(20)
)
AS
BEGIN
    DECLARE @MESSAGES NVARCHAR(500);
    DECLARE @Status BIT;
    DECLARE @AuditDetails NVARCHAR(MAX);

    BEGIN TRANSACTION;

    BEGIN TRY
       
	    SET @AuditDetails = CONCAT('Acción: ', @Accion, ', UserId: ', @UserId, ', Roles Ids: ', @RoleIds);

        IF @Accion = 'INSERT'
        BEGIN
            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @UserId, value
            FROM STRING_SPLIT(@RoleIds, ',');

			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'UserRole',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Asignacion de roles a usuario exitoso.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            DELETE FROM [dbo].[UserRole]
            WHERE [UserId] = @UserId;

            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @UserId, value
            FROM STRING_SPLIT(@RoleIds, ',');

			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'UserRole',
											@User = @UserUpdated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Usuario actualizado exitosamente.';
            SET @Status = 1;
        END
        ELSE
        BEGIN
            SET @MESSAGES = 'Acción no válida. Debe ser INSERT o UPDATE.';
            SET @Status = 0;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @MESSAGES = ERROR_MESSAGE();
        SET @Status = 0;
		SET @UserCreated = ISNULL(@UserCreated,@UserUpdated);
		EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'UserRole',
											@User = @UserCreated,
											@Details = @MESSAGES;
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, CONVERT(NVARCHAR(100), @UserId) AS Data;
END
GO
