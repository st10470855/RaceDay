# RaceDay Database Setup Guide

## Overview

This guide explains how to create and verify the RaceDay database for Part 1 of the project.

The database is implemented using Microsoft SQL Server and the SQL script provided in this repository.

## Requirements

The following software is required:

* Microsoft SQL Server
* SQL Server Management Studio (SSMS)

## Creating the Database

1. Open SQL Server Management Studio (SSMS).
2. Connect to the SQL Server Database Engine.
3. Open a new query window.
4. Open the `RaceDay_Database.sql` script from the `docs` folder.
5. Execute the script.
6. Refresh the **Databases** folder in Object Explorer.
7. Confirm that the `RaceDayDB` database has been created.

## Verifying the Tables

After the database has been created:

1. Expand `RaceDayDB`.
2. Expand **Tables**.
3. Confirm that the tables created by the SQL script are displayed.
4. Verify that the database structure corresponds with the RaceDay ERD.

## SQL Script

The complete database creation script is available here:

`RaceDay_Database.sql`

The script contains the SQL required to create the RaceDay database and its tables.

## Database Documentation

The RaceDay ERD is available in the `docs` folder:

`RaceDay_ERD.png`

The ERD provides the planned entities, attributes, primary keys, foreign keys, and relationships for the RaceDay system.

## Verification

The database was successfully created and the required tables were verified using SQL Server Management Studio.

## Repository Structure

The Part 1 database documentation is stored in the `docs` folder so that the planning documents and SQL implementation can be reviewed together.
