CREATE DATABASE [turnos_db_2]
GO
USE [turnos_db_2]
GO


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/** Object:  Table [dbo].[T_DETALLES_TURNO]    Script Date: 27-09-2024 17:12:07 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_DETALLES_TURNO](
	[id_turno] [int] NOT NULL,
	[id_servicio] [int] NOT NULL,
	[observaciones] [varchar](200) NULL,
 CONSTRAINT [PK_T_DETALLES_TURNO] PRIMARY KEY CLUSTERED 
(
	[id_turno] ASC,
	[id_servicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/** Object:  Table [dbo].[T_SERVICIOS]    Script Date: 27-09-2024 17:12:07 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_SERVICIOS](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[costo] [int] NOT NULL,
	[enPromocion] [varchar](1) NOT NULL,
 CONSTRAINT [PK_T_SERVICIOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/** Object:  Table [dbo].[T_TURNOS]    Script Date: 27-09-2024 17:12:07 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_TURNOS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [varchar](10) NULL,
	[hora] [varchar](5) NULL,
	[cliente] [varchar](100) NULL,
	[fecha_cancelacion] [datetime2](7) NULL,
	[motivo_cancelacion] [nvarchar](max) NULL,
 CONSTRAINT [PK_T_TURNOS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240925001026_Initial', N'8.0.8')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240925001102_UpdateProperties', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[T_TURNOS] ON 

INSERT [dbo].[T_TURNOS] ([id], [fecha], [hora], [cliente], [fecha_cancelacion], [motivo_cancelacion]) VALUES (1, N'24/09/2024', N'21:33', N'Juancito', CAST(N'2024-09-24T21:44:03.6068444' AS DateTime2), N'se cancelo el turno')
INSERT [dbo].[T_TURNOS] ([id], [fecha], [hora], [cliente], [fecha_cancelacion], [motivo_cancelacion]) VALUES (2, N'29/08/2024', N'08:20', N'Oscar', NULL, NULL)
SET IDENTITY_INSERT [dbo].[T_TURNOS] OFF
GO