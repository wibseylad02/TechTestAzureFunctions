USE [PerchTest]
GO

/*
This check below is for dev purposes only.  
It can be omitted for production code if the table is already being used.
*/

/****** Object:  Table [dbo].[Audit]    Script Date: 27/06/2025 20:43:34 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Audit]') AND type in (N'U'))
DROP TABLE [dbo].[Audit]	-- this will also drop the associated index
GO

/****** Object:  Table [dbo].[Audit]    Script Date: 27/06/2025 20:43:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Audit](
	[Id] [nchar](24) NOT NULL,
	[BusinessId] int NOT NULL,
	[CreatedDateTime] datetime NOT NULL
) ON [PRIMARY]
GO

-- An index may also be useful on the combination of the Id, BusinessId and CreatedDateTime columns
-- Assume that duplicate combinations of Id, BusinessId and CreatedDateTime values are not permitted
CREATE UNIQUE CLUSTERED INDEX idx_Audit
    ON [dbo].[Audit] ( Id ASC, BusinessId ASC, CreatedDateTime ASC ) 
	WITH (IGNORE_DUP_KEY = OFF); 
GO
