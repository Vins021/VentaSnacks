USE [VentaSnack]
GO
/****** Object:  Table [dbo].[Articulo]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulo](
	[idArticulo] [varchar](20) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[costo] [money] NOT NULL,
	[promocion] [bit] NOT NULL,
	[imagen] [varbinary](max) NULL,
 CONSTRAINT [PK_Articulo] PRIMARY KEY CLUSTERED 
(
	[idArticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[idCliente] [varchar](20) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[idCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estado](
	[idEstado] [int] NOT NULL,
	[nombre] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
(
	[idEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[idFactura] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [varchar](20) NULL,
	[porcentajeDesc] [decimal](4, 2) NULL,
	[idEstado] [int] NOT NULL,
	[Total] [money] NOT NULL,
	[idTipoPago] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Linea]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Linea](
	[idLinea] [int] IDENTITY(1,1) NOT NULL,
	[idArticulo] [varchar](20) NOT NULL,
	[cantidad] [int] NOT NULL,
	[total] [money] NOT NULL,
 CONSTRAINT [PK_Linea] PRIMARY KEY CLUSTERED 
(
	[idLinea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LineaFactura]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LineaFactura](
	[idLinea] [int] NOT NULL,
	[idFactura] [int] NOT NULL,
 CONSTRAINT [PK_LineaFactura] PRIMARY KEY CLUSTERED 
(
	[idLinea] ASC,
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoPago]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoPago](
	[idTipoPago] [int] NOT NULL,
	[nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_TipoPago] PRIMARY KEY CLUSTERED 
(
	[idTipoPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 6/29/2021 7:37:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[idUser] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[idUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Articulo] ([idArticulo], [nombre], [costo], [promocion], [imagen]) VALUES (N'1', N'Papas', 700.0000, 0, NULL)
INSERT [dbo].[Articulo] ([idArticulo], [nombre], [costo], [promocion], [imagen]) VALUES (N'2', N'Bolis', 300.0000, 0, NULL)
INSERT [dbo].[Articulo] ([idArticulo], [nombre], [costo], [promocion], [imagen]) VALUES (N'3', N'Gelatinas', 500.0000, 1, NULL)
INSERT [dbo].[Articulo] ([idArticulo], [nombre], [costo], [promocion], [imagen]) VALUES (N'4', N'Coca', 1000.0000, 0, NULL)
INSERT [dbo].[Articulo] ([idArticulo], [nombre], [costo], [promocion], [imagen]) VALUES (N'5', N'Agua', 450.0000, 0, NULL)
GO
INSERT [dbo].[Cliente] ([idCliente], [nombre]) VALUES (N'206560371', N'Jonathan Rodríguez')
GO
INSERT [dbo].[Estado] ([idEstado], [nombre]) VALUES (0, N'Pendiente')
INSERT [dbo].[Estado] ([idEstado], [nombre]) VALUES (1, N'Aprobada')
INSERT [dbo].[Estado] ([idEstado], [nombre]) VALUES (2, N'Cancelada')
GO
SET IDENTITY_INSERT [dbo].[Linea] ON 

INSERT [dbo].[Linea] ([idLinea], [idArticulo], [cantidad], [total]) VALUES (1, N'5', 0, 0.0000)
INSERT [dbo].[Linea] ([idLinea], [idArticulo], [cantidad], [total]) VALUES (2, N'5', 1, 0.0000)
SET IDENTITY_INSERT [dbo].[Linea] OFF
GO
INSERT [dbo].[TipoPago] ([idTipoPago], [nombre]) VALUES (0, N'Tarjeta')
INSERT [dbo].[TipoPago] ([idTipoPago], [nombre]) VALUES (1, N'Efectivo')
GO
INSERT [dbo].[User] ([idUser], [password], [nombre]) VALUES (N'206560371', N'321', N'Jonathan Rodríguez')
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([idCliente])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Estado] FOREIGN KEY([idEstado])
REFERENCES [dbo].[Estado] ([idEstado])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Estado]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_TipoPago] FOREIGN KEY([idTipoPago])
REFERENCES [dbo].[TipoPago] ([idTipoPago])
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_TipoPago]
GO
ALTER TABLE [dbo].[Linea]  WITH CHECK ADD  CONSTRAINT [FK_Linea_Articulo] FOREIGN KEY([idArticulo])
REFERENCES [dbo].[Articulo] ([idArticulo])
GO
ALTER TABLE [dbo].[Linea] CHECK CONSTRAINT [FK_Linea_Articulo]
GO
ALTER TABLE [dbo].[LineaFactura]  WITH CHECK ADD  CONSTRAINT [FK_LineaFactura_Factura] FOREIGN KEY([idFactura])
REFERENCES [dbo].[Factura] ([idFactura])
GO
ALTER TABLE [dbo].[LineaFactura] CHECK CONSTRAINT [FK_LineaFactura_Factura]
GO
ALTER TABLE [dbo].[LineaFactura]  WITH CHECK ADD  CONSTRAINT [FK_LineaFactura_Linea] FOREIGN KEY([idLinea])
REFERENCES [dbo].[Linea] ([idLinea])
GO
ALTER TABLE [dbo].[LineaFactura] CHECK CONSTRAINT [FK_LineaFactura_Linea]
GO
