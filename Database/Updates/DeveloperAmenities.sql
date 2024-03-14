USE [Castlers]
GO

/****** Object:  Table [dbo].[DeveloperAmenities]    Script Date: 19-12-2023 00:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DeveloperAmenities](
	[amenitiesId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NULL,
	[tenderId] [int] NULL,
	[liftElivator] [bit] NULL,
	[generatorBackup] [bit] NULL,
	[swimmingPool] [bit] NULL,
	[gym] [bit] NULL,
	[clubHouse] [bit] NULL,
	[gamingRoom] [bit] NULL,
	[outdoorPlayCourt] [bit] NULL,
	[childrenPlayArea] [bit] NULL,
	[seniorCitizenPark] [bit] NULL,
	[joggingTrack] [bit] NULL,
	[landscapeGarden] [bit] NULL,
	[solarWaterSystem] [bit] NULL,
	[solarBackup] [bit] NULL,
	[evChargingStation] [bit] NULL,
	[securityCabin] [bit] NULL,
	[cctvCoverage] [bit] NULL,
	[societyOffice] [bit] NULL,
	[decorativeEntranceGate] [bit] NULL,
	[rainWaterHarvesting] [bit] NULL,
	[fireFightingSystem] [bit] NULL,
	[terraceGarden] [bit] NULL,
	[letterBox] [bit] NULL,
	[gasPipeLine] [bit] NULL,
	[securitySystem] [bit] NULL,
	[dishConnection] [bit] NULL,
	[videoDoorPhone] [bit] NULL,
	[intercomSystem] [bit] NULL,
	[easyDryerSystem] [bit] NULL,
	[airCondition] [bit] NULL,
	[waterPurifier] [bit] NULL,
	[modulerKitchen] [bit] NULL,
	[smartEnergyConsumption] [bit] NULL,
	[intilligentDetectionWaterManagement] [bit] NULL,
	[smokeDetectionAlert] [bit] NULL,
	[homeAutomation] [bit] NULL,
	[additionalAmenities] [bit] NULL,
	[amenitiesPdfUrl] [nvarchar](max) NULL,
	[creationDate] [datetime] NULL,
	[updationDate] [datetime] NULL,
	[isActive] [bit] NULL,
	[userAmenitiesPdfUrl] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[amenitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DeveloperAmenities]  WITH CHECK ADD FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO

ALTER TABLE [dbo].[DeveloperAmenities]  WITH CHECK ADD FOREIGN KEY([tenderId])
REFERENCES [dbo].[TenderDetails] ([tenderId])
GO



