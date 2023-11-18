CREATE LOGIN [SkiServiceManagement] WITH PASSWORD = 'Str0ng!Pass123';
GO
CREATE DATABASE [JetStream];
GO
USE [JetStream];
GO
CREATE USER [SkiServiceManagement] FOR LOGIN [SkiServiceManagement];
GO
GRANT SELECT, INSERT, UPDATE, DELETE TO [SkiServiceManagement];
GO
