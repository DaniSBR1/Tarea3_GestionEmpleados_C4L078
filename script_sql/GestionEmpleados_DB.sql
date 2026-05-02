-- ============================================================
-- Script SQL  |  Sistema de Gestión de Empleados
-- Base de datos: GestionEmpleados
-- SQL Server Express
-- Semana 6 – Lenguajes para Aplicaciones Comerciales, UCR
-- ============================================================

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

-- 4. Índice para mejorar búsquedas por Departamento
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = N'IX_Empleados_Departamento' AND object_id = OBJECT_ID(N'[dbo].[Empleados]'))
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Empleados_Departamento]
    ON [dbo].[Empleados] ([Departamento] ASC);
END
GO

-- 5. Seed de datos de prueba (10 empleados)
IF NOT EXISTS (SELECT TOP 1 1 FROM [dbo].[Empleados])
BEGIN
    SET IDENTITY_INSERT [dbo].[Empleados] ON;

    INSERT INTO [dbo].[Empleados] ([Id], [Nombre], [Apellidos], [Departamento], [Salario], [FechaIngreso], [Activo])
    VALUES
        (1,  N'Carlos',  N'Ramírez Solano',   N'TI',               1200000.00, '2020-03-15', 1),
        (2,  N'Ana',     N'González Mora',    N'TI',               1100000.00, '2021-06-01', 1),
        (3,  N'Laura',   N'Vargas Pérez',     N'TI',                980000.00, '2022-01-10', 1),
        (4,  N'Jorge',   N'Méndez Castillo',  N'Finanzas',         1500000.00, '2019-08-20', 1),
        (5,  N'María',   N'Fernández López',  N'Finanzas',         1350000.00, '2020-11-05', 1),
        (6,  N'Andrés',  N'Jiménez Arias',    N'Recursos Humanos',  900000.00, '2023-02-14', 1),
        (7,  N'Sofía',   N'Castro Brenes',    N'Recursos Humanos',  850000.00, '2023-07-03', 0),
        (8,  N'Diego',   N'Alvarado Rojas',   N'Operaciones',       760000.00, '2021-04-22', 1),
        (9,  N'Valeria', N'Herrera Núñez',    N'Operaciones',       720000.00, '2022-09-18', 1),
        (10, N'Roberto', N'Quesada Elizondo', N'Gerencia',         3500000.00, '2018-01-07', 1);

    SET IDENTITY_INSERT [dbo].[Empleados] OFF;

    PRINT '10 empleados insertados correctamente.';
END
ELSE
BEGIN
    PRINT 'Datos de seed ya existen.';
END
GO

-- 6. Registrar la migración en el historial EF Core
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
