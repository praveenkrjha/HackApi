IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[hspValidateEmail]') AND type in (N'P', N'PC'))
	EXEC('CREATE PROCEDURE [dbo].[hspValidateEmail] AS RETURN 0')
GO  
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[hspValidateEmail]  
(  
	@EmailId VARCHAR(80),
	@Token VARCHAR(100) = NULL
)  
  
AS  
BEGIN   
	
	IF NOT EXISTS(SELECT 1 FROM UserDetails where Email = @EmailID)
	BEGIN
		RAISERROR(60080,16,1,'hspValidateEmail','')    
	END	

	SELECT * FROM UserDetails where Email = @EmailID 
END
GO