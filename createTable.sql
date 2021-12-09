
CREATE DATABASE DevTest;

CREATE TABLE Properties
( 
	Property_Id int NOT NULL,
	address1 varchar(50),
	address2 varchar(50),
	City varchar(30),
	Country varchar(30),
	County varchar(30),
	district varchar(30),
	state varchar(2),
	zip varchar(10),
	zipPlus4 varchar(10),
	YearBuilt int NOT NULL,
	ListPrice money,
	MonthlyRent money,
	GrossYield decimal(15,2),
	CONSTRAINT PK_Properties PRIMARY KEY (Property_Id)
);
