USE [Castlers]
GO

/****** Object:  Table [dbo].[DeveloperConstructionSpec]    Script Date: 19-12-2023 00:26:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DeveloperConstructionSpec](
	[constructionSpecId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NULL,
	[tenderId] [int] NULL,
	[structure] [nvarchar](500) NULL,
	[wallBrickType] [nvarchar](500) NULL,
	[wallSize] [nvarchar](500) NULL,
	[flooringTilesHouse] [nvarchar](500) NULL,
	[flooringTilesTerrace] [nvarchar](500) NULL,
	[flooringTilesDryBalcony] [nvarchar](500) NULL,
	[flooringTilesBathroom] [nvarchar](500) NULL,
	[flooringTilesToilet] [nvarchar](500) NULL,
	[wallTilesKitchen] [nvarchar](500) NULL,
	[wallTilesBathroom] [nvarchar](500) NULL,
	[wallTilesToilets] [nvarchar](500) NULL,
	[wallTilesTerrace] [nvarchar](500) NULL,
	[wallTilesSitoutArea] [nvarchar](500) NULL,
	[kitchenPlatformSpec] [nvarchar](500) NULL,
	[kitchenPlatformType] [nvarchar](500) NULL,
	[mainDoorSpecification] [nvarchar](500) NULL,
	[internalDoorSpecification] [nvarchar](500) NULL,
	[bathroomDoorSpecification] [nvarchar](500) NULL,
	[terraceDoorSpecification] [nvarchar](500) NULL,
	[windows] [nvarchar](500) NULL,
	[electrical] [nvarchar](500) NULL,
	[waterSupply] [nvarchar](500) NULL,
	[constructionSpecPdfUrl] [nvarchar](max) NULL,
	[creationDate] [datetime] NULL,
	[updationDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[userConstructionSpecPdfUrl] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[constructionSpecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DeveloperConstructionSpec]  WITH CHECK ADD FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO

ALTER TABLE [dbo].[DeveloperConstructionSpec]  WITH CHECK ADD FOREIGN KEY([tenderId])
REFERENCES [dbo].[TenderDetails] ([tenderId])
GO


