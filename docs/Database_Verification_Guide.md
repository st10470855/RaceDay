# RaceDay Database Verification Guide

## Overview

This guide explains how to verify that the RaceDay database has been created correctly and that the expected sample data is available.

The verification process uses Microsoft SQL Server Management Studio (SSMS).

## 1. Confirm the Database

Open SSMS and connect to the SQL Server Database Engine.

In Object Explorer:

1. Expand **Databases**.
2. Locate `RaceDayDB`.
3. Confirm that the database is available.

The database should be named:

`RaceDayDB`

## 2. Verify the Tables

Expand:

`RaceDayDB` → `Tables`

The database contains the following main tables:

* `Role`
* `User`
* `Organizer`
* `Participant`
* `Event`
* `Category`
* `EventEnrolment`
* `Result`

The tables provide the database structure required for user management, events, categories, enrolments, and results.

## 3. Verify Table Relationships

The relationships between the tables should correspond with the RaceDay ERD.

Primary keys identify individual records, while foreign keys connect related records.

The relationships should support:

* User roles.
* Organiser profiles.
* Participant profiles.
* Organiser-created events.
* Event categories.
* Participant enrolments.
* Race results.

## 4. Verify Sample Data

The SQL script includes sample data that can be used to test the database.

Examples include:

* Mokoena Events
* Dlamini Sports Events
* Johannesburg Community 10K
* Soweto Family Walk
* Cape Town Coastal Cycle

Sample categories include:

* Junior
* Senior
* Masters

## 5. Run Verification Queries

The repository contains a separate SQL file containing database verification queries:

`Database_Verification_Queries.sql`

The queries can be executed in SSMS to check:

* The current database.
* Available tables.
* Number of records in the main tables.
* Sample events.
* Sample categories.
* Event enrolments.
* Recorded results.

## 6. Verify Data Integrity Rules

The database should enforce the validation rules defined in the SQL script.

Important rules include:

* Event types are limited to Run, Walk, or Cycle.
* Enrolment statuses are limited to Confirmed, Cancelled, or Pending.
* Event distances must be greater than zero.
* Finish positions must be greater than zero.
* Category age ranges must be logical.
* A participant cannot enrol in the same event more than once.

## 7. Compare With the ERD

The database structure should be compared with:

`RaceDay_ERD.png`

The entities, relationships, primary keys, and foreign keys should correspond with the planned database design.

Any deliberate differences between the ERD and SQL implementation should be documented and explained.

## 8. Verification Result

The RaceDay database was successfully created using SQL Server Management Studio.

The database and its tables were verified, and the sample data was available for testing.

The verification queries provide an additional repeatable way to check the database during future development.

## Related Documentation

The following Part 1 documents provide additional information:

* `RaceDay_ERD.png`
* `RaceDay_Database.sql`
* `Database_Setup_Guide.md`
* `Database_Verification_Queries.sql`
* `Database_Design_Decisions.md`

## Conclusion

Database verification confirms that the RaceDay database has been created and provides a reliable foundation for the API development required in Part 2.
