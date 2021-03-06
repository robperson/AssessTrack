/*
   Thursday, January 13, 20116:19:48 PM
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
select Has_Perms_By_Name(N'dbo.Files', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Files', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Files', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CourseTerms', 'Object', 'CONTROL') as Contr_Per BEGIN TRANSACTION
GO
ALTER TABLE dbo.CourseTermFiles ADD CONSTRAINT
	FK_CourseTermFiles_CourseTerms FOREIGN KEY
	(
	CourseTermID
	) REFERENCES dbo.CourseTerms
	(
	CourseTermID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.CourseTermFiles ADD CONSTRAINT
	FK_CourseTermFiles_Files FOREIGN KEY
	(
	FileID
	) REFERENCES dbo.Files
	(
	FileID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
COMMIT
select Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'CONTROL') as Contr_Per 