USE [Castlers]
GO
/****** Object:  StoredProcedure [dbo].[GetSocietyActiveTenderIdBySocietyId]    Script Date: 30-12-2023 23:01:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[GetSocietyActiveTenderIdBySocietyId] 
(
	@SocietyId INT,
	@TenderId INT OUT,
	@Message NVARCHAR(500) OUT
)
AS 
BEGIN

	SELECT @TenderId = tenderId 
	FROM TenderDetails
	WHERE registeredSocietyId = @SocietyId
	AND status = 2

	SELECT @TenderId AS TenderId

	IF EXISTS (SELECT 1 FROM Offer WHERE registeredSocietyId = @SocietyId AND tenderId = @TenderId)
		BEGIN
			SET @Message = 'Initimation already send';
		END
	ELSE 
		BEGIN
			IF (ISNULL(@TenderId, 0) <= 0)
				BEGIN
					SET @Message = 'Can not find active tender';
				END;
		END
		
	SELECT @Message AS [Message]
END;

