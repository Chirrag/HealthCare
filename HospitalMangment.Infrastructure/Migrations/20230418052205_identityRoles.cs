using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalMangment.Infrastructure.Migrations
{
    public partial class identityRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "07896c09-d9c6-4647-8acf-501d5774e651", "2", "Doctor", "Doctor" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bf09bb65-d5d6-41bf-94d6-94c9a5fdbdbc", "1", "Receptionist", "Receptionist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07896c09-d9c6-4647-8acf-501d5774e651");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bf09bb65-d5d6-41bf-94d6-94c9a5fdbdbc");
        }
    }
}
