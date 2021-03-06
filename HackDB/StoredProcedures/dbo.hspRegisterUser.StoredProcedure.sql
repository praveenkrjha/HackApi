IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hspRegisterUser]') AND type in (N'P', N'PC'))
	EXEC('CREATE PROCEDURE [dbo].[hspRegisterUser] AS RETURN 0')
GO  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[hspRegisterUser]
(
	@EmailID VARCHAR(80),
	@Password VARCHAR(100),
	@SecurityToken VARCHAR(500)=NULL,
	@DeviceModel VARCHAR(100)=NULL,
	@DeviceManufacturer VARCHAR(100)=NULL,
	@DeviceRegistrationId varchar(500),
	@UserId INT
)

AS
BEGIN

	declare @dbUserId INT
	SELECT @dbUserId = UserID FROM UserDetails where Email = @EmailID and Password=@Password
	IF isnull(@dbUserId, '') =''
	BEGIN
		RAISERROR(60080,16,1,'hspRegisterUser','')    
	END	

	UPDATE UserDetails SET SecurityToken=@SecurityToken, DeviceModel = @DeviceModel, DeviceManufacturer=@DeviceManufacturer, DeviceRegistrationId =@DeviceRegistrationId
	where UserId = @dbUserId

	SELECT * FROM UserDetails where UserId = @dbUserId

END

