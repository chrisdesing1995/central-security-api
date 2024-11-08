USE MarketMaxDB
GO
CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_User]
(
    @Id UNIQUEIDENTIFIER = NULL,
    @RoleIds NVARCHAR(MAX),
	@Name NVARCHAR(200),
	@SurName NVARCHAR(200),
    @Username NVARCHAR(50),
    @Password NVARCHAR(200),
    @Email NVARCHAR(100),
	@Phone  NVARCHAR(100) = NULL,
    @IsActive NVARCHAR(1),
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

            INSERT INTO [dbo].[User] ([Id],[Name],[SurName],[Username],[Password],[Email],[Phone],[IsActive],[CreatedAt],[UserCreated],[UpdatedAt],[UserUpdated])
			VALUES (@Id, @Name, @SurName, @Username, @Password, @Email, @Phone, @IsActive, GETDATE(), @UserCreated, NULL, NULL);

			
			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'User',
											@User = @UserCreated,
											@Details = @AuditDetails;

            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @Id, value
            FROM STRING_SPLIT(@RoleIds, ',');
			
			SET @AuditDetails = CONCAT('Acción: ', @Accion, ', Username: ', @Username, ', Roles Ids: ', @RoleIds);

			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'UserRole',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Usuario creado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            UPDATE [dbo].[User]
            SET [Username] = @Username,
				[Name] = @Name,
				[SurName] = @SurName,
                [Password] = @Password,
                [Email] = @Email,
				[Phone] = @Phone,
                [IsActive] = @IsActive,
                [UpdatedAt] =  GETDATE(),
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

			EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
								@TableName = 'User',
								@User = @UserUpdated,
								@Details = @AuditDetails;

            DELETE FROM [dbo].[UserRole]
            WHERE [UserId] = @Id;

            INSERT INTO [dbo].[UserRole] ([UserId],[RoleId])
            SELECT @Id, value
            FROM STRING_SPLIT(@RoleIds, ',');
			
			SET @AuditDetails = CONCAT('Acción: ', @Accion, ', Username: ', @Username, ', Roles Ids: ', @RoleIds);

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
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, CONVERT(NVARCHAR(100),@Id) AS Data;
END
