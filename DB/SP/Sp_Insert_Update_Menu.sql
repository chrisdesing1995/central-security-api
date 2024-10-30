USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_Menu]
(
    @Id UNIQUEIDENTIFIER = NULL,
    @MenuName NVARCHAR(100),
    @ParentId UNIQUEIDENTIFIER = NULL,
    @Url NVARCHAR(256),
    @Icon NVARCHAR(100),
    @SortOrder INT,
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
	    SET @AuditDetails = CONCAT('Acción: ', @Accion, ', MenuName: ', @MenuName, ', ParentId: ', ISNULL(CAST(@ParentId AS NVARCHAR(36)), 'NULL'), ', Url: ', @Url);

        IF EXISTS (SELECT 1 FROM [dbo].[Menu] WHERE [MenuName] = @MenuName
            AND ISNULL([ParentId], '00000000-0000-0000-0000-000000000000') = ISNULL(@ParentId, '00000000-0000-0000-0000-000000000000')
            AND (@Accion = 'INSERT' OR (@Accion = 'UPDATE' AND [Id] <> @Id)))
        BEGIN
            SET @MESSAGES = 'El nombre del menú '+@MenuName+' ya está en uso.';
            SET @Status = 0;

            ROLLBACK TRANSACTION;
            SELECT @MESSAGES AS Messages, @Status AS Status;
            RETURN;
        END

        IF @Accion = 'INSERT'
        BEGIN
            SET @Id = NEWID();

            INSERT INTO [dbo].[Menu] ([Id], [MenuName], [ParentId], [Url], [Icon], [SortOrder], [IsActive], [CreatedAt], [UserCreated], [UpdatedAt], [UserUpdated])
            VALUES (@Id, @MenuName, @ParentId, @Url, @Icon, @SortOrder, @IsActive, @CreatedAt, @UserCreated, NULL, NULL);

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'Menu',
											@User = @UserCreated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Menú creado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            UPDATE [dbo].[Menu]
            SET [MenuName] = @MenuName,
                [ParentId] = @ParentId,
                [Url] = @Url,
                [Icon] = @Icon,
                [SortOrder] = @SortOrder,
                [IsActive] = @IsActive,
                [UpdatedAt] = @UpdatedAt,
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

            EXEC [dbo].[Sp_Insert_AuditLog]
											@Action = @Accion,
											@TableName = 'Menu',
											@User = @UserUpdated,
											@Details = @AuditDetails;

            SET @MESSAGES = 'Menú actualizado exitosamente.';
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
GO
