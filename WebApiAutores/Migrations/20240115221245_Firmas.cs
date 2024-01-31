using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class Firmas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "10c405b5-2859-4312-b74f-577736bfc0c3", "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10c405b5-2859-4312-b74f-577736bfc0c3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f");

            migrationBuilder.AddColumn<string>(
                name: "RubroPresupuesto",
                table: "TipoObligacion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RubroPresupuesto",
                table: "FacturaTipoObligacion",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RubroPresupuesto",
                table: "TipoObligacion");

            migrationBuilder.DropColumn(
                name: "RubroPresupuesto",
                table: "FacturaTipoObligacion");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "10c405b5-2859-4312-b74f-577736bfc0c3", "836154dd-f822-449b-b60c-d53fec46098c", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f", 0, "abae227f-0776-47e0-837b-6a36a2f45b06", "omurgas@hotmail.com", false, false, null, "omurgas@hotmail.com", "omurgas@hotmail.com", "AQAAAAEAACcQAAAAEBusUGXSefzQsIaI74PHlCCY1lsjoOOEskMAF3JJKLDH2cB8wty30XUbyRrIez67NQ==", null, false, "2a54f889-2e2d-4800-945a-66833ece039c", false, "omurgas@hotmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[] { 1, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "10c405b5-2859-4312-b74f-577736bfc0c3", "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f" });
        }
    }
}
