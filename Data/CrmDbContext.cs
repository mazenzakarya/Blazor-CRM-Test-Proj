using Microsoft.EntityFrameworkCore;
using Blazor_CRM_Test_Proj.Models;

namespace Blazor_CRM_Test_Proj.Data;

public class CrmDbContext : DbContext
{
    public CrmDbContext(DbContextOptions<CrmDbContext> options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Opportunity> Opportunities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Customer entity
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            entity.HasIndex(e => e.Email).IsUnique();
        });

        // Configure Contact entity
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Message).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.ContactType).HasMaxLength(50);
            entity.Property(e => e.ContactDate).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Contacts)
                  .HasForeignKey(e => e.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure Opportunity entity
        modelBuilder.Entity<Opportunity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(50);
            entity.Property(e => e.EstimatedValue).HasColumnType("decimal(18,2)");
            entity.Property(e => e.ActualValue).HasColumnType("decimal(18,2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            
            entity.HasOne(e => e.Customer)
                  .WithMany(c => c.Opportunities)
                  .HasForeignKey(e => e.CustomerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed customers
        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "+1-555-0123",
                Company = "Acme Corp",
                Address = "123 Main St, New York, NY 10001",
                CreatedDate = DateTime.UtcNow.AddDays(-30),
                IsActive = true
            },
            new Customer
            {
                Id = 2,
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                Phone = "+1-555-0456",
                Company = "Tech Solutions Inc",
                Address = "456 Oak Ave, Los Angeles, CA 90210",
                CreatedDate = DateTime.UtcNow.AddDays(-15),
                IsActive = true
            },
            new Customer
            {
                Id = 3,
                FirstName = "Bob",
                LastName = "Johnson",
                Email = "bob.johnson@example.com",
                Phone = "+1-555-0789",
                Company = "Global Enterprises",
                Address = "789 Pine St, Chicago, IL 60601",
                CreatedDate = DateTime.UtcNow.AddDays(-7),
                IsActive = true
            }
        );

        // Seed contacts
        modelBuilder.Entity<Contact>().HasData(
            new Contact
            {
                Id = 1,
                Subject = "Initial Contact",
                Message = "First contact with the customer to discuss their needs.",
                ContactType = "Phone",
                ContactDate = DateTime.UtcNow.AddDays(-25),
                CustomerId = 1,
                IsFollowUpRequired = true,
                FollowUpDate = DateTime.UtcNow.AddDays(5)
            },
            new Contact
            {
                Id = 2,
                Subject = "Product Demo",
                Message = "Demonstrated our CRM solution to the customer.",
                ContactType = "Meeting",
                ContactDate = DateTime.UtcNow.AddDays(-10),
                CustomerId = 2,
                IsFollowUpRequired = false
            }
        );

        // Seed opportunities
        modelBuilder.Entity<Opportunity>().HasData(
            new Opportunity
            {
                Id = 1,
                Title = "CRM Implementation",
                Description = "Full CRM system implementation for Acme Corp",
                Status = "Proposal",
                EstimatedValue = 50000.00m,
                ExpectedCloseDate = DateTime.UtcNow.AddDays(30),
                CustomerId = 1,
                CreatedDate = DateTime.UtcNow.AddDays(-20)
            },
            new Opportunity
            {
                Id = 2,
                Title = "Software License",
                Description = "Annual software license for Tech Solutions",
                Status = "Negotiation",
                EstimatedValue = 25000.00m,
                ExpectedCloseDate = DateTime.UtcNow.AddDays(15),
                CustomerId = 2,
                CreatedDate = DateTime.UtcNow.AddDays(-5)
            }
        );
    }
}

