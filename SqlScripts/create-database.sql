IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'StackOverflowTagsDb')
BEGIN
  CREATE DATABASE [StackOverflowTagsDb];
END;
GO

USE [StackOverflowTagsDb]
GO

IF OBJECT_ID('Tags', 'U') IS NULL
BEGIN
	CREATE TABLE Tags (
	  id int NOT NULL IDENTITY,
	  name varchar(50) DEFAULT NULL,
	  count int DEFAULT NULL,
	  percentage real DEFAULT NULL,
	  PRIMARY KEY (id)
	);
END;
GO
