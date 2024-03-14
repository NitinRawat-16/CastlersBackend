USE [Castlers]
GO

/****** Object:  StoredProcedure [dbo].[usp_AddMembersPreferredDevelopers]    Script Date: 25-12-2023 16:23:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_AddMembersPreferredDevelopers](
	-- Parameters
	@MemberId INT,
	@electionId INT,
	@developerFirst INT,
	@developerSecond INT,
	@developerThird INT,
	@IsVoted BIT,
	@CreationDate DATETIME,
	@UpdationDate DATETIME,
	@PreferredDeveloperId INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		INSERT INTO MembersPreferredDevelopers
		VALUES (@MemberId, @electionId, @developerFirst, @developerSecond, @developerThird, @IsVoted,
		@CreationDate, @UpdationDate)

		SET @PreferredDeveloperId = (SELECT SCOPE_IDENTITY())

	END TRY
	BEGIN CATCH
	END CATCH

END;
GO


