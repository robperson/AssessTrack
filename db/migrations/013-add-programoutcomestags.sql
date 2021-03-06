/*
   Sunday, January 16, 201112:26:27 PM
   User: 
   Server: ROBPERSON-PC\SQLEXPRESS
   Database: CMSData
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Tags', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Tags', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Tags', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
CREATE TABLE dbo.TagProgramOutcomes
	(
	TagProgramOutcomeID uniqueidentifier NOT NULL,
	TagID uniqueidentifier NOT NULL,
	ProgramOutcomeID uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.TagProgramOutcomes ADD CONSTRAINT
	DF_TagProgramOutcomes_TagProgramOutcomeID DEFAULT newsequentialid() FOR TagProgramOutcomeID
GO
ALTER TABLE dbo.TagProgramOutcomes ADD CONSTRAINT
	PK_TagProgramOutcomes PRIMARY KEY CLUSTERED 
	(
	TagProgramOutcomeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.TagProgramOutcomes ADD CONSTRAINT
	FK_TagProgramOutcomes_Tags FOREIGN KEY
	(
	TagID
	) REFERENCES dbo.Tags
	(
	TagID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.TagProgramOutcomes ADD CONSTRAINT
	FK_TagProgramOutcomes_ProgramOutcomes FOREIGN KEY
	(
	ProgramOutcomeID
	) REFERENCES dbo.ProgramOutcomes
	(
	ProgramOutcomeID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.TagProgramOutcomes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.TagProgramOutcomes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.TagProgramOutcomes', 'Object', 'CONTROL') as Contr_Per 