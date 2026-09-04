-- RaceDay Database Verification Queries
-- Part 1
-- Purpose: Verify that the RaceDayDB database and sample data
-- have been created successfully.

USE RaceDayDB;
GO

-- 1. Confirm the current database
SELECT DB_NAME() AS CurrentDatabase;
GO

-- 2. List the RaceDay database tables
SELECT
SCHEMA_NAME(schema_id) AS SchemaName,
name AS TableName
FROM sys.tables
ORDER BY name;
GO

-- 3. Check the number of rows in the Role table
SELECT COUNT(*) AS RoleCount
FROM dbo.Role;
GO

-- 4. Check the number of users
SELECT COUNT(*) AS UserCount
FROM dbo.[User];
GO

-- 5. Check the number of organisers
SELECT COUNT(*) AS OrganizerCount
FROM dbo.Organizer;
GO

-- 6. Check the number of participants
SELECT COUNT(*) AS ParticipantCount
FROM dbo.Participant;
GO

-- 7. Check the number of events
SELECT COUNT(*) AS EventCount
FROM dbo.[Event];
GO

-- 8. Check the number of categories
SELECT COUNT(*) AS CategoryCount
FROM dbo.Category;
GO

-- 9. Check the number of enrolments
SELECT COUNT(*) AS EnrolmentCount
FROM dbo.EventEnrolment;
GO

-- 10. Check the number of results
SELECT COUNT(*) AS ResultCount
FROM dbo.Result;
GO

-- 11. Display the sample events
SELECT *
FROM dbo.[Event];
GO

-- 12. Display the sample categories
SELECT *
FROM dbo.Category;
GO

-- 13. Display the sample enrolments
SELECT *
FROM dbo.EventEnrolment;
GO

-- 14. Display the recorded results
SELECT *
FROM dbo.Result;
GO

-- Verification complete.
-- The queries above can be used to confirm that the database,
-- tables, and sample records are available.
