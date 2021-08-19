
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/27/2021 18:40:31
-- Generated from EDMX file: F:\U\Calidad del Software\VentaSnacks\VentaSnacks\Models\VentaSnack.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VentaSnack];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Factura_Cliente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Factura] DROP CONSTRAINT [FK_Factura_Cliente];
GO
IF OBJECT_ID(N'[dbo].[FK_Factura_Estado]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Factura] DROP CONSTRAINT [FK_Factura_Estado];
GO
IF OBJECT_ID(N'[dbo].[FK_Factura_TipoPago]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Factura] DROP CONSTRAINT [FK_Factura_TipoPago];
GO
IF OBJECT_ID(N'[dbo].[FK_Linea_Articulo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Linea] DROP CONSTRAINT [FK_Linea_Articulo];
GO
IF OBJECT_ID(N'[dbo].[FK_LineaFactura_Factura]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineaFactura] DROP CONSTRAINT [FK_LineaFactura_Factura];
GO
IF OBJECT_ID(N'[dbo].[FK_LineaFactura_Linea]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LineaFactura] DROP CONSTRAINT [FK_LineaFactura_Linea];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Articulo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Articulo];
GO
IF OBJECT_ID(N'[dbo].[Cliente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cliente];
GO
IF OBJECT_ID(N'[dbo].[Estado]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Estado];
GO
IF OBJECT_ID(N'[dbo].[Factura]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Factura];
GO
IF OBJECT_ID(N'[dbo].[Linea]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Linea];
GO
IF OBJECT_ID(N'[dbo].[LineaFactura]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LineaFactura];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO
IF OBJECT_ID(N'[dbo].[TipoPago]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TipoPago];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Articuloes'
CREATE TABLE [dbo].[Articuloes] (
    [idArticulo] varchar(20)  NOT NULL,
    [nombre] varchar(50)  NOT NULL,
    [costo] decimal(19,4)  NOT NULL,
    [promocion] bit  NOT NULL,
    [imagen] varbinary(max)  NULL
);
GO

-- Creating table 'ArticuloFacturas'
CREATE TABLE [dbo].[ArticuloFacturas] (
    [idArticulo] varchar(20)  NOT NULL,
    [idFactura] int  NOT NULL,
    [cantidad] int  NOT NULL,
    [total] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'Clientes'
CREATE TABLE [dbo].[Clientes] (
    [idCliente] varchar(20)  NOT NULL,
    [nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'Estadoes'
CREATE TABLE [dbo].[Estadoes] (
    [idEstado] int  NOT NULL,
    [nombre] varchar(20)  NOT NULL
);
GO

-- Creating table 'Facturas'
CREATE TABLE [dbo].[Facturas] (
    [idFactura] int IDENTITY(1,1) NOT NULL,
    [idCliente] varchar(20)  NULL,
    [porcentajeDesc] decimal(4,2)  NULL,
    [idEstado] int  NOT NULL,
    [Total] decimal(19,4)  NOT NULL,
    [idTipoPago] int  NOT NULL,
    [fecha] datetime  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'TipoPagoes'
CREATE TABLE [dbo].[TipoPagoes] (
    [idTipoPago] int  NOT NULL,
    [nombre] varchar(30)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [idUser] varchar(50)  NOT NULL,
    [password] varchar(50)  NOT NULL,
    [nombre] varchar(50)  NOT NULL
);
GO

-- Creating table 'Lineas'
CREATE TABLE [dbo].[Lineas] (
    [idLinea] int IDENTITY(1,1) NOT NULL,
    [idArticulo] varchar(20)  NOT NULL,
    [cantidad] int  NOT NULL,
    [total] decimal(19,4)  NOT NULL
);
GO

-- Creating table 'LineaFactura'
CREATE TABLE [dbo].[LineaFactura] (
    [Facturas_idFactura] int  NOT NULL,
    [Lineas_idLinea] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idArticulo] in table 'Articuloes'
ALTER TABLE [dbo].[Articuloes]
ADD CONSTRAINT [PK_Articuloes]
    PRIMARY KEY CLUSTERED ([idArticulo] ASC);
GO

-- Creating primary key on [idArticulo], [idFactura] in table 'ArticuloFacturas'
ALTER TABLE [dbo].[ArticuloFacturas]
ADD CONSTRAINT [PK_ArticuloFacturas]
    PRIMARY KEY CLUSTERED ([idArticulo], [idFactura] ASC);
GO

-- Creating primary key on [idCliente] in table 'Clientes'
ALTER TABLE [dbo].[Clientes]
ADD CONSTRAINT [PK_Clientes]
    PRIMARY KEY CLUSTERED ([idCliente] ASC);
GO

-- Creating primary key on [idEstado] in table 'Estadoes'
ALTER TABLE [dbo].[Estadoes]
ADD CONSTRAINT [PK_Estadoes]
    PRIMARY KEY CLUSTERED ([idEstado] ASC);
GO

-- Creating primary key on [idFactura] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [PK_Facturas]
    PRIMARY KEY CLUSTERED ([idFactura] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [idTipoPago] in table 'TipoPagoes'
ALTER TABLE [dbo].[TipoPagoes]
ADD CONSTRAINT [PK_TipoPagoes]
    PRIMARY KEY CLUSTERED ([idTipoPago] ASC);
GO

-- Creating primary key on [idUser] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([idUser] ASC);
GO

-- Creating primary key on [idLinea] in table 'Lineas'
ALTER TABLE [dbo].[Lineas]
ADD CONSTRAINT [PK_Lineas]
    PRIMARY KEY CLUSTERED ([idLinea] ASC);
GO

-- Creating primary key on [Facturas_idFactura], [Lineas_idLinea] in table 'LineaFactura'
ALTER TABLE [dbo].[LineaFactura]
ADD CONSTRAINT [PK_LineaFactura]
    PRIMARY KEY CLUSTERED ([Facturas_idFactura], [Lineas_idLinea] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idArticulo] in table 'ArticuloFacturas'
ALTER TABLE [dbo].[ArticuloFacturas]
ADD CONSTRAINT [FK_ArticuloFactura_Articulo]
    FOREIGN KEY ([idArticulo])
    REFERENCES [dbo].[Articuloes]
        ([idArticulo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [idFactura] in table 'ArticuloFacturas'
ALTER TABLE [dbo].[ArticuloFacturas]
ADD CONSTRAINT [FK_ArticuloFactura_Factura]
    FOREIGN KEY ([idFactura])
    REFERENCES [dbo].[Facturas]
        ([idFactura])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ArticuloFactura_Factura'
CREATE INDEX [IX_FK_ArticuloFactura_Factura]
ON [dbo].[ArticuloFacturas]
    ([idFactura]);
GO

-- Creating foreign key on [idCliente] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_Factura_Cliente]
    FOREIGN KEY ([idCliente])
    REFERENCES [dbo].[Clientes]
        ([idCliente])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Factura_Cliente'
CREATE INDEX [IX_FK_Factura_Cliente]
ON [dbo].[Facturas]
    ([idCliente]);
GO

-- Creating foreign key on [idEstado] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_Factura_Estado]
    FOREIGN KEY ([idEstado])
    REFERENCES [dbo].[Estadoes]
        ([idEstado])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Factura_Estado'
CREATE INDEX [IX_FK_Factura_Estado]
ON [dbo].[Facturas]
    ([idEstado]);
GO

-- Creating foreign key on [idTipoPago] in table 'Facturas'
ALTER TABLE [dbo].[Facturas]
ADD CONSTRAINT [FK_Factura_TipoPago]
    FOREIGN KEY ([idTipoPago])
    REFERENCES [dbo].[TipoPagoes]
        ([idTipoPago])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Factura_TipoPago'
CREATE INDEX [IX_FK_Factura_TipoPago]
ON [dbo].[Facturas]
    ([idTipoPago]);
GO

-- Creating foreign key on [idArticulo] in table 'Lineas'
ALTER TABLE [dbo].[Lineas]
ADD CONSTRAINT [FK_Linea_Articulo]
    FOREIGN KEY ([idArticulo])
    REFERENCES [dbo].[Articuloes]
        ([idArticulo])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Linea_Articulo'
CREATE INDEX [IX_FK_Linea_Articulo]
ON [dbo].[Lineas]
    ([idArticulo]);
GO

-- Creating foreign key on [Facturas_idFactura] in table 'LineaFactura'
ALTER TABLE [dbo].[LineaFactura]
ADD CONSTRAINT [FK_LineaFactura_Factura]
    FOREIGN KEY ([Facturas_idFactura])
    REFERENCES [dbo].[Facturas]
        ([idFactura])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Lineas_idLinea] in table 'LineaFactura'
ALTER TABLE [dbo].[LineaFactura]
ADD CONSTRAINT [FK_LineaFactura_Linea]
    FOREIGN KEY ([Lineas_idLinea])
    REFERENCES [dbo].[Lineas]
        ([idLinea])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LineaFactura_Linea'
CREATE INDEX [IX_FK_LineaFactura_Linea]
ON [dbo].[LineaFactura]
    ([Lineas_idLinea]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------