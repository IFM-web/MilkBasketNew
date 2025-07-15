using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MilkBasket.Models;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using MilkBasket.Models; // Change this to your actual namespace

public class MenuViewComponent : ViewComponent
{
    private readonly IConfiguration _configuration;

    public MenuViewComponent(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<MenuItem> menuItems = new List<MenuItem>();

        string connectionString = _configuration.GetConnectionString("connect");

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "SELECT MenuId, MenuName, SubMenuName, ProfileId, PrintOrder,MenuUrl FROM Milkbs_Menu ORDER BY PrintOrder";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader rdr = await cmd.ExecuteReaderAsync();

            while (await rdr.ReadAsync())
            {
                menuItems.Add(new MenuItem
                {
                    MenuId = rdr.GetInt32(0),
                    MenuName = rdr.GetString(1),
                    SubMenuName = rdr.IsDBNull(2) ? null : rdr.GetString(2),
                    ProfileId = rdr.GetInt32(3),
                    PrintOrder = rdr.GetDecimal(4),

					MenuUrl = rdr.IsDBNull(5) ? null : rdr.GetString(5),
				});
            }
        }

        // Group by MenuName
        var groupedMenus = menuItems
            .GroupBy(m => m.MenuName)
			.Select(g => new MenuViewModel
			{
				MenuName = g.Key,
				SubMenus = g
		.Where(x => !string.IsNullOrEmpty(x.SubMenuName))
		.Select(x => new SubMenuItem
		{
			SubMenuName = x.SubMenuName,
			MenuUrl = x.MenuUrl
		}).ToList()
			}).ToList();


		return View(groupedMenus);
    }
}
