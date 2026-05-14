CREATE DATABASE db_Colegio;
GO

USE db_Colegio;
GO

CREATE TABLE [Estudiantes]
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Nombre] NVARCHAR(150) NOT NULL,
    [TipoPersona] NVARCHAR(150) NOT NULL,
    [Voto] BIT NOT NULL,
    [CostoMatricula] DECIMAL NOT NULL,
    [Descuento] INT NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL
);
GO

CREATE TABLE [Salones]
(
    [Id] INT PRIMARY KEY IDENTITY(1,1),
    [Codigo] NVARCHAR(150) NOT NULL,
    [CupoMax] INT NOT NULL,
    [CupoActual] INT NOT NULL,
    [Fecha] SMALLDATETIME NOT NULL,

    [Estudiante] INT NOT NULL REFERENCES [Estudiantes]([Id])
);
GO

CREATE TABLE [Historicos]
(
  [Id] INT NOT NULL IDENTITY(1, 1) PRIMARY KEY,

  [Usuario] NVARCHAR(100) NULL,
  [Tabla] NVARCHAR(100) NULL,
  [Accion] NVARCHAR(50) NULL,
  [RegistroId] INT NULL,

  [Descripcion] NVARCHAR(500) NOT NULL,
  [Cambios] NVARCHAR(500) NULL,
  [ValorAnterior] NVARCHAR(1000) NULL,
  [ValorNuevo] NVARCHAR(1000) NULL,

  [Origen] NVARCHAR(100) NULL,
  [Exitoso] BIT NOT NULL DEFAULT 1,
  [Error] NVARCHAR(1000) NULL,

  [Fecha] SMALLDATETIME NOT NULL
);
GO
