-- Insertar roles
INSERT INTO [Role] (Id, RoleName, Description, CreatedAt, UserCreated)
VALUES 
(NEWID(), 'Admin', 'Administrator role with full permissions', GETDATE(), 'system'),
(NEWID(), 'Manager', 'Role for managers with limited permissions', GETDATE(), 'system'),
(NEWID(), 'User', 'Regular user role with basic permissions', GETDATE(), 'system');


-- Insertar usuarios
INSERT INTO [User] (Id, Username, Password, Email, IsActive, CreatedAt, UserCreated)
VALUES 
(NEWID(), 'adminUser', 'adminPasswordHash', 'admin@example.com', 'A', GETDATE(), 'system'),
(NEWID(), 'managerUser', 'managerPasswordHash', 'manager@example.com', 'A', GETDATE(), 'system'),
(NEWID(), 'regularUser', 'userPasswordHash', 'user@example.com', 'A', GETDATE(), 'system');


-- Insertar relaciones entre usuarios y roles
INSERT INTO UserRole (UserId, RoleId)
VALUES 
((SELECT Id FROM [User] WHERE Username = 'adminUser'), (SELECT Id FROM [Role] WHERE RoleName = 'Admin')),
((SELECT Id FROM [User] WHERE Username = 'managerUser'), (SELECT Id FROM [Role] WHERE RoleName = 'Manager')),
((SELECT Id FROM [User] WHERE Username = 'regularUser'), (SELECT Id FROM [Role] WHERE RoleName = 'User'));


-- Insertar ítems padres
INSERT INTO [Menu] (Id, MenuName, ParentId, Url, Icon, SortOrder, IsActive, CreatedAt, UserCreated)
VALUES
(NEWID(), 'Seguridad', NULL, NULL, 'icon-seguridad', 1, 'A', GETDATE(), 'system'),
(NEWID(), 'Inventario', NULL, NULL, 'icon-inventario', 2, 'A', GETDATE(), 'system'),
(NEWID(), 'Facturación', NULL, NULL, 'icon-facturacion', 3, 'A', GETDATE(), 'system'),
(NEWID(), 'Configuración', NULL, NULL, 'icon-configuracion', 4, 'A', GETDATE(), 'system');

-- Insertar ítems hijos bajo 'Seguridad'
INSERT INTO [Menu] (Id, MenuName, ParentId, Url, Icon, SortOrder, IsActive, CreatedAt, UserCreated)
VALUES
(NEWID(), 'Usuario', (SELECT Id FROM [Menu] WHERE MenuName = 'Seguridad'), '/seguridad/usuario', 'icon-usuario', 1, 'A', GETDATE(), 'system'),
(NEWID(), 'Roles', (SELECT Id FROM [Menu] WHERE MenuName = 'Seguridad'), '/seguridad/roles', 'icon-roles', 2, 'A', GETDATE(), 'system'),
(NEWID(), 'Menú', (SELECT Id FROM [Menu] WHERE MenuName = 'Seguridad'), '/seguridad/menu', 'icon-menu', 3, 'A', GETDATE(), 'system');

-- Insertar ítems hijos bajo 'Inventario'
INSERT INTO [Menu] (Id, MenuName, ParentId, Url, Icon, SortOrder, IsActive, CreatedAt, UserCreated)
VALUES
(NEWID(), 'Proveedores', (SELECT Id FROM [Menu] WHERE MenuName = 'Inventario'), '/inventario/proveedores', 'icon-proveedores', 1, 'A', GETDATE(), 'system'),
(NEWID(), 'Productos', (SELECT Id FROM [Menu] WHERE MenuName = 'Inventario'), '/inventario/productos', 'icon-productos', 2, 'A', GETDATE(), 'system'),
(NEWID(), 'Bodegas', (SELECT Id FROM [Menu] WHERE MenuName = 'Inventario'), '/inventario/bodegas', 'icon-bodegas', 3, 'A', GETDATE(), 'system'),
(NEWID(), 'Inventario (Ingresos/Egresos)', (SELECT Id FROM [Menu] WHERE MenuName = 'Inventario'), '/inventario/movimientos', 'icon-movimientos', 4, 'A', GETDATE(), 'system');

-- Insertar ítems hijos bajo 'Facturación'
INSERT INTO [Menu] (Id, MenuName, ParentId, Url, Icon, SortOrder, IsActive, CreatedAt, UserCreated)
VALUES
(NEWID(), 'Clientes', (SELECT Id FROM [Menu] WHERE MenuName = 'Facturación'), '/facturacion/clientes', 'icon-clientes', 1, 'A', GETDATE(), 'system'),
(NEWID(), 'Orden de Venta', (SELECT Id FROM [Menu] WHERE MenuName = 'Facturación'), '/facturacion/orden-venta', 'icon-orden-venta', 2, 'A', GETDATE(), 'system'),
(NEWID(), 'Orden de Compra', (SELECT Id FROM [Menu] WHERE MenuName = 'Facturación'), '/facturacion/orden-compra', 'icon-orden-compra', 3, 'A', GETDATE(), 'system');

-- Insertar ítems hijos bajo 'Configuración'
INSERT INTO [Menu] (Id, MenuName, ParentId, Url, Icon, SortOrder, IsActive, CreatedAt, UserCreated)
VALUES
(NEWID(), 'Parámetros del Sistema', (SELECT Id FROM [Menu] WHERE MenuName = 'Configuración'), '/configuracion/parametros-sistema', 'icon-parametros-sistema', 1, 'A', GETDATE(), 'system'),
(NEWID(), 'Parámetros Generales', (SELECT Id FROM [Menu] WHERE MenuName = 'Configuración'), '/configuracion/parametros-generales', 'icon-parametros-generales', 2, 'A', GETDATE(), 'system');


-- Asignar todos los ítems de menú al rol 'Admin'
INSERT INTO RoleMenu (RoleId, MenuId)
SELECT (SELECT Id FROM [Role] WHERE RoleName = 'Admin'), Id FROM [Menu];

-- Asignar los ítems de menú al rol 'Manager' para 'Inventario' y 'Facturación'
INSERT INTO RoleMenu (RoleId, MenuId)
SELECT (SELECT Id FROM [Role] WHERE RoleName = 'Manager'), Id FROM [Menu]
WHERE MenuName = 'Inventario'
   OR ParentId = (SELECT Id FROM [Menu] WHERE MenuName = 'Inventario')
   OR MenuName = 'Facturación'
   OR ParentId = (SELECT Id FROM [Menu] WHERE MenuName = 'Facturación');

-- Asignar los ítems de menú al rol 'User' solo para 'Facturación'
INSERT INTO RoleMenu (RoleId, MenuId)
SELECT (SELECT Id FROM [Role] WHERE RoleName = 'User'), Id FROM [Menu]
WHERE MenuName = 'Facturación'
   OR ParentId = (SELECT Id FROM [Menu] WHERE MenuName = 'Facturación');
