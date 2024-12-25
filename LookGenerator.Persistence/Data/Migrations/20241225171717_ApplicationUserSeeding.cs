﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LookGenerator.Persistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2"), 0, "e776d7ee-27c0-49f4-b537-fe46e6b63116", "mrsplash2356@gmail.com", true, false, null, "MRSPLASH2356@GMAIL.COM", "IHOR", "AQAAAAIAAYagAAAAEOKYhGWVaehJHAu1LXdA75F9+7oHaLAW4L4assrn9+PT4Pj8yh8cJQTBkk11ByC0Qg==", null, false, null, "7f6b6c59-b6a1-4f78-8d9c-3b02cf4706a6", false, "Ihor" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("e2f395ee-a3e4-4524-bdb9-97ec622f8e02"), new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e2f395ee-a3e4-4524-bdb9-97ec622f8e02"), new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("226b1dad-0065-44c6-acef-93186e7cd0f2"));
        }
    }
}