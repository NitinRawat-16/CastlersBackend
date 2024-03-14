USE[Castlers]
GO

/****** Object:  Table [dbo].[DeveloperAmenitiesDetails]    Script Date: 19-12-2023 00:25:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DeveloperAmenitiesDetails](
    [amenitiesDetailsId][int] IDENTITY(1, 1) NOT NULL,
    [developerAmenitiesId][int] NULL,
    [liftElivator][nvarchar](500) NULL,
    [generatorBackup][nvarchar](500) NULL,
    [swimmingPool][nvarchar](500) NULL,
    [gym][nvarchar](500) NULL,
    [clubHouse][nvarchar](500) NULL,
    [gamingRoom][nvarchar](500) NULL,
    [outdoorPlayCourt][nvarchar](500) NULL,
    [childrenPlayArea][nvarchar](500) NULL,
    [seniorCitizenPark][nvarchar](500) NULL,
    [joggingTrack][nvarchar](500) NULL,
    [landscapeGarden][nvarchar](500) NULL,
    [solarWaterSystem][nvarchar](500) NULL,
    [solarBackup][nvarchar](500) NULL,
    [evChargingStation][nvarchar](500) NULL,
    [securityCabin][nvarchar](500) NULL,
    [cctvCoverage][nvarchar](500) NULL,
    [societyOffice][nvarchar](500) NULL,
    [decorativeEntranceGate][nvarchar](500) NULL,
    [rainWaterHarvesting][nvarchar](500) NULL,
    [fireFightingSystem][nvarchar](500) NULL,
    [terraceGarden][nvarchar](500) NULL,
    [letterBox][nvarchar](500) NULL,
    [gasPipeLine][nvarchar](500) NULL,
    [securitySystem][nvarchar](500) NULL,
    [dishConnection][nvarchar](500) NULL,
    [videoDoorPhone][nvarchar](500) NULL,
    [intercomSystem][nvarchar](500) NULL,
    [easyDryerSystem][nvarchar](500) NULL,
    [airCondition][nvarchar](500) NULL,
    [waterPurifier][nvarchar](500) NULL,
    [modulerKitchen][nvarchar](500) NULL,
    [smartEnergyConsumption][nvarchar](500) NULL,
    [intilligentDetectionWaterManagement][nvarchar](500) NULL,
    [smokeDetectionAlert][nvarchar](500) NULL,
    [homeAutomation][nvarchar](500) NULL,
    [additionalAmenities][nvarchar](500) NULL,
PRIMARY KEY CLUSTERED
(
    [amenitiesDetailsId] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON[PRIMARY]
) ON[PRIMARY]
GO

ALTER TABLE [dbo].[DeveloperAmenitiesDetails]  WITH CHECK ADD FOREIGN KEY([developerAmenitiesId])
REFERENCES[dbo].[DeveloperAmenities]([amenitiesId])
GO


