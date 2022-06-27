-- Create Database

CREATE DATABASE EmployeeDB;

USE EmployeeDB;

DROP TABLE IF EXISTS [EmployeeContact];
DROP TABLE IF EXISTS [EmployeeInformation];

CREATE TABLE [EmployeeInformation] (
	[employee_id] int PRIMARY KEY IDENTITY(1,1),
	[first_name] varchar(255),
	[last_name] varchar(255), 
	[hire_date] DATE
);

CREATE TABLE [EmployeeContact] (
	[contact_id] int PRIMARY KEY IDENTITY(1,1),
	[employee_id] int FOREIGN KEY REFERENCES EmployeeInformation([employee_id]),
	[phone_number] varchar(12),
	[email] varchar(255)
);

-- Populate Database

INSERT into EmployeeInformation ([first_name], [last_name], [hire_date]) 
values ('Tony', 'Stark', '2008-05-02');
DECLARE @new_employee_id int;
SELECT @new_employee_id = SCOPE_IDENTITY();
INSERT INTO EmployeeContact ([employee_id], [phone_number], [email]) values (@new_employee_id, '111-222-3333', 'jarvis@avengers.com');

INSERT into EmployeeInformation ([first_name], [last_name], [hire_date]) 
values ('Natasha', 'Romanoff', '2010-05-07');
SELECT @new_employee_id = SCOPE_IDENTITY();
INSERT INTO EmployeeContact ([employee_id], [phone_number], [email]) values (@new_employee_id, '111-222-3333', 'blackwidow@avengers.com');

INSERT into EmployeeInformation ([first_name], [last_name], [hire_date]) 
values ('Steve', 'Rogers', '2011-07-19');
SELECT @new_employee_id = SCOPE_IDENTITY();
INSERT INTO EmployeeContact ([employee_id], [phone_number], [email]) values (@new_employee_id, '111-222-3333', 'cap@avengers.com');

INSERT into EmployeeInformation ([first_name], [last_name], [hire_date]) 
values ('Thor', 'Odinson', '2011-05-02');
SELECT @new_employee_id = SCOPE_IDENTITY();
INSERT INTO EmployeeContact ([employee_id], [phone_number], [email]) values (@new_employee_id, '111-222-3333', 'strongest@avengers.com');

INSERT into EmployeeInformation ([first_name], [last_name], [hire_date]) 
values ('Bruce', 'Banner', '2012-05-04');
SELECT @new_employee_id = SCOPE_IDENTITY();
INSERT INTO EmployeeContact ([employee_id], [phone_number], [email]) values (@new_employee_id, '111-222-3333', 'smash@avengers.com');
