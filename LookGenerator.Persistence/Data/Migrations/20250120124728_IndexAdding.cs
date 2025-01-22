using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookGenerator.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class IndexAdding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36ebe603-de50-4297-8e1b-631951aa6e71", "AQAAAAIAAYagAAAAEBN1FJA+mAIKGkfAfQ14ZBEMewTtaYie774v1+TpzxJxfe6npYuAsP7RefiuWXYMJw==" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_Name",
                table: "AspNetRoles",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_Name",
                table: "AspNetRoles");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7a5b703c-d0fd-4a19-bf0e-9a414d2403c0", "AQAAAAIAAYagAAAAED+CINWmNG14PdojmRdmZMeXsd504RFCq004BHVucOUJhgSUyjytOhuJ9aHK/CJHuw==" });
        }
    }
}
