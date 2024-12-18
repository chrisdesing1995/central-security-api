USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_Role]
(
    @Id UNIQUEIDENTIFIER = NULL,
    @RoleName NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
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
       
	    SET @AuditDetails = CONCAT('Acci�n: ', @Accion, ', RoleName: ', @RoleName);

        IF EXISTS (SELECT 1 FROM [dbo].[Role] WHERE [RoleName] = @RoleName
            AND (@Accion = 'INSERT' OR (@Accion = 'UPDATE' AND [Id] <> @Id)))
        BEGIN
            SET @MESSAGES = 'El rol '+@RoleName+' ya est� en uso.';
            SET @Status = 0;

            ROLLBACK TRANSACTION;
            SELECT @MESSAGES AS Messages, @Status AS Status, NULL AS Data;
            RETURN;
        END

        IF @Accion = 'INSERT'
        BEGIN
            SET @Id = NEWID();

            INSERT INTO [dbo].[Role] ([Id], [RoleName], [Description], [CreatedAt], [UserCreated], [UpdatedAt], [UserUpdated])
            VALUES (@Id, @RoleName, @Description, GETDATE(), @UserCreated, NULL, NULL);

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'Role',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Rol '+@RoleName+' creado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            UPDATE [dbo].[Role]
            SET [RoleName] = @RoleName,
                [Description] = @Description,
                [UpdatedAt] = GETDATE(),
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'Role',
											@User = @UserUpdated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Rol '+@RoleName+' actualizado exitosamente.';
            SET @Status = 1;
        END
        ELSE
        BEGIN
            SET @MESSAGES = 'Acci�n no v�lida. Debe ser INSERT o UPDATE.';
            SET @Status = 0;
        END

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        SET @MESSAGES = ERROR_MESSAGE();
        SET @Status = 0;
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, CONVERT(NVARCHAR(100), @Id) AS Data;
END
GO
