using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Raunstrup.Service.Infrastructure.Migrations
{
    public partial class InitializeCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    DiscountGroup = table.Column<int>(nullable: false),
                    RowVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemNo = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    PurchasePrice = table.Column<double>(nullable: false),
                    SalePrice = table.Column<double>(nullable: false),
                    MeasuringUnit = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    RowVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Profession",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    HourPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profession", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNo = table.Column<int>(nullable: false),
                    Cpr = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNo = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PostalCode = table.Column<int>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    IsProjectleader = table.Column<bool>(nullable: false),
                    ProfessionRefID = table.Column<int>(nullable: false),
                    Specialisation = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    RowVersion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employee_Profession_ProfessionRefID",
                        column: x => x.ProfessionRefID,
                        principalTable: "Profession",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkingTitle = table.Column<string>(nullable: true),
                    ProjectleaderRefId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountProcent = table.Column<int>(nullable: false),
                    TotalPriceWithDiscount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    PayForUsedItems = table.Column<bool>(nullable: false),
                    CustomerId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Offer_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Offer_Employee_ProjectleaderRefId",
                        column: x => x.ProjectleaderRefId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OfferAssignedItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferRefId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    OfferPricePer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringUnit = table.Column<string>(nullable: true),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferAssignedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferAssignedItem_Offer_OfferRefId",
                        column: x => x.OfferRefId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferDriving",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferRefId = table.Column<int>(nullable: false),
                    EmployeeRefId = table.Column<int>(nullable: false),
                    TodaysDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferDriving", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferDriving_Employee_EmployeeRefId",
                        column: x => x.EmployeeRefId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferDriving_Offer_OfferRefId",
                        column: x => x.OfferRefId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferEmployee",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferRefId = table.Column<int>(nullable: false),
                    EmployeeRefId = table.Column<int>(nullable: false),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferEmployee", x => x.id);
                    table.ForeignKey(
                        name: "FK_OfferEmployee_Employee_EmployeeRefId",
                        column: x => x.EmployeeRefId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferEmployee_Offer_OfferRefId",
                        column: x => x.OfferRefId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferUsedItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferRefId = table.Column<int>(nullable: false),
                    EmployeeRefId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Amount = table.Column<int>(nullable: false),
                    OfferPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MeasuringUnit = table.Column<string>(nullable: true),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferUsedItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferUsedItem_Employee_EmployeeRefId",
                        column: x => x.EmployeeRefId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferUsedItem_Offer_OfferRefId",
                        column: x => x.OfferRefId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OfferWorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferRefId = table.Column<int>(nullable: false),
                    EmployeeRefId = table.Column<int>(nullable: false),
                    DateOfWorking = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    HourlyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Rowversion = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferWorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OfferWorkingHours_Employee_EmployeeRefId",
                        column: x => x.EmployeeRefId,
                        principalTable: "Employee",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferWorkingHours_Offer_OfferRefId",
                        column: x => x.OfferRefId,
                        principalTable: "Offer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_ProfessionRefID",
                table: "Employee",
                column: "ProfessionRefID");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_CustomerId",
                table: "Offer",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offer_ProjectleaderRefId",
                table: "Offer",
                column: "ProjectleaderRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferAssignedItem_OfferRefId",
                table: "OfferAssignedItem",
                column: "OfferRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferDriving_EmployeeRefId",
                table: "OfferDriving",
                column: "EmployeeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferDriving_OfferRefId",
                table: "OfferDriving",
                column: "OfferRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEmployee_EmployeeRefId",
                table: "OfferEmployee",
                column: "EmployeeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferEmployee_OfferRefId",
                table: "OfferEmployee",
                column: "OfferRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferUsedItem_EmployeeRefId",
                table: "OfferUsedItem",
                column: "EmployeeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferUsedItem_OfferRefId",
                table: "OfferUsedItem",
                column: "OfferRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferWorkingHours_EmployeeRefId",
                table: "OfferWorkingHours",
                column: "EmployeeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_OfferWorkingHours_OfferRefId",
                table: "OfferWorkingHours",
                column: "OfferRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "OfferAssignedItem");

            migrationBuilder.DropTable(
                name: "OfferDriving");

            migrationBuilder.DropTable(
                name: "OfferEmployee");

            migrationBuilder.DropTable(
                name: "OfferUsedItem");

            migrationBuilder.DropTable(
                name: "OfferWorkingHours");

            migrationBuilder.DropTable(
                name: "Offer");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Profession");
        }
    }
}
