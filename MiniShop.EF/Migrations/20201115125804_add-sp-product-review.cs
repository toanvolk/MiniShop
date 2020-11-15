using Microsoft.EntityFrameworkCore.Migrations;

namespace MiniShop.EF.Migrations
{
    public partial class addspproductreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"
                CREATE proc sp_mnshop_getproductreview
                @fromDate datetime2,
                @toDate datetime2
                as
                begin
	                select p.[Name] as ProductName, COUNT(t.ProductId) [Count]
	                from Product p with(nolock)
		                left join (select ProductId 
					                from TouchHistory with(nolock)
					                where CreatedDate between @fromDate and @toDate ) t  on p.Id = t.ProductId
	                group by p.[Name]
                end
            ";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
