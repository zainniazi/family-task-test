using Core.Abstractions;
using Domain.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataLayer
{
    public class FamilyTaskContext : DbContext
    {

        public FamilyTaskContext(DbContextOptions<FamilyTaskContext> options):base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>(entity => {
                entity.HasKey(k => k.Id);
                entity.ToTable("Member");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(k => k.Id);
                entity.ToTable("Task");
            });

            modelBuilder.Entity<Task>()
                .HasOne(t => t.AssignedMember)
                .WithMany(x => x.Tasks)
                .HasForeignKey(m => m.AssignedMemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Task_AssignedMemberId");
        }
    }
}