USE [master]
GO
/****** Object:  Database [FacturacionDb]    Script Date: 6/06/2017 4:22:58 p. m. ******/
CREATE DATABASE [FacturacionDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FacturacionUsers', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FacturacionUsers.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FacturacionUsers_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\FacturacionUsers_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FacturacionDb] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FacturacionDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FacturacionDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FacturacionDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FacturacionDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FacturacionDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FacturacionDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [FacturacionDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FacturacionDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FacturacionDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FacturacionDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FacturacionDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FacturacionDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FacturacionDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FacturacionDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FacturacionDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FacturacionDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FacturacionDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FacturacionDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FacturacionDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FacturacionDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FacturacionDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FacturacionDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FacturacionDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FacturacionDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FacturacionDb] SET  MULTI_USER 
GO
ALTER DATABASE [FacturacionDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FacturacionDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FacturacionDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FacturacionDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [FacturacionDb] SET DELAYED_DURABILITY = DISABLED 
GO
USE [FacturacionDb]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[nombre] [nvarchar](max) NULL,
	[direccion] [nvarchar](max) NULL,
	[tel_casa] [nvarchar](max) NULL,
	[Activo] [bit] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Categorias](
	[idCategoria] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](300) NULL,
	[imagen] [image] NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[idCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clientes](
	[nit] [varchar](50) NOT NULL,
	[nombre] [varchar](70) NOT NULL,
	[direccion] [varchar](70) NOT NULL,
	[telefono] [varchar](15) NULL,
	[email] [varchar](50) NULL,
	[creado] [datetime] NULL,
	[modificado] [datetime] NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[nit] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Compras]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Compras](
	[idCompra] [int] NOT NULL,
	[idFactura] [int] NOT NULL,
	[idProveedor] [varchar](50) NOT NULL,
	[total] [decimal](10, 2) NULL,
	[fecha] [date] NOT NULL,
	[modificado] [datetime] NULL,
	[usuario] [varchar](50) NULL,
 CONSTRAINT [PK__Compras__48B99DB78D8051A0] PRIMARY KEY CLUSTERED 
(
	[idCompra] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Cotizaciones]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Cotizaciones](
	[idCotizacion] [int] NOT NULL,
	[nitCliente] [varchar](50) NULL,
	[nombre] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NULL,
	[fecha] [date] NOT NULL,
	[subTotal] [decimal](10, 2) NULL,
	[descuento] [decimal](5, 2) NULL,
	[total] [decimal](9, 2) NULL,
	[usuario] [varchar](50) NULL,
	[estado] [varchar](50) NULL,
 CONSTRAINT [PK_Facturas] PRIMARY KEY CLUSTERED 
(
	[idCotizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DetallesCompra]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DetallesCompra](
	[idCompra] [int] NOT NULL,
	[idDetalle] [int] NOT NULL,
	[idProducto] [varchar](50) NOT NULL,
	[cantidad] [decimal](10, 2) NOT NULL,
	[precio] [decimal](10, 2) NULL,
	[precioVenta] [decimal](10, 2) NULL,
	[descuento] [decimal](2, 2) NULL,
	[subTotal] [decimal](10, 2) NULL,
	[observaciones] [varchar](200) NULL,
 CONSTRAINT [PK__Detalles__EC253398662E0C22] PRIMARY KEY CLUSTERED 
(
	[idCompra] ASC,
	[idDetalle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DetallesCotizacion]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DetallesCotizacion](
	[idDetalle] [int] NOT NULL,
	[idCotizacion] [int] NOT NULL,
	[idProducto] [varchar](50) NOT NULL,
	[cantidad] [decimal](9, 2) NOT NULL,
	[precio] [decimal](9, 2) NOT NULL,
	[descuento] [decimal](2, 2) NULL,
	[subTotal] [decimal](9, 2) NULL,
 CONSTRAINT [PK_Detalles] PRIMARY KEY CLUSTERED 
(
	[idDetalle] ASC,
	[idCotizacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Facturas](
	[idFactura] [int] NOT NULL,
	[nitCliente] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NULL,
	[fecha] [date] NOT NULL,
	[subTotal] [decimal](9, 2) NULL,
	[descuento] [decimal](9, 2) NULL,
	[total] [decimal](9, 2) NULL,
	[idCotizacion] [int] NULL,
	[usuario] [varchar](75) NULL,
	[tipoPago] [int] NULL,
	[idPago] [varchar](50) NULL,
	[iva] [decimal](2, 2) NULL,
 CONSTRAINT [PK_Facturas_1] PRIMARY KEY CLUSTERED 
(
	[idFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Productos](
	[idProducto] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[precioCompra] [decimal](10, 2) NULL,
	[precio] [decimal](9, 2) NOT NULL,
	[descuentoVenta] [decimal](2, 2) NULL,
	[existencia] [decimal](9, 2) NULL,
	[observacion] [varchar](200) NULL,
	[idCategoria] [int] NULL,
	[imagen] [image] NULL,
	[creado] [datetime] NULL,
	[modificado] [datetime] NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Proveedores](
	[idProveedor] [varchar](50) NOT NULL,
	[empresa] [varchar](50) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NOT NULL,
	[telefono] [varchar](15) NOT NULL,
	[email] [varchar](50) NULL,
	[creado] [datetime] NULL,
	[modificado] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[idProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoPago]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TipoPago](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [varchar](100) NULL,
 CONSTRAINT [PK_TipoPago] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 6/06/2017 4:23:00 p. m. ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Compras]  WITH CHECK ADD  CONSTRAINT [FK__Compras__idProve__45F365D3] FOREIGN KEY([idProveedor])
REFERENCES [dbo].[Proveedores] ([idProveedor])
GO
ALTER TABLE [dbo].[Compras] CHECK CONSTRAINT [FK__Compras__idProve__45F365D3]
GO
ALTER TABLE [dbo].[DetallesCompra]  WITH CHECK ADD  CONSTRAINT [FK_DetallesCompra_Compras] FOREIGN KEY([idCompra])
REFERENCES [dbo].[Compras] ([idCompra])
GO
ALTER TABLE [dbo].[DetallesCompra] CHECK CONSTRAINT [FK_DetallesCompra_Compras]
GO
ALTER TABLE [dbo].[DetallesCompra]  WITH CHECK ADD  CONSTRAINT [FK_DetallesCompra_Productos] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Productos] ([idProducto])
GO
ALTER TABLE [dbo].[DetallesCompra] CHECK CONSTRAINT [FK_DetallesCompra_Productos]
GO
ALTER TABLE [dbo].[DetallesCotizacion]  WITH CHECK ADD  CONSTRAINT [FK_Detalles_Productos] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Productos] ([idProducto])
GO
ALTER TABLE [dbo].[DetallesCotizacion] CHECK CONSTRAINT [FK_Detalles_Productos]
GO
ALTER TABLE [dbo].[DetallesCotizacion]  WITH CHECK ADD  CONSTRAINT [FK_DetallesCotizacion_Cotizaciones] FOREIGN KEY([idCotizacion])
REFERENCES [dbo].[Cotizaciones] ([idCotizacion])
GO
ALTER TABLE [dbo].[DetallesCotizacion] CHECK CONSTRAINT [FK_DetallesCotizacion_Cotizaciones]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Clientes] FOREIGN KEY([nitCliente])
REFERENCES [dbo].[Clientes] ([nit])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Clientes]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_Cotizaciones] FOREIGN KEY([idCotizacion])
REFERENCES [dbo].[Cotizaciones] ([idCotizacion])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_Cotizaciones]
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD  CONSTRAINT [FK_Facturas_TipoPago] FOREIGN KEY([tipoPago])
REFERENCES [dbo].[TipoPago] ([id])
GO
ALTER TABLE [dbo].[Facturas] CHECK CONSTRAINT [FK_Facturas_TipoPago]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias] FOREIGN KEY([idCategoria])
REFERENCES [dbo].[Categorias] ([idCategoria])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias]
GO
/****** Object:  StoredProcedure [dbo].[sp_ComprasDia]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_ComprasDia](@fecha date)
as
begin

	SELECT count(c.nombre) FROM Cotizaciones c
	where c.fecha like @fecha	
end
GO
/****** Object:  StoredProcedure [dbo].[sp_CotizacionesDia]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_CotizacionesDia](@fecha date)
as
begin

	SELECT count(c.nombre) FROM Cotizaciones c
	where c.fecha like @fecha	and c.estado='Cotizado' 
end

GO
/****** Object:  StoredProcedure [dbo].[sp_FacturasDia]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_FacturasDia](@fecha date)
as
begin

	SELECT count(f.nombre) FROM Facturas f
	where f.fecha like @fecha	
end

GO
/****** Object:  StoredProcedure [dbo].[sp_VentasDia]    Script Date: 6/06/2017 4:23:00 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[sp_VentasDia](@fecha date)
as
begin

	SELECT count(c.nombre) FROM Cotizaciones c
	where c.fecha like @fecha	and c.estado='Vendido' 
end

GO
USE [master]
GO
ALTER DATABASE [FacturacionDb] SET  READ_WRITE 
GO
