﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Migrations
{
    public partial class Configurations_Update_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartingSeed",
                table: "Keywords",
                type: "TEXT",
                maxLength: 8000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 8000,
                oldDefaultValue: "---");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StartingSeed",
                table: "Keywords",
                type: "TEXT",
                maxLength: 8000,
                nullable: false,
                defaultValue: "---",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 8000,
                oldDefaultValue: "");
        }
    }
}
