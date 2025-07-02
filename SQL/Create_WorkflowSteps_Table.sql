USE [PerchTest]
GO

/*
This check below is for dev purposes only.  
It can be omitted for production code if the table is already being used.
*/

/****** Object:  Table [dbo].[WorkflowSteps]    Script Date: 27/06/2025 20:43:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[WorkflowSteps]') AND type in (N'U'))
DROP TABLE [dbo].[WorkflowSteps]	-- this will also drop the associated index
GO

/****** Object:  Table [dbo].[WorkflowSteps]    Script Date: 27/06/2025 20:43:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WorkflowSteps](
	[WorkflowStepId] [int] NOT NULL IDENTITY(1, 1),	-- best to have an individual unique record ID
	[Id] [nchar](24) NOT NULL,	-- for the _id value passed in
	[BusinessId] int NOT NULL, -- for the BusinessId value (e.g. 1) 
	[Type] [nvarchar](20) NOT NULL,
	[Property] [nvarchar](30), 
	[EntryType] [nchar](10),
	[StepName] [nvarchar](30) NOT NULL,
	[Weight] [int] NOT NULL,
	[DelayTimeInMs] [int] NOT NULL,
	[ProcessDateTime] [datetime] NOT NULL
) ON [PRIMARY]
GO

/*
	The indexes below are optional and would not be required for small test data sets where a full table scan would be quick,
	but would be useful in production for larger volumes of data
*/


-- Assume that WorkflowStepId Id values are not permitted as this is the Primary Key
CREATE UNIQUE CLUSTERED INDEX WorkflowSteps_idx_PK
    ON [dbo].[WorkflowSteps] ( WorkflowStepId ASC ) 
	WITH (IGNORE_DUP_KEY = OFF); 
GO

-- An index may also be useful on the Id column and BusinessId column too
-- Assume that duplicate Id and BusinessId combination values are OK
CREATE INDEX WorkflowSteps_idx_1
    ON [dbo].[WorkflowSteps] ( Id ASC, BusinessId ASC ) 
GO
