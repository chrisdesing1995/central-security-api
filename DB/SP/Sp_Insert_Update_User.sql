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
	@ObjectId UNIQUEIDENTIFIER = NULL,
	@ObjectFile NVARCHAR(MAX) = NULL,
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
	
		SET @AuditDetails = CONCAT('Acci�n: ', @Accion, ', Username: ', @Username, ', Email: ', @Email);

        IF EXISTS ( SELECT 1 FROM [dbo].[User] WHERE ([Username] = @Username OR [Email] = @Email)
            AND (@Accion = 'INSERT' OR (@Accion = 'UPDATE' AND [Id] <> @Id)))
        BEGIN
            SET @MESSAGES = 'El nombre de usuario o el Email ya est�n en uso.';
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

			EXEC [dbo].[Sp_Insert_Update_UserRole] @UserId = @Id,
													@RoleIds = @RoleIds,
													@UserCreated = @UserCreated,
													@UserUpdated = @UserUpdated,
													@Accion = @Accion;

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

            EXEC [dbo].[Sp_Insert_Update_UserRole] @UserId = @Id,
													@RoleIds = @RoleIds,
													@UserCreated = @UserCreated,
													@UserUpdated = @UserUpdated,
													@Accion = @Accion;
			IF @ObjectFile IS NOT NULL
			BEGIN
				SET @UserCreated = ISNULL(@UserCreated,@UserUpdated);
				EXEC [dbo].[Sp_Insert_Update_ObjectFile] @Id = @ObjectId,
														@EntityId = @Id,
														@EntityName = 'User',
														@ObjectData = @ObjectFile,
														@UserCreated = @UserCreated,
														@UserUpdated = @UserUpdated;
			END


            SET @MESSAGES = 'Usuario actualizado exitosamente.';
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
		SET @UserCreated = ISNULL(@UserCreated,@UserUpdated);
		EXEC [dbo].[Sp_Insert_AuditLog] @Action = @Accion,
											@TableName = 'User',
											@User = @UserUpdated,
											@Details = @MESSAGES;

    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, CONVERT(NVARCHAR(100),@Id) AS Data;
END
