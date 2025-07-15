namespace MilkBasket.Models
{
    public class MenuItem
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public int ProfileId { get; set; }
        public decimal PrintOrder { get; set; }
        public string MenuUrl { get; set; }
    }
}
