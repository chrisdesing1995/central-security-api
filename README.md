# Central Security API

## Descripción

El repositorio `central-security-api` es el núcleo del sistema de autenticación y seguridad para la plataforma. Esta API centralizada gestiona la autenticación, autorización, y la administración de usuarios, roles y permisos, además de proporcionar servicios para el registro y auditoría de acciones en los diferentes módulos de la plataforma. La API utiliza JWT (JSON Web Tokens) para la autenticación y autorización segura de los usuarios.

## Características

- **Gestión de Usuarios**: Registro, actualización y eliminación de usuarios.
- **Autenticación y Autorización**: Generación y validación de JWT para asegurar el acceso a los servicios.
- **Roles y Permisos**: Administración centralizada de roles y permisos para controlar el acceso a las funcionalidades de la plataforma.
- **Auditoría**: Registro centralizado de eventos y acciones para monitoreo y análisis.
  
## Endpoints Principales

### Autenticación
- `POST /auth/login`: Autentica a un usuario y genera un JWT.
- `POST /auth/register`: Registra un nuevo usuario en el sistema.
- `POST /auth/logout`: Invalida el token de sesión actual.

### Gestión de Usuarios
- `GET /users`: Obtiene la lista de usuarios registrados.
- `POST /users`: Crea un nuevo usuario.
- `PUT /users/{id}`: Actualiza la información de un usuario.
- `DELETE /users/{id}`: Elimina un usuario.

### Roles y Permisos
- `GET /roles`: Lista de roles disponibles en el sistema.
- `POST /roles`: Crea un nuevo rol.
- `GET /permissions`: Obtiene la lista de permisos configurados.
- `POST /permissions`: Configura permisos para roles específicos.

### Auditoría
- `POST /audit`: Registra un evento de auditoría en el sistema.
- `GET /audit`: Obtiene registros de auditoría para análisis.

## Instalación

### Requisitos Previos
- .NET Core 6 o superior
- SQL Server

### Pasos de Instalación
1. Clonar el repositorio:
   ```bash
   git clone https://github.com/tu-usuario/central-security-api.git
