-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
USE PerchTest
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		David Grant
-- Create date: 29th June 2025
-- Description:	Inserts a new record into the Audit 
--				table with the specified ID and BusinessId (= 1)
--				and the current date and time,
--				then retrieves the Workflow steps
--				which match the input ID and BusinessId value
-- =============================================

DROP PROCEDURE IF EXISTS [dbo].[GetNetWorkflowStep]
GO

CREATE PROCEDURE [dbo].[GetNetWorkflowStep]
	@ID nvarchar(24),
	@BusinessId int = 1
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Assume that the current date time is needed
	DECLARE @AuditDateTime DATETIME = GETDATE()

	BEGIN
		INSERT INTO [dbo].[Audit]
		VALUES (@ID, @BusinessId, @AuditDateTime)
	END
	--GO

	BEGIN
		SELECT WorkflowStepId Id, StepName, [Weight], DelayTimeInMs  
		FROM dbo.WorkflowSteps 
		WHERE Id = @ID and BusinessId = @BusinessId
	END
	--GO
END
GO
