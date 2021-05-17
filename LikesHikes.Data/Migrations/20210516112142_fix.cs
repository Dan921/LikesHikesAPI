using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LikesHikes.Data.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoutes_Reports_ReportId",
                table: "UsersRoutes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportId",
                table: "UsersRoutes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<double>(
                name: "Length",
                table: "Routes",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoutes_Reports_ReportId",
                table: "UsersRoutes",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersRoutes_Reports_ReportId",
                table: "UsersRoutes");

            migrationBuilder.AlterColumn<Guid>(
                name: "ReportId",
                table: "UsersRoutes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "Length",
                table: "Routes",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddForeignKey(
                name: "FK_UsersRoutes_Reports_ReportId",
                table: "UsersRoutes",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
