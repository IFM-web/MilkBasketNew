namespace MilkBasket.Models.Admin
{
    public class Adm_MenuList
    {
        public int MenuRightsId { get; set; }
        public int ProfileId { get; set; }
        public int Companyid { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string CreateUser { get; set; }
        public int FlagView { get; set; }
        public int FlagAdd { get; set; }
        public int FlagModify { get; set; }
        public int FlagDelete { get; set; }
        public int type { get; set; }
       
        public string mod { get; set; }
    }
}
