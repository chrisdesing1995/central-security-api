-- Insertar un par�metro general
INSERT INTO GeneralParameter (Id, Code, Description, UserCreated)
VALUES (NEWID(), 'DOCUMENTTYPES', 'Tipos de documentos permitidos', 'System');

-- Insertar detalles asociados a este par�metro
DECLARE @GeneralParameterId UNIQUEIDENTIFIER = (SELECT Id FROM GeneralParameter WHERE Code = 'DOCUMENTTYPES');

INSERT INTO GeneralParameterDetail (Id, GeneralParameterId, Code, Value1, UserCreated)
VALUES 
    (NEWID(), @GeneralParameterId, 'CED', 'C�dula', 'System'),
    (NEWID(), @GeneralParameterId, 'RUC', 'RUC', 'System'),
    (NEWID(), @GeneralParameterId, 'PAS', 'Pasaporte', 'System');
