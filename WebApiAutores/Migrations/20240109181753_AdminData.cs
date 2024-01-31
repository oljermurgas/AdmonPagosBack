using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdminPagosApi.Migrations
{
    /// <inheritdoc />
    public partial class AdminData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
                                    SET IDENTITY_INSERT [AspNetRoles] ON;
                                    INSERT INTO [AspNetRoles] ([Id], [ConcurrencyStamp], [Name], [NormalizedName])
                                    VALUES (N'10c405b5-2859-4312-b74f-577736bfc0c3', N'ab4b05c8-609c-4b90-ac89-3a0a35e14211', N'Admin', N'Admin');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ConcurrencyStamp', N'Name', N'NormalizedName') AND [object_id] = OBJECT_ID(N'[AspNetRoles]'))
                                        SET IDENTITY_INSERT [AspNetRoles] OFF;
                                    GO

                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
                                        SET IDENTITY_INSERT [AspNetUsers] ON;
                                    INSERT INTO [AspNetUsers] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [SecurityStamp], [TwoFactorEnabled], [UserName])
                                    VALUES (N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f', 0, N'67c7a467-2d1d-4e91-8920-5c68541c28c0', N'omurgas@hotmail.com', CAST(0 AS bit), CAST(0 AS bit), NULL, N'omurgas@hotmail.com', N'omurgas@hotmail.com', N'AQAAAAEAACcQAAAAEKobSZ3eIolSrzSXRtH2swX38/Hh2NE4/SRiP0cJrUFrQeUvTSKfAQFChFfq9SMnHg==', NULL, CAST(0 AS bit), N'46ca839e-5e2c-4d41-8bb1-55799c6a6189', CAST(0 AS bit), N'omurgas@hotmail.com');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[AspNetUsers]'))
                                        SET IDENTITY_INSERT [AspNetUsers] OFF;
                                    
                                    GO
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'RoleId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
                                        SET IDENTITY_INSERT [AspNetUserRoles] ON;
                                        INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
                                        VALUES (N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f', N'10c405b5-2859-4312-b74f-577736bfc0c3');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'RoleId') AND [object_id] = OBJECT_ID(N'[AspNetUserRoles]'))
                                        SET IDENTITY_INSERT [AspNetUserRoles] OFF;
                                    
                                    GO
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
                                        SET IDENTITY_INSERT [AspNetUserClaims] ON;
                                    INSERT INTO [AspNetUserClaims] ([Id], [ClaimType], [ClaimValue], [UserId])
                                    VALUES (1, N'http://schemas.microsoft.com/ws/2008/06/identity/claims/role', N'Admin', N'9b9524b0-a0ec-4de3-ad79-cfb125d40c4f');
                                    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'ClaimType', N'ClaimValue', N'UserId') AND [object_id] = OBJECT_ID(N'[AspNetUserClaims]'))
                                        SET IDENTITY_INSERT [AspNetUserClaims] OFF;
                                    GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: "10c405b5-2859-4312-b74f-577736bfc0c3");

            migrationBuilder.DeleteData(
                table: "AspNetUserClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9b9524b0-a0ec-4de3-ad79-cfb125d40c4f");
        }
    }
}
