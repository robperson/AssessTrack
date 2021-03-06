/*
   Thursday, January 13, 20115:51:04 PM
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
ALTER TABLE dbo.Files ADD
	Title varchar(100) NULL,
	Description varchar(300) NULL
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Files', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Files', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Files', 'Object', 'CONTROL') as Contr_Per 