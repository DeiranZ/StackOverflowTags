CREATE DATABASE [StackOverflowTagsDb]
GO

USE [StackOverflowTagsDb]
GO

CREATE TABLE Tags (
  id int NOT NULL IDENTITY,
  name varchar(50) DEFAULT NULL,
  count int DEFAULT NULL,
  percentage real DEFAULT NULL,
  PRIMARY KEY (id)
);
GO
