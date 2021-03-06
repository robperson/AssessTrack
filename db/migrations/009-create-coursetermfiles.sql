/*
   Thursday, January 13, 20116:18:24 PM
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
CREATE TABLE dbo.CourseTermFiles
	(
	CourseTermFileID uniqueidentifier NOT NULL,
	CourseTermID uniqueidentifier NOT NULL,
	FileID uniqueidentifier NOT NULL
	)  ON [PRIMARY]
GO
ALTER TABLE dbo.CourseTermFiles ADD CONSTRAINT
	DF_CourseTermFiles_CourseTermFileID DEFAULT newsequentialid() FOR CourseTermFileID
GO
ALTER TABLE dbo.CourseTermFiles ADD CONSTRAINT
	PK_CourseTermFiles PRIMARY KEY CLUSTERED 
	(
	CourseTermFileID
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
select Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.CourseTermFiles', 'Object', 'CONTROL') as Contr_Per 