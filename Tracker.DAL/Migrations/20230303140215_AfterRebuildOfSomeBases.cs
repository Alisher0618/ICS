using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tracker.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AfterRebuildOfSomeBases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityProjectEntities_Activities_ActivityId",
                table: "ActivityProjectEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityProjectEntities_Projects_ProjectId",
                table: "ActivityProjectEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUserEntities_Projects_ProjectId",
                table: "ProjectsUserEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUserEntities_Users_UserId",
                table: "ProjectsUserEntities");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProjectsUserEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ProjectsUserEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ActivityProjectEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActivityId",
                table: "ActivityProjectEntities",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityProjectEntities_Activities_ActivityId",
                table: "ActivityProjectEntities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityProjectEntities_Projects_ProjectId",
                table: "ActivityProjectEntities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUserEntities_Projects_ProjectId",
                table: "ProjectsUserEntities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUserEntities_Users_UserId",
                table: "ProjectsUserEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ActivityProjectEntities_Activities_ActivityId",
                table: "ActivityProjectEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ActivityProjectEntities_Projects_ProjectId",
                table: "ActivityProjectEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUserEntities_Projects_ProjectId",
                table: "ProjectsUserEntities");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectsUserEntities_Users_UserId",
                table: "ProjectsUserEntities");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "ProjectsUserEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ProjectsUserEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectId",
                table: "ActivityProjectEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<Guid>(
                name: "ActivityId",
                table: "ActivityProjectEntities",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityProjectEntities_Activities_ActivityId",
                table: "ActivityProjectEntities",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ActivityProjectEntities_Projects_ProjectId",
                table: "ActivityProjectEntities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUserEntities_Projects_ProjectId",
                table: "ProjectsUserEntities",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectsUserEntities_Users_UserId",
                table: "ProjectsUserEntities",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
