IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hspGetAllBeacons]') AND type in (N'P', N'PC'))
	EXEC('CREATE PROCEDURE [dbo].[hspGetAllBeacons] AS RETURN 0')
GO  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[hspGetAllBeacons]  
(  
	@UserID INT,
	@BeaconId INT=NULL,
	@UUID varchar(100)=NULL,
	@Major INT=NULL,
	@Minor INT=NULL
)  
  
AS  
BEGIN   
	
	IF NOT EXISTS(SELECT 1 FROM UserDetails where UserID=@UserID /* and SecurityToken=@SecurityToken*/)
	BEGIN
		RAISERROR(60080,16,1,'hspGetAllBeacons','')    
	END	
	
	--Get beacon id from UUID, major and minor
	if(isnull(@UUID,'') <> '' AND  isnull(@Major,'') <> '' AND isnull(@Minor,'') <> '')
	BEGIN
		SELECT @BeaconId = BeaconId FROM BeaconDetails WHERE UUID = @UUID AND Major = @Major AND Minor = @Minor
	END

	SELECT * FROM BeaconDetails WHERE isnull(@BeaconId,'') = '' OR BeaconId = @BeaconId
END
GO