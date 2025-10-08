using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Challenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    Inventory = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ProductId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("UserId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AuthTokenId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuthToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClientId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Client_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IndividualAmount = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    ClientId = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: false),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp(6) without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OrderItemId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItem_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItem_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItem_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthToken_UserId",
                table: "AuthToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Client_Email",
                table: "Client",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Client_UserId",
                table: "Client",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ClientId",
                table: "Order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ClientId",
                table: "OrderItem",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderId_ProductId",
                table: "OrderItem",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ProductId",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_User_Email",
                table: "User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthToken");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
