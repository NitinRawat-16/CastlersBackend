USE [master]
GO
/****** Object:  Database [Castlers]    Script Date: 10-12-2023 12:42:10 ******/
CREATE DATABASE [Castlers]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Castlers', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Castlers.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Castlers_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Castlers_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Castlers] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Castlers].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Castlers] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Castlers] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Castlers] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Castlers] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Castlers] SET ARITHABORT OFF 
GO
ALTER DATABASE [Castlers] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Castlers] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Castlers] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Castlers] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Castlers] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Castlers] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Castlers] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Castlers] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Castlers] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Castlers] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Castlers] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Castlers] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Castlers] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Castlers] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Castlers] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Castlers] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Castlers] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Castlers] SET RECOVERY FULL 
GO
ALTER DATABASE [Castlers] SET  MULTI_USER 
GO
ALTER DATABASE [Castlers] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Castlers] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Castlers] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Castlers] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Castlers] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Castlers] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Castlers', N'ON'
GO
ALTER DATABASE [Castlers] SET QUERY_STORE = OFF
GO
USE [Castlers]
GO
/****** Object:  UserDefinedTableType [dbo].[udt_MemberDetails]    Script Date: 10-12-2023 12:42:11 ******/
CREATE TYPE [dbo].[udt_MemberDetails] AS TABLE(
	[registeredSocietyId] [int] NOT NULL,
	[memberName] [nvarchar](800) NOT NULL,
	[mobileNumber] [nvarchar](11) NOT NULL,
	[email] [nvarchar](400) NOT NULL,
	[societyMemberDesignationId] [int] NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Update_MemberDetails]    Script Date: 10-12-2023 12:42:11 ******/
CREATE TYPE [dbo].[Update_MemberDetails] AS TABLE(
	[SocietyId] [int] NULL,
	[Name] [nvarchar](400) NULL,
	[mobileNumber] [nvarchar](50) NULL,
	[email] [nvarchar](400) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Update_SocietyMember]    Script Date: 10-12-2023 12:42:11 ******/
CREATE TYPE [dbo].[Update_SocietyMember] AS TABLE(
	[societyMemberDetailsId] [int] NULL,
	[Name] [nvarchar](400) NULL,
	[mobileNumber] [nvarchar](50) NULL,
	[email] [nvarchar](400) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Update_SocietyMemberNew2]    Script Date: 10-12-2023 12:42:11 ******/
CREATE TYPE [dbo].[Update_SocietyMemberNew2] AS TABLE(
	[societyMemberDetailsId] [int] NULL,
	[registeredSocietyId] [int] NOT NULL,
	[Name] [nvarchar](400) NULL,
	[mobileNumber] [nvarchar](50) NULL,
	[email] [nvarchar](400) NULL,
	[societyMemberDesginationid] [int] NULL
)
GO
/****** Object:  Table [dbo].[AdminDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminDetails](
	[adminDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [nvarchar](50) NOT NULL,
	[lastName] [nvarchar](50) NOT NULL,
	[email] [nvarchar](50) NOT NULL,
	[mobileNumber] [nvarchar](20) NOT NULL,
	[adminCode] [nvarchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[adminDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[mobileNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[adminCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Blogs]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Blogs](
	[blogId] [int] IDENTITY(1,1) NOT NULL,
	[blogsTypeMasterId] [int] NULL,
	[heading] [nvarchar](max) NULL,
	[description] [nvarchar](max) NULL,
	[path] [nvarchar](max) NULL,
	[isDeleted] [bit] NULL,
	[createdBy] [int] NULL,
	[updatedBy] [int] NULL,
	[createdDate] [datetime] NULL,
	[updatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[blogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogsTypeMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogsTypeMaster](
	[blogsTypeId] [int] IDENTITY(1,1) NOT NULL,
	[typeName] [nvarchar](100) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[blogsTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesignationType]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesignationType](
	[designationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[designationName] [nvarchar](50) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[designationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperKYC]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperKYC](
	[developerKYCId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NOT NULL,
	[orgnisationTypeId] [int] NOT NULL,
	[orgnisationPanNumber] [nvarchar](10) NOT NULL,
	[incorporationDocPath] [nvarchar](800) NOT NULL,
	[incorporationName] [nvarchar](800) NOT NULL,
	[gstNumber] [nvarchar](200) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[developerKYCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperMaster](
	[developerId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](250) NOT NULL,
	[address] [nvarchar](500) NULL,
	[logoPath] [nvarchar](1000) NOT NULL,
	[siteLink] [nvarchar](200) NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
	[email] [nvarchar](250) NOT NULL,
	[profilePath] [nvarchar](250) NULL,
	[mobileNumber] [nvarchar](11) NOT NULL,
	[registeredDeveloperCode] [nvarchar](100) NOT NULL,
	[experienceYear] [int] NULL,
	[organisationTypeId] [int] NOT NULL,
	[projectsInHand] [int] NULL,
	[numberOfRERARegisteredProjects] [int] NULL,
	[totalCompletedProjects] [int] NULL,
	[totalConstructionAreaDevTillToday] [nvarchar](100) NULL,
	[sizeOfTheLargestProjectHandled] [nvarchar](100) NULL,
	[experienceInHighRiseBuildings] [nvarchar](100) NULL,
	[avgTurnOverforLastThreeYears] [nvarchar](100) NULL,
	[affilicationToAnyDevAssociation] [bit] NULL,
	[awardsAndRecognition] [nvarchar](max) NULL,
	[haveBusinessInMultipleCities] [bit] NULL,
	[affilicationDevAssociationName] [nvarchar](200) NULL,
	[city] [nvarchar](100) NULL,
	[reviewRatingScore] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[developerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperPastProjectDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperPastProjectDetails](
	[developerPastProjectDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[projectName] [nvarchar](100) NOT NULL,
	[projectLocation] [nvarchar](100) NULL,
	[reraRegistrationNumber] [nvarchar](100) NULL,
	[projectStartDate] [datetime] NULL,
	[projectEndDate] [datetime] NULL,
	[developerId] [int] NULL,
	[reraCertificateUrl] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[developerPastProjectDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeveloperTenderDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeveloperTenderDetails](
	[developerTenderDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[tenderCode] [nvarchar](100) NULL,
	[percentageOfIncreaseArea] [float] NULL,
	[quantamOfAreaAtDiscountRate] [float] NULL,
	[expectedDiscountRate] [float] NULL,
	[corpusFund] [float] NULL,
	[rentPerSqFtFlat] [float] NULL,
	[rentPerSqFtOffice] [float] NULL,
	[rentPerSqFtShop] [float] NULL,
	[parkingPerMember] [int] NULL,
	[typeOfProject] [nvarchar](100) NULL,
	[refundableDepositPerMemberForFlat] [float] NULL,
	[refundableDepositPerMemberForOffice] [float] NULL,
	[refundableDepositPerMemberForShop] [float] NULL,
	[shiftingChargesForFlatOfficeShop] [float] NULL,
	[bettermentChargesPerMember] [float] NULL,
	[developerTenderPdfPath] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[developerTenderDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinalizedDeveloper]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinalizedDeveloper](
	[finalizedDeveloperId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NOT NULL,
	[letterOfIntrestId] [int] NOT NULL,
	[offerId] [int] NOT NULL,
	[memberCount] [int] NOT NULL,
	[voteCount] [int] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[finalizedDeveloperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LetterOfInterest]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LetterOfInterest](
	[letterOfInterestId] [int] IDENTITY(1,1) NOT NULL,
	[developerId] [int] NULL,
	[isListed] [bit] NOT NULL,
	[yearOfExperience] [int] NULL,
	[numberOfProjectsCompleted] [int] NULL,
	[numberOfProjectsInHand] [int] NULL,
	[officeAddress] [nvarchar](800) NOT NULL,
	[contactDetails] [nvarchar](100) NOT NULL,
	[emailId] [nvarchar](250) NULL,
	[isAcceptedRules] [bit] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[offerId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[letterOfInterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Offer]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offer](
	[offerId] [int] IDENTITY(1,1) NOT NULL,
	[nameOfSociety] [nvarchar](500) NOT NULL,
	[existingMemberCount] [int] NOT NULL,
	[age] [float] NOT NULL,
	[sizeOfPlot] [int] NOT NULL,
	[plotDimensions] [nvarchar](60) NOT NULL,
	[approchRoadWidth] [float] NOT NULL,
	[totalCarpetBldgArea] [float] NOT NULL,
	[logitude] [float] NULL,
	[latitude] [float] NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[developerId] [int] NULL,
	[tenderId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[offerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrgnisationType]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrgnisationType](
	[orgnisationTypeId] [int] IDENTITY(1,1) NOT NULL,
	[typeName] [nvarchar](50) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[orgnisationTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartnerKYC]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartnerKYC](
	[partnerKYCId] [int] IDENTITY(1,1) NOT NULL,
	[designationTypeId] [int] NOT NULL,
	[developerId] [int] NOT NULL,
	[email] [nvarchar](250) NOT NULL,
	[contactNumber] [nvarchar](11) NULL,
	[panCard] [nvarchar](10) NOT NULL,
	[adharCard] [nvarchar](12) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
	[partnerFileUrl] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[partnerKYCId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PreferedDeveloperVoting]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreferedDeveloperVoting](
	[developerVotingId] [int] IDENTITY(1,1) NOT NULL,
	[letterOfIntrestId] [int] NOT NULL,
	[offerId] [int] NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[memberEmail] [nvarchar](300) NOT NULL,
	[isVotingCompleted] [bit] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[developerVotingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProgressReportDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProgressReportDetails](
	[projectProgressReportIDetailsd] [int] IDENTITY(1,1) NOT NULL,
	[projectProgressReportId] [int] NOT NULL,
	[projectStageId] [int] NOT NULL,
	[developerId] [int] NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[projectProgressReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProgressReportMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProgressReportMaster](
	[projectProgressReportId] [int] IDENTITY(1,1) NOT NULL,
	[projectProgressReportName] [nvarchar](250) NOT NULL,
	[isActive] [bit] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[projectProgressReportId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectProgressStage]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectProgressStage](
	[projectProgressStageId] [int] IDENTITY(1,1) NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[offerId] [int] NOT NULL,
	[developerId] [int] NOT NULL,
	[projectStageId] [int] NOT NULL,
	[status] [int] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[projectProgressStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectStageMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectStageMaster](
	[projectStageId] [int] IDENTITY(1,1) NOT NULL,
	[stageName] [nvarchar](250) NOT NULL,
	[isActive] [bit] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[projectStageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[refreshTokenId] [uniqueidentifier] NOT NULL,
	[token] [nvarchar](max) NOT NULL,
	[jwtId] [varchar](50) NOT NULL,
	[userId] [int] NULL,
	[createdDate] [datetime] NOT NULL,
	[expiryDate] [datetime] NOT NULL,
	[isUsed] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[refreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisteredSociety]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredSociety](
	[registeredSocietyId] [int] IDENTITY(1,1) NOT NULL,
	[societyRegistrationNumber] [nvarchar](50) NULL,
	[societyName] [nvarchar](500) NOT NULL,
	[existingMemberCount] [int] NOT NULL,
	[age] [float] NOT NULL,
	[plotDimensions] [nvarchar](70) NULL,
	[approchRoadWidth] [float] NULL,
	[totalCarpetBldgArea] [float] NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[societyRegisteredCode] [nvarchar](15) NULL,
	[commercialUse] [bit] NULL,
	[residentialUse] [bit] NULL,
	[mixedUse] [bit] NULL,
	[numberOfCommercialTenaments] [int] NULL,
	[numberOfResidentialTenaments] [int] NULL,
	[numberOfMixedTenaments] [int] NULL,
	[totalCommercialBuiltUpBldgArea] [float] NULL,
	[totalResidentialBuiltUpBldgArea] [float] NULL,
	[totalMixedBuiltUpBldgArea] [float] NULL,
	[societyDevelopmentTypeId] [int] NOT NULL,
	[societyDevelopmentSubType] [nvarchar](300) NOT NULL,
	[registeredAddress] [nvarchar](max) NOT NULL,
	[email] [nvarchar](700) NOT NULL,
	[sizeOfPlot] [int] NULL,
	[city] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[registeredSocietyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisteredSocietyDocDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredSocietyDocDetails](
	[societyDocDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[societyDocId] [int] NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[docPath] [nvarchar](500) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[societyDocDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocietyDevelopmentType]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocietyDevelopmentType](
	[societyDevelopmentTypeId] [int] IDENTITY(1,1) NOT NULL,
	[developmentTypeName] [nvarchar](300) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[societyDevelopmentTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocietyDocMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocietyDocMaster](
	[societyDocId] [int] IDENTITY(1,1) NOT NULL,
	[documentName] [nvarchar](250) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
	[societyDocTypeId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[societyDocId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocietyDocTypeMaster]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocietyDocTypeMaster](
	[societyDocTypeId] [int] IDENTITY(1,1) NOT NULL,
	[societyDocType] [nvarchar](100) NOT NULL,
	[createdBy] [nvarchar](100) NULL,
	[createdDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[societyDocTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocietyMemberDesignation]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocietyMemberDesignation](
	[societyMemberDesignationId] [int] IDENTITY(1,1) NOT NULL,
	[designationType] [nvarchar](800) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[societyMemberDesignationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocietyMemberDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocietyMemberDetails](
	[societyMemberDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[memberName] [nvarchar](800) NOT NULL,
	[mobileNumber] [nvarchar](11) NULL,
	[email] [nvarchar](400) NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NULL,
	[updatedBy] [uniqueidentifier] NULL,
	[societyMemberDesignationId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[societyMemberDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [uq_member_index] UNIQUE NONCLUSTERED 
(
	[mobileNumber] ASC,
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TenderDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenderDetails](
	[tenderId] [int] IDENTITY(1,1) NOT NULL,
	[registeredSocietyId] [int] NOT NULL,
	[percentageOfIncreaseArea] [float] NULL,
	[quantamOfAreaAtDiscountRate] [float] NULL,
	[expectedDiscountRate] [float] NULL,
	[corpusFund] [float] NULL,
	[rentPerSqFtFlat] [float] NULL,
	[rentPerSqFtOffice] [float] NULL,
	[rentPerSqFtShop] [float] NULL,
	[parkingPerMember] [int] NULL,
	[typeOfProject] [nvarchar](100) NULL,
	[refundableDepositPerMemberForFlat] [float] NULL,
	[refundableDepositPerMemberForOffice] [float] NULL,
	[refundableDepositPerMemberForShop] [float] NULL,
	[shiftingChargesForFlatOfficeShop] [float] NULL,
	[bettermentChargesPerMember] [float] NULL,
	[isApprovedBySociety] [bit] NULL,
	[tenderCode] [nvarchar](100) NULL,
	[status] [int] NULL,
	[reason] [nvarchar](200) NULL,
	[CreationDate] [datetime] NULL,
	[UpdationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[tenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TenderNotice]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenderNotice](
	[tenderNoticeId] [int] IDENTITY(1,1) NOT NULL,
	[registeredSocietyId] [int] NULL,
	[startDate] [datetime] NOT NULL,
	[endDate] [datetime] NOT NULL,
	[publicationDate] [datetime] NULL,
	[presentationDate] [datetime] NULL,
	[status] [int] NULL,
	[isActive] [bit] NULL,
	[createdBy] [nvarchar](100) NULL,
	[createdDate] [datetime] NULL,
	[updatedBy] [nvarchar](100) NULL,
	[updatedDate] [datetime] NULL,
	[tenderCode] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tenderNoticeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TenderStatus]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TenderStatus](
	[tenderStatusId] [int] IDENTITY(1,1) NOT NULL,
	[status] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tenderStatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[test]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[test](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[userId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](100) NULL,
	[userDisplayName] [nvarchar](500) NULL,
	[email] [nvarchar](300) NULL,
	[address] [nvarchar](700) NULL,
	[userCode] [nvarchar](50) NOT NULL,
	[isActive] [bit] NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [User_userCode_UQ] UNIQUE NONCLUSTERED 
(
	[userCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccess]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccess](
	[userAccessId] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[userRoleId] [int] NOT NULL,
 CONSTRAINT [PK_UserAccess] PRIMARY KEY CLUSTERED 
(
	[userAccessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserOTPDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOTPDetails](
	[OTPDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](100) NOT NULL,
	[userMobileNumber] [nvarchar](20) NOT NULL,
	[OTP] [nvarchar](20) NOT NULL,
	[creationDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OTPDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[userRoleId] [int] NOT NULL,
	[roleName] [nvarchar](200) NOT NULL,
	[createdBy] [uniqueidentifier] NOT NULL,
	[createdDate] [datetime] NOT NULL,
	[updatedBy] [uniqueidentifier] NOT NULL,
	[updatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[userRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DeveloperMaster] ADD  DEFAULT ((0)) FOR [organisationTypeId]
GO
ALTER TABLE [dbo].[Blogs]  WITH CHECK ADD FOREIGN KEY([blogsTypeMasterId])
REFERENCES [dbo].[BlogsTypeMaster] ([blogsTypeId])
GO
ALTER TABLE [dbo].[DeveloperKYC]  WITH CHECK ADD  CONSTRAINT [Fk_DeveloperKYC_Developer] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[DeveloperKYC] CHECK CONSTRAINT [Fk_DeveloperKYC_Developer]
GO
ALTER TABLE [dbo].[DeveloperKYC]  WITH CHECK ADD  CONSTRAINT [Fk_DeveloperKYC_OrgnisationType] FOREIGN KEY([orgnisationTypeId])
REFERENCES [dbo].[OrgnisationType] ([orgnisationTypeId])
GO
ALTER TABLE [dbo].[DeveloperKYC] CHECK CONSTRAINT [Fk_DeveloperKYC_OrgnisationType]
GO
ALTER TABLE [dbo].[DeveloperPastProjectDetails]  WITH CHECK ADD FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[DeveloperTenderDetails]  WITH CHECK ADD FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[DeveloperTenderDetails]  WITH CHECK ADD FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[FinalizedDeveloper]  WITH CHECK ADD  CONSTRAINT [Fk_FinalizedDeveloper_DeveloperMaster] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[FinalizedDeveloper] CHECK CONSTRAINT [Fk_FinalizedDeveloper_DeveloperMaster]
GO
ALTER TABLE [dbo].[FinalizedDeveloper]  WITH CHECK ADD  CONSTRAINT [Fk_FinalizedDeveloper_LetterOfInterest] FOREIGN KEY([letterOfIntrestId])
REFERENCES [dbo].[LetterOfInterest] ([letterOfInterestId])
GO
ALTER TABLE [dbo].[FinalizedDeveloper] CHECK CONSTRAINT [Fk_FinalizedDeveloper_LetterOfInterest]
GO
ALTER TABLE [dbo].[FinalizedDeveloper]  WITH CHECK ADD  CONSTRAINT [Fk_FinalizedDeveloper_Offer] FOREIGN KEY([offerId])
REFERENCES [dbo].[Offer] ([offerId])
GO
ALTER TABLE [dbo].[FinalizedDeveloper] CHECK CONSTRAINT [Fk_FinalizedDeveloper_Offer]
GO
ALTER TABLE [dbo].[LetterOfInterest]  WITH CHECK ADD  CONSTRAINT [Fk_LetterOfInterest_Developer] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[LetterOfInterest] CHECK CONSTRAINT [Fk_LetterOfInterest_Developer]
GO
ALTER TABLE [dbo].[LetterOfInterest]  WITH CHECK ADD  CONSTRAINT [Fk_LetterOfInterest_Offer] FOREIGN KEY([offerId])
REFERENCES [dbo].[Offer] ([offerId])
GO
ALTER TABLE [dbo].[LetterOfInterest] CHECK CONSTRAINT [Fk_LetterOfInterest_Offer]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[Offer]  WITH NOCHECK ADD  CONSTRAINT [FK__Offer__tenderId__703EA55A] FOREIGN KEY([tenderId])
REFERENCES [dbo].[TenderDetails] ([tenderId])
GO
ALTER TABLE [dbo].[Offer] NOCHECK CONSTRAINT [FK__Offer__tenderId__703EA55A]
GO
ALTER TABLE [dbo].[Offer]  WITH CHECK ADD  CONSTRAINT [Fk_Offer_RegisterdSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[Offer] CHECK CONSTRAINT [Fk_Offer_RegisterdSociety]
GO
ALTER TABLE [dbo].[PartnerKYC]  WITH CHECK ADD  CONSTRAINT [Fk_PartnerKYC_DesignationType] FOREIGN KEY([designationTypeId])
REFERENCES [dbo].[DesignationType] ([designationTypeId])
GO
ALTER TABLE [dbo].[PartnerKYC] CHECK CONSTRAINT [Fk_PartnerKYC_DesignationType]
GO
ALTER TABLE [dbo].[PartnerKYC]  WITH CHECK ADD  CONSTRAINT [Fk_PartnerKYC_Developer] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[PartnerKYC] CHECK CONSTRAINT [Fk_PartnerKYC_Developer]
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting]  WITH CHECK ADD  CONSTRAINT [Fk_PreferedDeveloperVoting_LetterOfInterest] FOREIGN KEY([letterOfIntrestId])
REFERENCES [dbo].[LetterOfInterest] ([letterOfInterestId])
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting] CHECK CONSTRAINT [Fk_PreferedDeveloperVoting_LetterOfInterest]
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting]  WITH CHECK ADD  CONSTRAINT [Fk_PreferedDeveloperVoting_Offer] FOREIGN KEY([offerId])
REFERENCES [dbo].[Offer] ([offerId])
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting] CHECK CONSTRAINT [Fk_PreferedDeveloperVoting_Offer]
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting]  WITH CHECK ADD  CONSTRAINT [Fk_PreferedDeveloperVoting_RegisterdSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[PreferedDeveloperVoting] CHECK CONSTRAINT [Fk_PreferedDeveloperVoting_RegisterdSociety]
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressReportDetails_DeveloperMaster] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails] CHECK CONSTRAINT [Fk_ProjectProgressReportDetails_DeveloperMaster]
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressReportDetails_ProjectProgressReportMaster] FOREIGN KEY([projectProgressReportId])
REFERENCES [dbo].[ProjectProgressReportMaster] ([projectProgressReportId])
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails] CHECK CONSTRAINT [Fk_ProjectProgressReportDetails_ProjectProgressReportMaster]
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressReportDetails_ProjectStageMaster] FOREIGN KEY([projectStageId])
REFERENCES [dbo].[ProjectStageMaster] ([projectStageId])
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails] CHECK CONSTRAINT [Fk_ProjectProgressReportDetails_ProjectStageMaster]
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressReportDetails_RegisteredSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[ProjectProgressReportDetails] CHECK CONSTRAINT [Fk_ProjectProgressReportDetails_RegisteredSociety]
GO
ALTER TABLE [dbo].[ProjectProgressStage]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressStage_DeveloperMaster] FOREIGN KEY([developerId])
REFERENCES [dbo].[DeveloperMaster] ([developerId])
GO
ALTER TABLE [dbo].[ProjectProgressStage] CHECK CONSTRAINT [Fk_ProjectProgressStage_DeveloperMaster]
GO
ALTER TABLE [dbo].[ProjectProgressStage]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressStage_Offer] FOREIGN KEY([offerId])
REFERENCES [dbo].[Offer] ([offerId])
GO
ALTER TABLE [dbo].[ProjectProgressStage] CHECK CONSTRAINT [Fk_ProjectProgressStage_Offer]
GO
ALTER TABLE [dbo].[ProjectProgressStage]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressStage_ProjectStageMaster] FOREIGN KEY([projectStageId])
REFERENCES [dbo].[ProjectStageMaster] ([projectStageId])
GO
ALTER TABLE [dbo].[ProjectProgressStage] CHECK CONSTRAINT [Fk_ProjectProgressStage_ProjectStageMaster]
GO
ALTER TABLE [dbo].[ProjectProgressStage]  WITH CHECK ADD  CONSTRAINT [Fk_ProjectProgressStage_RegisteredSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[ProjectProgressStage] CHECK CONSTRAINT [Fk_ProjectProgressStage_RegisteredSociety]
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[RegisteredSociety]  WITH CHECK ADD  CONSTRAINT [Fk_RegisteredSociety_SocietyDevelopmentType] FOREIGN KEY([societyDevelopmentTypeId])
REFERENCES [dbo].[SocietyDevelopmentType] ([societyDevelopmentTypeId])
GO
ALTER TABLE [dbo].[RegisteredSociety] CHECK CONSTRAINT [Fk_RegisteredSociety_SocietyDevelopmentType]
GO
ALTER TABLE [dbo].[RegisteredSocietyDocDetails]  WITH CHECK ADD  CONSTRAINT [Fk_RegisteredSocietyDocDetails_RegisteredSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[RegisteredSocietyDocDetails] CHECK CONSTRAINT [Fk_RegisteredSocietyDocDetails_RegisteredSociety]
GO
ALTER TABLE [dbo].[RegisteredSocietyDocDetails]  WITH CHECK ADD  CONSTRAINT [Fk_RegisteredSocietyDocDetails_SocietyDocMaster] FOREIGN KEY([societyDocId])
REFERENCES [dbo].[SocietyDocMaster] ([societyDocId])
GO
ALTER TABLE [dbo].[RegisteredSocietyDocDetails] CHECK CONSTRAINT [Fk_RegisteredSocietyDocDetails_SocietyDocMaster]
GO
ALTER TABLE [dbo].[SocietyDocMaster]  WITH CHECK ADD FOREIGN KEY([societyDocTypeId])
REFERENCES [dbo].[SocietyDocTypeMaster] ([societyDocTypeId])
GO
ALTER TABLE [dbo].[SocietyMemberDetails]  WITH CHECK ADD  CONSTRAINT [Fk_SocietyMemberDetails_RegisterdSociety] FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[SocietyMemberDetails] CHECK CONSTRAINT [Fk_SocietyMemberDetails_RegisterdSociety]
GO
ALTER TABLE [dbo].[SocietyMemberDetails]  WITH CHECK ADD  CONSTRAINT [Fk_SocietyMemberDetails_SocietyMemberDesignation] FOREIGN KEY([societyMemberDesignationId])
REFERENCES [dbo].[SocietyMemberDesignation] ([societyMemberDesignationId])
GO
ALTER TABLE [dbo].[SocietyMemberDetails] CHECK CONSTRAINT [Fk_SocietyMemberDetails_SocietyMemberDesignation]
GO
ALTER TABLE [dbo].[TenderDetails]  WITH CHECK ADD FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[TenderNotice]  WITH CHECK ADD FOREIGN KEY([registeredSocietyId])
REFERENCES [dbo].[RegisteredSociety] ([registeredSocietyId])
GO
ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [Fk_User_UserAccess] FOREIGN KEY([userId])
REFERENCES [dbo].[User] ([userId])
GO
ALTER TABLE [dbo].[UserAccess] CHECK CONSTRAINT [Fk_User_UserAccess]
GO
ALTER TABLE [dbo].[UserAccess]  WITH CHECK ADD  CONSTRAINT [Fk_UserRole_UserAccess] FOREIGN KEY([userRoleId])
REFERENCES [dbo].[UserRole] ([userRoleId])
GO
ALTER TABLE [dbo].[UserAccess] CHECK CONSTRAINT [Fk_UserRole_UserAccess]
GO
/****** Object:  StoredProcedure [dbo].[AddDeveloperTenderDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddDeveloperTenderDetails]
(
	@developerTenderDetailsId INT,
	@developerId INT,
	@registeredSocietyId INT,
	@tenderCode NVARCHAR(100),
	@percentageOfIncreaseArea FLOAT,
	@quantamOfAreaAtDiscountRate FLOAT,
	@expectedDiscountRate FLOAT,
	@corpusFund FLOAT,
	@rentPerSqFtFlat FLOAT,
	@rentPerSqFtOffice FLOAT,
	@rentPerSqFtShop FLOAT,
	@parkingPerMember INT,
	@typeOfProject NVARCHAR(100),
	@refundableDepositPerMemberForFlat FLOAT,
	@refundableDepositPermemberForOffice FLOAT,
	@refundableDepositPerMemberForShop FLOAT,
	@shiftingChargesForFlatOfficeShop FLOAT,
	@bettermentChargesPerMember FLOAT,
	@developerTenderPdfPath NVARCHAR(MAX),
	@Id INT OUTPUT
)
AS 
BEGIN 
	
	If(@developerTenderDetailsId <= 0) 
		BEGIN
			INSERT INTO DeveloperTenderDetails 
				Values
				(
					@developerId, @registeredSocietyId, @tenderCode, @percentageOfIncreaseArea,
					@quantamOfAreaAtDiscountRate, @expectedDiscountRate, @corpusFund,
					@rentPerSqFtFlat, @rentPerSqFtOffice, @rentPerSqFtShop,
					@parkingPerMember, @typeOfProject,
					@refundableDepositPerMemberForFlat,
					@refundableDepositPermemberForOffice,
					@refundableDepositPerMemberForShop,
					@shiftingChargesForFlatOfficeShop,
					@bettermentChargesPerMember,
					@developerTenderPdfPath
				)
			
			-- Returning the tenderId
			SET @Id = (SELECT SCOPE_IDENTITY())
		END
	ELSE 
		BEGIN
			UPDATE DeveloperTenderDetails
				SET 
					developerId = @developerId,
					registeredSocietyId = @registeredSocietyId,
					tenderCode = @tenderCode,
					percentageOfIncreaseArea = @percentageOfIncreaseArea,
					quantamOfAreaAtDiscountRate = @quantamOfAreaAtDiscountRate,
					expectedDiscountRate = @expectedDiscountRate,
					corpusFund = @corpusFund,
					rentPerSqFtFlat = @rentPerSqFtFlat,
					rentPerSqFtOffice = @rentPerSqFtOffice,
					rentPerSqFtShop = @rentPerSqFtShop,
					parkingPerMember = @parkingPerMember,
					typeofProject = @typeOfProject,
					refundableDepositPerMemberForFlat = @refundableDepositPerMemberForFlat,
					refundableDepositPermemberForOffice = @refundableDepositPermemberForOffice,
					refundableDepositPerMemberForShop = @refundableDepositPerMemberForShop,
					shiftingChargesForFlatOfficeShop = @shiftingChargesForFlatOfficeShop,
					bettermentChargesPerMember = @bettermentChargesPerMember,
					developerTenderPdfPath = @developerTenderPdfPath
		
		-- Returning the tenderId
		SET @Id = (SELECT SCOPE_IDENTITY())
		END
		SELECT @Id
END;
GO
/****** Object:  StoredProcedure [dbo].[AddLetterOfInterestReceivedDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddLetterOfInterestReceivedDetails] 
(
	@DeveloperId INT,
	@TenderId INT,
	@Interested BIT,
	@LetterOfInterestId INT OUT
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @YearOfExperience INT,
			@OfficeAddress NVARCHAR(100),
			@ContactDetails NVARCHAR(100),
			@EmailId NVARCHAR(100),
			@IsListed BIT,
			@OfferId INT

	SELECT @OfferId = offerId FROM Offer
	WHERE developerId = @DeveloperId AND tenderId = @TenderId

	SELECT 
		@YearOfExperience = experienceYear,
		@OfficeAddress = address,
		@ContactDetails = mobileNumber,
		@EmailId = email,
		@IsListed = 1
	FROM DeveloperMaster
	WHERE developerId = @DeveloperId
	
	INSERT INTO LetterOfInterest
	VALUES (@DeveloperId, @IsListed, @YearOfExperience, NULL, NULL, @OfficeAddress, @ContactDetails, @EmailId, @Interested, 
	NEWID(), GETDATE(), NEWID(), GETDATE(), @OfferId)

	SET @LetterOfInterestId = (SELECT SCOPE_IDENTITY())

	SELECT @LetterOfInterestId

END;
GO
/****** Object:  StoredProcedure [dbo].[AddLetterOfInterestSendDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddLetterOfInterestSendDetails] 
(
	@DeveloperId INT,
	@SocietyId INT,
	@TenderId INT,
	@OfferId INT OUT
)
AS
BEGIN
	
		SET NOCOUNT ON;

		DECLARE
			@NameOfSociety NVARCHAR(100),
			@ExistingMemberCount INT,
			@Age INT,
			@PlotDimensions NVARCHAR(100),
			@SizeOfPlot INT,
			@ApprochRoadWidth FLOAT,
			@TotalCarpetBldgArea FLOAT

		SELECT
			@NameOfSociety = societyName,
			@ExistingMemberCount = existingMemberCount,
			@Age = age,
			@SizeOfPlot = sizeOfPlot,
			@PlotDimensions = plotDimensions,
			@ApprochRoadWidth = approchRoadWidth,
			@TotalCarpetBldgArea = ISNULL(totalCarpetBldgArea, 0)
		FROM RegisteredSociety 
		WHERE registeredSocietyId = @SocietyId

		INSERT INTO Offer 
		VALUES (@NameOfSociety, @ExistingMemberCount, @Age, @SizeOfPlot, @PlotDimensions, @ApprochRoadWidth,
		@TotalCarpetBldgArea, NULL, NULL, NEWID(), GETDATE(), NEWID(), GETDATE(), @SocietyId, @DeveloperId, @TenderId)

		SET @OfferId = (SELECT SCOPE_IDENTITY())
		SELECT @OfferId
END;

GO
/****** Object:  StoredProcedure [dbo].[AddNewBlog]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddNewBlog]
(
	@typeId INT,
	@heading NVARCHAR(MAX),
	@description NVARCHAR(MAX),
	@path NVARCHAR(MAX),
	@isDeleted BIT,
	@createdBy INT,
	@blogId INT OUTPUT
)
AS 
BEGIN
		
	INSERT INTO Blogs
		(blogsTypeMasterId, heading, [description], [path], isDeleted, createdBy, updatedBy, createdDate, updatedDate)
	VALUES 
		(@typeId, @heading, @description, @path, @isDeleted, @createdBy, @createdBy, GETDATE(), GETDATE())

	SET @blogId = (SELECT SCOPE_IDENTITY())

	SELECT @blogId
END;

GO
/****** Object:  StoredProcedure [dbo].[AddNewDeveloper]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[AddNewDeveloper]
@name nvarchar(250),
@address nvarchar(500),
@city nvarchar(100),
@logoPath nvarchar(1000),
@siteLink nvarchar(200),
@email nvarchar(250),
@profilePath nvarchar(250),
@mobileNumber nvarchar(11),
@registeredDeveloperCode uniqueidentifier,
@createdBy uniqueidentifier,
@createdDate datetime,
@updatedBy uniqueidentifier,
@updatedDate datetime,
@organisationTypeId int,
@experienceYear int,
@projectsInHand int,
@numberOfRERARegisteredProjects int,
@totalCompletedProjects int,
@totalConstructionAreaDevTillToday nvarchar(100),
@sizeOfTheLargestProjectHandled nvarchar(100),
@experienceInHighRiseBuildings nvarchar(100),
@avgTurnOverforLastThreeYears nvarchar(100),
@affilicationToAnyDevAssociation bit,
@awardsAndRecognition nvarchar(100),
@haveBusinessInMultipleCities bit,
@affilicationDevAssociationName nvarchar(100),

@developerId INT OUTPUT
AS

BEGIN
     INSERT INTO dbo.DeveloperMaster
	 (
        [name],
		[address],
		logoPath, 
		siteLink,
		[email],
		profilePath,
		mobileNumber,
		registeredDeveloperCode,
		createdBy,
		createdDate,
		updatedBy,
		updatedDate,
		organisationTypeId,
		experienceYear,
		projectsInHand,
		numberOfRERARegisteredProjects,
		totalCompletedProjects,
		totalConstructionAreaDevTillToday,
		sizeOfTheLargestProjectHandled,
		experienceInHighRiseBuildings,
		avgTurnOverforLastThreeYears,
		affilicationToAnyDevAssociation,
		awardsAndRecognition,
		haveBusinessInMultipleCities,
		affilicationDevAssociationName
	 )
	VALUES
	(
		@name,
		@address,
		@logoPath,
		@siteLink,
		@email,
		@profilePath,
		@mobileNumber,
		NEWID(),
		NEWID(),
		GetDate(),
		NEWID(),
		GETDATE(),
		@organisationTypeId,
		@experienceYear,
		@projectsInHand,
		@numberOfRERARegisteredProjects,
		@totalCompletedProjects,
		@totalConstructionAreaDevTillToday,
		@sizeOfTheLargestProjectHandled,
		@experienceInHighRiseBuildings,
		@avgTurnOverforLastThreeYears,
		@affilicationToAnyDevAssociation,
		@awardsAndRecognition,
		@haveBusinessInMultipleCities,
		@affilicationDevAssociationName

	)
	SET @developerId = (SELECT SCOPE_IDENTITY())

   SELECT @developerId AS DeveloperId
END
GO
/****** Object:  StoredProcedure [dbo].[AddPartnerKYCDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddPartnerKYCDetails]
(
	@designationTypeId INT,
	@developerId INT,
	@email NVARCHAR(250),
	@contactNumber NVARCHAR(11),
	@panCard NVARCHAR(10),
	@aadharCard NVARCHAR(12),
	@partnerFileUrl NVARCHAR(MAX),
	@createdDate DATETIME,
	@updatedDate DATETIME,
	@partnerKYCId INT OUTPUT
)
AS 
BEGIN 
	INSERT INTO PartnerKYC
		VALUES
			(
				@designationTypeId,
				@developerId,
				@email,
				@contactNumber,
				@panCard,
				@aadharCard,
				@createdDate,
				@updatedDate,
				NEWID(),
				NEWID(),
				@partnerFileUrl
			)

		SET @partnerKYCId = (SELECT SCOPE_IDENTITY())

		SELECT @partnerKYCId
END;
GO
/****** Object:  StoredProcedure [dbo].[AddRegisteredSocietyNewMembersList]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[AddRegisteredSocietyNewMembersList](
	@newMemberDetails udt_MemberDetails  READONLY,
	@Message NVARCHAR(500) OUTPUT
)
AS 
BEGIN

	BEGIN TRY	
		MERGE SocietyMemberDetails AS Target
		USING @newMemberDetails AS Source
		ON Source.email = Target.email AND Source.mobileNumber = Target.mobileNumber
		AND Source.registeredSocietyId = Target.registeredSocietyId
		--For Inserts
		WHEN NOT MATCHED BY Target THEN
			INSERT (registeredSocietyId, memberName, mobileNumber, email, createdDate, updatedDate, createdBy, updatedBy)
			VALUES (registeredSocietyId, memberName, mobileNumber, email, GETDATE(), GETDATE(), NEWID(), NEWID())
		--For Updates
		WHEN MATCHED THEN UPDATE SET 
			Target.registeredSocietyId = Source.registeredSocietyId,
			Target.memberName = Source.memberName,
			Target.mobileNumber = Source.mobileNumber,
			Target.email = Source.email,
			Target.updatedDate = GETDATE(),
			Target.updatedBy = NEWID();
			--Target.societyMemberDesignationId = 4;
		END TRY
	BEGIN CATCH
		SET @Message = (SELECT ERROR_MESSAGE())
	END CATCH

	SELECT @Message
END;


GO
/****** Object:  StoredProcedure [dbo].[AddSendTenderNoticeDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddSendTenderNoticeDetails]
(
	@RegisteredSocietyId INT,
	@TenderCode NVARCHAR(100),
	@EndDate DATETIME,
	@StartDate DATETIME,
	@PublicationDate DATETIME,
	@PresentationDate DATETIME,
	@TenderNoticeId INT OUT
)
AS
BEGIN
	
	INSERT INTO TenderNotice
	VALUES (@RegisteredSocietyId, @StartDate, @EndDate, @PublicationDate, @PresentationDate, 1, 1, 
	NEWID(), GETDATE(), NEWID(), GETDATE(), @TenderCode)

	SET @TenderNoticeId = (SELECT SCOPE_IDENTITY())
	SELECT @TenderNoticeId

END;
GO
/****** Object:  StoredProcedure [dbo].[AddTenderDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AddTenderDetails]
(
	@tenderId INT,
	@registeredSocietyId INT,
	@percentageOfIncreaseArea FLOAT,
	@quantamOfAreaAtDiscountRate FLOAT,
	@expectedDiscountRate FLOAT,
	@corpusFund FLOAT,
	@rentPerSqFtFlat FLOAT,
	@rentPerSqFtOffice FLOAT,
	@rentPerSqFtShop FLOAT,
	@parkingPerMember INT,
	@typeOfProject NVARCHAR(100),
	@refundableDepositPerMemberForFlat FLOAT,
	@refundableDepositPermemberForOffice FLOAT,
	@refundableDepositPerMemberForShop FLOAT,
	@shiftingChargesForFlatOfficeShop FLOAT,
	@bettermentChargesPerMember FLOAT,
	@isApprovedBySociety BIT,
	@tenderCode NVARCHAR(100),
	@status INT,
	@tenderPK INT OUTPUT
)
AS 
BEGIN 
	
	If(@tenderId <= 0) 
		BEGIN
			INSERT INTO TenderDetails 
				Values
				(
					@registeredSocietyId, @percentageOfIncreaseArea,
					@quantamOfAreaAtDiscountRate, @expectedDiscountRate, @corpusFund,
					@rentPerSqFtFlat, @rentPerSqFtOffice, @rentPerSqFtShop,
					@parkingPerMember, @typeOfProject,
					@refundableDepositPerMemberForFlat,
					@refundableDepositPermemberForOffice,
					@refundableDepositPerMemberForShop,
					@shiftingChargesForFlatOfficeShop,
					@bettermentChargesPerMember,
					@isApprovedBySociety,
					@tenderCode,
					@status, 
					NULL,
					GETDATE(),
					GETDATE()
				)
			
			-- Returning the tenderId
			SET @tenderPK = (SELECT SCOPE_IDENTITY())
		END
	ELSE 
		BEGIN
			UPDATE TenderDetails
				SET 

					registeredSocietyId = @registeredSocietyId,
					percentageOfIncreaseArea = @percentageOfIncreaseArea,
					quantamOfAreaAtDiscountRate = @quantamOfAreaAtDiscountRate,
					expectedDiscountRate = @expectedDiscountRate,
					corpusFund = @corpusFund,
					rentPerSqFtFlat = @rentPerSqFtFlat,
					rentPerSqFtOffice = @rentPerSqFtOffice,
					rentPerSqFtShop = @rentPerSqFtShop,
					parkingPerMember = @parkingPerMember,
					typeofProject = @typeOfProject,
					refundableDepositPerMemberForFlat = @refundableDepositPerMemberForFlat,
					refundableDepositPermemberForOffice = @refundableDepositPermemberForOffice,
					refundableDepositPerMemberForShop = @refundableDepositPerMemberForShop,
					shiftingChargesForFlatOfficeShop = @shiftingChargesForFlatOfficeShop,
					bettermentChargesPerMember = @bettermentChargesPerMember,
					isApprovedBySociety = @isApprovedBySociety,
					tenderCode = @tenderCode,
					[status] = @status,
					updationDate = GETDATE()
					
		
		-- Returning the tenderId
		SET @tenderPK = (SELECT SCOPE_IDENTITY())
		END
		SELECT @tenderId
END;
GO
/****** Object:  StoredProcedure [dbo].[DeleteRegisteredSocietyMemberById]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

	CREATE Procedure [dbo].[DeleteRegisteredSocietyMemberById] (
		@societyMemberId int,
		@societyId int
	)
	As 
	Begin
		Delete From SocietyMemberDetails
			Where societyMemberDetailsId = @societyMemberId 
			And registeredSocietyId = @societyId
	End
GO
/****** Object:  StoredProcedure [dbo].[GetAllBlogs]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetAllBlogs]
AS 
BEGIN
	
	SELECT B.blogId, b.blogsTypeMasterId AS typeId, b.heading, b.description, '0' AS createdBy,
		   b.path,BTM.typeName
	FROM Blogs B 
	INNER JOIN BlogsTypeMaster BTM ON B.blogsTypeMasterId = BTM.blogsTypeId
	WHERE B.isDeleted = 0
		
END;
GO
/****** Object:  StoredProcedure [dbo].[GetAllRegisteredSocietyMembersList]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetAllRegisteredSocietyMembersList]
AS
BEGIN
    SELECT * FROM dbo.SocietyMemberDetails
END
GO
/****** Object:  StoredProcedure [dbo].[GetBlogById]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetBlogById] 
( @blogId INT)
AS
BEGIN

	SELECT B.blogId, b.blogsTypeMasterId AS typeId, b.heading, b.description, b.path, '0' AS createdBy, BTM.typeName
	FROM Blogs B 
	INNER JOIN BlogsTypeMaster BTM ON B.blogsTypeMasterId = BTM.blogsTypeId
	WHERE B.blogId = @blogId

END;
GO
/****** Object:  StoredProcedure [dbo].[GetDeveloperByID]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[GetDeveloperByID]
@developerId int
AS
BEGIN
    SELECT *
  --      developerId,
  --      [name],
  --      [address],
  --      logoPath,
  --      siteLink,
		--[email],
		--profilePath,
		--mobileNumber,
		--experienceYear,
		--registeredDeveloperCode,
  --      createdBy,
  --      createdDate,
  --      updatedBy,
  --      updatedDate
    FROM dbo.DeveloperMaster where developerId = @developerId
END
GO
/****** Object:  StoredProcedure [dbo].[GetDeveloperList]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[GetDeveloperList]
AS
BEGIN
    SELECT * FROM dbo.DeveloperMaster
END
GO
/****** Object:  StoredProcedure [dbo].[GetDeveloperTenderBySocietyId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GetDeveloperTenderBySocietyId]
(
	@regSocietyId INT
)
AS
BEGIN
		
	SELECT DTD.developerId, DTD.registeredSocietyId, DM.name, DTD.developerTenderPdfPath
		FROM DeveloperTenderDetails DTD 
		INNER JOIN DeveloperMaster DM ON DTD.developerId = DM.developerId
		WHERE DTD.registeredSocietyId = @regSocietyId
END;
GO
/****** Object:  StoredProcedure [dbo].[GetRegisterdSocietyTechnicalDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRegisterdSocietyTechnicalDetails]
(
	@RegisteredSocietyId INT
)
AS 
BEGIN

	SELECT 
		registeredSocietyId,
		societyName,
		sizeOfPlot,
		plotDimensions,
		residentialUse,
		commercialUse,
		mixedUse,
		numberOfMixedTenaments,
		numberOfCommercialTenaments,
		numberOfResidentialTenaments,
		totalCommercialBuiltUpBldgArea,
		totalResidentialBuiltUpBldgArea,
		totalMixedBuiltUpBldgArea,
		approchRoadWidth

	FROM RegisteredSociety 
	WHERE registeredSocietyId = @RegisteredSocietyId


END;
GO
/****** Object:  StoredProcedure [dbo].[GetRegisteredSocietyByID]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- problem ******
CREATE   PROCEDURE [dbo].[GetRegisteredSocietyByID]
@registeredSocietyId int
AS
BEGIN
    SELECT *,
  --      registeredSocietyId,
  --      societyRegistrationNumber,
  --      societyName,
  --      existingMemberCount,
  --      age,
		--sizeOfPlot,
  --      plotDimensions,
  --      approchRoadWidth,
  --      totalCarpetBldgArea,
  --      --preliminaryStatus,
		----secondaryStatus,
  --      createdBy,
  --      createdDate,
  --      updatedBy,
  --      updatedDate,
		-1 AS ActiveTenderId
    FROM dbo.RegisteredSociety where registeredSocietyId = @registeredSocietyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegisteredSocietyDocDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRegisteredSocietyDocDetails]
(
	@registeredSocietyId INT
)
AS
BEGIN
		
	SELECT RSDD.registeredSocietyId, SDTM.societyDocType, SCM.documentName, RSDD.docPath 
	FROM RegisteredSocietyDocDetails RSDD 
		INNER JOIN SocietyDocMaster SCM ON RSDD.societyDocId = SCM.societyDocId
		INNER JOIN SocietyDocTypeMaster SDTM ON SCM.societyDocTypeId = SDTM.societyDocTypeId
	WHERE RSDD.registeredSocietyId = @registeredSocietyId

END;
GO
/****** Object:  StoredProcedure [dbo].[GetRegisteredSocietyList]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRegisteredSocietyList] 
AS
BEGIN

    SELECT DISTINCT RS.*, 0 AS ActiveTenderId FROM dbo.RegisteredSociety RS
	--LEFT JOIN TenderDetails TD ON RS.registeredSocietyId = TD.registeredSocietyId AND TD.status = 2
END


GO
/****** Object:  StoredProcedure [dbo].[GetRegisteredSocietyMembersBySocietyId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[GetRegisteredSocietyMembersBySocietyId] (
	@societyId INT
)
AS
BEGIN
    SELECT * FROM dbo.SocietyMemberDetails
	Where registeredSocietyId = @societyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetRegisteredSocietyWithTechnicalDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetRegisteredSocietyWithTechnicalDetails] 
(
	@RegisteredSocietyId INT
)

AS 
BEGIN
	
	SELECT RS.*, SDT.developmentTypeName
	FROM RegisteredSociety RS
	INNER JOIN SocietyDevelopmentType SDT ON RS.societyDevelopmentTypeId = SDT.societyDevelopmentTypeId
	WHERE RS.registeredSocietyId = @RegisteredSocietyId;
	
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyActiveTenderIdBySocietyId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSocietyActiveTenderIdBySocietyId] 
(
	@SocietyId INT,
	@TenderId INT OUT
)
AS 
BEGIN

	SELECT @TenderId = tenderId 
	FROM TenderDetails
	WHERE registeredSocietyId = @SocietyId
	AND status = 2

	SELECT @TenderId AS TenderId
END;

GO
/****** Object:  StoredProcedure [dbo].[GetSocietyApprovedTender]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetSocietyApprovedTender]

AS
BEGIN

SELECT RS.societyName, TD.* 
	FROM RegisteredSociety RS
	INNER JOIN TenderDetails TD ON RS.registeredSocietyId = TD.registeredSocietyId
	WHERE TD.isApprovedBySociety = 1

END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyLetterOfInterestReceived]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSocietyLetterOfInterestReceived]
(
	@SocietyId INT
)
AS 
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @TenderId INT

	DECLARE @OfferIds TABLE 
	( OfferId INT)

	SELECT @TenderId = tenderId
	FROM TenderDetails 
	WHERE registeredSocietyId = @SocietyId
	AND status = 2

	INSERT INTO @OfferIds
	SELECT offerId FROM Offer WHERE tenderId = @TenderId


	SELECT DM.developerId, DM.name AS developerName, DM.Email AS developerEmail, DM.mobileNumber AS developerPhone,
	CONVERT(NVARCHAR(100), DM.registeredDeveloperCode) AS developerCode, DM.address AS developerAddress
	FROM LetterOfInterest LOI
	INNER JOIN DeveloperMaster DM ON LOI.developerId = DM.developerId
	WHERE LOI.offerId IN (SELECT * FROM @OfferIds)

END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyMemberDesignations]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[GetSocietyMemberDesignations] 
As 
Begin
	Select * from
		SocietyMemberDesignation
End
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyNameById]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSocietyNameById]
(
	@regSocietyId INT
)
AS 
BEGIN
		SELECT societyName 
		FROM RegisteredSociety
		WHERE registeredSocietyId = @regSocietyId
END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyPreTenderCompliances]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSocietyPreTenderCompliances] 
(
	@SocietyId INT
)
AS 
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE @TenderId INT

	SELECT TOP 1 @TenderId = tenderId
	FROM TenderDetails
	WHERE registeredSocietyId = @SocietyId
	AND isApprovedBySociety = 1 AND status = 2

	SELECT DM.name AS developerName, 
	CASE
		WHEN LOI.isAcceptedRules IS NULL THEN 'Pending'
		WHEN LOI.isAcceptedRules = 0 THEN 'Not Accepted'
		WHEN LOI.isAcceptedRules = 1 THEN 'Accepted' END AS status
	FROM Offer O
	INNER JOIN DeveloperMaster DM ON DM.developerId = O.developerId
	LEFT JOIN LetterOfInterest LOI ON LOI.offerId = O.offerId
	WHERE O.tenderId = @TenderId

END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyTenderDetailsByTenderId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSocietyTenderDetailsByTenderId]
(
	@TenderId INT
)
AS 
BEGIN 
	
	SELECT * 
	FROM TenderDetails 
	WHERE tenderId = @TenderId

END;
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyTendersDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetSocietyTendersDetails]
(
	@regSocietyId INT
)
AS 
BEGIN
	
	SELECT * FROM TenderDetails 
		WHERE registeredSocietyId = @regSocietyId

END;
GO
/****** Object:  StoredProcedure [dbo].[SaveSocietyDocument]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SaveSocietyDocument]
(
	@RegisteredSocietyName NVARCHAR(100),
	@DocumentName NVARCHAR(100),
	@DocPath NVARCHAR(MAX)
)
AS 
BEGIN 
	
	DECLARE @registeredSocietyId INT, @documentNameId INT, @AlreadyExist INT

	SELECT @AlreadyExist = societyDocDetailsId
		FROM RegisteredSocietyDocDetails
		WHERE docPath = @DocPath

	SELECT @registeredSocietyId = registeredSocietyId
		FROM RegisteredSociety
		WHERE societyName = @RegisteredSocietyName

	SELECT @documentNameId = societyDocId
		FROM SocietyDocMaster
		WHERE documentName = @DocumentName
	
	IF(@AlreadyExist > 0)
		BEGIN
			UPDATE RegisteredSocietyDocDetails 
				SET updatedDate = GETDATE()
			WHERE societyDocDetailsId = @AlreadyExist
		END
	ELSE 
		BEGIN
			INSERT INTO RegisteredSocietyDocDetails
			VALUES (@documentNameId, @registeredSocietyId, @DocPath, GETDATE(), GETDATE(), NULL, NULL)
		END

END;
GO
/****** Object:  StoredProcedure [dbo].[UpdateDeveloper]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[UpdateDeveloper]
@developerId int,
@name nvarchar(250),
@address [nvarchar](500),
@logoPath nvarchar(1000),
@siteLink nvarchar(200),
@email nvarchar(250),
@profilePath nvarchar(250),
@mobileNumber int,
@registeredDeveloperCode uniqueidentifier,
@createdBy uniqueidentifier,
@createdDate datetime,
@updatedBy uniqueidentifier,
@updatedDate datetime
AS

BEGIN

     UPDATE dbo.DeveloperMaster
	  SET
		[name] = @name,
        [address] = @address,
        logoPath = @logoPath,
		siteLink = @siteLink,
		[email] = @email,
		profilePath = @profilePath,
		mobileNumber = @mobileNumber,
		registeredDeveloperCode = @registeredDeveloperCode,
        createdBy = @createdBy,
        createdDate = @createdDate,
        updatedBy = @updatedBy,
        updatedDate = @updatedDate
		WHERE 
		developerId = @developerId
END

GO
/****** Object:  StoredProcedure [dbo].[UpdateMember]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateMember] (
	@MemberDetail Update_SocietyMemberNew2 Readonly
)
AS
BEGIN
	MERGE SocietyMemberDetails AS Target
	USING @memberDetail	AS Source
    ON Source.societyMemberDetailsId = Target.societyMemberDetailsId --Source.Email = Target.Email OR Source.mobileNumber = Target.MobileNumber
	WHEN NOT MATCHED By Target THEN 
		INSERT (registeredSocietyId, memberName, mobileNumber, email, createdDate, updatedDate, createdBy, updatedBy, societyMemberDesignationId)
		VALUES (registeredSocietyId, name, mobileNumber, email, getdate(), getdate(), newid(), newid(), societyMemberDesginationid)
    WHEN MATCHED THEN UPDATE SET
        Target.Membername = Source.Name,
        Target.email = Source.email,
		Target.mobileNumber = Source.mobileNumber,
		Target.UpdatedDate = GETDATE();
	
END



   
GO
/****** Object:  StoredProcedure [dbo].[usp_AddAdmin]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_AddAdmin]
(
	@AdminDetailsId INT,
	@FirstName NVARCHAR(50),
	@LastName NVARCHAR(50),
	@Email NVARCHAR(50), 
	@MobileNumber NVARCHAR(20),
	@AdminCode NVARCHAR(50),
	@IsActive BIT,
	@Id INT OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		INSERT INTO AdminDetails
		VALUES (@FirstName, @LastName, @Email, @MobileNumber, @AdminCode, @IsActive, GETDATE(), GETDATE(), NEWID(), NEWID())
	
		SET @Id = (SELECT SCOPE_IDENTITY())
		SELECT @Id AS Id

	END TRY
	BEGIN CATCH
		SET @Id = 0
		SELECT @Id AS Id
	END CATCH
END


GO
/****** Object:  StoredProcedure [dbo].[usp_AddDeveloperPastProjectDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AddDeveloperPastProjectDetails] (
	@ProjectName NVARCHAR(100),
	@ProjectLocation NVARCHAR(100),
	@ReraRegistrationNumber NVARCHAR(100),
	@ReraCertificationUrl NVARCHAR(MAX),
	@ProjectStartDate DATETIME,
	@ProjectEndDate DATETIME,
	@DeveloperId INT,
	@Id INT OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
		
		INSERT INTO DeveloperPastProjectDetails
		VALUES (@ProjectName, @ProjectLocation, @ReraRegistrationNumber, @ProjectStartDate, @ProjectEndDate, @DeveloperId,
		@ReraCertificationUrl)

		SET @Id = (SELECT SCOPE_IDENTITY())
	END TRY
	BEGIN CATCH
	END CATCH
END

GO
/****** Object:  StoredProcedure [dbo].[usp_GetTenderDetailsBySocietyId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_GetTenderDetailsBySocietyId] (
	@RegisteredSocietyId INT
)
AS
BEGIN 
	SET NOCOUNT ON;

	SELECT top 1 * FROM TenderDetails 
	ORDER BY CreationDate DESc

END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateDeveloperReviewRating]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateDeveloperReviewRating] (
	-- Parameters
	@DeveloperId INT,
	@ReviewRatingScore INT
)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM DeveloperMaster WHERE developerId = @DeveloperId) 
		BEGIN
			UPDATE DeveloperMaster
				SET 
					reviewRatingScore = @ReviewRatingScore,
					updatedDate = GETDATE(),
					updatedBy = NEWID()
			WHERE developerId = @DeveloperId

			SELECT 1
		END;
	ELSE 
		BEGIN 
			SELECT 0
		END
END;
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateTenderStatus]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_UpdateTenderStatus](
	@TenderId INT,
	@TenderStatus INT
)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE TenderDetails 
	SET 
		[status] = @TenderStatus
	WHERE tenderId = @TenderId

END;
GO
/****** Object:  StoredProcedure [dbo].[uspAddNewDeveloper]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE [dbo].[uspAddNewDeveloper]
@name nvarchar(250),
@address nvarchar(500),
@logoPath nvarchar(1000),
@siteLink nvarchar(200),
@email nvarchar(250),
@profilePath nvarchar(250),
@mobileNumber nvarchar(11),
@registeredDeveloperCode uniqueidentifier,
@createdBy uniqueidentifier,
@createdDate datetime,
@updatedBy uniqueidentifier,
@updatedDate datetime,
@developerId int OUTPUT
AS

BEGIN
     INSERT INTO dbo.DeveloperMaster	 
   VALUES
   (
		@name,
		@address,
		'xyz',
		@siteLink,
		GETDATE(),
		GETDATE(),
		NEWID(),
		NEWID(),
		@email,
		'XYZ',
		@mobileNumber,
		' '
   )
   SET @developerId = (SELECT SCOPE_IDENTITY())

   SELECT @developerId
END
GO
/****** Object:  StoredProcedure [dbo].[uspAddNewSociety]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspAddNewSociety] (
	@societyRegistrationNumber NVARCHAR(20),
	@societyName NVARCHAR(100),
	@registeredAddress NVARCHAR(500),
	@existingMemberCount INT,
	@age FLOAT,
	@email NVARCHAR(50),
	@societyRegisteredCode NVARCHAR(20),
	@societyDevelopmentTypeId INT,
	@societyDevelopmentSubType NVARCHAR(50),
	@createdBy UNIQUEIDENTIFIER,
	@createdDate DATETIME,
	@updatedBy UNIQUEIDENTIFIER,
	@updatedDate DATETIME,
	@city NVARCHAR(50),
	@registeredSocietyId INT OUTPUT
	--@commercialUse bit,
	--@residentialUse bit,
	--@mixedUse bit,
	--@numberOfCommercialTenaments int,
	--@numberOfResidentialTenaments int,
	--@totalCommercialBuiltUpBldgArea float,
	--@totalResidentialBuiltUpBldgArea float,
)
AS
BEGIN
    INSERT INTO [dbo].[RegisteredSociety]
           ([societyRegistrationNumber]
           ,[societyName]
           ,[existingMemberCount]
           ,[age]
		   ,[email]
           ,[createdBy]
           ,[createdDate]
           ,[updatedBy]
           ,[updatedDate]
           ,[societyRegisteredCode]
           ,[societyDevelopmentTypeId]
           ,[societyDevelopmentSubType]
           ,[registeredAddress]
		   ,[city]
           --,[plotDimensions]
           --,[approchRoadWidth]
           --,[totalCarpetBldgArea]
           --,[commercialUse]
           --,[residentialUse]
           --,[mixedUse]
           --,[numberOfCommercialTenaments]
           --,[numberOfResidentialTenaments]
           --,[numberOfMixedTenaments]
           --,[totalCommercialBuiltUpBldgArea]
           --,[totalResidentialBuiltUpBldgArea]
           --,[totalMx`x`ixedBuiltUpBldgArea]
		   )
     VALUES
           (@societyRegistrationNumber,
            @societyName,
            @existingMemberCount,
            @age,
			@email,
            @createdBy,
            @createdDate,
            @updatedBy,
            @updatedDate,
            @societyRegisteredCode,
            @societyDevelopmentTypeId,
            @societyDevelopmentSubType,
			@registeredAddress,
			@city
            --@plotDimensions,
            --@approchRoadWidth,
            --@totalCarpetBldgArea,
            --@commercialUse,
            --@residentialUse,
            --@mixedUse,
            --@numberOfCommercialTenaments,
            --@numberOfResidentialTenaments,
            --(isNull(@numberOfCommercialTenaments,0) + isNull(@numberOfResidentialTenaments,0)),
            --@totalCommercialBuiltUpBldgArea,
            --@totalResidentialBuiltUpBldgArea,
            --(isNull(@totalCommercialBuiltUpBldgArea,0) + isNull(@totalResidentialBuiltUpBldgArea,0)),
			)

		SET @registeredSocietyId = (SELECT SCOPE_IDENTITY())

		INSERT INTO [User]
		VALUES(@societyName, @societyName, @email, @registeredAddress, @societyRegisteredCode, 1, @createdDate, @updatedDate, @createdBy, @updatedBy)
		
		INSERT INTO UserAccess
		VALUES ((SELECT TOP 1 userId FROM [User] ORDER BY userId DESC), 1)

	SELECT @registeredSocietyId;
			
END;
GO
/****** Object:  StoredProcedure [dbo].[uspAddSocietyCommitteeMembers]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCedure [dbo].[uspAddSocietyCommitteeMembers] 
	@socMemberDetailsUDT As dbo.udt_MemberDetails readonly,
	@Message NVARCHAR(500) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRY
	
		INSERT INTO dbo.SocietyMemberDetails
		(
			[registeredSocietyId],
			[memberName],
			[mobileNumber],
			[email],
			[createdDate],
			[updatedDate],
			[createdBy],
			[updatedBy],
			[societyMemberDesignationId]
		)
		SELECT registeredSocietyId, memberName, mobileNumber, email, GETDATE(), GETDATE(), NEWID(), NEWID(),
		societyMemberDesignationId  FROM @socMemberDetailsUDT
	
	END TRY
	BEGIN CATCH

		DELETE FROM RegisteredSociety 
		WHERE registeredSocietyId = (SELECT TOP 1 registeredSocietyId FROM @socMemberDetailsUDT)
	
		SET @Message = (SELECT ERROR_MESSAGE())
		SELECT @Message
		
	END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[uspAddUserDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspAddUserDetails] (
	@UserName NVARCHAR(100),
	@UserDisplayName NVARCHAR(100),
	@Email NVARCHAR(100),
	@Address NVARCHAR(100),
	@UserCode NVARCHAR(100),
	@IsActive BIT,
	@CreatedDate DATETIME,
	@UpdatedDate DATETIME,
	@CreatedBy UNIQUEIDENTIFIER,
	@UpdatedBy UNIQUEIDENTIFIER,
	@UserId INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO dbo.[User]
	VALUES(@UserName, @UserDisplayName, @Email, @Address, @UserCode, @IsActive, @CreatedDate, @UpdatedDate, @CreatedBy, @UpdatedBy)

	SELECT @UserId = SCOPE_IDENTITY()

	SELECT @UserId

END;
GO
/****** Object:  StoredProcedure [dbo].[uspApprovedSocietyTenderCode]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspApprovedSocietyTenderCode] (
	@TenderId INT,
	@TenderCode NVARCHAR(20),
	@IsApproved BIT,
	@Reason NVARCHAR(200),
	@IsUpdated BIT OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		IF EXISTS(SELECT 1 FROM TenderDetails WHERE tenderId = @TenderId AND [status] = 1)
			BEGIN
				UPDATE TenderDetails
					SET 
						tenderCode = @TenderCode,
						isApprovedBySociety = @IsApproved,
						[status] = CASE WHEN @IsApproved = 1
											THEN(SELECT tenderStatusId FROM TenderStatus WHERE [status] = 'Active')
										WHEN @IsApproved = 0
											THEN (SELECT tenderStatusId FROM TenderStatus WHERE [status] ='Rejected')
										END,
						reason = @Reason
				WHERE tenderId = @TenderId		
			END
		SET @IsUpdated = 1
	END TRY
	BEGIN CATCH
		SET @IsUpdated = 0
	END CATCH

	SELECT @IsUpdated AS IsUpdated

END;
GO
/****** Object:  StoredProcedure [dbo].[uspGetAdminByCode]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetAdminByCode] (
	@AdminCode NVARCHAR(50)
)
AS 
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM AdminDetails
	WHERE adminCode = @AdminCode

END;

GO
/****** Object:  StoredProcedure [dbo].[uspGetDeveloperByCode]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create procedure [dbo].[uspGetDeveloperByCode] (
	@DeveloperCode NVARCHAR(50)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM DeveloperMaster 
	WHERE registeredDeveloperCode = @DeveloperCode

END;
GO
/****** Object:  StoredProcedure [dbo].[uspGetOTPDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspGetOTPDetails] (
	@UserName NVARCHAR(50),
	@MobileNumber NVARCHAR(20),
	@OTP NVARCHAR(10)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM UserOTPDetails 
	WHERE userName = @UserName
	AND userMobileNumber = @MobileNumber

END;
GO
/****** Object:  StoredProcedure [dbo].[uspGetRegisteredSocietyByCode]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetRegisteredSocietyByCode] (
	@SocietyCode NVARCHAR(100)
)
As 
BEGIN
	SET NOCOUNT ON;

	SELECT *, -1 AS ActiveTenderId
	FROM RegisteredSociety
	WHERE societyRegisteredCode = @SocietyCode

END;
GO
/****** Object:  StoredProcedure [dbo].[uspGetTenderDetailsBySocietyId]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspGetTenderDetailsBySocietyId]
(
	@regSocietyId INT
)
AS 
BEGIN
	SELECT 
		*		
	FROM TenderDetails WHERE registeredSocietyId = @regSocietyId
END;
GO
/****** Object:  StoredProcedure [dbo].[uspGetUserByCode]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspGetUserByCode] (
	@UserCode NVARCHAR(100)
)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT * 
	FROM [User]
	WHERE userCode = @UserCode

END;
GO
/****** Object:  StoredProcedure [dbo].[uspIsUserExists]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

--EXEC  [dbo].[uspIsUserExists] 'MHPUN067649', 'Member', '9074748652', ''

CREATE PROCEDURE [dbo].[uspIsUserExists] (
	@UserName NVARCHAR(50),
	@UserRole NVARCHAR(50),
	@UserMobileNumber NVARCHAR(50),
	@Message NVARCHAR(100) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @Msg NVARCHAR(100)
	DECLARE @MobileVerfied TABLE (Result NVARCHAR(100))
	
	IF (@UserRole = 'Admin')
		BEGIN 
			DECLARE @UserNameMatched INT = 0, @MobileNumberMatched INT = 0

			SELECT @UserNameMatched = 1 FROM AdminDetails 
			WHERE adminCode = @UserName

			SELECT @MobileNumberMatched = 1 FROM AdminDetails 
			WHERE mobileNumber = @UserMobileNumber

			--SELECT @AdminId = userId
			--FROM [User] 
			--WHERE userCode = @UserName

			--SELECT @IsAdmin = userRoleId
			--FROM UserAccess
			--WHERE userId = @AdminId

			--SELECT @IsUserAdmin = 1
			--FROM UserRole 
			--WHERE userRoleId = @IsAdmin AND roleName = 'Admin'

			--SET @Msg = (SELECT * FROM @MobileVerfied)

			IF(@UserNameMatched = 1 AND @MobileNumberMatched = 1)
				BEGIN
					SET @Message = 'User exist'
				END;
			ELSE IF (@UserNameMatched = 0 AND @MobileNumberMatched = 0)
				BEGIN 
					SET @Message = 'User name and mobile number not exist'
				END;
			ELSE IF (@MobileNumberMatched = 0)
				BEGIN
					SET @Message = 'Mobile number not exist'
				END;
			ELSE IF (@UserNameMatched = 0) 
				BEGIN
					SET @Message = 'User name not exist'
				END;
			
		END;
	ELSE IF (@UserRole = 'Member')
		BEGIN 
			DECLARE @SocietyId INT, @IsSociety INT, @IsUserSociety INT, @MemberId INT = NULL

			--SELECT @SocietyId = userId
			--FROM [User] 
			--WHERE userCode = @UserName

			--SELECT @IsSociety = userRoleId
			--FROM UserAccess
			--WHERE userId = @SocietyId

			--SELECT @IsUserSociety = 1
			--FROM UserRole 
			--WHERE userRoleId = @IsSociety AND roleName = 'Society'

			IF EXISTS (SELECT 1 FROM RegisteredSociety WHERE societyRegisteredCode = @UserName) 
				BEGIN
					SELECT @SocietyId = registeredSocietyId
					FROM RegisteredSociety
					WHERE societyRegisteredCode = @UserName

					SELECT @MemberId = societyMemberDetailsId 
					FROM SocietyMemberDetails
					WHERE registeredSocietyId = @SocietyId
					AND mobileNumber = @UserMobileNumber

					IF(@MemberId > 0) 
						BEGIN 
							SET @Message = 'User exist'
						END
					ELSE 
						BEGIN
							SET @Message = 'You are not the registered member of the society.'
						END
				END
			ELSE 
				BEGIN 
					SET @Message = 'Society Not Found!'
				END

			


			--INSERT INTO @MobileVerfied
			--EXEC uspVerifyMobileNumber @userMobileNumber, @UserRole

			--SET @Msg = (SELECT * FROM @MobileVerfied)
			
			--IF(@Msg = 'User exist' AND @IsUserSociety = 1)
			--	BEGIN
			--		SET @Message = 'User exist'
			--	END;
			--ELSE IF (@Msg = 'User not exist' AND @IsUserSociety IS NULL)
			--	BEGIN 
			--		SET @Message = 'User name and mobile number not exist'
			--	END;
			--ELSE IF (@Msg = 'User not exist')
			--	BEGIN
			--		SET @Message = 'Mobile number not exist'
			--	END;
			--ELSE IF (@IsUserSociety IS NULL) 
			--	BEGIN
			--		SET @Message = 'User name not exist'
			--	END;
	
		END;
	ELSE 
		BEGIN 
			DECLARE @DeveloperId INT, @IsDeveloper INT, @IsUserDeveloper INT

			SELECT @DeveloperId = userId
			FROM [User] 
			WHERE userCode = @UserName

			SELECT @IsDeveloper = userRoleId
			FROM UserAccess
			WHERE userId = @DeveloperId

			SELECT @IsUserDeveloper = 1
			FROM UserRole 
			WHERE userRoleId = @IsDeveloper AND roleName = 'Developer'

			INSERT INTO @MobileVerfied
			EXEC uspVerifyMobileNumber @userMobileNumber, @UserRole

			SET @Msg = (SELECT * FROM @MobileVerfied)

			IF(@Msg = 'User exist' AND @IsUserDeveloper = 1)
				BEGIN
					SET @Message = 'User exist'
				END;
			ELSE IF (@Msg = 'User not exist' AND @IsUserDeveloper IS NULL)
				BEGIN 
					SET @Message = 'User name and mobile number not exist'
				END;
			ELSE IF (@Msg = 'User not exist')
				BEGIN
					SET @Message = 'Mobile number not exist'
				END;
			ELSE IF (@IsUserDeveloper IS NULL) 
				BEGIN
					SET @Message = 'User name not exist'
				END;
			
		END;

		SELECT @Message AS [Message]

END;


GO
/****** Object:  StoredProcedure [dbo].[uspSaveOTPDetails]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[uspSaveOTPDetails] (
	
	@UserName NVARCHAR(100),
	@MobileNumber NVARCHAR(100),
	@OTP NVARCHAR(20),
	@Id INT OUTPUT
)
AS 
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT * FROM UserOTPDetails WHERE userName = @UserName AND userMobileNumber = @MobileNumber )
		BEGIN 
			
			UPDATE UserOTPDetails 
			SET 
				OTP = @OTP,
				creationDate = GETDATE(),
				@Id = OTPDetailsId
			WHERE userName = @UserName
			AND userMobileNumber = @MobileNumber

		END;
	ELSE
		BEGIN

			INSERT INTO UserOTPDetails
			VALUES(@UserName, @MobileNumber, @OTP, GETDATE())
			SET @Id = (SELECT SCOPE_IDENTITY())

		END;
	
	
	SELECT @Id

END;
GO
/****** Object:  StoredProcedure [dbo].[uspUpdateSociety]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE [dbo].[uspUpdateSociety]
@registeredSocietyId int,
@societyRegistrationNumber nvarchar(15),
@societyName nvarchar(500),
@registeredAddress nvarchar(max),
--@sizeOfPlot int,
--@plotDimensions nvarchar(60),
--@approchRoadWidth float,
--@totalCarpetBldgArea float,
--@commercialUse bit,
--@residentialUse bit,
--@mixedUse bit,
--@numberOfCommercialTenaments int,
--@numberOfResidentialTenaments int,
--@totalCommercialBuiltUpBldgArea float,
--@totalResidentialBuiltUpBldgArea float,
@societyDevelopmentTypeId int,
@societyDevelopmentSubType nvarchar(300),
@existingMemberCount int,
@age float,
@createdBy uniqueidentifier,
@createdDate datetime,
@updatedBy uniqueidentifier,
@updatedDate datetime,
@city nvarchar(100)
As
UPDATE [dbo].[RegisteredSociety]
   SET [societyRegistrationNumber] = @societyRegistrationNumber,
       [societyName] = @societyName,
       [existingMemberCount] = @existingMemberCount,
       [age] = @age,
       --[sizeOfPlot] = @sizeOfPlot,
       --[plotDimensions] = @plotDimensions,
       --[approchRoadWidth] = @approchRoadWidth,
       --[totalCarpetBldgArea] = @totalCarpetBldgArea,
       [createdBy] = @createdBy,
       [createdDate] = @createdDate,
       [updatedBy] = @updatedBy,
       [updatedDate] = @updatedDate,
       --[commercialUse] = @commercialUse,
       --[residentialUse] = @residentialUse,
       --[mixedUse] = @mixedUse,
       --[numberOfCommercialTenaments] = @numberOfCommercialTenaments,
       --[numberOfResidentialTenaments] = @numberOfResidentialTenaments,
       --[numberOfMixedTenaments] = (isNull(@numberOfCommercialTenaments,0) + isNull(@numberOfResidentialTenaments,0)),
       --[totalCommercialBuiltUpBldgArea] = @totalCommercialBuiltUpBldgArea,
       --[totalResidentialBuiltUpBldgArea] = @totalResidentialBuiltUpBldgArea,
       --[totalMixedBuiltUpBldgArea] = (isNull(@totalCommercialBuiltUpBldgArea,0) + isNull(@totalResidentialBuiltUpBldgArea,0)),
       [societyDevelopmentTypeId] = @societyDevelopmentTypeId,
       [societyDevelopmentSubType] = @societyDevelopmentSubType,
       [registeredAddress] = @registeredAddress,
	   [city] = @city
 WHERE registeredSocietyId = @registeredSocietyId
GO
/****** Object:  StoredProcedure [dbo].[uspUpdateTechnicalDetailsSociety]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspUpdateTechnicalDetailsSociety] 
(
	@registeredSocietyId INT,
	@sizeOfPlot INT,
	@plotDimensions NVARCHAR(70),
    @residentialUse BIT,
	@commercialUse BIT,
	@mixedUse BIT,
	@numberOfWings INT,
	@numberOfCommercialTenaments INT,
	@numberOfResidentialTenaments INT,
    @totalCommercialBuiltUpBldgArea FLOAT,
	@totalResidentialBuiltUpBldgArea FLOAT,
	@totalBuiltUpArea FLOAT,
	@approchRoadWidth FLOAT,
	@createdBy UNIQUEIDENTIFIER,
	@updatedBy UNIQUEIDENTIFIER
)
AS 
BEGIN
	
	UPDATE RegisteredSociety
		SET sizeOfPlot = @sizeOfPlot,
			plotDimensions = @plotDimensions,
			residentialUse = @residentialUse,
			commercialUse = @commercialUse,
			mixedUse = @mixedUse,
			numberOfMixedTenaments = (isNull(@numberOfCommercialTenaments,0) + isNull(@numberOfResidentialTenaments,0)),
			numberOfCommercialTenaments = @numberOfCommercialTenaments,
			numberOfResidentialTenaments = @numberOfResidentialTenaments,
			totalCommercialBuiltUpBldgArea = @totalCommercialBuiltUpBldgArea,
			totalResidentialBuiltUpBldgArea = @totalResidentialBuiltUpBldgArea,
			totalMixedBuiltUpBldgArea = (isNull(@totalCommercialBuiltUpBldgArea,0) + isNull(@totalResidentialBuiltUpBldgArea,0)),
			approchRoadWidth = @approchRoadWidth

	WHERE registeredSocietyId = @registeredSocietyId

	SELECT @registeredSocietyId

END;
GO
/****** Object:  StoredProcedure [dbo].[uspVerifyMobileNumber]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspVerifyMobileNumber] (
	
	@MobileNumber NVARCHAR(20),
	@UserRole NVARCHAR(20)	
)
AS
BEGIN
	SET NOCOUNT ON;
	
	DECLARE @Msg NVARCHAR(100), @IsCorrect INT = 0
	
	IF(@UserRole = 'Admin')
		BEGIN
			
			SET @Msg = 'User exist'

		END;
	ELSE IF(@UserRole = 'Member')
		BEGIN
			
			SELECT @IsCorrect = societyMemberDetailsId
			FROM SocietyMemberDetails
			WHERE mobileNumber = @MobileNumber

			SET @Msg = IIF(@IsCorrect > 0, 'User exist', 'User not exist')

		END;
	ELSE 
		BEGIN
			
			SELECT @IsCorrect = developerId
			FROM DeveloperMaster
			WHERE mobileNumber = @MobileNumber

			SET @Msg = IIF(@IsCorrect > 0, 'User exist', 'User not exist')
		END;

		SELECT @Msg 
END;
GO
/****** Object:  StoredProcedure [dbo].[VerifyIsSocietyExist]    Script Date: 10-12-2023 12:42:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[VerifyIsSocietyExist] 
	@SocietyEmail NVARCHAR(100),
	@MemberMobileNumber NVARCHAR(11)
AS
BEGIN 
	
	DECLARE @IsSocietyExist INT = (SELECT COUNT(*) FROM [User] WHERE email = @SocietyEmail)
	DECLARE @IsMemberExist INT = (SELECT COUNT(*) FROM SocietyMemberDetails WHERE mobileNumber = @MemberMobileNumber)
	DECLARE @Result INT;

	IF(@IsSocietyExist > 0 AND @IsMemberExist > 0)
		BEGIN 
			SET @Result = 3
		END
	ELSE IF (@IsSocietyExist > 0 AND @IsMemberExist <= 0)
		BEGIN 
			SET @Result = 1
		END
	ELSE IF (@IsSocietyExist <= 0 AND @IsMemberExist > 0)
		BEGIN 
			SET @Result = 2
		END

	SELECT @Result

END;
GO
USE [master]
GO
ALTER DATABASE [Castlers] SET  READ_WRITE 
GO
