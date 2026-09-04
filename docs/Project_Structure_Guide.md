# RaceDay Project Structure Guide

## Overview

The RaceDay project is maintained as a single GitHub repository containing the documentation, database design, SQL implementation, and GitHub Actions configuration for the project.

The repository is organised so that the Part 1 planning and database documentation can be easily reviewed before development of the API and MVC application.

## Current Repository Structure

```text
RaceDay/
├── .github/
│   └── workflows/
│       └── part1-validation.yml
│
├── docs/
│   ├── RaceDay_ERD.png
│   ├── RaceDay API Endpoint Plan.pdf
│   ├── RaceDay_Database.sql
│   ├── RaceDay_CI_Green_Build.png
│   ├── Database_Setup_Guide.md
│   └── Project_Structure_Guide.md
│
└── README.md
```

## Folder and File Purposes

### `.github/workflows/`

This folder contains the GitHub Actions workflow used to automatically validate the required Part 1 documentation.

The current workflow is:

`part1-validation.yml`

The workflow checks that the required Part 1 documentation files are present in the repository.

### `docs/`

The `docs` folder contains the main Part 1 planning and database documentation.

#### `RaceDay_ERD.png`

Contains the RaceDay Entity Relationship Diagram.

The ERD documents the planned database entities, attributes, primary keys, foreign keys, and relationships.

#### `RaceDay API Endpoint Plan.pdf`

Contains the planned API endpoints for the RaceDay system.

The endpoint plan documents the HTTP methods, routes, descriptions, required roles, request bodies, and expected responses.

#### `RaceDay_Database.sql`

Contains the SQL Server script used to create the RaceDay database and its tables.

The SQL implementation is intended to correspond with the database structure represented by the ERD.

#### `RaceDay_CI_Green_Build.png`

Contains a screenshot showing the successful GitHub Actions validation build.

This provides visual evidence that the Part 1 CI/CD validation workflow completed successfully.

#### `Database_Setup_Guide.md`

Provides instructions for creating and verifying the RaceDay database using Microsoft SQL Server Management Studio.

#### `Project_Structure_Guide.md`

This document explains the organisation and purpose of the RaceDay project files and folders.

## `README.md`

The repository README provides an overview of the RaceDay system.

It includes:

* A description of the RaceDay system
* The Organiser role
* The Participant role
* Links to the Part 1 documentation
* CI/CD information
* Evidence of the successful GitHub Actions build
* The location for the Part 1 video link

## Future Development

The repository will be extended in later parts of the project.

Future application code will be organised separately from the Part 1 planning documentation so that the database design, API implementation, and MVC application can be maintained within the same GitHub repository.

## Documentation Principle

The `docs` folder is used to keep project documentation together and make the planning decisions easy to review.

This structure also helps ensure that the repository remains understandable as the RaceDay system grows.
