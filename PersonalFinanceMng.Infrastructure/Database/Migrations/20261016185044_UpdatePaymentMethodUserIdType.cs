using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceMng.Infrastructure.Database.Migrations
{
    public partial class UpdatePaymentMethodUserIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Remove o índice antigo que usa a coluna UserId
            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_UserId_Name",
                table: "PaymentMethods");

            // 2. Remove a coluna antiga (int) que está travando a conversão
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentMethods");

            // 3. Adiciona a nova coluna já com o tipo correto (UNIQUEIDENTIFIER)
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "PaymentMethods",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            // 4. Recria o índice único combinando o novo UserId e o Name
            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_UserId_Name",
                table: "PaymentMethods",
                columns: new[] { "UserId", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_UserId_Name",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentMethods");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "PaymentMethods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_UserId_Name",
                table: "PaymentMethods",
                columns: new[] { "UserId", "Name" },
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}