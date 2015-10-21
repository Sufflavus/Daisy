USE [Daisy]
GO

/****** Object:  Table [dbo].[Article]    Script Date: 10/21/2015 22:12:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Article](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Text] [nvarchar](200) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Article] ADD  CONSTRAINT [DF_Article_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO


/****** Object:  Table [dbo].[Comment]    Script Date: 10/21/2015 22:12:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comment](
	[Id] [uniqueidentifier] NOT NULL,
	[ArticleId] [uniqueidentifier] NOT NULL,
	[Text] [nvarchar](50) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_Article] FOREIGN KEY([ArticleId])
REFERENCES [dbo].[Article] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_Article]
GO

ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_Id]  DEFAULT (newid()) FOR [Id]
GO

ALTER TABLE [dbo].[Comment] ADD  CONSTRAINT [DF_Comment_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO

