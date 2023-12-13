using FluentMigrator;
using FluentMigrator.Postgres;

namespace RentCarWorkship.Migrations;


[Migration(1)]
public class CarMigration: Migration
{
    public override void Up()
    {
        Create.Table("cars")
            .WithColumn("CarId").AsInt64().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt64()
            .WithColumn("CanBeRented").AsBoolean()
            .WithColumn("model").AsString()
            .WithColumn("color").AsString()
            .WithColumn("identifier").AsString()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("latitude").AsDouble()
            .WithColumn("longitude").AsDouble()
            .WithColumn("minutePrice").AsDouble()
            .WithColumn("dayPrice").AsDouble().Nullable();
    }

    public override void Down()
    {
        Delete.Table("cars");
    }
}