using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace endavaPractica.Net.Models;

public partial class Event2Context : DbContext
{
    public Event2Context()
    {
    }

    public Event2Context(DbContextOptions<Event2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<TicketCategory> TicketCategories { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-OH83BHK\\SQLEXPRESS;Initial Catalog=event2;Integrated Security=True;TrustServerCertificate=True;encrypt=false;MultipleActiveResultSets=true").UseLazyLoadingProxies();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB8550F1ACD7");

            entity.ToTable("customers", "database_schema");

            entity.HasIndex(e => e.Email, "UQ__customer__AB6E61643F8E45A2").IsUnique();

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customer_name");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__events__2370F727BAE69681");

            entity.ToTable("events", "database_schema");

            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.EventDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_description");
            //entity.Property(e => e.EventEndDate)
            //    .HasColumnType("datetime")
            //    .HasColumnName("event_end_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_name");
            //entity.Property(e => e.EventStartDate)
            //    .HasColumnType("datetime")
            //    .HasColumnName("event_start_date");
            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.VenueId).HasColumnName("venue_id");

            entity.HasOne(d => d.EventType).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_events_event_type");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_events_venue");
        });

        modelBuilder.Entity<EventType>(entity =>
        {
            entity.HasKey(e => e.EventTypeId).HasName("PK__event_ty__BB84C6F3C198378B");

            entity.ToTable("event_types", "database_schema");

            entity.HasIndex(e => e.EventTypeName, "UQ__event_ty__04BC409E4EF38D55").IsUnique();

            entity.Property(e => e.EventTypeId).HasColumnName("event_type_id");
            entity.Property(e => e.EventTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("event_type_name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__46596229DCB926DA");

            entity.ToTable("orders", "database_schema");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.NumberOfTickets).HasColumnName("number_of_tickets");
            entity.Property(e => e.OrderedAt)
                .HasColumnType("datetime")
                .HasColumnName("ordered_at");
            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.TotalPrice).HasColumnName("total_price");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_customer");

            entity.HasOne(d => d.TicketCategory).WithMany(p => p.Orders)
                .HasForeignKey(d => d.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_orders_ticket_category");
        });

        modelBuilder.Entity<TicketCategory>(entity =>
        {
            entity.HasKey(e => e.TicketCategoryId).HasName("PK__ticket_c__3FC8DEA271C6906A");

            entity.ToTable("ticket_categories", "database_schema");

            entity.Property(e => e.TicketCategoryId).HasColumnName("ticket_category_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.TicketCategoryDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ticket_category_description");

            entity.HasOne(d => d.Event).WithMany(p => p.TicketCategories)
                .HasForeignKey(d => d.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_ticket_category_events");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.VenueId).HasName("PK__venue__82A8BE8D18134DF8");

            entity.ToTable("venues", "database_schema");

            entity.Property(e => e.VenueId).HasColumnName("venue_id");
            entity.Property(e => e.Capacity).HasColumnName("capacity");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("venue_location");
            entity.Property(e => e.VenueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("venue_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
