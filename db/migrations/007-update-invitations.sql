/*
   Tuesday, January 11, 20113:41:01 AM
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
select Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Sites', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Sites', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Sites', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.Invitations ADD
	Accepted bit NOT NULL CONSTRAINT DF_Invitations_Accepted DEFAULT 0
GO
ALTER TABLE dbo.Invitations ADD CONSTRAINT
	FK_Invitations_Sites FOREIGN KEY
	(
	SiteID
	) REFERENCES dbo.Sites
	(
	SiteID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Invitations ADD CONSTRAINT
	FK_Invitations_CourseTerms FOREIGN KEY
	(
	CourseTermID
	) REFERENCES dbo.CourseTerms
	(
	CourseTermID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Invitations', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Invitations', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Invitations', 'Object', 'CONTROL') as Contr_Per 