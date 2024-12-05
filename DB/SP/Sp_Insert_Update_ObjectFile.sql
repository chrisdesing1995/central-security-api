USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_ObjectFile]
(
	@Id UNIQUEIDENTIFIER = NULL,
    @EntityId UNIQUEIDENTIFIER,
    @EntityName NVARCHAR(100),
    @ObjectData NVARCHAR(MAX),
    @UserCreated NVARCHAR(100) = NULL,
    @UserUpdated NVARCHAR(100) = NULL
)
AS
BEGIN
    DECLARE @MESSAGES NVARCHAR(500);
    DECLARE @Status BIT;
    DECLARE @AuditDetails NVARCHAR(MAX);
	DECLARE @Accion NVARCHAR(20);

    BEGIN TRANSACTION;

    BEGIN TRY
       
		SET @Accion = CASE WHEN @Id IS NULL THEN 'INSERT' ELSE 'UPDATE' END;

	    SET @AuditDetails = CONCAT('Acción: ', @Accion, ', EntityId: ', @EntityId,', EntityName: ', @EntityName);

        IF @Id IS NULL
        BEGIN
            SET @Id = NEWID();

            INSERT INTO [dbo].[ObjectFile] ([Id],[EntityId],[EntityName] ,[ObjectData],[CreatedAt] ,[UserCreated],[UpdatedAt],[UserUpdated])
            VALUES (@Id, @EntityId, @EntityName, @ObjectData, GETDATE(), @UserCreated, NULL, NULL);

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'ObjectFile',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Foto de perfil guardado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Id IS NOT NULL
        BEGIN
            UPDATE [dbo].[ObjectFile]
            SET [EntityId] = @EntityId,
                [EntityName] = @EntityName,
				[ObjectData] = @ObjectData,
                [UpdatedAt] = GETDATE(),
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'ObjectFile',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Foto de perfil actulizado exitosamente.';
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
											@TableName = 'ObjectFile',
											@User = @UserCreated,
											@Details = @MESSAGES;
    END CATCH

    SELECT @MESSAGES AS Messages, @Status AS Status, CONVERT(NVARCHAR(100), @Id) AS Data;
END
GO
