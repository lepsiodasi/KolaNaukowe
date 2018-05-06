using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KolaNaukowe.web.Migrations
{
    public partial class subject3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_StudentResearchGroups_researchGroupId",
                table: "Subject");

            migrationBuilder.RenameColumn(
                name: "researchGroupId",
                table: "Subject",
                newName: "researchGroupsId");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_researchGroupId",
                table: "Subject",
                newName: "IX_Subject_researchGroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_StudentResearchGroups_researchGroupsId",
                table: "Subject",
                column: "researchGroupsId",
                principalTable: "StudentResearchGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_StudentResearchGroups_researchGroupsId",
                table: "Subject");

            migrationBuilder.RenameColumn(
                name: "researchGroupsId",
                table: "Subject",
                newName: "researchGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_researchGroupsId",
                table: "Subject",
                newName: "IX_Subject_researchGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_StudentResearchGroups_researchGroupId",
                table: "Subject",
                column: "researchGroupId",
                principalTable: "StudentResearchGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
