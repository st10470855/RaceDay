# RaceDay Part 1 Requirements Checklist

## Overview

This checklist is used to track the RaceDay Part 1 requirements and supporting deliverables.

The checklist provides a quick overview of the planning, database, documentation, version-control, and CI/CD work completed for Part 1.

## Part 1 Deliverables

| Requirement                                 | Status            | Evidence                                 |
| ------------------------------------------- | ----------------- | ---------------------------------------- |
| ERD completed                               | ✅ Complete        | `docs/RaceDay_ERD.png`                   |
| ERD contains the planned database structure | ✅ Complete        | `docs/RaceDay_ERD.png`                   |
| API Endpoint Plan completed                 | ✅ Complete        | `docs/RaceDay API Endpoint Plan.pdf`     |
| Authentication endpoints planned            | ✅ Complete        | API Endpoint Plan                        |
| User Profile endpoints planned              | ✅ Complete        | API Endpoint Plan                        |
| Event endpoints planned                     | ✅ Complete        | API Endpoint Plan                        |
| Category endpoints planned                  | ✅ Complete        | API Endpoint Plan                        |
| Event Enrolment endpoints planned           | ✅ Complete        | API Endpoint Plan                        |
| Results endpoints planned                   | ✅ Complete        | API Endpoint Plan                        |
| SQL database script completed               | ✅ Complete        | `docs/RaceDay_Database.sql`              |
| Database created and verified in SQL Server | ✅ Complete        | `docs/Database_Setup_Guide.md`           |
| Database verification queries documented    | ✅ Complete        | `docs/Database_Verification_Queries.sql` |
| Organiser role documented                   | ✅ Complete        | `docs/Roles_and_Permissions_Guide.md`    |
| Participant role documented                 | ✅ Complete        | `docs/Roles_and_Permissions_Guide.md`    |
| Project structure documented                | ✅ Complete        | `docs/Project_Structure_Guide.md`        |
| Database design decisions documented        | ✅ Complete        | `docs/Database_Design_Decisions.md`      |
| GitHub Actions workflow created             | ✅ Complete        | `.github/workflows/part1-validation.yml` |
| GitHub Actions validation successful        | ✅ Complete        | `docs/RaceDay_CI_Green_Build.png`        |
| README created and updated                  | ✅ Complete        | `README.md`                              |
| Part 1 video                                | ⏳ To be completed | YouTube link to be added                 |

## Git and Version Control

The RaceDay project is maintained in a GitHub repository.

Meaningful commits are used to record project progress and document changes made during development.

The repository uses the `main` branch for the current project version.

## CI/CD

GitHub Actions is used to validate the presence of the required Part 1 documentation.

The workflow is located at:

`.github/workflows/part1-validation.yml`

A successful build is documented using:

`docs/RaceDay_CI_Green_Build.png`

## Documentation

The main Part 1 documentation is stored in the `docs` folder.

This includes:

* Entity Relationship Diagram
* API Endpoint Plan
* SQL database script
* Database Setup Guide
* Database Design Decisions
* Database Verification Queries
* Project Structure Guide
* Roles and Permissions Guide
* Part 1 Requirements Checklist

## Final Submission Items

Before submitting Part 1, the following items should be checked:

* [x] GitHub repository contains the Part 1 documentation.
* [x] ERD is stored in the `docs` folder.
* [x] API Endpoint Plan is stored in the `docs` folder.
* [x] SQL script is stored in the `docs` folder.
* [x] GitHub Actions workflow is present.
* [x] GitHub Actions build has passed.
* [x] CI/CD screenshot is included.
* [ ] Part 1 video has been recorded.
* [ ] Part 1 video has been uploaded to YouTube as an unlisted video.
* [ ] YouTube link has been added to the README.
* [ ] GitHub repository link is ready for ARC submission.
* [ ] At least 20 meaningful Part 1 commits have been completed.

## Current Progress

The project is currently progressing through the Part 1 documentation and version-control requirements.

The remaining items will be completed before the final Part 1 submission.

## Conclusion

This checklist provides a central record of the RaceDay Part 1 requirements and makes it easier to identify outstanding work before submission.
