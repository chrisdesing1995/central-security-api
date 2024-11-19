USE [MarketMaxDB]
GO

CREATE OR ALTER PROCEDURE [dbo].[Sp_Insert_Update_GeneralParameter]
(
    @Id UNIQUEIDENTIFIER = NULL,
    @Code NVARCHAR(200),
    @Description NVARCHAR(255) = NULL,
    @IsActive NVARCHAR(1) = 'A',
    @UserCreated NVARCHAR(100) = NULL,
    @UserUpdated NVARCHAR(100) = NULL,
    @Accion NVARCHAR(20),
    @Detalles dbo.GeneralParameterDetailDto READONLY
)
AS
BEGIN
    DECLARE @MESSAGES NVARCHAR(500);
    DECLARE @Status BIT;
    DECLARE @AuditDetails NVARCHAR(MAX);
    DECLARE @CreatedAt DATETIME = GETDATE();
    DECLARE @UpdatedAt DATETIME = NULL;
    DECLARE @ListCodes NVARCHAR(MAX) = '';

    BEGIN TRANSACTION;

    BEGIN TRY
        
        IF EXISTS (SELECT 1 FROM [dbo].[GeneralParameter] WHERE [Code] = @Code
            AND (@Accion = 'INSERT' OR (@Accion = 'UPDATE' AND [Id] <> @Id)))
        BEGIN
            SET @MESSAGES = 'El código ' + @Code + ' ya está en uso.';
            SET @Status = 0;

            ROLLBACK TRANSACTION;
            SELECT @MESSAGES AS Messages, @Status AS Status, NULL AS Data;
            RETURN;
        END

        IF @Accion = 'INSERT'
        BEGIN

			SET @Id = NEWID();

            INSERT INTO [dbo].[GeneralParameter] 
            ([Id], [Code], [Description], [IsActive], [CreatedAt], [UserCreated], [UpdatedAt], [UserUpdated])
            VALUES 
            (@Id, @Code, @Description, @IsActive, @CreatedAt, @UserCreated, NULL, NULL);

            INSERT INTO [dbo].[GeneralParameterDetail] 
            (Id, GeneralParameterId, Code, Value1, Value2, Value3, Value4, Value5, CreatedAt, UserCreated)
            SELECT NEWID(), @Id, Code, Value1, Value2, Value3, Value4, Value5, GETDATE(), @UserCreated
            FROM @Detalles;

			SELECT @ListCodes = STRING_AGG(Code, ', ') 
            FROM [dbo].[GeneralParameterDetail]
            WHERE GeneralParameterId = @Id;

			SET @AuditDetails = CONCAT('Acción: ', @Accion, 'Detalles insertados con Code: ', @ListCodes);
            EXEC [dbo].[Sp_Insert_AuditLog]
                @Action = @Accion,
                @TableName = 'GeneralParameterDetail',
                @User = @UserCreated,
                @Details = @AuditDetails;

			SET @AuditDetails = CONCAT('Acción: ', @Accion, ', Code: ', @Code);
            EXEC [dbo].[Sp_Insert_AuditLog]
                @Action = @Accion,
                @TableName = 'GeneralParameter',
                @User = @UserCreated,
                @Details = @AuditDetails;

            SET @MESSAGES = 'Parámetro ' + @Code + ' creado exitosamente.';
            SET @Status = 1;
        END
        ELSE IF @Accion = 'UPDATE'
        BEGIN
            SET @UpdatedAt = GETDATE();

            UPDATE [dbo].[GeneralParameter]
            SET [Code] = @Code,
                [Description] = @Description,
                [IsActive] = @IsActive,
                [UpdatedAt] = @UpdatedAt,
                [UserUpdated] = @UserUpdated
            WHERE [Id] = @Id;

            DELETE FROM [dbo].[GeneralParameterDetail]
            WHERE GeneralParameterId = @Id
              AND Code IN (SELECT Code FROM @Detalles);

            SELECT @ListCodes = STRING_AGG(Code, ', ') 
            FROM [dbo].[GeneralParameterDetail]
            WHERE GeneralParameterId = @Id
              AND Code NOT IN (SELECT Code FROM @Detalles);

			SET @AuditDetails = CONCAT('Acción: DELETE', 'Se eliminaron los siguientes códigos de detalles: ', @ListCodes);
            EXEC [dbo].[Sp_Insert_AuditLog]
                @Action = 'DELETE',
                @TableName = 'GeneralParameterDetail',
                @User = @UserUpdated,
				@Details = @AuditDetails;

            -- Actualizar o insertar detalles
            DECLARE @DetailId UNIQUEIDENTIFIER;
            DECLARE @DetailCode NVARCHAR(10);
            DECLARE @Value1 NVARCHAR(100);
            DECLARE @Value2 NVARCHAR(100);
            DECLARE @Value3 NVARCHAR(100);
            DECLARE @Value4 NVARCHAR(100);
            DECLARE @Value5 NVARCHAR(100);

            DECLARE detalle_cursor CURSOR FOR
            SELECT Id, Code, Value1, Value2, Value3, Value4, Value5
            FROM @Detalles;

            OPEN detalle_cursor;
            FETCH NEXT FROM detalle_cursor INTO @DetailId, @DetailCode, @Value1, @Value2, @Value3, @Value4, @Value5;

            WHILE @@FETCH_STATUS = 0
            BEGIN
                IF EXISTS (SELECT 1 FROM [dbo].[GeneralParameterDetail]
                           WHERE Id = @DetailId AND GeneralParameterId = @Id)
                BEGIN
                    UPDATE [dbo].[GeneralParameterDetail]
                    SET Value1 = @Value1,
                        Value2 = @Value2,
                        Value3 = @Value3,
                        Value4 = @Value4,
                        Value5 = @Value5,
                        UpdatedAt = @UpdatedAt,
                        UserUpdated = @UserUpdated
                    WHERE Id = @DetailId;

					SET @AuditDetails = CONCAT('Acción: ', @Accion ,'Detalle actualizado con Codes: ', @DetailCode);

                    EXEC [dbo].[Sp_Insert_AuditLog]
                        @Action = @Accion,
                        @TableName = 'GeneralParameterDetail',
                        @User = @UserUpdated,
                        @Details = @AuditDetails;
                END
                ELSE
                BEGIN
                    -- Insertar nuevo registro si no existe
                    INSERT INTO [dbo].[GeneralParameterDetail] 
                    (Id, GeneralParameterId, Code, Value1, Value2, Value3, Value4, Value5, CreatedAt, UserCreated)
                    VALUES (NEWID(), @Id, @DetailCode, @Value1, @Value2, @Value3, @Value4, @Value5, @UpdatedAt, @UserUpdated);

					SET @AuditDetails = CONCAT('Acción: INSERT' ,'Nuevo detalle insertado con Code: ', @DetailCode);
                    EXEC [dbo].[Sp_Insert_AuditLog]
                        @Action = 'INSERT',
                        @TableName = 'GeneralParameterDetail',
                        @User = @UserUpdated,
                        @Details = @AuditDetails;
                END

                FETCH NEXT FROM detalle_cursor INTO @DetailId, @DetailCode, @Value1, @Value2, @Value3, @Value4, @Value5;
            END

            CLOSE detalle_cursor;
            DEALLOCATE detalle_cursor;

			SET @AuditDetails = CONCAT('Acción: ', @Accion, ', Code: ', @Code);
            EXEC [dbo].[Sp_Insert_AuditLog]
                @Action = @Accion,
                @TableName = 'GeneralParameter',
                @User = @UserUpdated,
                @Details = @AuditDetails;

            SET @MESSAGES = 'Parámetro ' + @Code + ' actualizado exitosamente.';
            SET @Status = 1;
        END
        ELSE
        BEGIN
            SET @MESSAGES = 'Acción no válida. Debe ser INSERT o UPDATE.';
            SET @Status = 0;
            ROLLBACK TRANSACTION;
            RETURN;
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
