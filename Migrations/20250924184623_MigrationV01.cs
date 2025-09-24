using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlazorCRMTestProj.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastContactDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    ContactDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ContactType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsFollowUpRequired = table.Column<bool>(type: "bit", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opportunities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EstimatedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualValue = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    ExpectedCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActualCloseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opportunities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Opportunities_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Company", "CreatedDate", "Email", "FirstName", "IsActive", "LastContactDate", "LastName", "Phone" },
                values: new object[,]
                {
                    { 1, "123 Main St, New York, NY 10001", "Acme Corp", new DateTime(2025, 8, 25, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6195), "john.doe@example.com", "John", true, null, "Doe", "+1-555-0123" },
                    { 2, "456 Oak Ave, Los Angeles, CA 90210", "Tech Solutions Inc", new DateTime(2025, 9, 9, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6203), "jane.smith@example.com", "Jane", true, null, "Smith", "+1-555-0456" },
                    { 3, "789 Pine St, Chicago, IL 60601", "Global Enterprises", new DateTime(2025, 9, 17, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6207), "bob.johnson@example.com", "Bob", true, null, "Johnson", "+1-555-0789" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "ContactDate", "ContactType", "CustomerId", "FollowUpDate", "IsFollowUpRequired", "Message", "Subject" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 8, 30, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6362), "Phone", 1, new DateTime(2025, 9, 29, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6365), true, "First contact with the customer to discuss their needs.", "Initial Contact" },
                    { 2, new DateTime(2025, 9, 14, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6369), "Meeting", 2, null, false, "Demonstrated our CRM solution to the customer.", "Product Demo" }
                });

            migrationBuilder.InsertData(
                table: "Opportunities",
                columns: new[] { "Id", "ActualCloseDate", "ActualValue", "CreatedDate", "CustomerId", "Description", "EstimatedValue", "ExpectedCloseDate", "Notes", "Status", "Title" },
                values: new object[,]
                {
                    { 1, null, null, new DateTime(2025, 9, 4, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6399), 1, "Full CRM system implementation for Acme Corp", 50000.00m, new DateTime(2025, 10, 24, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6397), null, "Proposal", "CRM Implementation" },
                    { 2, null, null, new DateTime(2025, 9, 19, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6405), 2, "Annual software license for Tech Solutions", 25000.00m, new DateTime(2025, 10, 9, 18, 46, 21, 596, DateTimeKind.Utc).AddTicks(6402), null, "Negotiation", "Software License" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                table: "Contacts",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Opportunities_CustomerId",
                table: "Opportunities",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Opportunities");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
