USE [master]
GO
/****** Object:  Database [MachineLogViewer]    Script Date: 02/06/2015 23:28:44 ******/
CREATE DATABASE [MachineLogViewer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MachineLogViewer', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MachineLogViewer.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MachineLogViewer_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MachineLogViewer_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MachineLogViewer] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MachineLogViewer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MachineLogViewer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MachineLogViewer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MachineLogViewer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MachineLogViewer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MachineLogViewer] SET ARITHABORT OFF 
GO
ALTER DATABASE [MachineLogViewer] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [MachineLogViewer] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MachineLogViewer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MachineLogViewer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MachineLogViewer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MachineLogViewer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MachineLogViewer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MachineLogViewer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MachineLogViewer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MachineLogViewer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MachineLogViewer] SET  ENABLE_BROKER 
GO
ALTER DATABASE [MachineLogViewer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MachineLogViewer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MachineLogViewer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MachineLogViewer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MachineLogViewer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MachineLogViewer] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [MachineLogViewer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MachineLogViewer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MachineLogViewer] SET  MULTI_USER 
GO
ALTER DATABASE [MachineLogViewer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MachineLogViewer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MachineLogViewer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MachineLogViewer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MachineLogViewer]
GO
/****** Object:  User [web]    Script Date: 02/06/2015 23:28:44 ******/
CREATE USER [web] FOR LOGIN [web] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [web]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 02/06/2015 23:28:44 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 02/06/2015 23:28:44 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 02/06/2015 23:28:44 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 02/06/2015 23:28:44 ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 02/06/2015 23:28:44 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 02/06/2015 23:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Description] [nvarchar](max) NULL,
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
/****** Object:  Table [dbo].[LogEntry]    Script Date: 02/06/2015 23:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogEntry](
	[LogEntryId] [bigint] IDENTITY(1,1) NOT NULL,
	[MachineId] [int] NOT NULL,
	[EventTime] [datetime] NOT NULL,
	[ReceivedTime] [datetime] NULL,
	[Category] [int] NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.LogEntry] PRIMARY KEY CLUSTERED 
(
	[LogEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Machine]    Script Date: 02/06/2015 23:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[MachineId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](12) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[ExpiryDate] [datetime] NOT NULL,
	[UserId] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Machine] PRIMARY KEY CLUSTERED 
(
	[MachineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Takings]    Script Date: 02/06/2015 23:28:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Takings](
	[TakingsId] [bigint] IDENTITY(1,1) NOT NULL,
	[MachineId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Currency] [nvarchar](3) NOT NULL,
	[SumTotal] [decimal](18, 2) NOT NULL,
	[SumCash] [decimal](18, 2) NOT NULL,
	[SumCashless] [decimal](18, 2) NOT NULL,
	[SumProduct1] [decimal](18, 2) NOT NULL,
	[SumProduct2] [decimal](18, 2) NOT NULL,
	[SumProduct3] [decimal](18, 2) NOT NULL,
	[SumProduct4] [decimal](18, 2) NOT NULL,
	[SumProduct5] [decimal](18, 2) NOT NULL,
	[SumProduct6] [decimal](18, 2) NOT NULL,
	[SumProduct7] [decimal](18, 2) NOT NULL,
	[SumProduct8] [decimal](18, 2) NOT NULL,
	[SumProduct9] [decimal](18, 2) NOT NULL,
	[SumProduct10] [decimal](18, 2) NOT NULL,
	[SumProduct11] [decimal](18, 2) NOT NULL,
	[SumProduct12] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_dbo.Takings] PRIMARY KEY CLUSTERED 
(
	[TakingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 02/06/2015 23:28:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[AspNetUserRoles]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 02/06/2015 23:28:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MachineId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_MachineId] ON [dbo].[LogEntry]
(
	[MachineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Code]    Script Date: 02/06/2015 23:28:44 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Code] ON [dbo].[Machine]
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_UserId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[Machine]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_MachineId]    Script Date: 02/06/2015 23:28:44 ******/
CREATE NONCLUSTERED INDEX [IX_MachineId] ON [dbo].[Takings]
(
	[MachineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
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
ALTER TABLE [dbo].[LogEntry]  WITH CHECK ADD  CONSTRAINT [FK_dbo.LogEntry_dbo.Machine_MachineId] FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([MachineId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LogEntry] CHECK CONSTRAINT [FK_dbo.LogEntry_dbo.Machine_MachineId]
GO
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Machine_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_dbo.Machine_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Takings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Takings_dbo.Machine_MachineId] FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([MachineId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Takings] CHECK CONSTRAINT [FK_dbo.Takings_dbo.Machine_MachineId]
GO
USE [master]
GO
ALTER DATABASE [MachineLogViewer] SET  READ_WRITE 
GO
