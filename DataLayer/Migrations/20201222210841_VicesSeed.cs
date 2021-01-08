using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class VicesSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVice_AspNetUsers_UserId",
                table: "UserVice");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVice_Vices_ViceId",
                table: "UserVice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVice",
                table: "UserVice");

            migrationBuilder.RenameTable(
                name: "UserVice",
                newName: "UserVices");

            migrationBuilder.RenameIndex(
                name: "IX_UserVice_ViceId",
                table: "UserVices",
                newName: "IX_UserVices_ViceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVice_UserId",
                table: "UserVices",
                newName: "IX_UserVices_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVices",
                table: "UserVices",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Vices",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "LastModifiedAt", "Name" },
                values: new object[] { "1", new DateTime(2020, 12, 22, 21, 8, 40, 949, DateTimeKind.Utc).AddTicks(99), null, false, null, "Bautura" });

            migrationBuilder.InsertData(
                table: "Vices",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "LastModifiedAt", "Name" },
                values: new object[] { "2", new DateTime(2020, 12, 22, 21, 8, 40, 949, DateTimeKind.Utc).AddTicks(2009), null, false, null, "Mancare" });

            migrationBuilder.InsertData(
                table: "Vices",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsDeleted", "LastModifiedAt", "Name" },
                values: new object[] { "3", new DateTime(2020, 12, 22, 21, 8, 40, 949, DateTimeKind.Utc).AddTicks(2043), null, false, null, "Tigari" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserVices_AspNetUsers_UserId",
                table: "UserVices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVices_Vices_ViceId",
                table: "UserVices",
                column: "ViceId",
                principalTable: "Vices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserVices_AspNetUsers_UserId",
                table: "UserVices");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVices_Vices_ViceId",
                table: "UserVices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVices",
                table: "UserVices");

            migrationBuilder.DeleteData(
                table: "Vices",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Vices",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Vices",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.RenameTable(
                name: "UserVices",
                newName: "UserVice");

            migrationBuilder.RenameIndex(
                name: "IX_UserVices_ViceId",
                table: "UserVice",
                newName: "IX_UserVice_ViceId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVices_UserId",
                table: "UserVice",
                newName: "IX_UserVice_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVice",
                table: "UserVice",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserVice_AspNetUsers_UserId",
                table: "UserVice",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVice_Vices_ViceId",
                table: "UserVice",
                column: "ViceId",
                principalTable: "Vices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
