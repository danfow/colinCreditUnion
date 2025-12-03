using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace colinCreditUnion.Migrations
{
    /// <inheritdoc />
    public partial class setup5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Accounts",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                newName: "IX_Accounts_CustomerId");

            migrationBuilder.UpdateData(
                table: "Accounts",
                keyColumn: "CustomerId",
                keyValue: null,
                column: "CustomerId",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerId",
                table: "Accounts",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Customers_CustomerId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Accounts",
                newName: "CustomerID");

            migrationBuilder.RenameIndex(
                name: "IX_Accounts_CustomerId",
                table: "Accounts",
                newName: "IX_Accounts_CustomerID");

            migrationBuilder.AlterColumn<string>(
                name: "CustomerID",
                table: "Accounts",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Customers_CustomerID",
                table: "Accounts",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerID");
        }
    }
}
