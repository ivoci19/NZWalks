using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalks.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingnewchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Region",
        defaultValueSql: "NEWID()",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Walk",
        defaultValueSql: "NEWID()",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier");


            migrationBuilder.AlterColumn<Guid>(
        name: "Id",
        table: "Difficulty",
        defaultValueSql: "NEWID()",
        nullable: false,
        oldClrType: typeof(Guid),
        oldType: "uniqueidentifier");

        }


        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
