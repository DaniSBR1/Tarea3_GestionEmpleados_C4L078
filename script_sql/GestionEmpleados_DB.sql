
-- 1. Crear la base de datos (si no existe)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'GestionEmpleados')
BEGIN
    CREATE DATABASE GestionEmpleados;
END
GO

USE GestionEmpleados;
GO

-- 2. Tabla de historial de migraciones EF Core
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type = N'U')
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId]    NVARCHAR(150) NOT NULL,
        [ProductVersion] NVARCHAR(32)  NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
    );
END
GO

-- 3. Tabla principal Empleados
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Empleados]') AND type = N'U')
BEGIN
    CREATE TABLE [dbo].[Empleados] (
        [Id]           INT            NOT NULL IDENTITY(1,1),
        [Nombre]       NVARCHAR(80)   NOT NULL,
        [Apellidos]    NVARCHAR(100)  NOT NULL,
        [Departamento] NVARCHAR(100)  NOT NULL,
        [Salario]      DECIMAL(18,2)  NOT NULL,
        [FechaIngreso] DATETIME2      NOT NULL,
        [Activo]       BIT            NOT NULL DEFAULT 1,
        CONSTRAINT [PK_Empleados] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
    PRINT 'Tabla Empleados creada correctamente.';
END
ELSE
BEGIN
    PRINT 'Tabla Empleados ya existe.';
END
GO


-- 4. Registrar la migración en el historial EF Core
IF NOT EXISTS (SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20260101000000_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260101000000_InitialCreate', N'10.0.0');
END
GO

-- ============================================================
-- Verificación final
-- ============================================================
SELECT
    [Id],
    [Nombre] + ' ' + [Apellidos]   AS NombreCompleto,
    [Departamento],
    FORMAT([Salario], 'N0')        AS Salario,
    FORMAT([FechaIngreso], 'dd/MM/yyyy') AS FechaIngreso,
    CASE [Activo] WHEN 1 THEN 'Activo' ELSE 'Dado de Baja' END AS Estado
FROM [dbo].[Empleados]
ORDER BY [Apellidos], [Nombre];
GO

PRINT '== Script ejecutado exitosamente ==';
GO
