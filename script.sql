USE [master]
GO
/****** Object:  Database [DiplomBetaDB]    Script Date: 11.05.2023 23:50:35 ******/
CREATE DATABASE [DiplomBetaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiplomBetaDB', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DiplomBetaDB.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DiplomBetaDB_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\DiplomBetaDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DiplomBetaDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiplomBetaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiplomBetaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiplomBetaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiplomBetaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DiplomBetaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiplomBetaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DiplomBetaDB] SET  MULTI_USER 
GO
ALTER DATABASE [DiplomBetaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiplomBetaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiplomBetaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiplomBetaDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DiplomBetaDB] SET DELAYED_DURABILITY = DISABLED 
GO
USE [DiplomBetaDB]
GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Phone] [nvarchar](11) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Carrier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](50) NOT NULL,
	[Address] [varchar](200) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[TypeId] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Point]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Point](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdClient] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[ProductCount] [int] NOT NULL,
	[IdTask] [int] NOT NULL,
	[Position] [int] NOT NULL,
	[Address] [varchar](200) NULL,
 CONSTRAINT [PK_Point] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceCarrier]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceCarrier](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdService] [int] NOT NULL,
	[IdCarrier] [int] NOT NULL,
	[Cost] [int] NOT NULL,
 CONSTRAINT [PK_ServiceCarrier] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Status]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Status](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StatusId] [int] NOT NULL,
	[Cost] [int] NULL,
	[Conclusion] [varchar](max) NULL,
	[CountRow] [int] NULL,
	[CountColumn] [int] NULL,
	[UserId] [int] NULL,
	[CarrierId] [int] NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskService]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskService](
	[IdTask] [int] NOT NULL,
	[IdService] [int] NOT NULL,
 CONSTRAINT [PK_TaskCost] PRIMARY KEY CLUSTERED 
(
	[IdTask] ASC,
	[IdService] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransportationCost]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransportationCost](
	[IdPosition] [int] NOT NULL,
	[IdTask] [int] NOT NULL,
	[Value] [int] NOT NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_TransportationCost_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeClient]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeClient](
	[id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeClient] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeConstraint]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeConstraint](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_TypeConstraint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Login] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Email] [varchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Сonstraint]    Script Date: 11.05.2023 23:50:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Сonstraint](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTask] [int] NOT NULL,
	[TypeConstraint] [int] NOT NULL,
	[ProductCount] [int] NULL,
	[IdPoints] [varchar](50) NULL,
 CONSTRAINT [PK_Сonstraint] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Carrier] ON 

INSERT [dbo].[Carrier] ([Id], [Name], [Address], [Phone], [Email]) VALUES (8, N'Перевозчик321', N'цммвй', N'12345678910', N'цукмцу')
INSERT [dbo].[Carrier] ([Id], [Name], [Address], [Phone], [Email]) VALUES (9, N'Поставщик', N'йцсу', N'12с', N'12ус')
INSERT [dbo].[Carrier] ([Id], [Name], [Address], [Phone], [Email]) VALUES (10, N'Новый поставщик', N'zxc', N'уцс', N'уйцс')
SET IDENTITY_INSERT [dbo].[Carrier] OFF
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (10, N'Заказчик 1', N'qdwd', N'qwd', 1)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (11, N'Заказчик 2', N'йцу', N'цуй', 1)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (12, N'Заказчик 3', N'ячва', N'вфыв', 2)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (13, N'Поставщик 1', N'йзвл', N'йцвз', 2)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (15, N'Поставщик 2', N'йцуу', N'цйуйц', 2)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (16, N'Поставщик 3', N'йцувй', N'йцвйц', 2)
INSERT [dbo].[Client] ([Id], [CompanyName], [Address], [Email], [TypeId]) VALUES (22, N'Новый клиент-заказчик', N'Нормальный адрес', N'йцвс', 1)
SET IDENTITY_INSERT [dbo].[Client] OFF
SET IDENTITY_INSERT [dbo].[Point] ON 

INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (3, 13, N'Склад у моря', 15, 1, 1, N'Да')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (4, 16, N'Склад (крутой)', 20, 1, 2, N'Дом Угу')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (70, 10, N'Магазин 1', 55, 1, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (80, 11, N'Магазин Здоровский', 15, 1, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (92, 10, N'Магазин 3', 30, 1, 3, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (94, 10, N'Магазин 1', 10, 4, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (95, 10, N'Магазин 2', 200, 4, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (96, 13, N'Склад 1', 30, 4, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (97, 13, N'Склад 2', 500, 4, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (98, 10, N'Магазин 3', 20, 4, 3, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (109, 10, N'Магазин 15', 90, 8, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (110, 10, N'Магазин 2', 203, 8, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (111, 13, N'Склад 1', 10, 8, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (112, 13, N'Склад 2', 524, 8, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (115, 13, N'Склад 3', 60, 8, 3, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (116, 13, N'Склад 1', 0, 10, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (117, 13, N'Склад 2', 0, 10, 2, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (118, 22, N'Магазин 1', 0, 10, 1, N'')
INSERT [dbo].[Point] ([Id], [IdClient], [Name], [ProductCount], [IdTask], [Position], [Address]) VALUES (119, 22, N'Магазин 2', 0, 10, 2, N'')
SET IDENTITY_INSERT [dbo].[Point] OFF
SET IDENTITY_INSERT [dbo].[Service] ON 

INSERT [dbo].[Service] ([Id], [Name]) VALUES (1, N'Перевозка')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (2, N'Такелажные работы')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (3, N'Погрузка-Разгрузка')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (4, N'Страховка груза')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (5, N'Ответственное хранение')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (6, N'Срочная доставка')
INSERT [dbo].[Service] ([Id], [Name]) VALUES (7, N'Мягкая упаковка')
SET IDENTITY_INSERT [dbo].[Service] OFF
SET IDENTITY_INSERT [dbo].[ServiceCarrier] ON 

INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (13, 1, 8, 50)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (15, 3, 8, 150)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (16, 1, 9, 100)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (17, 2, 9, 200)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (18, 6, 9, 500)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (19, 1, 10, 200)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (20, 4, 10, 1000)
INSERT [dbo].[ServiceCarrier] ([Id], [IdService], [IdCarrier], [Cost]) VALUES (21, 6, 10, 500)
SET IDENTITY_INSERT [dbo].[ServiceCarrier] OFF
INSERT [dbo].[Status] ([Id], [Name]) VALUES (1, N'Cоздана')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (2, N'Обработана')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (3, N'Отправлена')
INSERT [dbo].[Status] ([Id], [Name]) VALUES (4, N'Выполнена')
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([Id], [StatusId], [Cost], [Conclusion], [CountRow], [CountColumn], [UserId], [CarrierId]) VALUES (1, 1, 152, N'Для минимальной стоимости перевозки груза необходимо осуществить следующие поставки: 
       осуществить поставку груза из пункта «Склад у моря» в пункт «Магазин ауе» в размере 15 единиц;
       осуществить поставку груза из пункта «Склад (крутой)» в пункт «Магазин 1» в размере 20 единиц;
       осуществить поставку груза из пункта «Склад 3» в пункт «Магазин 1» в размере 6 единиц;

Стоимость провоза: 152', 2, 3, 1, NULL)
INSERT [dbo].[Task] ([Id], [StatusId], [Cost], [Conclusion], [CountRow], [CountColumn], [UserId], [CarrierId]) VALUES (4, 1, 890, N'Для минимальной стоимости перевозки груза необходимо осуществить следующие поставки: 
       осуществить поставку груза из пункта «Склад 1» в пункт «Магазин 1» в размере 10 единиц;
       осуществить поставку груза из пункта «Склад 1» в пункт «Магазин 2» в размере 20 единиц;
       осуществить поставку груза из пункта «Склад 2» в пункт «Магазин 2» в размере 180 единиц;
       осуществить поставку груза из пункта «Склад 2» в пункт «Магазин 3» в размере 20 единиц;

Стоимость провоза: 890', 2, 3, 1, NULL)
INSERT [dbo].[Task] ([Id], [StatusId], [Cost], [Conclusion], [CountRow], [CountColumn], [UserId], [CarrierId]) VALUES (8, 2, 3244, N'Для минимальной стоимости перевозки груза необходимо осуществить следующие поставки: 
       осуществить поставку груза из пункта «Склад 1» в пункт «Магазин 2» в размере 10 единиц;
       осуществить поставку груза из пункта «Склад 2» в пункт «Магазин 15» в размере 90 единиц;
       осуществить поставку груза из пункта «Склад 2» в пункт «Магазин 2» в размере 193 единиц;

Стоимость провоза: 2144
Сумма доставки перевозчика: 300

Расходы на доп услуги: 
       Перевозка: 100
       Такелажные работы: 200
       Срочная доставка: 500

Итого: 3244', 3, 2, 1, 9)
INSERT [dbo].[Task] ([Id], [StatusId], [Cost], [Conclusion], [CountRow], [CountColumn], [UserId], [CarrierId]) VALUES (10, 1, NULL, NULL, NULL, NULL, 1, 10)
SET IDENTITY_INSERT [dbo].[Task] OFF
INSERT [dbo].[TaskService] ([IdTask], [IdService]) VALUES (8, 1)
INSERT [dbo].[TaskService] ([IdTask], [IdService]) VALUES (8, 2)
INSERT [dbo].[TaskService] ([IdTask], [IdService]) VALUES (8, 6)
INSERT [dbo].[TaskService] ([IdTask], [IdService]) VALUES (10, 1)
INSERT [dbo].[TaskService] ([IdTask], [IdService]) VALUES (10, 4)
SET IDENTITY_INSERT [dbo].[TransportationCost] ON 

INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (2, 1, 0, 80)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (5, 1, 0, 81)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (3, 1, 4, 82)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (0, 1, 10, 83)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (1, 1, 2, 84)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (4, 1, 5, 85)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (2, 4, 5, 100)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (5, 4, 6, 101)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (0, 4, 1, 102)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (1, 4, 2, 103)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (3, 4, 3, 104)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (4, 4, 4, 105)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (4, 8, 9, 130)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (5, 8, 60, 131)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (0, 8, 5, 132)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (1, 8, 6, 133)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (2, 8, 6, 134)
INSERT [dbo].[TransportationCost] ([IdPosition], [IdTask], [Value], [Id]) VALUES (3, 8, 8, 135)
SET IDENTITY_INSERT [dbo].[TransportationCost] OFF
INSERT [dbo].[TypeClient] ([id], [Name]) VALUES (1, N'Заказчик')
INSERT [dbo].[TypeClient] ([id], [Name]) VALUES (2, N'Поставщик')
INSERT [dbo].[TypeConstraint] ([Id], [Name]) VALUES (1, N'не перевозить')
INSERT [dbo].[TypeConstraint] ([Id], [Name]) VALUES (2, N'поставить не менее')
INSERT [dbo].[TypeConstraint] ([Id], [Name]) VALUES (3, N'поставить не более')
INSERT [dbo].[TypeConstraint] ([Id], [Name]) VALUES (4, N'потребности пунктов')
INSERT [dbo].[TypeConstraint] ([Id], [Name]) VALUES (5, N'поставить из')
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Login], [Password], [Email]) VALUES (1, N'123', N'123', N'Nikita123@mail.ru')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Task] ADD  CONSTRAINT [DF_Task_UserId]  DEFAULT ((0)) FOR [UserId]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_TypeClient] FOREIGN KEY([TypeId])
REFERENCES [dbo].[TypeClient] ([id])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_TypeClient]
GO
ALTER TABLE [dbo].[Point]  WITH CHECK ADD  CONSTRAINT [FK_Point_Client] FOREIGN KEY([IdClient])
REFERENCES [dbo].[Client] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Point] CHECK CONSTRAINT [FK_Point_Client]
GO
ALTER TABLE [dbo].[Point]  WITH CHECK ADD  CONSTRAINT [FK_Point_Task] FOREIGN KEY([IdTask])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Point] CHECK CONSTRAINT [FK_Point_Task]
GO
ALTER TABLE [dbo].[ServiceCarrier]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCarrier_Carrier] FOREIGN KEY([IdCarrier])
REFERENCES [dbo].[Carrier] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceCarrier] CHECK CONSTRAINT [FK_ServiceCarrier_Carrier]
GO
ALTER TABLE [dbo].[ServiceCarrier]  WITH CHECK ADD  CONSTRAINT [FK_ServiceCarrier_Service] FOREIGN KEY([IdService])
REFERENCES [dbo].[Service] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceCarrier] CHECK CONSTRAINT [FK_ServiceCarrier_Service]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Carrier] FOREIGN KEY([CarrierId])
REFERENCES [dbo].[Carrier] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Carrier]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Status] FOREIGN KEY([StatusId])
REFERENCES [dbo].[Status] ([Id])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Status]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON UPDATE SET DEFAULT
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
ALTER TABLE [dbo].[TaskService]  WITH CHECK ADD  CONSTRAINT [FK_TaskService_Service] FOREIGN KEY([IdService])
REFERENCES [dbo].[Service] ([Id])
GO
ALTER TABLE [dbo].[TaskService] CHECK CONSTRAINT [FK_TaskService_Service]
GO
ALTER TABLE [dbo].[TaskService]  WITH CHECK ADD  CONSTRAINT [FK_TaskService_Task] FOREIGN KEY([IdTask])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TaskService] CHECK CONSTRAINT [FK_TaskService_Task]
GO
ALTER TABLE [dbo].[TransportationCost]  WITH CHECK ADD  CONSTRAINT [FK_TransportationCost_Task] FOREIGN KEY([IdTask])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TransportationCost] CHECK CONSTRAINT [FK_TransportationCost_Task]
GO
ALTER TABLE [dbo].[Сonstraint]  WITH CHECK ADD  CONSTRAINT [FK_Сonstraint_Task] FOREIGN KEY([IdTask])
REFERENCES [dbo].[Task] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Сonstraint] CHECK CONSTRAINT [FK_Сonstraint_Task]
GO
ALTER TABLE [dbo].[Сonstraint]  WITH CHECK ADD  CONSTRAINT [FK_Сonstraint_TypeConstraint] FOREIGN KEY([TypeConstraint])
REFERENCES [dbo].[TypeConstraint] ([Id])
GO
ALTER TABLE [dbo].[Сonstraint] CHECK CONSTRAINT [FK_Сonstraint_TypeConstraint]
GO
USE [master]
GO
ALTER DATABASE [DiplomBetaDB] SET  READ_WRITE 
GO
