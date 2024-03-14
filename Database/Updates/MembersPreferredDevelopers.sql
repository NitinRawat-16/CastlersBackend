USE [Castlers]
GO

/****** Object:  Table [dbo].[MembersPreferredDevelopers]    Script Date: 25-12-2023 16:19:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MembersPreferredDevelopers](
	[preferredDeveloperId] [int] IDENTITY(1,1) NOT NULL,
	[memberId] [int] NULL,
	[electionId] [int] NULL,
	[developerFirst] [int] NULL,
	[developerSecond] [int] NULL,
	[developerThird] [int] NULL,
	[isVoted] [bit] NOT NULL,
	[creationDate] [datetime] NULL,
	[updationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[preferredDeveloperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MembersPreferredDevelopers]  WITH CHECK ADD FOREIGN KEY([developerFirst])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO

ALTER TABLE [dbo].[MembersPreferredDevelopers]  WITH CHECK ADD FOREIGN KEY([developerSecond])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO

ALTER TABLE [dbo].[MembersPreferredDevelopers]  WITH CHECK ADD FOREIGN KEY([developerThird])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO

ALTER TABLE [dbo].[MembersPreferredDevelopers]  WITH CHECK ADD FOREIGN KEY([electionId])
REFERENCES [dbo].[ElectionDetails] ([electionId])
GO

ALTER TABLE [dbo].[MembersPreferredDevelopers]  WITH CHECK ADD FOREIGN KEY([memberId])
REFERENCES [dbo].[SocietyMemberDetails] ([societyMemberDetailsId])
GO


