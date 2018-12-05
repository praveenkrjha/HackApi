use HackDb
Go
create table UserDetails
(
	UserId INT IDENTITY(1,1),
	FirstName varchar(200),
	LastName varchar(200),
	Email varchar(400),
	Password varchar(500),
	SecurityToken varchar(500),
	DeviceModel varchar(200),
	DeviceManufacturer varchar(200),
	DeviceRegistrationId varchar(500)
)
Go
--drop table UserDetails
select * from UserDetails
--8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92
insert into UserDetails (FirstName, LastName, Email, Password) values ('Praveen', 'Jha', 'praveen.jha@jda.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92')
insert into UserDetails (FirstName, LastName, Email, Password) values ('Nagarjuna', 'Gade', 'Nagarjuna.Gade@jda.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92')
insert into UserDetails (FirstName, LastName, Email, Password) values ('R', 'Prashanth', 'R.Prashanth@jda.com', '8D969EEF6ECAD3C29A3A629280E686CF0C3F5D5A86AFF3CA12020C923ADC6C92')

-----------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------

CREATE TABLE BeaconDetails
(
	BeaconId int identity(1,1),
	UUID varchar(200),
	Major int,
	Minor int,	
	BeaconLocation varchar(200)
)
GO

INSERT INTO BeaconDetails (UUID, Major, Minor, BeaconLocation) values ('c42da800-1afb-450c-9a50-5322f762cc4c', 0, 0, 'Beacon at Conf room entrance')
INSERT INTO BeaconDetails (UUID, Major, Minor, BeaconLocation) values ('c42da800-1afb-450c-9a50-5322f762cc4c', 50, 50, 'Beacon at Conf room exit')
INSERT INTO BeaconDetails (UUID, Major, Minor, BeaconLocation) values ('c42da800-1afb-450c-9a50-5322f762cc4c', 100, 100, 'Beacon at center')


CREATE TABLE ProductDetails
(
	ProductId int identity(1,1),
	ProductName varchar(100),
	ProductDescription varchar(500),
	LocationX int,
	LocationY int
)
GO
INSERT INTO ProductDetails(ProductName, ProductDescription, LocationX, LocationY) VALUES ('Water Bottle', 'Water bottle at corner end of Nalanda Conf room.', 1650, 720)
INSERT INTO ProductDetails(ProductName, ProductDescription, LocationX, LocationY) VALUES ('Fridge', 'Fridge kept at entrance of Nalanda Conf room.', 100, 720)
INSERT INTO ProductDetails(ProductName, ProductDescription, LocationX, LocationY) VALUES ('TV', 'Wall TV at center of Nalanda Conf room.', 800, 20)
GO
CREATE TABLE Facilities
(
	FacilityId int identity(1,1),
	FacilityName varchar(100),
	FacilityDescription varchar(500),
	LocationX int,
	LocationY int
)
GO
INSERT INTO Facilities(FacilityName, FacilityDescription, LocationX, LocationY) VALUES ('Entry', 'Nalanda Entry Gate.', 10, 10)
INSERT INTO Facilities(FacilityName, FacilityDescription, LocationX, LocationY) VALUES ('Exit', 'Nalanda Exit Gate.', 1650, 10)


--WaterBottle
-- Fridge
--Monitor
--Entry
--Exit