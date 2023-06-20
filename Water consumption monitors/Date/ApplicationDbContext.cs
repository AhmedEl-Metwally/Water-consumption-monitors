using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Date
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Slidedistribution> Slidedistributions { get; set; }
        public virtual DbSet<Subscriber> Subscribers { get; set; }
        public virtual DbSet<Subscription> Subscriptions { get; set; }
        public virtual DbSet<TypesOfRealEstate> TypesOfRealEstates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=WaterconsumptionDB;Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.Property(e => e.InvoiceNumber).IsFixedLength();

                entity.Property(e => e.FiscalYear).IsFixedLength();

                entity.Property(e => e.HouseType).IsFixedLength();

                entity.Property(e => e.SubscriberNumber).IsFixedLength();

                entity.Property(e => e.SubscriptionNumber).IsFixedLength();

                entity.HasOne(d => d.HouseTypeNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.HouseType)
                    .HasConstraintName("FK_Invoices_Types of real estate");

                entity.HasOne(d => d.SubscriberNumberNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SubscriberNumber)
                    .HasConstraintName("FK_Invoices_Subscriber");

                entity.HasOne(d => d.SubscriptionNumberNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.SubscriptionNumber)
                    .HasConstraintName("FK_Invoices_Subscriptions");
                    base.OnModelCreating(modelBuilder); 
            });

            modelBuilder.Entity<Slidedistribution>(entity =>
            {
                entity.HasKey(e => e.SlideNumber)
                    .HasName("PK_Slide distribution");

                entity.Property(e => e.SlideNumber).IsFixedLength();
            });

            modelBuilder.Entity<Subscriber>(entity =>
            {
                entity.Property(e => e.SubscriberIdentityNumber).IsFixedLength();

                entity.Property(e => e.SubscriberNote).IsFixedLength();

                entity.Property(e => e.SubscriberPhoneNumber).IsFixedLength();
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.SubscriptionNumber).IsFixedLength();

                entity.Property(e => e.HouseType).IsFixedLength();

                entity.Property(e => e.SubscriberNumber).IsFixedLength();

                entity.HasOne(d => d.HouseTypeNavigation)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.HouseType)
                    .HasConstraintName("FK_Subscriptions_Types of real estate");

                entity.HasOne(d => d.SubscriberNumberNavigation)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.SubscriberNumber)
                    .HasConstraintName("FK_Subscriptions_Subscriber");
            });

            modelBuilder.Entity<TypesOfRealEstate>(entity =>
            {
                entity.HasKey(e => e.TypesCode)
                    .HasName("PK_Types of real estate");

                entity.Property(e => e.TypesCode).IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
