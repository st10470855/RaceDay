using Microsoft.EntityFrameworkCore;
using RaceDay.API.Models;

namespace RaceDay.API.Data;

public static class RaceDayModelConfiguration
{
    public static void Configure(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(e => e.RoleID);
            entity.HasIndex(e => e.RoleName).IsUnique();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserID);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);

            entity.HasOne(e => e.Role)
                .WithMany()
                .HasForeignKey(e => e.RoleID);
        });

        modelBuilder.Entity<Organizer>(entity =>
        {
            entity.ToTable("Organizer");
            entity.HasKey(e => e.OrganizerID);
            entity.HasIndex(e => e.UserID).IsUnique();
            entity.Property(e => e.OrganisationName).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Website).HasMaxLength(150);

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Organizer>(e => e.UserID);
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.ToTable("Participant");
            entity.HasKey(e => e.ParticipantID);
            entity.HasIndex(e => e.UserID).IsUnique();
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Club).HasMaxLength(150);

            entity.HasOne(e => e.User)
                .WithOne()
                .HasForeignKey<Participant>(e => e.UserID);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event", table =>
            {
                table.HasCheckConstraint(
                    "CK_Event_Distance", "[Distance] > 0");
                table.HasCheckConstraint(
                    "CK_Event_EventType",
                    "[EventType] IN ('Run', 'Walk', 'Cycle')");
            });

            entity.HasKey(e => e.EventID);
            entity.Property(e => e.EventName).HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Location).HasMaxLength(200);
            entity.Property(e => e.Distance).HasColumnType("decimal(6,2)");
            entity.Property(e => e.EventType).HasMaxLength(20);

            entity.HasOne(e => e.Organizer)
                .WithMany()
                .HasForeignKey(e => e.OrganizerID);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");
            entity.HasKey(e => e.CategoryID);
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.MinDistance).HasColumnType("decimal(6,2)");
            entity.Property(e => e.MaxDistance).HasColumnType("decimal(6,2)");

            entity.HasOne(e => e.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(e => e.EventID);
        });

        modelBuilder.Entity<EventEnrolment>(entity =>
        {
            entity.ToTable("EventEnrolment", table =>
                table.HasCheckConstraint(
                    "CK_EventEnrolment_Status",
                    "[Status] IN ('Confirmed', 'Cancelled', 'Pending')"));

            entity.HasKey(e => e.EnrolmentID);
            entity.HasIndex(e => new { e.ParticipantID, e.EventID })
                .IsUnique();
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(e => e.Participant)
                .WithMany()
                .HasForeignKey(e => e.ParticipantID);

            entity.HasOne(e => e.Event)
                .WithMany(e => e.EventEnrolments)
                .HasForeignKey(e => e.EventID);

            entity.HasOne(e => e.Category)
                .WithMany(e => e.EventEnrolments)
                .HasForeignKey(e => e.CategoryID);
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.ToTable("Result", table =>
            {
                table.HasCheckConstraint(
                    "CK_Result_FinishPosition", "[FinishPosition] > 0");
                table.HasCheckConstraint(
                    "CK_Result_OverallPosition",
                    "[OverallPosition] IS NULL OR [OverallPosition] > 0");
            });

            entity.HasKey(e => e.ResultID);
            entity.HasIndex(e => e.EnrolmentID).IsUnique();

            entity.HasOne(e => e.EventEnrolment)
                .WithOne(e => e.Result)
                .HasForeignKey<Result>(e => e.EnrolmentID);

            entity.HasOne(e => e.RecordedByUser)
                .WithMany()
                .HasForeignKey(e => e.RecordedBy)
                .IsRequired(false);
        });
    }
}