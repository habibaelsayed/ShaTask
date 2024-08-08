using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShaTask.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeletion3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeader",
                table: "InvoiceDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeader_InvoiceHeaderID",
                table: "InvoiceDetails",
                column: "InvoiceHeaderID",
                principalTable: "InvoiceHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeader_InvoiceHeaderID",
                table: "InvoiceDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_InvoiceHeader",
                table: "InvoiceDetails",
                column: "InvoiceHeaderID",
                principalTable: "InvoiceHeader",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
