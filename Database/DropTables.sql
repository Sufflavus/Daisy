USE [Daisy]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Comment_Article]') AND parent_object_id = OBJECT_ID(N'[dbo].[Comment]'))
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [FK_Comment_Article]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Comment_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [DF_Comment_Id]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Comment_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Comment] DROP CONSTRAINT [DF_Comment_CreateDate]
END

GO

USE [Daisy]
GO

/****** Object:  Table [dbo].[Comment]    Script Date: 10/21/2015 22:13:39 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
DROP TABLE [dbo].[Comment]
GO


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Article_Id]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Article] DROP CONSTRAINT [DF_Article_Id]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_Article_CreateDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Article] DROP CONSTRAINT [DF_Article_CreateDate]
END

GO

USE [Daisy]
GO

/****** Object:  Table [dbo].[Article]    Script Date: 10/21/2015 22:13:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Article]') AND type in (N'U'))
DROP TABLE [dbo].[Article]
GO