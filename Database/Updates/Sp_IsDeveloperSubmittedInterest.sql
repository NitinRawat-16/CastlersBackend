

CREATE PROCEDURE IsDeveloperSubmittedInterest
(
	@OfferId INT,
	@DeveloperId INT,
	@Id INT OUT
)
AS
BEGIN 
	SET NOCOUNT ON;
	BEGIN TRY
		SELECT @Id = letterOfInterestId
		FROM LetterOfInterest
		WHERE developerId = @DeveloperId AND offerId = @OfferId
	END TRY
	BEGIN CATCH
	END CATCH
END;