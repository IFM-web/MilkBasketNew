namespace MilkBasket.Models
{
    public class MenuViewModel
    {
        public string MenuName { get; set; }
        public string MenuUrl { get; set; }
        public List<SubMenuItem> SubMenus { get; set; }
    }

	public class SubMenuItem
	{
		public string SubMenuName { get; set; }
		public string MenuUrl { get; set; }
	}


}
