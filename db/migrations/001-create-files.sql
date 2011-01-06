USE [CMSData]
GO

/****** Object:  Table [dbo].[Files]    Script Date: 01/05/2011 03:17:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Files](
	[FileID] [uniqueidentifier] NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[Mimetype] [varchar](100) NOT NULL,
	[Data] [image] NOT NULL,
	[OwnerID] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Files] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Files]  WITH CHECK ADD  CONSTRAINT [FK_Files_Files] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[Profiles] ([MembershipID])
GO

ALTER TABLE [dbo].[Files] CHECK CONSTRAINT [FK_Files_Files]
GO

ALTER TABLE [dbo].[Files] ADD  CONSTRAINT [DF_Files_FileID]  DEFAULT (newsequentialid()) FOR [FileID]
GO

