USE [Castlers]
GO
/****** Object:  StoredProcedure [dbo].[AddNewDeveloper]    Script Date: 19-12-2023 00:29:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER   PROCEDURE [dbo].[AddNewDeveloper]
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
@lastThreeYearReturns nvarchar(20),
@financialSecurityToTheSociety nvarchar(100),

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
		affilicationDevAssociationName,
		lastThreeYearReturns,
		financialSecurityToTheSociety
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
		@affilicationDevAssociationName,
		@lastThreeYearReturns,
		@financialSecurityToTheSociety


	)
	SET @developerId = (SELECT SCOPE_IDENTITY())

   SELECT @developerId AS DeveloperId
END

