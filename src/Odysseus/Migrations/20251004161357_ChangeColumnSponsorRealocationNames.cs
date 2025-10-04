using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Odysseus.Host.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnSponsorRealocationNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequiresSponsorship",
                table: "JobApplications",
                newName: "OfferSponsorship");

            migrationBuilder.RenameColumn(
                name: "RequiresRelocation",
                table: "JobApplications",
                newName: "OfferRelocation");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OfferSponsorship",
                table: "JobApplications",
                newName: "RequiresSponsorship");

            migrationBuilder.RenameColumn(
                name: "OfferRelocation",
                table: "JobApplications",
                newName: "RequiresRelocation");
        }
    }
}
