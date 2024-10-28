USE MarketMaxDB
GO
CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_User]
(
    @Id UNIQUEIDENTIFIER,
    @RoleIds NVARCHAR(MAX),
    @Username NVARCHAR(50),
    @Password NVARCHAR(200),
    @Email NVARCHAR(100),
    @IsActive NVARCHAR(1),
    @CreatedAt DATETIME2(7) = NULL,
    @UserCreated NVARCHAR(100) = NULL,
    @UpdatedAt DATETIME2(7) = NULL,
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
	
		SET @AuditDetails = CONCAT('Acción: ', @Accion, ', Username: ', @Username, ', Email: ', @Email);

        IF EXISTS ( SELECT 1 FROM [dbo].[User] WHERE ([Username] = @Username OR [Email] = @Email)
            AND (@Accion = 'INSERT' OR (@Accion = 'UPDATE' AND [Id] <> @Id)))
        BEGIN
            SET @MESSAGES = 'El nombre de usuario o el Email ya están en uso.';
            SET @Status = 0;

            ROLLBACK TRANSACTION;
            SELECT @MESSAGES AS Messages, @Status AS Status;
            RETURN;
        END

        IF @Accion = 'INSERT'
        BEGIN
            SET @Id = NEWID();

            INSERT INTO [dbo].[User] ([Id],[Username],[Password],[Email],[IsActive],[CreatedAt],[UserCreated],[UpdatedAt],[UserUpdated])
            VALUES (@Id, @Username, @Password, @Email, @IsActive, @CreatedAt, @UserCreated, NULL, NULL);

            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @Id, value
            FROM STRING_SPLIT(@RoleIds, ',');
			
			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'User',
											@UserId = @Id,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Usuario creado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            UPDATE [dbo].[User]
            SET [Username] = @Username,
                [Password] = @Password,
                [Email] = @Email,
                [IsActive] = @IsActive,
                [UpdatedAt] = @UpdatedAt,
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

            DELETE FROM [dbo].[UserRole]
            WHERE [UserId] = @Id;

            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @Id, value
            FROM STRING_SPLIT(@RoleIds, ',');

			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'User',
											@UserId = @Id,
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
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status;
END
