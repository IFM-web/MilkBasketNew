namespace MilkBasket.Models
{
    public class MenuMaster
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ShortName { get; set; }
        public int ButtonView { get; set; }
        public int ButtonAdds { get; set; }
        public int ButtonModify { get; set; }
        public int ButtonInquire { get; set; }
        public int ButtonDelete { get; set; }
        public int Status { get; set; }
        public int Parent { get; set; }
        public string PageName { get; set; }
        public string MenuParent { get; set; }

        public IFormFile MenuImage { get; set; }
        public decimal PrintOrder { get; set; }
        public string Other1 { get; set; }
        public string Other2 { get; set; }
        public string Other3 { get; set; }

        public string ExistingImage { get; set; }
        public string menu_url { get; set; }
        public string mod { get; set; }

        public string CreateUser { get; set; }
        public int type { get; set; }
    }
}
