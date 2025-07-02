-- Seed values for WorkflowSteps, so that the correct data is returned by the Stored Procedure
DECLARE @SeedDateTime datetime = GETDATE()


INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68237230ae186b7597dc6daa',1, 'crm', 'Validate agent', 5, 1000, @SeedDateTime); -- 1

SET @SeedDateTime = DATEADD(millisecond, 600, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68237230ae186b7597dc6daa', 1, 'crm', 'Confirm agent is free', 100, 1000, @SeedDateTime);  -- 2

SET @SeedDateTime = DATEADD(millisecond, 6000, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68237230ae186b7597dc6daa', 1, 'crm', 'Confirm with agent', 9, 1000, @SeedDateTime);  -- 3


-- 3 extra unsused records to set the WorkflowStepId identity column values correctly
SET @SeedDateTime = DATEADD(millisecond, 400, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68236ef4c8715b4c05c20a55', 2, 'crm', 'Step4', 8, 1000, @SeedDateTime); 

SET @SeedDateTime = DATEADD(millisecond, 500, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68236ef4c8715b4c05c20a55', 2, 'crm', 'Step5', 12, 1000, @SeedDateTime); 

SET @SeedDateTime = DATEADD(millisecond, 600, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68236ef4c8715b4c05c20a55', 2, 'crm', 'Step6', 9, 1000, @SeedDateTime); 


SET @SeedDateTime = DATEADD(millisecond, 724000, @SeedDateTime) 

INSERT INTO dbo.WorkflowSteps 
(Id, BusinessId, [Type], StepName, [Weight], DelayTimeInMs, ProcessDateTime)
VALUES ('68237230ae186b7597dc6daa', 2, 'crm', 'Book meeting room', 100, 720000, @SeedDateTime); -- 7


