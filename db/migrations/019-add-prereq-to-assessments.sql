/*
   Friday, October 14, 20116:23:44 PM
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
ALTER TABLE dbo.Assessments ADD
	PrereqAssessmentID uniqueidentifier NULL,
	PrereqMinScorePct float(53) NULL
GO
ALTER TABLE dbo.Assessments ADD CONSTRAINT
	FK_Assessments_AssessmentsPrereq FOREIGN KEY
	(
	PrereqAssessmentID
	) REFERENCES dbo.Assessments
	(
	AssessmentID
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Assessments SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
select Has_Perms_By_Name(N'dbo.Assessments', 'Object', 'ALTER') as ALT_Per, Has_Perms_By_Name(N'dbo.Assessments', 'Object', 'VIEW DEFINITION') as View_def_Per, Has_Perms_By_Name(N'dbo.Assessments', 'Object', 'CONTROL') as Contr_Per 