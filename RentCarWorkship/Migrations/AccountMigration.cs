using FluentMigrator;
namespace RentCarWorkship.Migrations;

[Migration(0)]
public class AccountMigration : Migration
{
    public override void Up()
    {
        Create.Table("accounts")
            .WithColumn("id").AsInt64().PrimaryKey().Identity()
            .WithColumn("email").AsString()
            .WithColumn("username").AsString()
            .WithColumn("password").AsString()
            .WithColumn("role").AsString()
            .WithColumn("refreshToken").AsString()
            .WithColumn("refreshTokenExpiredTime").AsString();
    }

    public override void Down()
    {
        Delete.Table("accounts");
    }
}