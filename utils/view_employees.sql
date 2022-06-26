/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) 
	  Info.[employee_id]
	  ,[first_name]
	  ,[last_name]
	  ,[hire_date]
      ,[phone_number]
      ,[email]

  FROM [EmployeeDB].[dbo].[EmployeeContact] as Contact
  JOIN [EmployeeDB].[dbo].[EmployeeInformation] as Info
  ON Contact.employee_id = Info.employee_id;