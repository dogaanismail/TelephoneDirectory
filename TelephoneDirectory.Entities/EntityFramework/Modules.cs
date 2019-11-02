namespace TelephoneDirectory.Entities.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Modules
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string ModuleName { get; set; }

        public int? DisplayOrder { get; set; }

        [StringLength(50)]
        public string PageIcon { get; set; }

        [StringLength(50)]
        public string PageUrl { get; set; }

        public int? ParentModuleID { get; set; }
    }
}
