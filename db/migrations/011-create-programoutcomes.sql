/*
   Saturday, January 15, 201111:12:01 PM
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
CREATE TABLE dbo.ProgramOutcomes
	(
	ProgramOutcomeID uniqueidentifier NOT NULL,
	Label varchar(50) NOT NULL,
	Description varchar(300) NOT NULL,
	SiteID uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.ProgramOutcomes ADD CONSTRAINT
	DF_ProgramOutcomes_ProgramOutcomeID DEFAULT newsequentialid() FOR ProgramOutcomeID
GO
ALTER TABLE dbo.ProgramOutcomes ADD CONSTRAINT
	PK_ProgramOutcomes PRIMARY KEY CLUSTERED 
	(
	ProgramOutcomeID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.ProgramOutcomes', 'Object', 'CONTROL') as Contr_Per 