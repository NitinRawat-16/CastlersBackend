USE [Castlers]
GO

/****** Object:  StoredProcedure [dbo].[usp_AddElectionDetails]    Script Date: 25-12-2023 16:22:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[usp_AddElectionDetails](
  -- Parameters
  @TenderId INT,
  @StartDate DATETIME,
  @EndDate DATETIME,
  @Status NVARCHAR(50),
  @TotalVoters INT,
  @TotalVoted INT,
  @CreationDate DATETIME,
  @UpdationDate DATETIME,
  @ElectionId INT OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;

	BEGIN TRY
		
		INSERT INTO ElectionDetails
		VALUES (@TenderId, @StartDate, @EndDate, @Status, @TotalVoters, @TotalVoted,
		@CreationDate, @UpdationDate)


		SET @ElectionId = (SELECT SCOPE_IDENTITY())

	END TRY
	BEGIN CATCH
	END CATCH
END;
GO


