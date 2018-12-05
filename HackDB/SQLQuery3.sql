use HackDb
Go

EXEC hspValidateEmail 'PRAVEEN.JHA@JDA.COM'

exec hspRegisterUser @EmailId=N'praveen.jha@jda.com',@Password=N'8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92',@SecurityToken=N'Of8nUWuK0aJPNAWE0j4k8Snmmkjsb1ipg5w1l_hL1ks6tAu99eXg_rDun4Z-5humCvLHahIfsRVwEOfrcxX91w',@DeviceModel=N'A5(2016)',@DeviceManufacturer=N'Samsung',@DeviceRegistrationId=N'string',@UserId=1

exec hspGetAllBeacons 1,null, 'c42da800-1afb-450c-9a50-5322f762cc4c',100,100

select * from BeaconDetails

select * from userdetails
