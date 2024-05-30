using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForEvidence.Migrations
{
    public partial class pro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp1 = @"CREATE PROCEDURE InsertBType 
   @TName varchar(55)
   AS
   BEGIN
   INSERT INTO Types (TName) VALUES (@TName)
   END";
            migrationBuilder.Sql(sp1);

            var sp2 = @"CREATE PROCEDURE GetBType 
   AS
   BEGIN
   SELECT * FROM Types
   END";
            migrationBuilder.Sql(sp2);

            var sp3 = @"CREATE PROCEDURE UpdateBType 
 @TypeID int,
 @TName varchar(55)
 AS
 BEGIN
 UPDATE Types SET TName=@TName WHERE TypeID=@TypeID
 END";
            migrationBuilder.Sql(sp3);

            var sp4 = @"CREATE PROCEDURE DeleteBType 
@TypeID int
AS
BEGIN
DELETE FROM Types WHERE TypeID=@TypeID
END";
            migrationBuilder.Sql(sp4);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
