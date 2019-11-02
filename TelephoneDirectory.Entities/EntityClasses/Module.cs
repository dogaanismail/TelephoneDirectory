namespace TelephoneDirectory.Entities.EntityClasses
{
    public class Module
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public int? DisplayOrder { get; set; }
        public string PageIcon { get; set; }
        public string PageUrl { get; set; }
        public int? ParentModuleID { get; set; }      
    }
}
