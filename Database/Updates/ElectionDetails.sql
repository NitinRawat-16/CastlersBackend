USE [Castlers]
GO

/****** Object:  Table [dbo].[ElectionDetails]    Script Date: 25-12-2023 16:21:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ElectionDetails](
	[electionId] [int] IDENTITY(1,1) NOT NULL,
	[tenderId] [int] NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NOT NULL,
	[status] [nvarchar](50) NULL,
	[totalVoters] [int] NULL,
	[totalVoted] [int] NULL,
	[creationDate] [datetime] NULL,
	[updationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[electionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ElectionDetails]  WITH CHECK ADD FOREIGN KEY([tenderId])
REFERENCES [dbo].[TenderDetails] ([tenderId])
GO


