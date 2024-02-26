# N5

DDL:
CREATE DATABASE N5;

USE N5;

CREATE TABLE PermissionTypes(
	Id INT IDENTITY NOT NULL,
	Description VARCHAR(50) NOT NULL,
	CONSTRAINT Pk_PermissionTypes_Id PRIMARY KEY (Id)
);

CREATE TABLE Permissions(
	Id INT IDENTITY NOT NULL,
	EmployeeForename VARCHAR(50) NOT NULL,
	EmployeeSurname VARCHAR(50) NOT NULL,
	PermissionType INT NOT NULL,
	PermissionDate Date NOT NULL,
	CONSTRAINT Pk_Permissions_Id PRIMARY KEY (Id),
	CONSTRAINT Fk_PermissionTypes_PermissionType FOREIGN KEY (PermissionType) REFERENCES PermissionTypes(Id)
);

Configuration:
Change the values in appsetting
